myapp.controller("home", function ($scope, $http) {
    $scope.index = 0;
    let thumbnail;

    $scope.autosilide = function () {
        
        for (let count = 0; count < thumbnail.length; count++) {
            thumbnail[count].style.display = `none`;
            thumbnail[$scope.index].style.display = 'block';
        };
        $scope.index++;
        setTimeout($scope.autosilide, 3000);
        if ($scope.index > thumbnail.length - 1) { $scope.index = 0; }
    };

    $scope.slides = function (indexOfInput) {
        $scope.index += indexOfInput;
        if ($scope.index > thumbnail.length - 1) { $scope.index = 0; }

        for (let count = 0; count < thumbnail.length; count++) {
            thumbnail[count].style.display = `none`;
            thumbnail[$scope.index].style.display = 'block';
        };


    };
   
    $scope.GetNew = function () {
        $http({
            method: "GET",
            url: "/home/GetNew"
        }).then(function (res) {
            $scope.newslist = res.data;
            console.log(res.data);
        }), (function (error) {
            console.log(error);
        })
    };
    $scope.GetNew();
    angular.element(function () {
        thumbnail = document.getElementsByClassName(`newimage`);
        $scope.autosilide();
    });

})