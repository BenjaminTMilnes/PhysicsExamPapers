var application = angular.module("PhysicsExamPapers", []);

var ExamPositions = { Introduction: 0, Questions: 1, Conclusion: 2 }

application.controller("ExamController", ["$scope", "$http", function ($scope, $http) {

    $scope.examTemplate = {};
    $scope.examPosition = ExamPositions.Introduction;
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

    $scope.getQuestion("", function (data) {
        $scope.currentQuestion = data;
        $scope.questionContent = $scope.currentQuestion.Content;
    });

}]);