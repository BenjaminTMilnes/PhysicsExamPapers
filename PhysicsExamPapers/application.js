var application = angular.module("PhysicsExamPapers", []);

application.controller("ExamController", ["$scope", "$http", function ($scope, $http) {

    $scope.examTemplate = {};


    $scope.getQuestion = function (reference) {
        $http.get("api/questions/1").then(function (response) {
            return response.data;
        });
    };

}]);