
       function edad(fecha) {
           var miArray = new Array(6)
           miArray = fecha.split('/');
           var dia_nacim, mes_nacim, anio_nacim;
           dia_nacim = miArray[0];
           mes_nacim = miArray[1];
           anio_nacim = miArray[2];           
           var fecha_hoy = new Date();
           var ahora_anio = fecha_hoy.getYear();
           var ahora_mes = fecha_hoy.getMonth();
           var ahora_dia = fecha_hoy.getDate();

           var fecha_nacimiento = new Date(anio_nacim, mes_nacim, dia_nacim, 0, 0, 0, 0);

           var edad = (ahora_anio + 1900) - anio_nacim;
           if (ahora_mes < (mes_nacim - 1)) { edad--; }
           if (((mes_nacim - 1) == ahora_mes) && (ahora_dia < dia_nacim)) { edad--; }
           if (edad > 1900) { edad -= 1900; }

           if (fecha_nacimiento < fecha_hoy && edad >= 14 && edad <= 100) {
               document.getElementById('txtEdad').value = edad;
           }
           else {
               document.getElementById('txtEdad').value = "";
           }
           


           $(function () {

               var maxAniosCotizados = document.getElementById('txtEdad').value - 14;

               $("#slider-txtAniosCotizados").slider({

                   range: "min",

                   value: 0,

                   min: 0,

                   max: maxAniosCotizados,

                   step: 1,

                   slide: function (event, ui) {

                       $("#txtSemanasCotizadas").val(ui.value);
                       $("#HiddenField1").val(ui.value);
                   }

               });

               $("#txtSemanasCotizadas").val($("#slider-txtAniosCotizados").slider("value"));


           });


       }




