const nutdatmuabtn = document.getElementById('nutdatmuabtn');
    let danhsachsanphamtronggiohang = [];

    nutdatmuabtn.addEventListener('click', function(){
        let cotronggiohang = false;

        danhsachsanphamtronggiohang = JSON.parse(localStorage.getItem('giohang')) || [];
        

        if(danhsachsanphamtronggiohang == null){
            sanpham.soluongtronggiohang = 1;
            danhsachsanphamtronggiohang.push(sanpham)
        }
        else{
            for(let dem = 0; dem < danhsachsanphamtronggiohang.length; dem++){
                if(danhsachsanphamtronggiohang[dem].ma == sanpham.ma){
                    danhsachsanphamtronggiohang[dem].soluongtronggiohang = parseInt(danhsachsanphamtronggiohang[dem].soluongtronggiohang) +1;
                    cotronggiohang = true;
                }
            }
        }

        if(cotronggiohang == false){
            sanpham.soluongtronggiohang = 1;     
            danhsachsanphamtronggiohang.push(sanpham)      
        }
        alert('Thêm vào giỏ hàng thành công');
        localStorage.setItem('giohang', JSON.stringify(danhsachsanphamtronggiohang));
    })