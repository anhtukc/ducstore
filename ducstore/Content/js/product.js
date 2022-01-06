myapp.controller("product", function ($scope, $http) {
    $scope.index = 0;
    $scope.typeproductid = '';
    $scope.typeproductname = '';
    $scope.showproduct = [];
    $scope.relativeproduct = [];
    $scope.shoppingproduct = JSON.parse(localStorage.getItem('list')) || [];
    $scope.user = {
        'name': "",
        'address': "",
        'phonenumber': ""
    };
    $scope.totalprice = 0;

    $scope.Gettype = function () {
        $http({
            method: "GET",
            url: "/products/GetTypeproduct"
        }).then(function (res) {
            $scope.types = res.data;

        })
    }
    $scope.GetProduct = function () {
        $http({
            method: "GET",
            url: "/products/GetProduct"
        }).then(function (res) {
            $scope.products = res.data;
            $scope.showproduct = res.data;
            console.log($scope.products);
        })
    }
    $scope.FilterRemove = function () {
        $scope.showproduct = $scope.products;
    }
    $scope.SetlocationStore = function () {
        localStorage.setItem('list', JSON.stringify($scope.shoppingproduct));
        $scope.shoppingproduct = JSON.parse(localStorage.getItem('list')) || [];
    }
    $scope.Filter = function (type) {
        $scope.showproduct = [];

        $.each($scope.products, function (index, val) {
            if (val.typeproductid == type) {

                $scope.showproduct.push(val);
            }
        })
    };

    $scope.GetProduct();
    $scope.Gettype();
    $scope.buy = function (id) {
        let check = true;
        let price = 0;
        $.each($scope.products, function (index, val) {
            if (val.productid == id) {
                $scope.product = val;
                
            }
        })

        if ($scope.product.promotion == 0) {
            price = $scope.product.price;
        }
        else {
            price = $scope.product.promotion;
        }

        $.each($scope.shoppingproduct, function (index, val) {
            if (val.productid == id) {

                val.quantity += 1;              
                val.grandpaid = price * val.quantity;
            
                check = false;
            }
        })
       
        if (check == true) {
            $scope.shoppingproduct.push({
                'picture': $scope.product.picture,
                'productid': $scope.product.productid,
                'productname': $scope.product.productname,
                'price': price,
                'quantity': 1,
                'grandpaid': price
            });
        }
        $scope.SetlocationStore();
        alert('Thêm sản phẩm thành công');
    }
    $scope.GetRelative = function (type, productid) {
       
        $.each($scope.products, function (index, val) {
            if (val.typeproductid == type) {
                $scope.relativeproduct.push(val);
            }
        })
        console.log($scope.relativeproduct);
    }
   
    $scope.CaculateTotalprice = function () {
        $scope.totalprice = 0;
        $.each($scope.shoppingproduct, function (i, item) {
            $scope.totalprice += item.grandpaid;
        });
    };


    $scope.ChangeQuantity = function (id, number) {
        let price = 0;
        $.each($scope.shoppingproduct, function (i, item) {
            if (item.productid == id) {
                item.quantity += number;
                if (item.promotion > 0) {
                    price = item.promotion;
                }
                else {
                    price = item.price;
                }
                if (item.quantity <= 0) {
                    item.quantity = 1;
                }
                item.grandpaid = price * item.quantity;
                console.log(item);
            }
        });
        $scope.totalprice = 0;
        $scope.CaculateTotalprice();
        $scope.SetlocationStore();
    }
    
    $scope.DeleteItem = function (id) {
        $.each($scope.shoppingproduct, function (i, item) {
            if (item.productid == id) {
                $scope.shoppingproduct.splice(i, 1);
                $scope.CaculateTotalprice();
                $scope.SetlocationStore();            
            };
        });

    };

    $scope.OrderBuy = function () {
        let list = [];
        $.each($scope.shoppingproduct, function (i, val) {
            list.push({
                'productid': val.productid,
                'productname': val.productname,
                'price': val.price,
                'quantity': val.quantity,
                'grandpaid': val.grandpaid
            })
        });
        let customer = {
            'customername': $scope.user.name,
            'phonenumber': '0' + $scope.user.phonenumber,
            'address': $scope.user.address,
        };
        let resquest = {
            'phonenumber': '0' + $scope.user.phonenumber,
            'totalprice': $scope.totalprice,
            'list':list
        }
        if (customer.customername != "" && customer.phonenumber != "" && $scope.shoppingproduct != []) {
            $http({
                method: 'Post',
                type: 'Json',
                url: "/shopping/CheckCustomerInfo",
                data: JSON.stringify(customer)
            }).then(function () {
                $http({
                    method: "Post",
                    type: "Json",
                    url: "/shopping/Create",
                    data: JSON.stringify(resquest)
                }).then(function (res) {

                    if (res.data == "CreateSuccessful") {
                        alert("Đặt hàng thành công");
                        $scope.shoppingproduct = [];
                        
                        $scope.CaculateTotalprice = 0;
                        $scope.SetlocationStore();
                        location.reload();
                    }
                });
            });
        }

    };
})