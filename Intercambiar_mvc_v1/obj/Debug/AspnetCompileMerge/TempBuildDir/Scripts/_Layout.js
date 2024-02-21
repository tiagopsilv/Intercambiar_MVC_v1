var Logado = 0;
var chaveCookie = "";
var tab_cad_2 = false;
var tab_cad_3 = false;
var tab_cad_4 = false;
var Cad_1_Proximo_count = 0;
var Cad_2_Proximo_count = 0;
var Cad_3_Proximo_count = 0;

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

function ValidaFaceLoginTxt ()
{
    var url = readCookie("IntercambiarCookieFaceLoginTxt");

    if (url != null)
    {
        setCookie("IntercambiarCookieFaceLoginTxt", null, -1);
        window.location.href = url;
    }
    else
    {
        setCookie("IntercambiarCookieFaceLoginTxt", null, -1);
    }

}

function ValidaLogin() {

    if ($("#inputEmail").val() == "") {
        $("#inputEmail").css({ "border-color": "#F00", "padding": "2px" });
        return false;
    }
    else {
        $("#inputEmail").css({ "border-color": "#A9A9A9", "padding": "0px" });
        if ($("#inputPassword").val() == "") {
            $("#inputPassword").css({ "border-color": "#F00", "padding": "2px" });
            return false;
        }
        else
        {
            $("#inputPassword").css({ "border-color": "#A9A9A9", "padding": "0px" });
            return true;
        }
    }

}

function LogarWeb(setting) {
    if (setting == true) {
        $("#Login").hide();
        $("#Perfil_Foto").css("display", "block");
        $("#PerfilNomeUsuario").css("display", "block");
        $("#PerfilAcoes").css("display", "block");
    }
    else {
        $("#Login").show();
        $("#Perfil_Foto").css("display", "none");
        $("#PerfilNomeUsuario").css("display", "none");
        $("#PerfilAcoes").css("display", "none");
    }
}

function LogarMobile(setting) {
    $("#Login").hide();
    $("#Perfil_Foto").css("display", "none");
    $("#PerfilNomeUsuario").css("display", "none");
    $("#PerfilAcoes").css("display", "none");


    $("#CelularUsuario").css("display", "block");

    if (setting != true) {
        $("#Login_Resp").css("display", "block");
        $("#CelularUsuario").css("display", "none");
        $("#Login_Resp").attr("src", "/Imagens/Login.png");
    }
    else
    {
        $("#Login_Resp").css("display", "none");
        $("#CelularUsuario").css("display", "block");
    }
}

//Funções Cadastro

function CarregarEstado() {
    $("#Cad_Terceiro_Estado").empty();
    $("#Cad_Terceiro_Estado").append("<option value='0'>--Selecione o Estado--</option>")
    $.ajax({
        url: '/Social/GetEstados',
        type: 'post',
        async: false,
        cache: false,
        success: function (states) {
            // states is your JSON array
            var $select = $('#Cad_Terceiro_Estado');
            $.each(states, function (i, state) {
                $('<option>', {
                    value: state.ID
                }).html(state.nome).appendTo($select);
            });
        }
    });

}

function CarregarCidade(idEstado) {
    $("#Cad_Terceiro_Cidade").empty();
    $("#Cad_Terceiro_Cidade").append("<option value='0'>--Selecione a Cidade--</option>")
    var idEs = { IdEstado: idEstado };
    $.ajax({
        url: '/Social/GetCidades',
        type: 'post',
        data: idEs,
        async: false,
        cache: false,
        success: function (states) {
            // states is your JSON array
            var $select = $('#Cad_Terceiro_Cidade');
            $.each(states, function (i, state) {
                $('<option>', {
                    value: state.id
                }).html(state.nome).appendTo($select);
            });
        }
    });

}

function f_Cad_Email() {
    var email = $("#cad_Email").val();
    if (email != "") {
        var filtro = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
        if (filtro.test(email)) {

            var Endereco = { _Email: $('#cad_Email').val() };
            $.ajax({
                url: '/Social/VerificarEmail',
                type: 'post',
                data: Endereco,
                async: false,
                cache: false,
                success: function (data) {
                    if (data.existe == true) {
                        $("#cad_Email_ok").css("display", "none");
                        $("#cad_Email_erro").css("display", "block");
                        $("#cad_Email_erro").html("E-mail já cadastrado!");
                        tab_cad_2 = false;
                        return false;
                    }
                    else {
                        $("#cad_Email_ok").css("display", "block");
                        $("#cad_Email_erro").css("display", "none");
                        return true;
                    }
                }
            });

        } else {
            $("#cad_Email_ok").css("display", "none");
            $("#cad_Email_erro").css("display", "block");
            $("#cad_Email_erro").html("Endereço de email invalido!");
            tab_cad_2 = false;
            return false;
        }
    } else {
        $("#cad_Email_ok").css("display", "none");
        $("#cad_Email_erro").css("display", "block");
        $("#cad_Email_erro").html("Email é obrigatorio!");
        tab_cad_2 = false;
        return false;
    }
}

function f_cad_Primeiro_Nome() {
    var txt = $("#cad_Primeiro_Nome").val();
    if (txt != "") {
        $("#cad_Primeiro_Nome_ok").css("display", "block");
        $("#cad_Primeiro_Nome_erro").css("display", "none");
        return true;
    } else {
        $("#cad_Primeiro_Nome_ok").css("display", "none");
        $("#cad_Primeiro_Nome_erro").css("display", "block");
        $("#cad_Primeiro_Nome_erro").html("Preenchimento é obrigatorio!");
        tab_cad_2 = false;
        return false;
    }
}

function f_cad_Sobrenome() {
    var txt = $("#cad_Sobrenome").val();
    if (txt != "") {
        $("#cad_Sobrenome_ok").css("display", "block");
        $("#cad_Sobrenome_erro").css("display", "none");
        return true;
    } else {
        $("#cad_Sobrenome_ok").css("display", "none");
        $("#cad_Sobrenome_erro").css("display", "block");
        $("#cad_Sobrenome_erro").html("Preenchimento é obrigatorio!");
        tab_cad_2 = false;
        return false;
    }
}

function f_Cad_Segundo_Celular() {
    var txt = $("#Cad_Segundo_Celular").val();

    if (txt != "") {

        txt = txt.replace("(", "");
        txt = txt.replace(")", "");
        txt = txt.replace("-", "");
        txt = txt.replace(" ", "").trim();

        if (txt.length < 10) {
            $("#Cad_Segundo_Celular_ok").css("display", "none");
            $("#Cad_Segundo_Celular_erro").css("display", "block");
            $("#Cad_Segundo_Celular_erro").html("Valor invalido!");
            if (Cad_2_Proximo_count >= 1) {
                $("#Cad_Segundo_Celular_erro").css("font-weight", "bold");
                if (Cad_2_Proximo_count >= 2) {
                    $("#Cad_Segundo_Celular_erro").css("text-decoration", "underline");
                }
            }
            tab_cad_2 = false;
            return false;
        }
        else {
            $("#Cad_Segundo_Celular_ok").css("display", "block");
            $("#Cad_Segundo_Celular_erro").css("display", "none");
            return true;
        }
    } else {
        $("#Cad_Segundo_Celular_ok").css("display", "block");
        $("#Cad_Segundo_Celular_erro").css("display", "none");
        return true;
    }
}

function f_Cad_Segundo_DtNasc() {
var txt = $("#Cad_Segundo_DtNasc").val();

    if (txt != "") {
        if (verificaData(txt)) {
            $("#Cad_Segundo_DtNasc_ok").css("display", "block");
            $("#Cad_Segundo_DtNasc_erro").css("display", "none");
            return true;
        }
        else
        {
            $("#Cad_Segundo_DtNasc_ok").css("display", "none");
            $("#Cad_Segundo_DtNasc_erro").css("display", "block");
            $("#Cad_Segundo_DtNasc_erro").html("Valor invalido!");
            tab_cad_2 = false;
            return false;
        }
    } else {
        $("#Cad_Segundo_DtNasc_ok").css("display", "none");
        $("#Cad_Segundo_DtNasc_erro").css("display", "block");
        $("#Cad_Segundo_DtNasc_erro").html("Preenchimento é obrigatorio!");
        tab_cad_2 = false;
        return false;
    }
}

function f_Cad_Segundo_Fone() {

    var txt = $("#Cad_Segundo_Fone").val();

    txt = txt.replace("(", "");
    txt = txt.replace(")", "");
    txt = txt.replace("-", "");
    txt = txt.replace(" ", "").trim();

    if (txt != "") {
        if (txt == '0000000000') {
            $("#Cad_Segundo_Fone_ok").css("display", "none");
            $("#Cad_Segundo_Fone_erro").css("display", "block");
            $("#Cad_Segundo_Fone_erro").html("Valor invalido!");
            if (Cad_2_Proximo_count >= 1) {
                $("#Cad_Segundo_Fone_erro").css("font-weight", "bold");
                if (Cad_2_Proximo_count >= 2) {
                    $("#Cad_Segundo_Fone_erro").css("text-decoration", "underline");
                }
            }
            tab_cad_2 = false;
            return false;
        } else if (txt == '00000000000') {
            $("#Cad_Segundo_Fone_ok").css("display", "none");
            $("#Cad_Segundo_Fone_erro").css("display", "block");
            $("#Cad_Segundo_Fone_erro").html("Valor invalido!");
            tab_cad_2 = false;
            if (Cad_2_Proximo_count >= 1) {
                $("#Cad_Segundo_Fone_erro").css("font-weight", "bold");
                if (Cad_2_Proximo_count >= 2) {
                    $("#Cad_Segundo_Fone_erro").css("text-decoration", "underline");
                }
            }
            return false;
        } else if (txt.length < 10) {
            $("#Cad_Segundo_Fone_ok").css("display", "none");
            $("#Cad_Segundo_Fone_erro").css("display", "block");
            $("#Cad_Segundo_Fone_erro").html("Valor invalido!");
            if (Cad_2_Proximo_count >= 1) {
                $("#Cad_Segundo_Fone_erro").css("font-weight", "bold");
                if (Cad_2_Proximo_count >= 2) {
                    $("#Cad_Segundo_Fone_erro").css("text-decoration", "underline");
                }
            }
            tab_cad_2 = false;
            return false;
        }
        else {
            $("#Cad_Segundo_Fone_ok").css("display", "block");
            $("#Cad_Segundo_Fone_erro").css("display", "none");
            return true;
        }
    } else {
        $("#Cad_Segundo_Fone_ok").css("display", "none");
        $("#Cad_Segundo_Fone_erro").css("display", "block");
        $("#Cad_Segundo_Fone_erro").html("Preenchimento é obrigatorio!");
        if (Cad_2_Proximo_count >= 1) {
            $("#Cad_Segundo_Fone_erro").css("font-weight", "bold");
            if (Cad_2_Proximo_count >= 2) {
                $("#Cad_Segundo_Fone_erro").css("text-decoration", "underline");
            }
        }
        tab_cad_2 = false;
        return false;
    }
}

function f_Cad_Terceiro_Cep() {
    var txt = $("#Cad_Terceiro_Cep").val();
    if (txt != "") {
        if (txt.length < 9) {
            $("#Cad_Terceiro_Cep_ok").css("display", "none");
            $("#Cad_Terceiro_Cep_erro").css("display", "block");
            $("#Cad_Terceiro_Cep_erro").html("Valor invalido!");
            tab_cad_3 = false;
            return false;
        }
        else {
            $("#Cad_Terceiro_Cep_ok").css("display", "block");
            $("#Cad_Terceiro_Cep_erro").css("display", "none");
        }
    }
    else {
        $("#Cad_Terceiro_Cep_ok").css("display", "none");
        $("#Cad_Terceiro_Cep_erro").css("display", "block");
        $("#Cad_Terceiro_Cep_erro").html("Preenchimento é obrigatorio!");
        tab_cad_3 = false;
        return false;
    }
}

function f_Cad_Terceiro_Endereco() {
    var txt = $("#Cad_Terceiro_Endereco").val();
    if (txt != "") {
        $("#Cad_Terceiro_Endereco_ok").css("display", "block");
        $("#Cad_Terceiro_Endereco_erro").css("display", "none");
    } else {
        $("#Cad_Terceiro_Endereco_ok").css("display", "none");
        $("#Cad_Terceiro_Endereco_erro").css("display", "block");
        $("#Cad_Terceiro_Endereco_erro").html("Preenchimento é obrigatorio!");
        tab_cad_3 = false;
        return false;
    }
}

function f_Cad_Terceiro_Estado() {
    var txt = $("#Cad_Terceiro_Estado").val();
    if (txt != 0) {
        $("#Cad_Terceiro_Estado_ok").css("display", "block");
        $("#Cad_Terceiro_Estado_erro").css("display", "none");
    } else {
        $("#Cad_Terceiro_Estado_ok").css("display", "none");
        $("#Cad_Terceiro_Estado_erro").css("display", "block");
        $("#Cad_Terceiro_Estado_erro").html("Preenchimento é obrigatorio!");
        tab_cad_3 = false;
        return false;
    }
}

function f_Cad_Terceiro_Cidade() {
    var txt = $("#Cad_Terceiro_Cidade").val();
    if (txt != "0") {
        $("#Cad_Terceiro_Cidade_ok").css("display", "block");
        $("#Cad_Terceiro_Cidade_erro").css("display", "none");
    } else {
        $("#Cad_Terceiro_Cidade_ok").css("display", "none");
        $("#Cad_Terceiro_Cidade_erro").css("display", "block");
        $("#Cad_Terceiro_Cidade_erro").html("Preenchimento é obrigatorio!");
        tab_cad_3 = false;
        return false;
    }
}

function f_Cad_Quarto_Senha() {
    var txt = $("#Cad_Quarto_Senha").val();
    if (txt != "") {
        if (txt.length < 4) {
            $("#Cad_Quarto_Senha_ok").css("display", "none");
            $("#Cad_Quarto_Senha_erro").css("display", "block");
            $("#Cad_Quarto_Senha_erro").html("No mínimo 4 caracteres!");
            tab_cad_3 = false;
            return false;
        }
        else {
            $("#Cad_Quarto_Senha_ok").css("display", "block");
            $("#Cad_Quarto_Senha_erro").css("display", "none");
        }
    } else {
        $("#Cad_Quarto_Senha_ok").css("display", "none");
        $("#Cad_Quarto_Senha_erro").css("display", "block");
        $("#Cad_Quarto_Senha_erro").html("Preenchimento é obrigatorio!");
        tab_cad_4 = false;
        return false;
    }
}

function f_Cad_Quarto_Confirmar() {

    var txt = $("#Cad_Quarto_Confirmar").val();
    if (txt != "") {
        if (txt != $("#Cad_Quarto_Senha").val()) {
            $("#Cad_Quarto_Confirmar_ok").css("display", "none");
            $("#Cad_Quarto_Confirmar_erro").css("display", "block");
            $("#Cad_Quarto_Confirmar_erro").html("Senha incorreta!");
            tab_cad_4 = false;
            return false;
        }
        else {
            $("#Cad_Quarto_Confirmar_ok").css("display", "block");
            $("#Cad_Quarto_Confirmar_erro").css("display", "none");
        }
    } else {
        $("#Cad_Quarto_Confirmar_ok").css("display", "none");
        $("#Cad_Quarto_Confirmar_erro").css("display", "block");
        $("#Cad_Quarto_Confirmar_erro").html("Preenchimento é obrigatorio!");
        tab_cad_4 = false;
        return false;
    }

}

function Logar() {
    var setting = false;

    $("#Login").hide();
    $("#Perfil_Foto").css("display", "none");
    $("#PerfilNomeUsuario").css("display", "none");
    $("#PerfilAcoes").css("display", "none");
    if (chaveCookie == "") {
        $.ajax({
            url: '/Social/chaveCookie',
            type: 'post',
            async: false,
            cache: false,
            success: function (data) {
                chaveCookie = data.chave;
                $("#PerfilNomeUsuario").html(data.resultadoNome);
                $("#PerfilNomeUsuario_Celular").html(data.resultadoNome);
                $("#Perfil_Foto").attr("src", data.resultadoFoto);
                $("#CelularUsuario").attr("src", data.resultadoFoto);
                $("#Perfil_Foto_Celular").attr("src", data.resultadoFoto);
            }
        });
    }

    if (readCookie(chaveCookie) == null) {
        Logado = 0;
        setting = false;
    }
    else {
        Logado = 1;
        setting = true;
    }

    if ($(window).width() >= 750) {
        $("#Top_busca").show();
        LogarWeb(setting);
    }
    else {
        LogarMobile(setting);
        $("#Top_busca").hide();
    }

}

function verificaData(Data) {
    Data = Data.substring(0, 10);
    var dma = -1;
    var data = Array(3);
    var ch = Data.charAt(0);
    for (i = 0; i < Data.length && ((ch >= '0' && ch <= '9') || (ch == '/' && i != 0)) ;) {
        data[++dma] = '';
        if (ch != '/' && i != 0)
            return false;
        if (i != 0) ch = Data.charAt(++i); if (ch == '0') ch = Data.charAt(++i); while (ch >= '0' && ch <= '9')
        {
            data[dma] += ch; ch = Data.charAt(++i);
        }
    }
    if (ch != '') return false;
    if (data[0] == '' || isNaN(data[0]) || parseInt(data[0]) < 1)
        return false;
    if (data[1] == '' || isNaN(data[1]) || parseInt(data[1]) < 1 || parseInt(data[1]) > 12)
        return false;
    if (data[2] == '' || isNaN(data[2]) || ((parseInt(data[2]) < 0 || parseInt(data[2]) > 99) && (parseInt(data[2]) < 1900 || parseInt(data[2]) > 9999)))
        return false;
    if (data[2] < 50)
        data[2] = parseInt(data[2]) + 2000;
    else if (data[2] < 100)
        data[2] = parseInt(data[2]) + 1900;
    switch (parseInt(data[1])) {
        case 2: { if (((parseInt(data[2]) % 4 != 0 || (parseInt(data[2]) % 100 == 0 && parseInt(data[2]) % 400 != 0)) && parseInt(data[0]) > 28) || parseInt(data[0]) > 29) return false; break; }
        case 4: case 6: case 9: case 11: { if (parseInt(data[0]) > 30) return false; break; }
        default: { if (parseInt(data[0]) > 31) return false; }
    }
    return true;
}

function ValidaCad_Segundo_Fone(txt) {
    txt = txt.replace("(", "");
    txt = txt.replace(")", "");
    txt = txt.replace("-", "");
    txt = txt.replace(" ", "").trim();

    if (txt != "") {
        if (txt == '0000000000') {
            $("#Cad_Segundo_Fone_ok").css("display", "none");
            $("#Cad_Segundo_Fone_erro").css("display", "block");
            $("#Cad_Segundo_Fone_erro").html("Valor invalido!");
            if (Cad_2_Proximo_count >= 1) {
                $("#Cad_Segundo_Fone_erro").css("font-weight", "bold");
                if (Cad_2_Proximo_count >= 2) {
                    $("#Cad_Segundo_Fone_erro").css("text-decoration", "underline");
                }
            }
            tab_cad_2 = false;
            return false;
        } else if (txt == '00000000000') {
            $("#Cad_Segundo_Fone_ok").css("display", "none");
            $("#Cad_Segundo_Fone_erro").css("display", "block");
            $("#Cad_Segundo_Fone_erro").html("Valor invalido!");
            if (Cad_2_Proximo_count >= 1) {
                $("#Cad_Segundo_Fone_erro").css("font-weight", "bold");
                if (Cad_2_Proximo_count >= 2) {
                    $("#Cad_Segundo_Fone_erro").css("text-decoration", "underline");
                }
            }
            tab_cad_2 = false;
            return false;
        } else if (txt.length < 10) {
            $("#Cad_Segundo_Fone_ok").css("display", "none");
            $("#Cad_Segundo_Fone_erro").css("display", "block");
            $("#Cad_Segundo_Fone_erro").html("Valor invalido!");
            if (Cad_2_Proximo_count >= 1) {
                $("#Cad_Segundo_Fone_erro").css("font-weight", "bold");
                if (Cad_2_Proximo_count >= 2) {
                    $("#Cad_Segundo_Fone_erro").css("text-decoration", "underline");
                }
            }
            tab_cad_2 = false;
            return false;
        }
        else {
            $("#Cad_Segundo_Fone_ok").css("display", "block");
            $("#Cad_Segundo_Fone_erro").css("display", "none");
        }
    } else {
        $("#Cad_Segundo_Fone_ok").css("display", "none");
        $("#Cad_Segundo_Fone_erro").css("display", "block");
        $("#Cad_Segundo_Fone_erro").html("Preenchimento é obrigatorio!");
        tab_cad_2 = false;
        return false;
    }
}
$(window).resize(function () {
    Logar();
});

$(document).ready(function () {

    Logado = 0;

    ValidaFaceLoginTxt();

    $("#Login").hide()
    Logar();


    $("CadastrarIntercambiarLogin").magnificPopup({
        items: {
            src: "#testeCadastrarIntercambiar",
            type: 'inline'
        }
    });

    //$("#CadastrarIntercambiarLogin").click(function () {



    //    //$.magnificPopup.close();
    //    $("#testeCadastrarIntercambiar").css("display", "block");
    //});

    $("#Login_Resp").click(function () {
        $("#LoginClick").trigger("click");
    });

    $("#FaceLoginTxt").click(function () {

        var Dis = { Dis: "1", url: window.location.href };

        if ($(window).width() < 750) {
            Dis = { Dis: "2", url: window.location.href };
        }

        setCookie("IntercambiarCookieFaceLoginTxt", $(location).attr('href'), 1);

        $.ajax({
            url: '/Social/GetFacebookLoginUrl',
            type: 'post',
            data: Dis,
            async: false,
            cache: false,
            success: function (data) {
                window.location.href = data.url;
            }
        });
    });

    $("#FaceLoginImg").click(function () {
        $("#FaceLoginTxt").trigger("click");
    });

    $("#LoginFaceMobile").click(function () {
        $("#FaceLoginTxt").trigger("click");
    });

    $("#BtnLogar").click(function () {

        if (ValidaLogin() == true) {
            var Log = { Id: 1, email: $('#inputEmail').val(), Senha: $('#inputPassword').val(), Recuperar: false };
            $.ajax({
                url: '/Social/Logar',
                type: 'post',
                data: Log,
                async: false,
                cache: false,
                success: function (data) {
                    if (data.resultado == true) {
                        Logar();
                        $("#PerfilNomeUsuario").html(data.resultadoNome);
                        $("#Perfil_Foto").attr("src", data.resultadoFoto);
                        $("#Login_Resp").css("display", "none");
                        $("#CelularUsuario").css("display", "block");
                        $("#CelularUsuario").attr("src", data.resultadoFoto);
                        $("#PerfilNomeUsuario_Celular").html(data.resultadoNome);
                        $("#Perfil_Foto_Celular").attr("src", data.resultadoFoto);
                        $.magnificPopup.close();
                        if(readCookie("IntercambiarCookieComentar") == "Sim")
                        {
                            $("#Texto_Comentario_Caixa").css("display", "none");
                            $("#Texto_Comentario_NaoLogado").css("display", "none");
                            $("#Texto_Comentario_Texto").css("display", "block");
                            setCookie("IntercambiarCookieComentar", null, -1);
                        }
                        if(data.resultadoAutor == true)
                        {
                            location.reload();
                        }
                    }
                    else {
                        $("#MensagemLogin").html(data.resultadoMensagem);
                        // $("#MensagemLogin").html(data.resultadoMensagem) 
                    }
                }
            });

        }
        else
        {
            $("#MensagemLogin").html("<span>Preenchimento dos campos é obrigatorio</span>");
        }

    });

    $("#inputEmail").keyup(function (e) {
        if (e.keyCode == 13) {
            $("#inputPassword").focus();
        }
    });

    $("#inputPassword").keyup(function (e) {
        if (e.keyCode == 13) {
            $("#BtnLogar").trigger("click");
        }
    });

    //Cadastro

    $('ul.tabs li').click(function () {
        var tab_id = $(this).attr('data-tab');

        if (tab_id != "tab-2") {
            if (tab_id != "tab-3") {
                if (tab_id != "tab-4") {
                    $('ul.tabs li').removeClass('current');
                    $('.tab-content').removeClass('current');

                    $(this).addClass('current');
                    $("#" + tab_id).addClass('current');
                }
                else
                {
                    if (tab_cad_4 == true) {
                        $('ul.tabs li').removeClass('current');
                        $('.tab-content').removeClass('current');

                        $(this).addClass('current');
                        $("#" + tab_id).addClass('current');
                    }
                    else {
                        $("#Cad_3_Proximo").trigger("click");
                    }
                }
            }
            else
            {
                if (tab_cad_3 == true) {
                    $('ul.tabs li').removeClass('current');
                    $('.tab-content').removeClass('current');

                    $(this).addClass('current');
                    $("#" + tab_id).addClass('current');
                }
                else {
                    $("#Cad_2_Proximo").trigger("click");
                }
            }
        }
        else
        {
            if (tab_cad_2 == true) {
                $('ul.tabs li').removeClass('current');
                $('.tab-content').removeClass('current');

                $(this).addClass('current');
                $("#" + tab_id).addClass('current');
            }
            else
            {
                $("#Cad_1_Proximo").trigger("click");
            }
        }
    });

    $('#CadastrarIntercambiarLogin').click(function () {
        $("#CadastrarIntercambiar1").trigger("click");
    });

    $('#CadastrarIntercambiar1').click(function () {
       
        var el = $('#testeCadastrarIntercambiar');

        $('#testeCadastrarIntercambiar').magnificPopup.open({
            removalDelay: 500, //delay removal by X to allow out-animation
            callbacks: {
                beforeOpen: function () {
                    this.st.mainClass = this.st.el.attr('data-effect');
                }
            },
        });

    });

    $('#Cad_Segundo_Fone').mask('(00) 0000-00009');
    $('#Cad_Segundo_Fone').blur(function(event) {
        if($(this).val().length == 15){ // Celular com 9 dígitos + 2 dígitos DDD e 4 da máscara
            $('#Cad_Segundo_Fone').mask('(00) 00000-0009');
        } else {
            $('#Cad_Segundo_Fone').mask('(00) 0000-00009');
        }
    });

    $('#Cad_Segundo_Celular').mask('(00) 00000-0009');

    $('#Cad_Segundo_DtNasc').mask("99/99/9999");

    $("#cad_Email").focusout(function () {
        if (f_Cad_Email())
            return true;
        else
            return false;
    });

    $("#cad_Primeiro_Nome").focusout(function () {
        if (f_cad_Primeiro_Nome())
            return true;
        else
            return false;
    });

    $("#cad_Sobrenome").focusout(function () {
        if (f_cad_Sobrenome())
            return true;
        else
            return false;
    });

    $("#Cad_1_Proximo").click(function () {
        var _ok;
        _ok = true;
        tab_cad_2 = false;

        if (f_cad_Primeiro_Nome() == false) {
            _ok = false;
        }

        if (f_cad_Sobrenome() == false) {
            _ok = false;
        }

        if (f_Cad_Email() == false) {
            _ok = false;
        }

        if (_ok == true) {
            var tab_id = "tab-2";
           
            $('ul.tabs li').removeClass('current');
            $('.tab-content').removeClass('current');

            $("#tab_Cad_2").addClass('current');
            $("#tab-2").addClass('current');
            tab_cad_2 = _ok;
            $("#Cad_Segundo_Fone").focus();
        }
        Cad_1_Proximo_count += 1;
        return _ok;
    });

    $("#Cad_Segundo_Fone").focusout(function () {
        var txt = $("#Cad_Segundo_Fone").val();
        ValidaCad_Segundo_Fone(txt);
        
    });

    $("#Cad_Segundo_Celular").focusout(function () {
        if (f_Cad_Segundo_Celular())
            return true;
        else
            return false;
    });

    $("#Cad_Segundo_DtNasc").focusout(function () {
        if (f_Cad_Segundo_DtNasc())
            return true;
        else
            return false;
    });

    $("#Cad_Segundo_Escolaridade").focusout(function () {

        $("#Cad_Segundo_Escolaridade_ok").css("display", "block");

    });

    $("#Cad_Segundo_Sexo").focusout(function () {

        $("#Cad_Segundo_Sexo_ok").css("display", "block");

    });

    $("#Cad_Terceiro_Cep").mask("99999-999");

    $("#Cad_2_Proximo").click(function () {
        var _ok;
        _ok = true;
        tab_cad_3 = false;

        if (f_Cad_Segundo_Fone() == false) {
            _ok = false;
        }

        if (f_Cad_Segundo_DtNasc() == false) {
            _ok = false;
        }

        $("#Cad_Segundo_Celular_ok").css("display", "block");
        $("#Cad_Segundo_Escolaridade_ok").css("display", "block");
        $("#Cad_Segundo_Sexo_ok").css("display", "block");

        if (_ok == true) {
            var tab_id = "tab-3";

            $('ul.tabs li').removeClass('current');
            $('.tab-content').removeClass('current');

            $("#tab_Cad_3").addClass('current');
            $("#tab-3").addClass('current');
            tab_cad_3 = _ok;
            $("#Cad_Terceiro_Cep").focus();
            CarregarEstado();
        }
        Cad_2_Proximo_count += 1;
        return _ok;

    });

    $("#Cad_Terceiro_Cep").focusout(function () {
        if (f_Cad_Terceiro_Cep())
            return true;
        else
            return false;
    });

    $("#Cad_Terceiro_Endereco").focusout(function () {
        if (f_Cad_Terceiro_Endereco())
            return true;
        else
            return false;
    });

    $("#Cad_Terceiro_Estado").focusout(function () {
        if (f_Cad_Terceiro_Estado())
            return true;
        else
            return false;
    });

    $("#Cad_Terceiro_Cidade").focusout(function () {
        if (f_Cad_Terceiro_Cidade())
            return true;
        else
            return false;
    });

    $("#Cad_Terceiro_Estado").change(function () {
        var val = $('#Cad_Terceiro_Estado option:selected').val();
        CarregarCidade(val);
    });

    $("#Cad_3_Proximo").click(function () {
        var _ok;
        _ok = true;
        tab_cad_4 = false;

        if (f_Cad_Terceiro_Cep() == false) {
            _ok = false;
        }

        if (f_Cad_Terceiro_Endereco() == false) {
            _ok = false;
        }

        if (f_Cad_Terceiro_Estado() == false) {
            _ok = false;
        }

        if (f_Cad_Terceiro_Cidade() == false) {
            _ok = false;
        }

        if (_ok == true) {
            var tab_id = "tab-4";

            $('ul.tabs li').removeClass('current');
            $('.tab-content').removeClass('current');

            $("#tab_Cad_4").addClass('current');
            $("#tab-4").addClass('current');
            tab_cad_3 = _ok;
            $("#Cad_Quarto_Senha").focus();
        }
        Cad_3_Proximo_count += 1;
        return _ok;

    });

    $("#Cad_Quarto_Senha").focusout(function () {
        if (f_Cad_Quarto_Senha())
            return true;
        else
            return false;
    });

    $("#Cad_Quarto_Confirmar").focusout(function () {
        if (f_Cad_Quarto_Confirmar())
            return true;
        else
            return false;
    });

    $("#Cad_4_Proximo").click(function () {
        var _ok;
        _ok = true;
        tab_cad_4 = false;

        if (f_Cad_Quarto_Senha() == false) {
            _ok = false;
        }

        if (f_Cad_Quarto_Confirmar() == false) {
            _ok = false;
        }

        if (f_cad_Primeiro_Nome() == false) {
            _ok = false;
            $("#tab_Cad_1").trigger("click");
        }

        if (f_cad_Sobrenome() == false) {
            _ok = false;
            $("#tab_Cad_1").trigger("click");
        }

        if (f_Cad_Email() == false) {
            _ok = false;
            $("#tab_Cad_1").trigger("click");
        }
        if (f_Cad_Segundo_Fone() == false) {
            _ok = false;
            $("#tab_Cad_2").trigger("click");
        }

        if (f_Cad_Segundo_DtNasc() == false) {
            _ok = false;
            $("#tab_Cad_2").trigger("click");
        }
        if (f_Cad_Terceiro_Cep() == false) {
            _ok = false;
            $("#tab_Cad_3").trigger("click");
        }

        if (f_Cad_Terceiro_Endereco() == false) {
            _ok = false;
            $("#tab_Cad_3").trigger("click");
        }

        if (f_Cad_Terceiro_Estado() == false) {
            _ok = false;
            $("#tab_Cad_3").trigger("click");
        }

        if (f_Cad_Terceiro_Cidade() == false) {
            _ok = false;
            $("#tab_Cad_3").trigger("click");
        }

        if (_ok == true) {
            var Log = { 
                Email: $('#cad_Email').val(),
                Nome: $('#cad_Primeiro_Nome').val(),
                locate: "br",
                sexo: "m",
                Endereco: $('#Cad_Terceiro_Endereco').val(),
                CEP: $('#Cad_Terceiro_Cep').val(),
                Cidade: $('#Cad_Terceiro_Cidade').val(),
                Estado: $('#Cad_Terceiro_Estado').val(),
                Fone: $('#Cad_Segundo_Fone').val(),
                Celular: $('#Cad_Segundo_Celular').val(),
                Escolaridade: $('#Cad_Segundo_Escolaridade').val(),
                Foto: "",
                Facebooku: "",
                Primeiro_Nome: $('#cad_Primeiro_Nome').val(),
                Sobrenome: $('#cad_Sobrenome').val(),
                DtNasc: $('#Cad_Segundo_DtNasc').val(),
                Senha: $('#Cad_Quarto_Senha').val()
            };
            $.ajax({
                url: '/Social/AddUser',
                type: 'post',
                data: Log,
                async: false,
                cache: false,
                success: function (data) {
                    if (data.resultado == true) {
                        var Log = { Id: 1, email: $('#cad_Email').val(), Senha: $('#Cad_Quarto_Senha').val(), Recuperar: false };
                        $.ajax({
                            url: '/Social/Logar',
                            type: 'post',
                            data: Log,
                            async: false,
                            cache: false,
                            success: function (data) {
                                if (data.resultado == true) {
                                    Logar();
                                    $("#PerfilNomeUsuario").html(data.resultadoNome);
                                    $("#Perfil_Foto").attr("src", data.resultadoFoto);
                                    $("#Login_Resp").css("display", "none");
                                    $("#CelularUsuario").css("display", "block");
                                    $("#CelularUsuario").attr("src", data.resultadoFoto);
                                    $("#PerfilNomeUsuario_Celular").html(data.resultadoNome);
                                    $("#Perfil_Foto_Celular").attr("src", data.resultadoFoto);
                                    $.magnificPopup.close();
                                    if (readCookie("IntercambiarCookieComentar") == "Sim") {
                                        setCookie("IntercambiarCookieComentar", null, -1);
                                        $("#Texto_Comentario_Caixa").css("display", "none");
                                        $("#Texto_Comentario_NaoLogado").css("display", "none");
                                        $("#Texto_Comentario_Texto").css("display", "block");
                                    }
                                    if (data.resultadoAutor == true) {
                                        location.reload();
                                    }
                                }
                                else {
                                    $("#MensagemLogin").html(data.resultadoMensagem);
                                    // $("#MensagemLogin").html(data.resultadoMensagem) 
                                }
                            }
                        });
                    }
                    else {
                        alert(data.resultado);
                    }
                }
            });
        }

        return _ok;

    });

    $("#LoginSair").click(function () {
        $.ajax({
            url: '/Social/Logoff',
            type: 'post',
            async: false,
            cache: false,
            success: function (data) {
                Logado = data.resultado;
                Logar();
                $("#MensagemLogin").html("");
                $("#inputPassword").val("");
            }
        });

    });

    $('#LoginSair_Celular').click(function () {
        $("#LoginSair").trigger("click");
        $.magnificPopup.close();
    });

    $('#CelularUsuario').click(function () {
        $.magnificPopup.open({
            items: {
                src: '#CelularUsuario_painel'
            },
            mainClass: 'mfp-zoom-out',
            type: 'inline'
        });
    });

    // Inline popups
    $('#Login').magnificPopup({
        delegate: 'a',
        removalDelay: 500, //delay removal by X to allow out-animation
        callbacks: {
            beforeOpen: function () {
                this.st.mainClass = this.st.el.attr('data-effect');
            }
        },
        midClick: true // allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source.
    });

    // Inline popups
    $('#Top_Menu_Resp_cLICK').magnificPopup({
        delegate: 'a',
        removalDelay: 500, //delay removal by X to allow out-animation
        callbacks: {
            beforeOpen: function () {
                this.st.mainClass = this.st.el.attr('data-effect');
            }
        },
        midClick: true // allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source.
    });


    // Image popups
    $('#image-popups').magnificPopup({
        delegate: 'a',
        type: 'image',
        removalDelay: 500, //delay removal by X to allow out-animation
        callbacks: {
            beforeOpen: function () {
                // just a hack that adds mfp-anim class to markup 
                this.st.image.markup = this.st.image.markup.replace('mfp-figure', 'mfp-figure mfp-with-anim');
                this.st.mainClass = this.st.el.attr('data-effect');
            }
        },
        closeOnContentClick: true,
        midClick: true // allow opening popup on middle mouse click. Always set it to true if you don't provide alternative source.
    });


    // Hinge effect popup
    $('a.hinge').magnificPopup({
        mainClass: 'mfp-with-fade',
        removalDelay: 1000, //delay removal by X to allow out-animation
        callbacks: {
            beforeClose: function () {
                this.content.addClass('hinge');
            },
            close: function () {
                this.content.removeClass('hinge');
            }
        },
        midClick: true
    });
});


window.fbAsyncInit = function () {
    FB.init({
        appId: '344612332403619',
        xfbml: true,
        version: 'v2.3'
    });
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/pt_BR/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

$(document).ready(function () {
    $("#target").click(function () {
        var url = "/Lista/Listar?tp=b&or=1&wl=" + $("#txtPesquisa").val();
        window.location.href = url;
    });
});


(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-62167326-1', 'auto');
ga('send', 'pageview');
