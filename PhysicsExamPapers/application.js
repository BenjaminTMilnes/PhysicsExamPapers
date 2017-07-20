﻿var application = angular.module("PhysicsExamPapers", ["ngSanitize"]);

var ExamPositions = { Introduction: 0, Questions: 1, Conclusion: 2 }

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

application.controller("ExamController", ["$scope", "$http", function ($scope, $http) {

    $scope.examTemplate = {};

    $scope.examPosition = ExamPositions.Introduction;
    $scope.introductionIsVisible = true;
    $scope.questionsAreVisible = false;
    $scope.conclusionIsVisible = false;

    $scope.checkAnswerButtonIsVisible = false;
    $scope.tryQuestionAgainButtonIsVisible = false;
    $scope.nextQuestionButtonIsVisible = false;
    $scope.correctTextIsVisible = false;
    $scope.incorrectTextIsVisible = false;

    $scope.questionTemplates = [];
    $scope.questionTemplateNumber = 0;

    $scope.currentQuestionTemplate = {};
    $scope.currentQuestion = {};

    $scope.history = [];

    $scope.getExamTemplate = function (reference) {

        reference = "static_content/Physics_GeneralRelativity.json";

        $http.get(reference).then(function (response) {
            $scope.examTemplate = response.data;
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
                if ($scope.answer == $scope.currentQuestion.CorrectAnswers[i].Content) {
                    answerIsCorrect = true;
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
        });

        $scope.answer = "";

        $scope.correctTextIsVisible = false;
        $scope.incorrectTextIsVisible = false;

        $scope.checkAnswerButtonIsVisible = true;
        $scope.nextQuestionButtonIsVisible = false;
    };

    $scope.beginExam = function () {
        $scope.examPosition = ExamPositions.Questions;

        $scope.introductionIsVisible = false;
        $scope.questionsAreVisible = true;
        $scope.conclusionIsVisible = false;

        $scope.lineariseTemplate();
        $scope.nextQuestion();
    }

    $scope.endExam = function () {
        $scope.examPosition = ExamPositions.Conclusion;

        $scope.introductionIsVisible = false;
        $scope.questionsAreVisible = false;
        $scope.conclusionIsVisible = true;
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