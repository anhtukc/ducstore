const anhquangcao = document.getElementsByClassName(`newimage`);
var index = 0;
autosilide();
    
    function autosilide(){
        let count =0;
        for(count; count<anhquangcao.length; count++){
            anhquangcao[count].style.display = `none`;         
        }
        if(index>anhquangcao.length-1)
            index=0;
        anhquangcao[index].style.display = `block`;
        anhquangcao[index].style.zIndex ='1';
        index++; 
       
        setTimeout(autosilide, 3000);
    };
    function slides(indexinput){
        let count =0;
        for(count; count<anhquangcao.length; count++){
            anhquangcao[count].style.display = `none`;         
        }
        index+=indexinput;
        if(index>anhquangcao.length-1)
            index=0;
        anhquangcao[index].style.display = `block`;
        
    };