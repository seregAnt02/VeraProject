

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

export async function ValidateAddSave(event, model){    

        const forma_add_update = document.getElementById('forma_add_udate_id');                        
       let label_error = document.getElementsByClassName('label-error-show')[0];            

        //forma_add_update.datetimebloodpressure.value = new Date();

        //console.log(forma_add_update.datetimebloodpressure.value);

                if(forma_add_update.sys.value === ''){                
                    forma_add_update.sys.classList.add('error');                                 
            
                    //label_error.setAttribute('display','block');
                    label_error.removeAttribute("hidden");
                    label_error.textContent = "Пожайлуста!, введите значение sys."        
                    return event.preventDefault();   
                } else{
                    forma_add_update.sys.classList.remove('error');
                }

                if(forma_add_update.dia.value === ''){                
                    forma_add_update.dia.classList.add('error');                                 
            
                    //label_error.setAttribute('display','block');
                    label_error.removeAttribute("hidden");
                    label_error.textContent = "Пожайлуста!, введите значение dia."        
                    return event.preventDefault();   
                } else{
                    forma_add_update.dia.classList.remove('error');
                }

                if(forma_add_update.pulse.value === ''){                
                    forma_add_update.pulse.classList.add('error');                                 
            
                    //label_error.setAttribute('display','block');
                    label_error.removeAttribute("hidden");
                    label_error.textContent = "Пожайлуста!, введите значение pulse."        
                    return event.preventDefault();   
                } else{
                    forma_add_update.pulse.classList.remove('error');
                }           
                
                if(!event.defaultPrevented){

                //     body.classList.add('body-color-load');                                                                       
                //     imgRadarDiv.style.marginTop = -(containerId.offsetHeight / 2) + 'px';            
                //     img_modal.hidden = false;                     
                const form = new FormData(model);
                //console.dir(result.children[1]);
                
                var hostname = $(window.location).attr('hostname');
                var url = hostname == "localhost" ? "/home/addsave" : "/vera/home/addsave";
                        //const url = "/home/addsave";                                                
                          let responsePost = await fetch( url, {
                                
                            method: 'POST',                        
                            body: form            
                        });
                        if (responsePost.ok === true){

                            console.log(responsePost.ok);
                            await responsePost.text();                      
                            // img_modal.hidden = true;
                            // body.classList.remove('body-color-load');
                            document.location = hostname == 'localhost' ? "/home/index" : "/vera/home/index";
                    }    
                                                    
                }

}