$(function () {
    /* $('#ttttt').click(function () {
         $('#ttttt').tooltip({
             position: {
                 my: "center bottom",
                 at: "center top-10",
                 collision: "none"
             },
             items: "#ttttt",
             content: "<div>" +
                         "<p class=text_gral_popup>" +
                             "<BR>La Pensión mensual estimada se calcula solo con fines ilustrativos " +
                             "y con el fin de que veas la importancia que tiene el ahorro y como " +
                             "éste puede traducirse en una pensión." +
                         "</p>" +
                      "</div>"
         });
     });*/

    $(document).ready(function () {
        $('#tooltip').tooltipster({
            content: $(
                        "<p class=text_gral_popup>" +
                            "Es aquella que el Estado asegura, considerando las semanas cotizadas, edad de retiro y salario promedio que tengas al momento de pensionarte, de acuerdo a lo establecido en el artículo 170 de la Ley del Seguro Social." +
                        "</p>"),
            trigger: 'click',            
        });
    });

    /*$("#tooltip").tooltip({
        position: {
            my: "center bottom",
            at: "center top-10",
            collision: "none"
        },
        items: "#tooltip",
        content: "<div class=tooltipInfo>" +
                    "<p class=text_gral_popup>" +
                        "<BR>La Pensión mensual estimada se calcula solo con fines ilustrativos " +
                        "y con el fin de que veas la importancia que tiene el ahorro y como " +
                        "éste puede traducirse en una pensión." +
                    "</p>" +
                 "</div>"
    }).mouseleave(function () {
        if ($('#tooltip').is(':focus')) {
            ui.tooltip.preventDefault();
            $('#tooltip').tooltip('open');
        }
    }).focusout(function () {
        $('#tooltip').tooltip('close');
    });*/
    $('#tooltip').css('cursor', 'pointer');
});

