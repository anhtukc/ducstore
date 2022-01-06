myapp.controller("signin", ['$scope', '$http', function ($scope, $http) {
    $scope.username = '';
    $scope.password = '';

    $scope.singinadmin = function () {
        let check = true;
        if ($scope.username == "" || $scope.password == "") {
            alert('Không bỏ trống tài khoản và mật khẩu');
        }
        if (check == true) {
            $http({
                method: 'Get',
                datatype: 'JSON',
                url: '/home/signinadmin/',
                params: {
                    'username': $scope.username,
                    'password': $scope.password
                }
            }).then(function (res) {
                if (res.data == 'ok') {
                    window.location.href = '/admin/products/index/';
                }
                else {
                    alert('sai tài khoản hoặc mật khẩu');
                }
            })
        }
    }
}])