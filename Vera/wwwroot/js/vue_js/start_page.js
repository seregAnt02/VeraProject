
// let {obj_js_add_measuring} = import('../vue_js/js_add_measuring.js');
// console.log(obj_js_add_measuring);

let vue_start_page = new Vue({
    el: '#containerId',
    data: {
        //massage:'Ура Vue!'
      //rows: []
    },
        methods:{
                   
            RegisterForm(){

                GetRegister();
            },
            LoginEnter(){
                            
              LoginIn();
            }
            
    }
  });    
  const body = document.getElementsByTagName('body')[0];
  const containerId = document.getElementById('containerId');
  const forma_register_login = document.getElementById('formaRegister');  
  const img_modal = document.getElementById('img_modal');    
  const imgRadarDiv = document.getElementById('imgRadarDiv');      
  
  // Прослушиваем событие изменения размера страницы
window.addEventListener("resize", function() {
  // Здесь получаем размеры экрана (inner/outerWidth, inner/outerHeight)  

  const img_captcha = document.getElementsByClassName('img-captcha')[0];      
  
  //console.dir(img_captcha);
  //OrientationMobil(img_captcha);

}, false); 

var hostname = $(window.location).attr('hostname');

async function LoginIn(){
      
    var url = hostname == "localhost" ? "/account/login" : "/vera/account/login";
  //const url = "/account/login";                                    
              let responseGet = await fetch( url, {                    
                method: 'GET',
                headers: {
                    'Content-Type': 'text/html;charset=utf-8'                                    
                }
    
            });                                    
            body.classList.add('body-color-load');                                                                       
            imgRadarDiv.style.marginTop = -(containerId.offsetHeight / 2) + 'px';   
            //imgRadarDiv.hidden = false;
            img_modal.hidden = false;
            if (responseGet.ok === true) {                                                                   
                            
              forma_register_login.innerHTML = await responseGet.text();                   
              // валидация формы login
              const validLoginForma = await import('../validate/valid_form_login.js');                            
                            
              forma_register_login.classList.remove('add-forms-del-ok-clear');              
              forma_register_login.classList.add('add-style-form-register');                    
              
              let btn_cancel = forma_register_login.getElementsByClassName('btn-cancel')[0];
              let btn_ok = forma_register_login.getElementsByClassName('btn-ok')[0];              
              let input_email = forma_register_login.getElementsByClassName('js-input-email').email;
              let input_password = forma_register_login.getElementsByClassName('js-input-password')[0];
              let input_captcha = forma_register_login.getElementsByClassName('js-input-captcha')[0];
              const img_captcha = document.getElementsByClassName('img-captcha')[0];                 

              img_modal.hidden = true;
              //imgRadarDiv.hidden = true;
              body.classList.remove('body-color-load');        
              
              //СhangeSizeForms(img_captcha);
              OrientationMobil(img_captcha);

              //console.dir(forma_register_login);
              
              btn_cancel.onclick = async function(){

                forma_register_login.classList.remove('add-style-form-register');
                forma_register_login.classList.remove('sizeFormMobilFormat');
                forma_register_login.classList.remove("mobil-orientation-form-register");
                forma_register_login.classList.add('add-forms-del-ok-clear');
                input_email.classList.remove('error');
                input_password.classList.remove('error');
                input_captcha.classList.remove('error');                
              }   
              
              btn_ok.onclick = async function(event){                                
                
                validLoginForma.validInput(event);                

                if(!event.defaultPrevented){

                    body.classList.add('body-color-load');                                                                       
                    imgRadarDiv.style.marginTop = -(containerId.offsetHeight / 2) + 'px';            
                    img_modal.hidden = false;       
                    const form = new FormData(forma_register_login.children.login_form);
                    forma_register_login.remove();

                    //var hostname = $(window.location).attr('hostname');
                    var url = hostname == "localhost" ? "/account/login" : "/vera/account/login";

                    //console.dir(window.location.host);                    
                    
                    let responsePost = await fetch( url, {
                                
                            method: 'POST',                        
                            body: form            
                        });
                        if (responsePost.ok === true){

                            await responsePost.text();                      
                            img_modal.hidden = true;
                            body.classList.remove('body-color-load');
                            document.location = hostname == "localhost" ? "/home/index" : "/vera/home/index";
                        }    
                                                    
                }                                
              }                                                        
            }   
}

 async function GetRegister(){    

      //var hostname = $(window.location).attr('hostname');
      var url = hostname == "localhost" ? "/account/register" : "/vera/account/register";
        //const url = "/account/register";                                
    
              let responseGet = await fetch( url, {
                    
                method: 'GET',
                headers: {
                    'Content-Type': 'text/html;charset=utf-8'                
                    //'Content-Type': 'application/json;charset=utf-8'
                    //"Content-Type": "application/x-www-form-urlencoded"
                }
    
            });                        
            body.classList.add('body-color-load');                                                                       
            imgRadarDiv.style.marginTop = -(containerId.offsetHeight / 2) + 'px';            
            img_modal.hidden = false;     
            if (responseGet.ok === true) {                                                                                               

              forma_register_login.innerHTML = await responseGet.text();        
              const valid_register = await import('../validate/valid_form_register.js');      
              const btn_cancel = forma_register_login.getElementsByClassName('btn-cancel')[0];                 
              const btn_ok = forma_register_login.getElementsByClassName('btn-ok')[0];
              const check_box_input = forma_register_login.getElementsByClassName('check_box-privacy-policy')[0];       
              const img_captcha = document.getElementsByClassName('img-captcha')[0];                

              PrivacyPolicy();                            

              check_box_input.onclick = async function(){
                  if(this.checked){

                      btn_ok.disabled = false;                      
                  } else{
                    btn_ok.disabled = true;
                  }

              }
              forma_register_login.classList.remove('add-forms-del-ok-clear');
              // img_captcha.classList.remove('mobil-img-captcha');
              forma_register_login.classList.add('add-style-form-register');                
                            
              //forma_register_login.style.display = 'block';              
            
              OrientationMobil(img_captcha);

              //console.dir(forma_register_login);

              btn_cancel.onclick = async function(){
                
                forma_register_login.classList.remove('sizeFormMobilFormat');                
                forma_register_login.classList.remove("mobil-orientation-form-register");
                //forma_register_login.style.display = 'none';     
                forma_register_login.classList.remove('add-style-form-register');
                forma_register_login.classList.remove('label-headers-min0');
                forma_register_login.classList.add('add-forms-del-ok-clear');                                     
              }  

              img_modal.hidden = true;   
              body.classList.remove('body-color-load');    

              btn_ok.onclick = async function(event){
                               
                valid_register.validRegister(event);

                //console.dir(forma_register_login.children[1]);
                
                if(!event.defaultPrevented){
                    const form = new FormData(forma_register_login.children[1]);   

                    //const url = "/account/register";                                                
                        let responsePost = await fetch( url, {
                              
                          method: 'POST',                        
                          body: form            
                      });
                      if (responsePost.ok === true){

                          await responsePost.text();                      
                          img_modal.hidden = true;
                          body.classList.remove('body-color-load');
                          //document.location = "/";
                          document.location = hostname == "localhost" ? "/account/registrationsuccessful" : "/vera/account/registrationsuccessful";
                      }
                }                
              }
            }   
            
    
}

function OrientationMobil(img_captcha){

  var width = $(window).width();             
  
  if(width <= 333){
        forma_register_login.classList.remove('add-style-form-register');        
        forma_register_login.classList.remove('label-headers');
                
        forma_register_login.classList.add('sizeFormMobilFormat');        
        
        img_captcha.classList.remove('img-captcha');
        //img_captcha.classList.add('mobil-img-captcha'); 

        console.dir("обычная");                
  }

  if (width > 333 && width < 576) {                     
  forma_register_login.classList.remove('add-style-form-register');  
  forma_register_login.classList.remove('sizeFormMobilFormat');   
  forma_register_login.classList.remove('add-forms-del-ok-clear');     
  forma_register_login.classList.remove('label-headers-min0');
  forma_register_login.classList.add('mobil-orientation-form-register');
  
  console.dir("горизонтальная");    
  // img_captcha.classList.remove('img-captcha');
  // img_captcha.classList.add('mobil-img-captcha');  
  } 

}

async function PrivacyPolicy(){

  let ref_a_check_box = forma_register_login.getElementsByClassName("ref-a-check-box")[0];  

  let containerId = document.getElementById('containerId');

  let startpage = document.getElementsByClassName('body-startpage')[0];

  let loadingForm = document.getElementsByClassName('loadingForm')[0];

  ref_a_check_box.onclick = async function(){

        //var hostname = $(window.location).attr('hostname');
        var url = hostname == "localhost" ? "/account/privacypolicy" : "/vera/account/privacypolicy";
          //const url = "/account/privacypolicy";                                
          
          let response = await fetch( url, {
                
            method: 'GET',
            headers: {
                'Content-Type': 'text/html;charset=utf-8'                
                //'Content-Type': 'application/json;charset=utf-8'
                //"Content-Type": "application/x-www-form-urlencoded"
            }

        });
                
        body.classList.add('body-color-load');
        
        img_modal.hidden = false;

        if (response.ok === true){                              

          containerId.hidden = true;

          loadingForm.innerHTML = await response.text();
          loadingForm.hidden = false;         
          startpage.classList.add('body-color-privacy-policy');                            

          let img_close_icon = loadingForm.getElementsByClassName('img-close-icon')[0];
          img_close_icon.onclick = async function(){
            
            var url_close_dark = hostname == "localhost" ? "/images/png-close-dark.png" : "/vera/images/png-close-dark.png";
            
            img_close_icon.setAttribute("src", url_close_dark);

            setTimeout(() => {

              var url_close = hostname == "localhost" ? "/images/png-close.png" : "/vera/images/png-close.png";

                img_close_icon.setAttribute("src", url_close);            

            }, 75);    

            containerId.hidden = false;                    
            //rgb(255, 255, 255)
            startpage.classList.remove('body-color-privacy-policy');
            loadingForm.hidden = true;
                        
          }

          body.classList.remove('body-color-load');
          img_modal.hidden = true;
        }
      
  }  
}
