myapp.controller('SellBills', function ($scope, $http) {
    $scope.sellbillid = '';
    $scope.productid = '';
    $scope.productname = '';
    $scope.quantity = 1;
    $scope.price = 0;
    $scope.remainingquantity = 0;
    $scope.grandpaid = 0;
    $scope.totalpaid = 0;
    $scope.Details = [];
    $scope.user = {
        'customername': "",
        'address': "",
        'phonenumber': "",
    };
    let CheckAddAble = true;
    let CheckCreateBillAble = true;
    const url = "/sellbills/";
    $scope.LogName = function () {      
        $.each($scope.productList, function (index,item) {
            if (item.productid == $scope.productid) {
                $scope.productname = item.productname;
                $scope.remainingquantity = item.quantity;
                $scope.price = item.price;
            }
        })
        console.log($scope.productname);

    }
    $scope.GetDetail = function (billid) {  
        $http({
            method: "Get",
            url: url+"/GetDetails/",
            params: { sellbillid: billid}
            
        }).then(function (res) {
             
            $scope.Details = res.data;
            $scope.caculateTotalPaid();
           
        })
    };
    $scope.GetProduct = function () {
        $http({
            method: "Get",
            url: url +"GetAllProduct/"

        }).then(function (res) {
            console.log(res);
            $scope.productList = res.data;

        })
    }
    $scope.CheckEmptyProductId = function () {
        if ($scope.productid == '') {
            alert('Không được bỏ trống mã sản phẩm');
            CheckAddAble = false;
        }      
    }

    $scope.CheckIdInsideList = function () {
        let check = false;
        $.each($scope.productList, function (index, item) {
            if (item.productid == $scope.productid) {
                check = true;
            }
        });

        if (check == false) {
            alert('Mã sản phẩm không hợp lệ. Vui lòng chọn lại');
            CheckAddAble = false;
        }
    }
    $scope.QuantityCantSmallThan1 = function () {
        if ($scope.quantity < 1 || $scope.remainingquantity <1) {
            alert('Số lượng không được nhỏ 1 sản phẩm');
            CheckAddAble = false;
        }
        
    }

    $scope.AddBillDetails = function () {
        let checkDuplicate = false;
        $scope.CheckEmptyProductId();
        $scope.QuantityCantSmallThan1();
        $scope.CheckIdInsideList();
        if (CheckAddAble == true) {
            $.each($scope.Details, function (index, item) {
                if (item.productid == $scope.productid) {
                    item.quantity += $scope.quantity;
                    item.grandpaid = item.quantity * item.price;
                    checkDuplicate = true;
                }
            })

            $.each($scope.productList, function (index, item) {
                if (item.productid == $scope.productid) {
                    item.quantity -= $scope.quantity;
                    
                }
            })

            if (checkDuplicate == false) {
                $scope.Details.push({
                    "productid": $scope.productid,
                    "productname": $scope.productname,
                    "price": $scope.price,
                    "quantity": $scope.quantity,
                    "grandpaid": $scope.price * $scope.quantity,
                });
            }
            $scope.caculateTotalPaid();
        }
        CheckAddAble = true;

        console.log($scope.Details);
    }

    $scope.caculateTotalPaid = function () {
        $scope.totalpaid = 0;
        $.each($scope.Details, function (index, item) {
            $scope.totalpaid += item.grandpaid;
        })
    }

    $scope.DeleteDetail = function (id) {
        $.each($scope.Details, function (index, item) {
            if (item.productid == id) {
                $scope.Details.splice(index, 1);
            }
        })
        $scope.caculateTotalPaid();
    }

    $scope.NewBill = function () {
        $scope.sellbillid = '';
        $scope.productid = '';
        $scope.productname = '';
        $scope.quantity = 1;
        $scope.price = 0;
        $scope.remainingquantity = 0;
        $scope.grandpaid = 0;
        $scope.totalpaid = 0;
        $scope.Details = [];
        $scope.user = {
            'name': "",
            'address': "",
            'phonenumber': "",
            'flag': true
        };
    }

    $scope.CheckEmptyDetailList = function () {
        if ($scope.Details == null) {
            alert('Vui lòng chọn sản phẩm cho hóa đơn');
            CheckCreateBillAble = false;
        }
    }

    $scope.CheckEmptyCustomer = function () {
        let mes = '';
        if ($scope.user.name == '') {
            mes += 'Vui lòng nhập họ tên \n';
            CheckCreateBillAble = false;
        }
        if ($scope.user.phonenumber == '') {
            mes += 'Vui lòng nhập số điện thoại \n';
            CheckCreateBillAble = false;
        }
        if ($scope.user.address == '') {
            mes += 'Vui lòng nhập địa chỉ \n';
            CheckCreateBillAble = false;
        }
        if (mes != '') {
            alert(mes);
        }
    }
    $scope.CreateBill = function () {
        $scope.CheckEmptyCustomer();
        $scope.CheckEmptyDetailList();
        let resquest = {
            'phonenumber':  $scope.user.phonenumber,
            'shippingtype': $scope.user.shipping,
            'totalprice': $scope.totalpaid,
            'list': $scope.Details
        };

        if (CheckCreateBillAble == true) {
            $http({
                method: 'Post',
                url: '/sellbills/CheckCustomerInfo',
                datatype: 'Json',
                data: JSON.stringify($scope.user)
            }).then($http({
                method: 'Post',
                url: '/sellbills/Create',
                datatype: 'Json',
                data: JSON.stringify(resquest)
            }).then(function (res) {
                if (res.data == 'CreateSuccessful') {
                    alert('Tạo hóa đơn thành công');
                }
            }))
        }
        CheckCreateBillAble = true;
    }

    $scope.DeleteBill = function (id) {
        let Confirme = window.confirm('Bạn có chắc chắn xóa hóa đơn?');
        if (Confirme) {
            $http({
                method: 'Post',
                url: 'sellbills/Delete',
                params: {id : id}
            }).then(function (res) {
                if (res.data == 'DeletedSuccessful') {
                    alert('Xóa thành công');
                    location.reload();
                }
            })
        }
    }

    $scope.ChangeQuantity = function (id, value) {
        console.log(id);
        $.each($scope.Details, function (index, item) {
            if (item.productid == id) {
                item.quantity += value;
                item.grandpaid = item.quantity * item.price;
            }
            
        })
        $scope.caculateTotalPaid();
    }

    $scope.SaveChange = function () {
        $http({
            method: 'Post',
            url: '/sellbills/EditBills',
            data: {
                'sellbillid': $scope.Details[0].sellbillid,
                'totalpaid': $scope.totalpaid,
                'list': $scope.Details
            }
        }).then(function (res) {
            alert(res.data);
            $scope.GetProduct();
        })
    }
    $scope.GetProduct();
})