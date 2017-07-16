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

    $scope.partNumber = 0;
    $scope.questionNumber = 0;
    $scope.numberOfRepetitions = 0;
    $scope.currentQuestion = {};

    $scope.history = [];

    $scope.getExamTemplate = function (reference) {

        reference = "static_content/Physics_GeneralRelativity.json";

        $http.get(reference).then(function (response) {
            $scope.examTemplate = response.data;
        })
    }

    $scope.getQuestion = function (reference, callback) {
        $http.get("api/questions/1").then(
                  function (response) {
                      callback(response.data);
                  });
    };

    $scope.answerQuestion = function () { };

    $scope.checkAnswer = function () {

        var answerIsCorrect = false;

        for (var i = 0; i < $scope.currentQuestion.CorrectAnswers.length; i++) {
            if ($scope.answer == $scope.currentQuestion.CorrectAnswers[i].Content) {
                answerIsCorrect = true;
            }
        }

        var completedQuestion = { AnswerIsCorrect: answerIsCorrect, Level: 1 };

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

        var numberOfParts = $scope.examTemplate.Parts.length;

        if (numberOfParts < 1) {
            $scope.endExam();
            return;
        }
        if ($scope.partNumber == 0) {
            $scope.partNumber = 1;
        }

        var part =  $scope.examTemplate.Parts[$scope.partNumber - 1];
        var numberOfQuestionsInCurrentPart = part.Questions.length;
        
        if ($scope.questionNumber < numberOfQuestionsInCurrentPart) {
            $scope.questionNumber++;
        }
        else if ($scope.partNumber < numberOfParts) {
            $scope.partNumber++;
            $scope.questionNumber = 1;
        }
        else {
            $scope.endExam();
            return;
        }

        var question =  part.Questions[$scope.questionNumber - 1];
        var reference =  question.Reference;

        $scope.getQuestion(reference, function (data) {
            $scope.currentQuestion = data;
            $scope.questionContent = $scope.currentQuestion.Content;
        });

        $scope.checkAnswerButtonIsVisible = true;
        $scope.nextQuestionButtonIsVisible = false;
    };

    $scope.beginExam = function () {
        $scope.examPosition = ExamPositions.Questions;
        $scope.introductionIsVisible = false;
        $scope.questionsAreVisible = true;

        $scope.nextQuestion();
    }

    $scope.endExam = function () {

        $scope.examPosition = ExamPositions.Conclusion;
        $scope.introductionIsVisible = false;
        $scope.questionsAreVisible = false;
        $scope.conclusionIsVisible = true;
    };

    $scope.examTemplate = $scope.getExamTemplate();

}]);