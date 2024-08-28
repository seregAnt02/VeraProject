
/*В стандартных HTTP-формах для метода POST доступны три кодировки, задаваемые через атрибут*/

//application/x-www-form-urlencoded
//multipart / form - data
//text / plain
const body = document.getElementsByTagName('body')[0];

function IsEmail(email) {

    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (!regex.test(email)) {

        return false;

    }
    else {

        return true;

    }
}

export function ValidateEmail(email) {
    let re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}


export function ValidateCountry(country) {
    let re = new RegExp('.co$');
    return re.test(String(country).toLowerCase());
}

function ValidatePhone(phone) {
    let re = /^[0-9\s]*$/;
    return re.test(String(phone));
}

window.onload = async function () { 
             
    //     // Прослушиваем событие изменения размера страницы
    // window.addEventListener("resize", function() {
    //     // Здесь получаем размеры экрана (inner/outerWidth, inner/outerHeight)
    //         console.log(window.outerWidth + "/" + window.outerHeight);
    // }, false);

    let { firstTable, ChartShow, GetDate} = await import('../vue_js/modul_table.js');        
        
    firstTable.GetLogin();    
    
    ChartShow();        

    const validate_add_save = import('../validate/valid_form_addSave.js');    

//---------------------------------------
//---------------------------------------

    const img_add = document.getElementById('img_add_row');
    img_add.onclick = async function(even){                

        var hostname = $(window.location).attr('hostname');
        var url_plus_dark = hostname == "localhost" ? "/images/png-icons-plus-dark.png" : "/vera/images/png-icons-plus-dark.png";
        img_add.setAttribute("src", url_plus_dark);

        setTimeout(() => {
            
            var url_plus = hostname == "localhost" ? "/images/png-icons-plus.png" : "/vera/images/png-icons-plus.png";
            img_add.setAttribute("src", url_plus);            

        }, 75);             

        const forma_loading = RowsAddUpdateTable();        

        //console.dir(forma_loading);
        
        forma_loading.then(result => {

            if(body.clientWidth < 576) result.classList.add("sizeFormMobilFormat");
            else if(!result.hasAttribute("forma-add-update add-forms-del-ok-clear")){

                result.classList.remove("forma-add-update-clear");
                result.classList.remove("add-forms-del-ok-clear");
                result.classList.add("forma-add-update");                                  
            }

            const btn_cancel = document.getElementById('btn_cancel');


            btn_cancel.onclick = async function(){                
                if(body.clientWidth < 576) {
                    result.classList.remove("sizeFormMobilFormat");
                    result.classList.remove("size-mobil-del");
                    result.classList.add("forma-add-update-clear");                
                }
                else{

                    result.classList.remove("forma-add-update");
                    result.classList.add("forma-add-update-clear");                
                }                                
            }            
            
            const btn_ok = document.getElementById('btn_ok');

            btn_ok.onclick = async function(event){

                (await validate_add_save).ValidateAddSave(even, result.children[1]);                
                
            }            

            result.children[1].children[1].textContent = "Добавить строку?";                      
        },
        error => {alert(error);})
        
        //console.log(forma_loading); 
    }
    
    const button_add_measuring = document.getElementById('button_add_measuring');
    button_add_measuring.onclick = async function () {            

       GetDate();                       
    }        

}

let forma_loading = document.getElementById('forma_loading');

export async function RowsAddUpdateTable(){  
    
    var hostname = $(window.location).attr('hostname');
    var url = hostname == "localhost" ? "/Home/TableAddUpdate" : "/vera/Home/TableAddUpdate";
    //const url = "/Home/TableAddUpdate";

    let response = await fetch( url, {
  
        method: 'GET',
        headers: {
            //'Content-Type': 'application/json;charset=utf-8'
            'Content-Type': 'text/html;charset=utf-8'
            //'Content-Type':'multipart/form-data'
            //"Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
        }

    });    

    if (response.ok === true) {      

      forma_loading.setAttribute("zIndex", "1");

      forma_loading.innerHTML = await response.text();      
      
      //console.dir(forma_loading);

    }

    return forma_loading;

}

export async function AddFormDelCancel(){    

    var hostname = $(window.location).attr('hostname');
    var url = hostname == "localhost" ? "/home/addformsdelCancel" : "/vera/home/addformsdelcancel";
    //const url = "/Home/AddFormsDelCancel";

    let response = await fetch( url, {
  
        method: 'GET',
        headers: {
            //'Content-Type': 'application/json;charset=utf-8'
            //'Content-Type': 'text/html;charset=utf-8'
            //'Content-Type':'multipart/form-data'
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
        }

    });    

    if (response.ok === true) {      

      forma_loading.setAttribute("zIndex", "1");

      forma_loading.innerHTML = await response.text();                    

      //console.dir(forma_loading);

    }

    return forma_loading;
}
