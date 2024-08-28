
const body = document.getElementsByTagName('body')[0];
const js_measuring = await import('../vue_js/js_add_measuring.js');
// console.log(js_measuring);

export let firstTable = new Vue({
    el: '#tableData',
    data: {
      rows: [],
      login: ''
    },
        methods:{
          eventUpdate(id, event, index){   

              IconEventClickUpdata(event)

              PutUpData(id, index);
          },
          eventDelete(id, event, index){

            IconEventClickDelete(event);

            DeleteData(id, index);
          },
          GetLogin(){
            
            const login_message  = GetLoginMessage();
            login_message.then(result => {
              
              this.login = result;              
            });                       

            return this.login;
          }

    }
  });    
  
// // Прослушиваем событие изменения размера страницы
// window.addEventListener("resize", function() {
//   // Здесь получаем размеры экрана (inner/outerWidth, inner/outerHeight)
//       console.log(window.outerWidth + "/" + window.outerHeight);
// }, false);

  //---------------//

  firstTable.GetLogin(); 

  //---------------//    

  async function GetLoginMessage(){
    
    var hostname = $(window.location).attr('hostname');
    var url = hostname == "localhost" ? "/api/pressure/login" : "/vera/api/pressure/login";

      //const url = "/api/pressure/login";

      let response =  await fetch( url, {
    
          method: 'GET',
          headers: {
              "Accept": "application/json",
              'Content-Type': 'application/json;charset=utf-8'            
          }

      });    

      if (response.ok === true) {            

        return await response.text();                  

      }

  }

  function IconEventClickUpdata(event){

    var hostname = $(window.location).attr('hostname');
    var url_put_dark = hostname == "localhost" ? "/images/png-icons-put-dark.png" : "/vera/images/png-icons-put-dark.png";
    var url_put = hostname == "localhost" ? "/images/png-icons-put.png" : "/vera/images/png-icons-put.png";

    event.target.setAttribute("src", url_put_dark);
        setTimeout(() => {

          event.target.setAttribute("src", url_put);            

      }, 75);
        
  }

  function IconEventClickDelete(event){

    var hostname = $(window.location).attr('hostname');
    var url_png_icons_delete_dark = hostname == "localhost" ? "/images/png-icons-delete-dark.png" : "/vera/images/png-icons-delete-dark.png";
      event.target.setAttribute("src", url_png_icons_delete_dark);
        setTimeout(() => {

          var url_png_icons_delete = hostname == "localhost" ? "/images/png-icons-delete.png" : "/vera/images/png-icons-delete.png";
          event.target.setAttribute("src", url_png_icons_delete);            

      }, 75);
      
  }

async function DeleteData(id, numberRow){

  let forma_addFormDelCancel = js_measuring.AddFormDelCancel();

  forma_addFormDelCancel.then(result => {

    if(body.clientWidth < 576) result.classList.add("size-mobil-del");
    else if(!result.hasAttribute("add-forms-del-ok")){

        result.classList.remove("add-forms-del-ok-clear");
        result.classList.add("add-forms-del-ok");                
      }

      result.children[0].children[0].textContent = result.children[0].children[0].textContent + "? № " + numberRow;
      
      //console.dir(result);

      const btn_form_add_cancel = result.children[0][0];

      btn_form_add_cancel.onclick = async function(){

        if(body.clientWidth < 576) result.classList.remove("size-mobil-del");
        else{
          result.classList.remove("add-forms-del-ok");
          result.classList.add("add-forms-del-ok-clear");
        }        
      }

      const btn_form_add_ok = result.children[0][1];

      btn_form_add_ok.onclick = async function(){

        var hostname = $(window.location).attr('hostname');
        var url_delete_data = hostname == "localhost" ? "/home/deletedata" : "/vera/home/deletedata";
        //const url = "/home/deletedata";                                

          let response = await fetch( url_delete_data, {
                
            method: 'POST',
            headers: {
                //'Content-Type': 'text/html;charset=utf-8'                
                //'Content-Type': 'application/json;charset=utf-8'
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: "id=" + id //JSON.stringify({id: argId})

        });

        if (response.ok === true) {

          const result = await response.text();                     
                    
          var url_location = hostname == "localhost" ? "/home/index" : "/vera/home/index";
          document.location.href = url_location;                      
        }   
        //console.dir(this);
      }
    //console.dir(result);
  });
          
             
}

  async function PutUpData(id, index){                      

    var hostname = $(window.location).attr('hostname');

      var url = hostname == "localhost" ? "/api/pressure/" + id : "/vera/api/pressure/" + id;

        //const url = "/api/pressure/" + id;                                

        let response = await fetch( url, {
              
          method: 'PUT',
          headers: {
              'Accept': 'application/json',
              'Content-Type':'application/json;charset=utf-8'              
              //"Content-Type": "application/x-www-form-urlencoded"
          }

      });

      //console.dir(id);

      if (response.ok === true) {

        const result = await response.json();  
        
        FormLoading(result, index);
      
      }      
      

  }
  
  //console.dir(firstTable.rows);

  async function FormLoading(modelBloodPressures, index){    

    let forma_loading = js_measuring.RowsAddUpdateTable();    


            forma_loading.then(result => {                           

              if(body.clientWidth < 576) result.classList.add("sizeFormMobilFormat");
              
              else if(!result.hasAttribute("forma-add-update")){

                result.classList.remove("forma-add-update-clear");                
                result.classList.remove("add-forms-del-ok-clear");
                result.classList.add("forma-add-update");                
            }            

            //console.dir(result);

             const btn_cancel = document.getElementById('btn_cancel')


            btn_cancel.onclick = async function(){
                                        
              if(body.clientWidth < 576) result.classList.remove("sizeFormMobilFormat");
              else{
                result.classList.remove("forma-add-update");
                result.classList.add("forma-add-update-clear");                       
              }                
            }            

            //console.dir(result);

            result.children[1].children[1].textContent = result.children[1].children[1].textContent + " " + index;
            
            for(let x = 0; x < result.children[1].length - 2; x++){

              let tagName = result.children[1][x].tagName;
              let id = result.children[1][x].id;
              let value = result.children[1][x].value;

              if(tagName == "INPUT"){

                if(id == "id") result.children[1][x].setAttribute('value', modelBloodPressures[0].id);
                if(id == "datetimebloodpressure") result.children[1][x].setAttribute('value', modelBloodPressures[0].datetimebloodpressure);
                if(id == "sys") result.children[1][x].setAttribute('value', modelBloodPressures[0].sys);
                if(id == "dia") result.children[1][x].setAttribute('value', modelBloodPressures[0].dia);             
                if(id == "pulse") result.children[1][x].setAttribute('value', modelBloodPressures[0].pulse);
                if(id == "comment") result.children[1][x].setAttribute('value', modelBloodPressures[0].comment);

              }              
            }            
            
            const btn_ok = document.getElementById('btn_ok');            
            btn_ok.onclick = async function(event){

              const forma_add_udate_id = document.getElementById("forma_add_udate_id");
      
              //console.log(forma_add_udate_id);
              //result.children[1].setAttribute('action', 'home/edit');              

              const form = new FormData(forma_add_udate_id);

              //console.dir(form);
              
                var hostname = $(window.location).attr('hostname');
                var url_edit = hostname == "localhost" ? "/home/edit" : "/vera/home/edit";
                //var url = hostname == "localhost" ? "/api/pressure/edit" : "/vera/api/pressure/edit";
                  
                  let responsePost = await fetch( url_edit, {
                        
                    method: 'POST',                        
                    body: form            
                });
                if (responsePost.ok === true){

                    //console.log(responsePost.ok);
                    await responsePost.text();                      
                    // img_modal.hidden = true;
                    // body.classList.remove('body-color-load');
                    document.location = hostname == 'localhost' ? "/home/index" : "/vera/home/index";
                    //var table_data = document.getElementById('tableData');
                    //console.log(table_data);
                }     
                
            }
      },      
        error => {alert(error);});        

  }  


  let x_scale_0 = [];
  let y_scale_0 = [];

  const chartObj  = new Chart( // инициализируем плагин
          document.querySelector('.chart'), // первым параметром передаем элемент canvas по селектору
          // вторым параметром передаем настройки в виде объекта
          { 
            type: 'line', // тип графика, в данном случае линейный
            data: { // общие данные графика в виде объекта
              labels: y_scale_0, // метки по оси X  'April', 'May', 'June', 'July', 'August'
              datasets: [ // набор данных, который будет отрисовываться в виде массива с объектами
                { 
                    label: 'Books read', // название для определенного графика в виде строки
                    data: x_scale_0, // данные в виде массива с числами, количество должно совпадать с количеством меток по оси X
                    borderColor: 'crimson', // назначаем цвет для линий в виде строки
                    borderWidth: 5, // назначаем ширину линий
                    cubicInterpolationMode: 'monotone' // добавили сглаживание углов
                }/*,
                // добавили еще один график с другими значениями и цветом
              {
                label: 'Books bought',
                data: [5, 2, 3, 1, 4],
                borderColor: 'teal',
                borderWidth: 5,
                backgroundColor: 'teal',
                cubicInterpolationMode: 'monotone'
              }*/
              ]
            },
            options: {} // дополнительные опции для графика в виде объекта, если не нужны - передаем пустой объект        
          }
        );


        var hostname = $(window.location).attr('hostname');

        var url = hostname == "localhost" ? "/api/pressure" : "/vera/api/pressure";

        let response = await fetch(url,{
              headers: {
                'Accept': 'application/json',
                'Content-Type':'application/json;charset=utf-8'              
                //"Content-Type": "application/x-www-form-urlencoded"
            }
        });

        let json_comit = await response.json(); // читаем ответ в формате JSON

        //console.log(json_comit);

        //ChartShow();

    export function ChartShow(){      

      x_scale_0.splice(0, x_scale_0.length);
      y_scale_0.splice(0, y_scale_0.length);

      for(let x = 0; x < json_comit.length; x++){    

        //таблица
        firstTable.rows.push(json_comit[x]);        
        //графика
        x_scale_0.push(json_comit[x].sys);            

        y_scale_0.push(new Intl.DateTimeFormat("ru", {dateStyle: "short", timeStyle: "short"}).format(new Date(json_comit[x].datetimebloodpressure)));        
                              
      }    
      chartObj.update();

    }

    export async function GetDate(){

      let date_start = document.getElementById('datetime_start');
      let date_end = document.getElementById('datetime_end');      
        
      var hostname = $(window.location).attr('hostname');
      var url = hostname == "localhost" ? "/api/pressure/" + date_start.value + "/" + date_end.value : "/vera/api/pressure/" + date_start.value + "/" + date_end.value;
      //const url = "/api/pressure/" + date_start.value + "/" + date_end.value;
  
      let mas = url.split("/");

      if(mas[3] != "" && mas[4] != ""){

              let response = await fetch( url, {
        
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json;charset=utf-8'
                    //'Content-Type': 'text/html;charset=utf-8'
                    //'Content-Type':'multipart/form-data'
                    //"Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
                }
        
            });

            if (response.ok === true) {

              const result = await response.json();
              
              firstTable.rows.splice(0, firstTable.rows.length);
      
              x_scale_0.splice(0, x_scale_0.length);
              y_scale_0.splice(0, y_scale_0.length);
      
              for(let x = 0; x < result.length; x++){
      
                //таблица
                firstTable.rows.push(result[x]);
                //графика
                x_scale_0.push(result[x].sys);    
                y_scale_0.push(new Intl.DateTimeFormat("ru", {dateStyle: "short", timeStyle: "short"}).format(new Date(result[x].datetimebloodpressure)));
      
             }
      
             chartObj.update();
                
            }
  
      }              
        
  }
    