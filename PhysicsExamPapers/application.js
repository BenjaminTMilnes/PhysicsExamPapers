var application = angular.module("PhysicsExamPapers", []);

var ExamPositions = { Introduction: 0, Questions: 1, Conclusion: 2 }

application.controller("ExamController", ["$scope", "$http", function ($scope, $http) {

    $scope.examTemplate = {};
    $scope.examPosition = ExamPositions.Introduction;
    $scope.introductionIsVisible = true;
    $scope.questionsAreVisible = false;
    $scope.conclusionIsVisible = false;

    $scope.partNumber = 0;
    $scope.questionNumber = 0;
    $scope.currentQuestion = {};

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

    $scope.nextQuestion = function () {

        var numberOfParts = $scope.examTemplate.Parts.length;

        if (numberOfParts < 1) {
            $scope.endExam();
        }
        if ($scope.partNumber == 0) {
            $scope.partNumber = 1;
        }

        var numberOfQuestionsInCurrentPart = $scope.examTemplate.Parts[$scope.partNumber - 1].Questions.length;

        if ($scope.questionNumber < numberOfQuestionsInCurrentPart) {
            $scope.questionNumber++;
        }
        else if ($scope.partNumber < numberOfParts) {
            $scope.partNumber++;
            $scope.questionNumber = 1;
        }
        else {
            $scope.endExam();
        }
                             

        var reference = $scope.examTemplate.Parts[$scope.partNumber - 1].Questions[$scope.questionNumber - 1].Reference;
        
        $scope.getQuestion( reference, function (data) {
                 $scope.currentQuestion = data;
            $scope.questionContent = $scope.currentQuestion.Content;
        });
    };

    $scope.beginExam = function () {
        $scope.examPosition = ExamPositions.Questions;
        $scope.introductionIsVisible = false;
        $scope.questionsAreVisible = true;

        $scope.nextQuestion();
    }

    $scope.endExam = function () {
             

    };
    $scope.examTemplate = $scope.getExamTemplate();

}]);