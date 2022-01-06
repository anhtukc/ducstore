    const openmenubtn = document.getElementById('momenu');
    const closemenubtn = document.getElementById('dongmenu');
    const menuan = document.getElementById('menuan');

    const opencatalog = document.getElementById('opencatalog');
    const danhmuc = document.getElementById('danhmuc');
    const closecatalog = document.getElementById('closecatalog');

    const thietkevungtimkiem = document.getElementById('vungtimkiem');
    const opensearchbtn = document.getElementById('movungtimkiem');
    const closesearchbtn = document.getElementById('dongvungtimkiem');
    openmenubtn.addEventListener('click', function open(){
            menuan.classList.add('active');
    });

   closemenubtn.addEventListener('click',function close(){
       menuan.classList.remove('active');
   });
   opensearchbtn.addEventListener('click', function opensearch(){
       thietkevungtimkiem.style.display = `block`;
       closesearchbtn.style.display = `block`;
   });

   closesearchbtn.addEventListener('click', function closesearch(){
       thietkevungtimkiem.style.display = `none`;
       closesearchbtn.style.display = `none`;
   })

  
   opencatalog.addEventListener('click', function open(){
       if(danhmuc.classList.contains('thietkedanhmucactive')){
           danhmuc.classList.remove('thietkedanhmucactive');
       }
       else
       {
           danhmuc.classList.add('thietkedanhmucactive');
       }
       
       
   })