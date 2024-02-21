$(document).ready(function () {
    var elmnt = $("#PerfilNomeUsuario").html();
    var src = $("#Perfil_Foto").attr('src');

    if (elmnt.length <= 10) {
        for (i = elmnt.length; i <= 11; i++) {
            elmnt += " &nbsp";
        }
    }
    else {
        if(elmnt.length > 26)
        {
            elmnt = elmnt.substring(1, 26)
        }
    }

    $("#Perfil_Nome_1").html(elmnt);
    $("#Perfil_Foto_1").attr("src", src);
});