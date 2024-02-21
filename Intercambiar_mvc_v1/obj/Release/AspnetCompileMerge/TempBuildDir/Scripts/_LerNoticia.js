var IdNoticia;

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function ValidaComentar() {
    var SValida = readCookie("IntercambiarCookieComentar");

    if (SValida != null && SValida == "Sim") {
        if (Verificar_Logado()) {
            setCookie("IntercambiarCookieComentar", null, -1);
            $("#Texto_Comentario_Caixa").css("display", "none");
            $("#Texto_Comentario_NaoLogado").css("display", "none");
            $("#Texto_Comentario_Texto").css("display", "block");
        }
        else
        {
            setCookie("IntercambiarCookieComentar", null, -1);
        }
    }
    else {
        setCookie("IntercambiarCookieComentar", null, -1);
    }

}

function Verificar_Logado() {
    var setting = false;
    var chaveCookie = "";

    if (chaveCookie == "") {
        $.ajax({
            url: '/Social/chaveCookie',
            type: 'post',
            async: false,
            cache: false,
            success: function (data) {
                chaveCookie = data.chave;
            }
        });
    }

    if (readCookie(chaveCookie) == null) {
        return false;
    }
    else {
        return true;
    }

}


$(document).ready(function () {

    var items = [];

    function CarregaIdNoticia() {
        IdNoticia = $(location).attr('href').substring(($(location).attr('href').search("/LerNoticia/"))).replace("/LerNoticia/", "").replace("#", "");

        for (var x = IdNoticia.length; x > 0 + 1; x--) {
            if (isNumeric(IdNoticia.substring(IdNoticia.length, x)) == false) {
                IdNoticia = IdNoticia.replace(IdNoticia.substring(IdNoticia.length, x), "");
            }

        }
    }

    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    function CarregarComentario() {
        var Log = { IdComentario: IdNoticia };
        $("#Texto_Comentario_Caixa_Lista").empty();
        $.ajax({
            url: '/Noticias/GetComentario',
            type: 'post',
            data: Log,
            async: false,
            cache: false,
            success: function (Comentarios) {
                // states is your JSON array
                var $select = $('#Texto_Comentario_Caixa_Lista');
                $.each(Comentarios, function (i, Comentario) {
                    items.push(Comentario);
                });
            }
        });
    }

    CarregaIdNoticia();

    ValidaComentar();

    CarregarComentario();
    
    $('#Texto_Comentario_Caixa_Lista').append.apply($('#Texto_Comentario_Caixa_Lista'), items);

    $(document).on("click", "#FaceInter", function () {
        FB.ui({
            method: 'feed',
            link: '@Request.Url.GetLeftPart(UriPartial.Path)',
            redirect_uri: 'http://www.intercambiar.com.br',
            caption: '@Html.DisplayFor(model => model.Titulo)',
            name: 'Intercambiar'
        }, function (response) { });
    });

    $("#Texto_Comentario_NaoLogado_Login_FaceLoginTxt").click(function () {
        setCookie("IntercambiarCookieComentar", "Sim", 1);
        $("#FaceLoginTxt").trigger("click");
    });

    $("#Texto_Comentario_NaoLogado_Login_FaceLogin").click(function () {
        setCookie("IntercambiarCookieComentar", "Sim", 1);
        $("#LoginClick").trigger("click");
    });

    $("#Texto_Comentario_NaoLogado_Login_Cadastrar").click(function () {
        setCookie("IntercambiarCookieComentar", "Sim", 1);
        $.magnificPopup.open({
            items: {
                src: '#testeCadastrarIntercambiar'
            },
            mainClass: 'mfp-zoom-out',
            type: 'inline'
        });
    });

    $("#Texto_Comentario_Caixa_comentar").click(function () {
        if (Verificar_Logado())
        {
            $("#Texto_Comentario_Caixa").css("display", "none");
            $("#Texto_Comentario_Texto").css("display", "block");
            $("#Comentario_Inputtext").val('');
        }
        else
        {
            if ($(window).width() >= 750) {
                $("#Texto_Comentario_Caixa").css("display", "none");
                $("#Texto_Comentario_NaoLogado").css("display", "block");
            }
            else {
                setCookie("IntercambiarCookieComentar", "Sim", 1);
                $("#LoginClick").trigger("click");
            }
        }
        
    });

    $("#Texto_Comentario_Caixa_Voltar").click(function () {
        $("#Texto_Comentario_Caixa").css("display", "block");
        $("#Texto_Comentario_NaoLogado").css("display", "none");
        $("#Texto_Comentario_Texto").css("display", "none");
    });

    $("#Texto_Comentario_Texto_Voltar").click(function () {
        $("#Texto_Comentario_Caixa").css("display", "block");
        $("#Texto_Comentario_NaoLogado").css("display", "none");
        $("#Texto_Comentario_Texto").css("display", "none");
    });

    $("#Comentario_Enviar").click(function () {

        var TxArray = $('#Comentario_Inputtext').val().split("\n");
        var Tx = "";

        for (i = 0; i < TxArray.length; i++) {
            Tx = Tx + "(*0*)" + TxArray[i] + "(*1*)";
        }

        $('#Comentario_Inputtext').val(Tx);

        var Log = { IdComentario: IdNoticia, Texto: Tx};

        var items = [];
        $("#Comentario_Inputtext").empty();
        $.ajax({
            url: '/Noticias/Comentar',
            type: 'post',
            data: Log,
            async: false,
            cache: false,
            success: function (data) {
                if (data.result == "sim")
                {
                    CarregaIdNoticia();
                    $("#Texto_Comentario_Texto_Voltar").trigger("click");
                    var Log = { IdComentario: IdNoticia };
                    $("#Texto_Comentario_Caixa_Lista").empty();
                    $.ajax({
                        url: '/Noticias/GetComentario',
                        type: 'post',
                        data: Log,
                        async: false,
                        cache: false,
                        success: function (Comentarios) {
                            // states is your JSON array
                            var $select = $('#Texto_Comentario_Caixa_Lista');
                            $.each(Comentarios, function (i, Comentario) {
                                items.push(Comentario);
                            });

                            $('#Texto_Comentario_Caixa_Lista').append.apply($('#Texto_Comentario_Caixa_Lista'), items);
                        }
                    });
                }
            }
        });
    });

});

!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');
