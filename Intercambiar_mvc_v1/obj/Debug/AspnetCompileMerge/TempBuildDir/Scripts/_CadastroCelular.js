
function f_Cad_Usuario_Celular() {
    var email = $("#Cad_Usuario_Celular").val();
    if (email != "") {
        var filtro = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
        if (filtro.test(email)) {

            var Endereco = { _Email: $('#Cad_Usuario_Celular').val() };
            $.ajax({
                url: '/Social/VerificarEmail',
                type: 'post',
                data: Endereco,
                async: false,
                cache: false,
                success: function (data) {
                    if (data.existe == true) {
                        $("#Cad_Usuario_Titulo_Celular_Celular").css("color", "red");
                        $('#Cad_Usuario_Celular').addClass('bordered');
                        $("#Cad_Usuario_Titulo_Celular_Celular").html("Email - Erro! E-mail já cadastrado!");
                        return false;
                    }
                    else {
                        $('#Cad_Usuario_Celular').removeClass('bordered');
                        $("#Cad_Usuario_Titulo_Celular_Celular").css("color", "black");
                        $("#Cad_Usuario_Titulo_Celular_Celular").html("Email");
                        return true;
                    }
                }
            });

        } else {
            $('#Cad_Usuario_Celular').addClass('bordered');
            $("#Cad_Usuario_Titulo_Celular_Celular").css("color", "red");
            $("#Cad_Usuario_Titulo_Celular_Celular").html("Email - Erro! Endereço de email invalido!");
            return false;
        }
    } else {
        $('#Cad_Usuario_Celular').addClass('bordered');
        $("#Cad_Usuario_Titulo_Celular_Celular").css("color", "red");
        $("#Cad_Usuario_Titulo_Celular_Celular").html("Email - Erro! Email é obrigatorio!");
        return false;
    }
}

function f_cad_TextObrigatorio(Campo, Label, Titulo) {

    var txt = $(Campo).val();

    if (txt == "") {
        $(Campo).addClass('bordered');
        $(Label).css("color", "red");
        $(Label).html(Titulo + " Erro! Preenchimento é obrigatorio!");
        return false;
    } else {
        $(Campo).removeClass('bordered');
        $(Label).css("color", "black");
        $(Label).html(Titulo);
        return true;
    }

}

function f_cad_Sobrenome() {
    var txt = $("#cad_Sobrenome_Celular").val();
    if (txt != "") {
        $('#cad_Sobrenome_Celular').addClass('bordered');
        $("#Cad_Usuario_Titulo_Sobrenome_Celular").css("color", "red");
        $("#Cad_Usuario_Titulo_Sobrenome_Celular").html("Sobrenome - Erro! Preenchimento é obrigatorio!");
        return true;
    } else {
        $('#cad_Sobrenome_Celular').removeClass('bordered');
        $("#Cad_Usuario_Titulo_Sobrenome_Celular").css("color", "black");
        $("#Cad_Usuario_Titulo_Sobrenome_Celular").html("Sobrenome");
        return false;
    }
}

function f_Cad_Segundo_Fone_Celular() {

    var txt = $("#Cad_Segundo_Fone_Celular").val();

    txt = txt.replace("(", "");
    txt = txt.replace(")", "");
    txt = txt.replace("-", "");
    txt = txt.replace(" ", "").trim();

    if (txt != "") {
        if (txt == '0000000000') {
            $('#Cad_Segundo_Fone_Celular').addClass('bordered');
            $("#Cad_Usuario_Titulo_Celular").css("color", "red");
            $("#Cad_Usuario_Titulo_Celular").html("Fone - Erro! Valor invalido!");
         
            return false;
        } else if (txt == '00000000000') {
            $('#Cad_Segundo_Fone_Celular').addClass('bordered');
            $("#Cad_Usuario_Titulo_Celular").css("color", "red");
            $("#Cad_Usuario_Titulo_Celular").html("Fone - Erro! Valor invalido!");

            return false;
        } else if (txt.length < 10) {
            $('#Cad_Segundo_Fone_Celular').addClass('bordered');
            $("#Cad_Usuario_Titulo_Celular").css("color", "red");
            $("#Cad_Usuario_Titulo_Celular").html("Fone - Erro! Valor invalido!");

            return false;
        }
        else {
            $('#Cad_Segundo_Fone_Celular').removeClass('bordered');
            $("#Cad_Usuario_Titulo_Celular").css("color", "black");
            $("#Cad_Usuario_Titulo_Celular").html("Fone");
            return true;
        }
    } else {
        $('#Cad_Segundo_Fone_Celular').addClass('bordered');
        $("#Cad_Usuario_Titulo_Celular").css("color", "red");
        $("#Cad_Usuario_Titulo_Celular").html("Fone - Erro! Preenchimento é obrigatorio!");

        return false;
    }
}

function f_Cad_Segundo_Celular_Celular() {
    var txt = $("#Cad_Segundo_Celula_Celular").val();

    if (txt != "") {

        txt = txt.replace("(", "");
        txt = txt.replace(")", "");
        txt = txt.replace("-", "");
        txt = txt.replace(" ", "").trim();

        if (txt.length < 10) {
            $('#Cad_Segundo_Celula_Celular').addClass('bordered');
            $('#Cad_Usuario_Titulo_Cel_Celular').css("color", "red");
            $('#Cad_Usuario_Titulo_Cel_Celular').html("Celular - Erro! Valor invalido!");

            return false;
        }
        else {
            $('#Cad_Segundo_Celula_Celular').removeClass('bordered');
            $("#Cad_Usuario_Titulo_Cel_Celular").css("color", "black");
            $("#Cad_Usuario_Titulo_Cel_Celular").html("Celular");
            return true;
        }
    } else {
        $('#Cad_Segundo_Celula_Celular').removeClass('bordered');
        $("#Cad_Usuario_Titulo_Cel_Celular").css("color", "black");
        $("#Cad_Usuario_Titulo_Cel_Celular").html("Celular");
        return true;
    }
}

function f_Cad_Segundo_DtNasc_Celular() {
    var txt = $("#Cad_Segundo_DtNasc_Celular").val();

    if (txt != "") {
        if (verificaData(txt)) {
            $('#Cad_Segundo_DtNasc_Celular').removeClass('bordered');
            $("#Cad_Usuario_Titulo_DtNasc_Celular").css("color", "black");
            $("#Cad_Usuario_Titulo_DtNasc_Celular").html("Data de Nascimento");
            return true;
        }
        else {
            $('#Cad_Segundo_DtNasc_Celular').addClass('bordered');
            $('#Cad_Usuario_Titulo_DtNasc_Celular').css("color", "red");
            $('#Cad_Usuario_Titulo_DtNasc_Celular').html("Data de Nascimento - Erro! Valor invalido!");
            return false;
        }
    } else {
        $('#Cad_Segundo_DtNasc_Celular').addClass('bordered');
        $('#Cad_Usuario_Titulo_DtNasc_Celular').css("color", "red");
        $('#Cad_Usuario_Titulo_DtNasc_Celular').html("Data de Nascimento - Erro! Preenchimento é obrigatorio!");
        return false;
    }
}

function f_Cad_Terceiro_Cep_Celular() {
    var txt = $("#Cad_Terceiro_Cep_Celular").val();
    if (txt != "") {
        if (txt.length < 9) {
            $('#Cad_Terceiro_Cep_Celular').addClass('bordered');
            $('#Cad_Usuario_Titulo_Cep_Celular').css("color", "red");
            $('#Cad_Usuario_Titulo_Cep_Celular').html("CEP- Erro! Valor invalido!");
            return false;
        }
        else {
            $('#Cad_Terceiro_Cep_Celular').removeClass('bordered');
            $("#Cad_Usuario_Titulo_Cep_Celular").css("color", "black");
            $("#Cad_Usuario_Titulo_Cep_Celular").html("CEP");
        }
    }
    else {
        $('#Cad_Terceiro_Cep_Celular').addClass('bordered');
        $('#Cad_Usuario_Titulo_Cep_Celular').css("color", "red");
        $('#Cad_Usuario_Titulo_Cep_Celular').html("CEP- Erro! Preenchimento é obrigatorio!");
        return false;
    }
}

function f_Cad_Terceiro_Endereco_Celular() {
    var txt = $("#Cad_Terceiro_Endereco_Celular").val();
    if (txt != "") {
        $('#Cad_Terceiro_Endereco_Celular').removeClass('bordered');
        $("#Cad_Usuario_Titulo_End_Celular").css("color", "black");
        $("#Cad_Usuario_Titulo_End_Celular").html("Endereço");
        return true;
    } else {
        $('#Cad_Terceiro_Endereco_Celular').addClass('bordered');
        $('#Cad_Usuario_Titulo_End_Celular').css("color", "red");
        $("#Cad_Usuario_Titulo_End_Celular").html("Endereço - Erro! Preenchimento é obrigatorio!");
        return false;
    }
}

function f_Cad_Terceiro_Estado_Celular() {
    var txt = $("#Cad_Terceiro_Estado_Celular").val();
    if (txt != 0) {
        $('#Cad_Terceiro_Estado_Celular').removeClass('bordered');
        $("#Cad_Usuario_Titulo_Estado_Celular").css("color", "black");
        $("#Cad_Usuario_Titulo_Estado_Celular").html("Estado");
    } else {

        $('#Cad_Terceiro_Estado_Celular').addClass('bordered');
        $('#Cad_Usuario_Titulo_Estado_Celular').css("color", "red");
        $("#Cad_Usuario_Titulo_Estado_Celular").html("Estado - Erro! Preenchimento é obrigatorio!");
        return false;
    }
}

function f_Cad_Terceiro_Cidade_Celular() {
    var txt = $("#Cad_Terceiro_Cidade_Celular").val();
    if (txt != "0") {
        $('#Cad_Terceiro_Cidade_Celular').removeClass('bordered');
        $("#Cad_Usuario_Titulo_Cidade_Celular").css("color", "black");
        $("#Cad_Usuario_Titulo_Cidade_Celular").html("Cidade");
    } else {
        $('#Cad_Terceiro_Cidade_Celular').addClass('bordered');
        $('#Cad_Usuario_Titulo_Cidade_Celular').css("color", "red");
        $("#Cad_Usuario_Titulo_Cidade_Celular").html("Cidade - Erro! Preenchimento é obrigatorio!");
        return false;
    }
}


function f_Cad_Quarto_Senha_Celular() {
    var txt = $("#Cad_Quarto_Senha_Celular").val();
    if (txt != "") {
        if (txt.length < 4) {
            $('#Cad_Quarto_Senha_Celular').addClass('bordered');
            $('#Cad_Usuario_Titulo_Senha_Celular').css("color", "red");
            $("#Cad_Usuario_Titulo_Senha_Celular").html("Senha - Erro! No mínimo 4 caracteres!");
            return false;
        }
        else {
            $('#Cad_Quarto_Senha_Celular').removeClass('bordered');
            $("#Cad_Usuario_Titulo_Senha_Celular").css("color", "black");
            $("#Cad_Usuario_Titulo_Senha_Celular").html("Senha");
        }
    } else {
        $('#Cad_Quarto_Senha_Celular').addClass('bordered');
        $('#Cad_Usuario_Titulo_Senha_Celular').css("color", "red");
        $("#Cad_Usuario_Titulo_Senha_Celular").html("Senha - Erro! No mínimo 4 caracteres!");
        return false;
    }
}

function f_Cad_Quarto_Confirmar_Celular() {

    var txt = $("#Cad_Quarto_Confirmar_Celular").val();
    if (txt != "") {
        if (txt != $("#Cad_Quarto_Senha_Celular").val()) {
            $('#Cad_Quarto_Confirmar_Celular').addClass('bordered');
            $('#Cad_Usuario_Titulo_Confirma_Celular').css("color", "red");
            $("#Cad_Usuario_Titulo_Confirma_Celular").html("Confirmar Senha - Erro! Senha incorreta!");
            return false;
        }
        else {
            $('#Cad_Quarto_Confirmar_Celular').removeClass('bordered');
            $("#Cad_Usuario_Titulo_Confirma_Celular").css("color", "black");
            $("#Cad_Usuario_Titulo_Confirma_Celular").html("Confirmar Senha");
        }
    } else {
        $('#Cad_Quarto_Confirmar_Celular').addClass('bordered');
        $('#Cad_Usuario_Titulo_Confirma_Celular').css("color", "red");
        $("#Cad_Usuario_Titulo_Confirma_Celular").html("Confirmar Senha - Erro! Preenchimento é obrigatorio!");
        return false;
    }

}

function CarregarEstado_Celular() {
    $("#Cad_Terceiro_Estado_Celular").empty();
    $("#Cad_Terceiro_Estado_Celular").append("<option value='0'>--Selecione o Estado--</option>")
    $.ajax({
        url: '/Social/GetEstados',
        type: 'post',
        async: false,
        cache: false,
        success: function (states) {
            // states is your JSON array
            var $select = $('#Cad_Terceiro_Estado_Celular');
            $.each(states, function (i, state) {
                $('<option>', {
                    value: state.ID
                }).html(state.nome).appendTo($select);
            });
        }
    });

}

function CarregarCidade_Celular(idEstado) {
    $("#Cad_Terceiro_Cidade_Celular").empty();
    $("#Cad_Terceiro_Cidade_Celular").append("<option value='0'>--Selecione a Cidade--</option>")
    var idEs = { IdEstado: idEstado };
    $.ajax({
        url: '/Social/GetCidades',
        type: 'post',
        data: idEs,
        async: false,
        cache: false,
        success: function (states) {
            // states is your JSON array
            var $select = $('#Cad_Terceiro_Cidade_Celular');
            $.each(states, function (i, state) {
                $('<option>', {
                    value: state.id
                }).html(state.nome).appendTo($select);
            });
        }
    });

}

$(document).ready(function () {


    CarregarEstado_Celular();

    $("#Cad_Usuario_Celular").focusout(function () {
        if (f_Cad_Usuario_Celular())
            return true;
        else
            return false;
    });

    $("#cad_Primeiro_Nome_Celular").focusout(function () {
        if (f_cad_TextObrigatorio("#cad_Primeiro_Nome_Celular", "#Cad_Usuario_Titulo_Primeiro_Nome_Celular", "Primeiro Nome"))
            return true;
        else
            return false;
    });

    $("#cad_Sobrenome_Celular").focusout(function () {
        if (f_cad_TextObrigatorio("#cad_Sobrenome_Celular", "#Cad_Usuario_Titulo_Sobrenome_Celular", "Sobrenome"))
            return true;
        else
            return false;
    });

    $('#Cad_Segundo_Fone_Celular').mask('(00) 0000-00009');
    $('#Cad_Segundo_Fone_Celular').blur(function (event) {
        if ($(this).val().length == 15) { // Celular com 9 dígitos + 2 dígitos DDD e 4 da máscara
            $('#Cad_Segundo_Fone_Celular').mask('(00) 00000-0009');
        } else {
            $('#Cad_Segundo_Fone_Celular').mask('(00) 0000-00009');
        }
    });

    $("#Cad_Segundo_Fone_Celular").focusout(function () {
        var txt = $("#Cad_Segundo_Fone_Celular").val();
        f_Cad_Segundo_Fone_Celular();

    });

    $('#Cad_Segundo_Celula_Celular').mask('(00) 00000-0009');

    $("#Cad_Segundo_Celula_Celular").focusout(function () {
        if (f_Cad_Segundo_Celular_Celular())
            return true;
        else
            return false;
    });

    $('#Cad_Segundo_DtNasc_Celular').mask("99/99/9999");

    $("#Cad_Segundo_DtNasc_Celular").focusout(function () {
        if (f_Cad_Segundo_DtNasc_Celular())
            return true;
        else
            return false;
    });

    $("#Cad_Terceiro_Cep_Celular").mask("99999-999");

    $("#Cad_Terceiro_Cep_Celular").focusout(function () {
        if (f_Cad_Terceiro_Cep_Celular())
            return true;
        else
            return false;
    });

    $("#Cad_Terceiro_Endereco_Celular").focusout(function () {
        if (f_Cad_Terceiro_Endereco_Celular())
            return true;
        else
            return false;
    });

    $("#Cad_Terceiro_Estado_Celular").focusout(function () {
        if (f_Cad_Terceiro_Estado_Celular())
            return true;
        else
            return false;
    });

    $("#Cad_Terceiro_Estado_Celular").change(function () {
        var val = $('#Cad_Terceiro_Estado_Celular option:selected').val();
        CarregarCidade_Celular(val);
    });

    $("#Cad_Terceiro_Cidade_Celular").focusout(function () {
        if (f_Cad_Terceiro_Cidade_Celular())
            return true;
        else
            return false;
    });

    $("#Cad_Quarto_Senha_Celular").focusout(function () {
        if (f_Cad_Quarto_Senha_Celular())
            return true;
        else
            return false;
    });

    $("#Cad_Quarto_Confirmar_Celular").focusout(function () {
        if (f_Cad_Quarto_Confirmar_Celular())
            return true;
        else
            return false;
    });

    $("#Cad_Salvar").click(function () {
            var _ok;
            _ok = true;

            if (f_Cad_Usuario_Celular() == false) {
                _ok = false;
            }

            if (f_Cad_Quarto_Senha_Celular() == false) {
                _ok = false;
            }

            if (f_Cad_Quarto_Confirmar_Celular() == false) {
                _ok = false;
            }

            if (f_cad_TextObrigatorio("#cad_Primeiro_Nome_Celular", "#Cad_Usuario_Titulo_Primeiro_Nome_Celular", "Primeiro Nome") == false) {
                _ok = false;
            }

            if (f_cad_TextObrigatorio("#cad_Sobrenome_Celular", "#Cad_Usuario_Titulo_Sobrenome_Celular", "Sobrenome") == false) {
                _ok = false;
            }

            if (f_Cad_Segundo_Fone_Celular() == false) {
                _ok = false;
            }

            if (f_Cad_Segundo_Celular_Celular() == false) {
                _ok = false;
            }

            if (f_Cad_Segundo_DtNasc_Celular() == false) {
                _ok = false;
            }

            if (f_Cad_Terceiro_Cep_Celular() == false) {
                _ok = false;
            }

            if (f_Cad_Terceiro_Endereco_Celular() == false) {
                _ok = false;
            }

            if (f_Cad_Terceiro_Estado_Celular() == false) {
                _ok = false;
            }

            if (f_Cad_Terceiro_Cidade_Celular() == false) {
                _ok = false;
            }

            if (_ok == true) {
                var Log = {
                    Email: $('#Cad_Usuario_Celular').val(),
                    Nome: $('#cad_Primeiro_Nome_Celular').val(),
                    locate: "br",
                    sexo: "m",
                    Endereco: $('#Cad_Terceiro_Endereco_Celular').val(),
                    CEP: $('#Cad_Terceiro_Cep_Celular').val(),
                    Cidade: $('#Cad_Terceiro_Cidade_Celular').val(),
                    Estado: $('#Cad_Terceiro_Estado_Celular').val(),
                    Fone: $('#Cad_Segundo_Fone_Celular').val(),
                    Celular: $('#Cad_Segundo_Celula_Celular').val(),
                    Escolaridade: $('#Cad_Segundo_Escolaridade_Celular').val(),
                    Foto: "",
                    Facebooku: "",
                    Primeiro_Nome: $('#cad_Primeiro_Nome_Celular').val(),
                    Sobrenome: $('#cad_Sobrenome_Celular').val(),
                    DtNasc: $('#Cad_Segundo_DtNasc_Celular').val(),
                    Senha: $('#Cad_Quarto_Senha_Celular').val()
                };
                $.ajax({
                    url: '/Social/AddUser',
                    type: 'post',
                    data: Log,
                    async: false,
                    cache: false,
                    success: function (data) {
                        if (data.resultado == true) {
                            

                            if (data.resultado == true) {
                                var Log = { Id: 1, email: $('#Cad_Usuario_Celular').val(), Senha: $('#Cad_Quarto_Senha_Celular').val(), Recuperar: false };
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
                                            window.location.href = "/";
                                        }
                                        else {
                                            $("#MensagemLogin").html(data.resultadoMensagem);
                                        }
                                        if (data.resultadoAutor == true) {
                                            location.reload();
                                        }
                                    }
                                });
                            }

                        }
                        else {
                            alert(data.resultado);
                        }
                    }
                });
            }
            else
            {
                $('#Cad_Usuario_Erros_Celular').css("color", "red");
                $("#Cad_Usuario_Erros_Celular").html("Erro! Verifiquei informações inseridas");
            }

            return _ok;
    });

    //$("#Salvar").click(function () {
    //    var _ok;
    //    _ok = true;

    //    alert("teste");

    //    //if (f_Cad_Quarto_Senha_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_Cad_Quarto_Confirmar_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_cad_Primeiro_Nome_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_cad_Sobrenome_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_Cad_Email_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_Cad_Segundo_Fone_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_Cad_Segundo_DtNasc_Celular() == false) {
    //    //    _ok = false;
    //    //}
    //    //if (f_Cad_Terceiro_Cep_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_Cad_Terceiro_Endereco_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_Cad_Terceiro_Estado_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (f_Cad_Terceiro_Cidade_Celular() == false) {
    //    //    _ok = false;
    //    //}

    //    //if (_ok == true) {
    //    //    var Log = { 
    //    //        Email: $('#cad_Email').val(),
    //    //        Nome: $('#cad_Primeiro_Nome').val(),
    //    //        locate: "br",
    //    //        sexo: "m",
    //    //        Endereco: $('#Cad_Terceiro_Endereco').val(),
    //    //        CEP: $('#Cad_Terceiro_Cep').val(),
    //    //        Cidade: $('#Cad_Terceiro_Cidade').val(),
    //    //        Estado: $('#Cad_Terceiro_Estado').val(),
    //    //        Fone: $('#Cad_Segundo_Fone').val(),
    //    //        Celular: $('#Cad_Segundo_Celular').val(),
    //    //        Escolaridade: $('#Cad_Segundo_Escolaridade').val(),
    //    //        Foto: "",
    //    //        Facebooku: "",
    //    //        Primeiro_Nome: $('#cad_Primeiro_Nome').val(),
    //    //        Sobrenome: $('#cad_Sobrenome').val(),
    //    //        DtNasc: $('#Cad_Segundo_DtNasc').val(),
    //    //        Senha: $('#Cad_Quarto_Senha').val()
    //    //    };
    //    //    $.ajax({
    //    //        url: '/Social/AddUser',
    //    //        type: 'post',
    //    //        data: Log,
    //    //        async: false,
    //    //        cache: false,
    //    //        success: function (data) {
    //    //            if (data.resultado == true) {
    //    //                alert(data.resultado);
    //    //            }
    //    //            else {
    //    //                alert(data.resultado);
    //    //            }
    //    //        }
    //    //    });
    //    //}

    //    return _ok;

    //});

});