var application = angular.module("PhysicsExamPapers", []);

var ExamPositions = { Introduction: 0, Questions: 1, Conclusion: 2 }

application.controller("ExamController", ["$scope", "$http", function ($scope, $http) {

    $scope.examTemplate = {};
    $scope.examPosition = ExamPositions.Introduction;
    $scope.introductionIsVisible =  true;
    $scope.questionsAreVisible = false;
    $scope.conclusionIsVisible = false;


    $scope.partNumber = 0;
    $scope.questionNumber = 0;
    $scope.currentQuestion = {};

    $scope.getExamTemplate = function (reference) {

        reference = "static_content/Physics_GeneralRelativity.json";

        $http.get(reference).then(function (response) {
            return response.data;
        })
    }

    $scope.getQuestion = function (reference, callback) {
        $http.get("api/questions/1").then(
                  function (response) {
                      callback(response.data);
                  });
    };

    $scope.nextQuestion = function () {
        $scope.getQuestion("", function (data) {

            var numberOfParts = $scope.examTemplate.Parts.length;


            var numberOfQuestionsInCurrentPart = $scope.examTemplate.Parts[$scope.partNumber].Questions.length;

            $scope.currentQuestion = data;
            $scope.questionContent = $scope.currentQuestion.Content;
        });
    };

    $scope.beginExam = function () {

        $scope.introductionIsVisible = false;
        $scope.questionsAreVisible = true;

        $scope.nextQuestion();
    }



}]);