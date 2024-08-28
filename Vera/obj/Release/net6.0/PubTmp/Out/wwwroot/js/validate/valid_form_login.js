
let forma_login = document.getElementById('formaRegister');

//let input_password = forma_login.getElementsByClassName('js-input-password').password;

//console.dir(input_email);
function validateEmail(email) {
    let re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}


function validateCountry(country) {
    let re = new RegExp('.co$');
    return re.test(String(country).toLowerCase());
}

function validatePhone(phone) {
    let re = /^[0-9\s]*$/;
    return re.test(String(phone));
}
//------------------------------
//------------------------------

export function validInput(event){

    let input_email = forma_login.getElementsByClassName('js-input-email').email;

    let label_error = forma_login.getElementsByClassName('label-error-show')[0];

    let input_password = forma_login.getElementsByClassName('js-input-password')[0];

    let input_captcha = forma_login.getElementsByClassName('js-input-captcha')[0];
   
    if(input_email.value === ''){                
        input_email.classList.add('error');                                 

        //label_error.setAttribute('display','block');
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите почту"        
        return event.preventDefault();   
    } else{
        input_email.classList.remove('error');
    }        

    if(!validateEmail(input_email.value)){
        label_error.removeAttribute("hidden");                
        label_error.textContent = "Пожайлуста!, введите валидную почту"                
        input_email.classList.add('error');
        return event.preventDefault();
    } else{
        input_email.classList.remove('error');
    }    
    
    if(input_password.value === ''){                
        input_password.classList.add('error');                                         
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите пароль"        
        return event.preventDefault();     
    } else{
        input_password.classList.remove('error');
    }

    if(input_captcha.value === ''){
        input_captcha.classList.add('error');
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите цифры с картинки."
        return event.preventDefault();
    } else{
        input_captcha.classList.remove('error');
    }
}
    
    