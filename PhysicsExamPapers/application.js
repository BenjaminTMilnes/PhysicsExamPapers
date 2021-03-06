﻿var application = angular.module("PhysicsExamPapers", ["ngSanitize"]);

var ExamPositions = { Introduction: 0, Questions: 1, Conclusion: 2 }

function reorderRandomly(array) {
    var currentIndex = array.length;
    var randomIndex = 0;
    var temporaryValue;

    while (currentIndex > 0) {
        randomIndex = Math.round(Math.random() * (currentIndex - 1));
        currentIndex -= 1;

        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
    }

    return array;
}

function generateRandomNumber(lowerLimit, upperLimit) {
    return Math.round(Math.random() * (upperLimit - lowerLimit) + lowerLimit);
}

application.directive("latex", function () {
    return {
        restrict: "E",
        link: function (scope, element, attributes) {
            var content = attributes.content;
            if (typeof (katex) === "undefined") {
                require(["katex"], function (katex) {
                    katex.render(content, element[0]);
                });
            }
            else {
                katex.render(content, element[0]);
            }
        }
    }
});

application.directive("compile", ["$compile", function ($compile) {
    return function (scope, element, attributes) {
        scope.$watch(function (scope) {
            return scope.$eval(attributes.compile);
        }, function (value) {
            element.html(value);
            $compile(element.contents())(scope);
        });
    };
}]);

application.directive("keypress", function () {
    return function (scope, element, attributes) {
        element.bind("keydown keypress", function (event) {
            if (event.which == 13) {
                scope.$apply(function () { scope.$eval(attributes.keypress); });

                event.preventDefault();
            }
        });
    };
});

application.controller("ExamController", ["$scope", "$http", "$location", function ($scope, $http, $location) {

    $scope.examTemplate = {};

    $scope.examPosition = ExamPositions.Introduction;
    $scope.introductionIsVisible = true;
    $scope.questionsAreVisible = false;
    $scope.conclusionIsVisible = false;

    $scope.numericAnswerSectionIsVisible = false;
    $scope.multipleChoiceAnswerSectionIsVisible = false;

    $scope.checkAnswerButtonIsVisible = false;
    $scope.tryQuestionAgainButtonIsVisible = false;
    $scope.nextQuestionButtonIsVisible = false;
    $scope.correctTextIsVisible = false;
    $scope.incorrectTextIsVisible = false;

    $scope.questionTemplates = [];
    $scope.questionTemplateNumber = 0;
    $scope.highestPossibleLevel = 0;

    $scope.currentQuestionTemplate = {};
    $scope.currentQuestion = {};

    $scope.history = [];
    $scope.results = {};

    $scope.getExamTemplate = function (reference) {

        reference = $location.search().exam;

        if (reference == "general_relativity") {
            reference = "static_content/Physics_GeneralRelativity.json";
        }
        if (reference == "waves") {
            reference = "static_content/Physics_Waves.json";
        }

        $http.get(reference).then(function (response) {
            $scope.examTemplate = response.data;
            $scope.lineariseTemplate();
        })
    }

    $scope.getQuestion = function (reference, callback) {
        $http.get("api/questions?template_reference=" + reference).then(
                  function (response) {
                      callback(response.data);
                  });
    };

    $scope.lineariseTemplate = function () {

        var questionTemplates = [];

        for (var n = 0; n < $scope.examTemplate.Parts.length; n++) {
            var part = $scope.examTemplate.Parts[n];

            for (var m = 0; m < part.Questions.length; m++) {
                var question = part.Questions[m];
                if (question.Level > $scope.highestPossibleLevel) {
                    $scope.highestPossibleLevel = question.Level;
                }

                for (var p = 0; p < question.Repeat; p++) {
                    var partNumber = n + 1;
                    var questionTemplate = { Reference: question.Reference, Level: question.Level, PartTitle: partNumber + ". " + part.Title };

                    questionTemplates.push(questionTemplate);
                }
            }
        }

        $scope.questionTemplates = questionTemplates;
    };

    $scope.checkAnswer = function () {

        var answerIsCorrect = false;

        if ($scope.answer !== "") {
            for (var i = 0; i < $scope.currentQuestion.CorrectAnswers.length; i++) {
                if ($scope.currentQuestion.IsMultipleChoice) {
                    if ($scope.answer == $scope.currentQuestion.CorrectAnswers[i].Key) {
                        answerIsCorrect = true;
                    }
                }
                else {
                    if ($scope.answer == $scope.currentQuestion.CorrectAnswers[i].Content) {
                        answerIsCorrect = true;
                    }
                }
            }
        }

        var completedQuestion = { AnswerIsCorrect: answerIsCorrect, Level: $scope.currentQuestionTemplate.Level, NumberOfHintsUsed: 0, TimeTaken: 0 };

        $scope.history.push(completedQuestion);

        $scope.checkAnswerButtonIsVisible = false;
        $scope.nextQuestionButtonIsVisible = true;

        if (answerIsCorrect) {
            $scope.correctTextIsVisible = true;
            $scope.incorrectTextIsVisible = false;
        }
        else {
            $scope.correctTextIsVisible = false;
            $scope.incorrectTextIsVisible = true;
        }

    };

    $scope.calculateResults = function () {

        var levelResults = [];
        var highestLevelResult = { Level: 1 };

        for (var a = 0; a < 100; a++) {
            var levelResult = { Level: a, NumberOfCorrectAnswers: 0, NumberOfAnswers: 0, Percentage: 0, HasPassedLevel: false };

            for (var b = 0; b < $scope.history.length; b++) {
                if ($scope.history[b].Level == a) {
                    levelResult.NumberOfAnswers++;

                    if ($scope.history[b].AnswerIsCorrect && $scope.history[b].NumberOfHintsUsed == 0) {
                        levelResult.NumberOfCorrectAnswers++;
                    }
                }
            }

            if (levelResult.NumberOfAnswers > 0) {
                levelResult.Percentage = Math.round(levelResult.NumberOfCorrectAnswers * 100.0 / levelResult.NumberOfAnswers);

                if (levelResult.Percentage > (a / 100) * 50 + 50) {
                    levelResult.HasPassedLevel = true;
                    highestLevelResult = levelResult;
                }

                levelResults.push(levelResult);
            }
        }

        $scope.results = { Level: highestLevelResult.Level, NumberOfQuestionsAnswered: $scope.history.length };
    };

    $scope.nextQuestion = function () {

        var numberOfQuestionTemplates = $scope.questionTemplates.length;

        if ($scope.questionTemplateNumber < numberOfQuestionTemplates) {
            $scope.questionTemplateNumber++;
        }
        else {
            $scope.endExam();
            return;
        }

        $scope.currentQuestionTemplate = $scope.questionTemplates[$scope.questionTemplateNumber - 1];

        $scope.getQuestion($scope.currentQuestionTemplate.Reference, function (data) {
            $scope.currentQuestion = data;
            $scope.partTitle = $scope.currentQuestionTemplate.PartTitle;
            $scope.questionContent = $scope.currentQuestion.Content;

            if ($scope.currentQuestionIsMultipleChoiceQuestion()) {
                $scope.currentQuestion.IsMultipleChoice = true;

                for (var a = 0; a < $scope.currentQuestion.CorrectAnswers.length; a++) {
                    $scope.currentQuestion.CorrectAnswers[a].IsCorrectAnswer = true;
                    $scope.currentQuestion.CorrectAnswers[a].Key = "ca" + a;
                }

                for (var a = 0; a < $scope.currentQuestion.IncorrectAnswers.length; a++) {
                    $scope.currentQuestion.IncorrectAnswers[a].IsCorrectAnswer = false;
                    $scope.currentQuestion.IncorrectAnswers[a].Key = "ia" + a;
                }

                var numberOfIncorrectAnswers = generateRandomNumber(3, 5);
                var incorrectAnswers = reorderRandomly($scope.currentQuestion.IncorrectAnswers);
                incorrectAnswers = incorrectAnswers.slice(0, numberOfIncorrectAnswers);

                var allAnswers = $scope.currentQuestion.CorrectAnswers.concat(incorrectAnswers);

                $scope.multipleChoiceAnswers = reorderRandomly(allAnswers);

                $scope.numericAnswerSectionIsVisible = false;
                $scope.multipleChoiceAnswerSectionIsVisible = true;
            }
            else {
                $scope.currentQuestion.IsMultipleChoice = false;

                $scope.numericAnswerSectionIsVisible = true;
                $scope.multipleChoiceAnswerSectionIsVisible = false;
            }

        });

        $scope.answer = "";

        $scope.correctTextIsVisible = false;
        $scope.incorrectTextIsVisible = false;

        $scope.checkAnswerButtonIsVisible = true;
        $scope.nextQuestionButtonIsVisible = false;
    };

    $scope.currentQuestionIsMultipleChoiceQuestion = function () {

        var currentQuestionIsMultipleChoiceQuestion = false;
        var allAnswers = $scope.currentQuestion.CorrectAnswers.concat($scope.currentQuestion.IncorrectAnswers);

        for (var a = 0; a < allAnswers.length; a++) {
            if (allAnswers[a].Type == 1) {
                currentQuestionIsMultipleChoiceQuestion = true;
            }
        }

        return currentQuestionIsMultipleChoiceQuestion;
    }

    $scope.next = function () {
        if ($scope.introductionIsVisible) {
            $scope.beginExam();
        }
        else if ($scope.questionsAreVisible && $scope.checkAnswerButtonIsVisible) {
            $scope.checkAnswer();
        }
        else if ($scope.questionsAreVisible && $scope.nextQuestionButtonIsVisible) {
            $scope.nextQuestion();
        }
        else if ($scope.conclusionIsVisible) {
            $scope.doExamAgain();
        }
    }

    $scope.beginExam = function () {
        $scope.examPosition = ExamPositions.Questions;

        $scope.introductionIsVisible = false;
        $scope.questionsAreVisible = true;
        $scope.conclusionIsVisible = false;

        $scope.nextQuestion();
    }

    $scope.endExam = function () {
        $scope.examPosition = ExamPositions.Conclusion;

        $scope.introductionIsVisible = false;
        $scope.questionsAreVisible = false;
        $scope.conclusionIsVisible = true;

        $scope.calculateResults();
    };

    $scope.doExamAgain = function () {
        $scope.examPosition = ExamPositions.Introduction;

        $scope.introductionIsVisible = true;
        $scope.questionsAreVisible = false;
        $scope.conclusionIsVisible = false;

        $scope.questionTemplateNumber = 0;
        $scope.history = [];
    };

    $scope.examTemplate = $scope.getExamTemplate();

}]);