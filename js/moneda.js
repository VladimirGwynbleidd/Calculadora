
    function formatCurrency(num) {
        num = num.toString().replace(/\$|\,/g, '');
        if (isNaN(num)) num = "0";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        cents = num % 100;
        num = Math.floor(num / 100).toString();
        if (cents < 10) cents = "0" + cents;
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
        return (((sign) ? '' : '-') + '$' + num + '.' + cents);
    }



        $(function () {
            // jQuery formatCurrency plugin: http://plugins.jquery.com/project/formatCurrency

            // Format while typing & warn on decimals entered, 2 decimal places
            $('#txtSalarioBaseCotizacion').blur(function () {
                $('#mensaje-txtSalarioBaseCotizacion').html(null);
                $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: 2 });
            })
			.keyup(function (e) {
			    var e = window.event || e;
			    var keyUnicode = e.charCode || e.keyCode;
			    if (e !== undefined) {
			        switch (keyUnicode) {
			            case 16: break; // Shift
			            case 17: break; // Ctrl
			            case 18: break; // Alt
			            case 27: this.value = ''; break; // Esc: clear entry
			            case 35: break; // End
			            case 36: break; // Home
			            case 37: break; // cursor left
			            case 38: break; // cursor up
			            case 39: break; // cursor right
			            case 40: break; // cursor down
			            case 78: break; // N (Opera 9.63+ maps the "." from the number key section to the "N" key too!) (See: http://unixpapa.com/js/key.html search for ". Del")
			            case 110: break; // . number block (Opera 9.63+ maps the "." from the number block to the "N" key (78) !!!)
			            case 190: break; // .
			            default: $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: -1, eventOnDecimalsEntered: true });
			        }
			    }
			})
			.bind('decimalsEntered', function (e, cents) {
			    if (String(cents).length > 2) {
			        var errorMsg = 'Sólo se permite 2 decimales (0.' + cents + ')';
			        $('#mensaje-txtSalarioBaseCotizacion').html(errorMsg);
			    }
			});




            // Format while typing & warn on decimals entered, 2 decimal places
            $('#txtSaldoActualAfore').blur(function () {
                $('#mensaje-txtSaldoActualAfore').html(null);
                $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: 2 });
            })
			.keyup(function (e) {
			    var e = window.event || e;
			    var keyUnicode = e.charCode || e.keyCode;
			    if (e !== undefined) {
			        switch (keyUnicode) {
			            case 16: break; // Shift
			            case 17: break; // Ctrl
			            case 18: break; // Alt
			            case 27: this.value = ''; break; // Esc: clear entry
			            case 35: break; // End
			            case 36: break; // Home
			            case 37: break; // cursor left
			            case 38: break; // cursor up
			            case 39: break; // cursor right
			            case 40: break; // cursor down
			            case 78: break; // N (Opera 9.63+ maps the "." from the number key section to the "N" key too!) (See: http://unixpapa.com/js/key.html search for ". Del")
			            case 110: break; // . number block (Opera 9.63+ maps the "." from the number block to the "N" key (78) !!!)
			            case 190: break; // .
			            default: $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: -1, eventOnDecimalsEntered: true });
			        }
			    }
			})
			.bind('decimalsEntered', function (e, cents) {
			    if (String(cents).length > 2) {
			        var errorMsg = 'Sólo se permite 2 decimales (0.' + cents + ')';
			        $('#mensaje-txtSaldoActualAfore').html(errorMsg);
			    }
			});



            // Format while typing & warn on decimals entered, 2 decimal places
            $('#txtAportacion').blur(function () {
                $('#mensaje-txtAportacion').html(null);
                $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: 2 });
            })
			.keyup(function (e) {
			    var e = window.event || e;
			    var keyUnicode = e.charCode || e.keyCode;
			    if (e !== undefined) {
			        switch (keyUnicode) {
			            case 16: break; // Shift
			            case 17: break; // Ctrl
			            case 18: break; // Alt
			            case 27: this.value = ''; break; // Esc: clear entry
			            case 35: break; // End
			            case 36: break; // Home
			            case 37: break; // cursor left
			            case 38: break; // cursor up
			            case 39: break; // cursor right
			            case 40: break; // cursor down
			            case 78: break; // N (Opera 9.63+ maps the "." from the number key section to the "N" key too!) (See: http://unixpapa.com/js/key.html search for ". Del")
			            case 110: break; // . number block (Opera 9.63+ maps the "." from the number block to the "N" key (78) !!!)
			            case 190: break; // .
			            default: $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: -1, eventOnDecimalsEntered: true });
			        }
			    }
			})
			.bind('decimalsEntered', function (e, cents) {
			    if (String(cents).length > 2) {
			        var errorMsg = 'Sólo se permite 2 decimales (0.' + cents + ')';
			        $('#mensaje-txtAportacion').html(errorMsg);
			    }
			});



            // Format while typing & warn on decimals entered, 2 decimal places
            $('#txtSaldoAFORE').blur(function () {
                $('#mensaje-txtSaldoAFORE').html(null);
                $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: 2 });
            })
			.keyup(function (e) {
			    var e = window.event || e;
			    var keyUnicode = e.charCode || e.keyCode;
			    if (e !== undefined) {
			        switch (keyUnicode) {
			            case 16: break; // Shift
			            case 17: break; // Ctrl
			            case 18: break; // Alt
			            case 27: this.value = ''; break; // Esc: clear entry
			            case 35: break; // End
			            case 36: break; // Home
			            case 37: break; // cursor left
			            case 38: break; // cursor up
			            case 39: break; // cursor right
			            case 40: break; // cursor down
			            case 78: break; // N (Opera 9.63+ maps the "." from the number key section to the "N" key too!) (See: http://unixpapa.com/js/key.html search for ". Del")
			            case 110: break; // . number block (Opera 9.63+ maps the "." from the number block to the "N" key (78) !!!)
			            case 190: break; // .
			            default: $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: -1, eventOnDecimalsEntered: true });
			        }
			    }
			})
			.bind('decimalsEntered', function (e, cents) {
			    if (String(cents).length > 2) {
			        var errorMsg = 'Sólo se permite 2 decimales (0.' + cents + ')';
			        $('#mensaje-txtSaldoAFORE').html(errorMsg);
			    }
			});

        });










        

