
export const forma_register = document.getElementById('formaRegister');

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

export function validRegister(event){

    const js_input_firsname = forma_register.getElementsByClassName('js-input-firsname')[0];    
    const js_input_lastname = forma_register.getElementsByClassName('js-input-lastname')[0];
    const js_input_email = forma_register.getElementsByClassName('js-input-email')[0];
    const js_input_city = forma_register.getElementsByClassName('js-input-city')[0];
    const js_input_year = forma_register.getElementsByClassName('js-input-year')[0];
    const js_input_password = forma_register.getElementsByClassName('js-input-password')[0];
    const js_input_passwordConfirm = forma_register.getElementsByClassName('js-input-passwordConfirm')[0];
    const js_input_captcha = forma_register.getElementsByClassName('js-input-captcha')[0];
    const label_error = forma_register.getElementsByClassName('label-error-show')[0];    
    if(js_input_firsname.value === ''){                    
        js_input_firsname.classList.add('error');                  
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите Имя."        
        return event.preventDefault();   
        } else{
            js_input_firsname.classList.remove('error');
    }
    if(js_input_lastname.value === ''){                
        js_input_lastname.classList.add('error');                                             
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите Фамилию."        
        return event.preventDefault();   
        } else{
            js_input_lastname.classList.remove('error');
    }
    if(js_input_email.value === ''){                
        js_input_email.classList.add('error');                                             
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите почту."        
        return event.preventDefault();   
        } else{
            js_input_email.classList.remove('error');
    }
    if(js_input_city.value === ''){                
        js_input_city.classList.add('error');                                             
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите город проживания."        
        return event.preventDefault();   
        } else{
            js_input_city.classList.remove('error');
    }
    if(js_input_year.value === ''){                
        js_input_year.classList.add('error');                                             
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите год рождения."        
        return event.preventDefault();   
        } else{
            js_input_year.classList.remove('error');
    }
    if(js_input_password.value === ''){                
        js_input_password.classList.add('error');                                             
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите пароль."        
        return event.preventDefault();   
        } else{
            js_input_password.classList.remove('error');
    }
    if(js_input_passwordConfirm.value === ''){                
        js_input_passwordConfirm.classList.add('error');                                             
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, повторите пароль."        
        return event.preventDefault();   
        } else{
            js_input_passwordConfirm.classList.remove('error');
    }
    if(js_input_captcha.value === ''){                
        js_input_captcha.classList.add('error');                                             
        label_error.removeAttribute("hidden");
        label_error.textContent = "Пожайлуста!, введите цифры с картинки."        
        return event.preventDefault();   
        } else{
            js_input_captcha.classList.remove('error');
    }
}

