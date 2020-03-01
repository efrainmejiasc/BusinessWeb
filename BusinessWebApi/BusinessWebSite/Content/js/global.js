

function LoginUser() {
    var user = $('#user').val();
    var password = $('#password').val();
    var flag = $('#check:checked').val();
    console.log(flag);
    if (user === '' || password === ''|| flag !== 'on') {
        alert('Todos los campos son obligatorios');
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Home/LoginUser",
        data: {user: user , password: password},
        datatype: "json",
        success: function (data) {
            if (data.Descripcion === "Autentificacion Exitosa") {
                alert(data.Descripcion);
            } else {
                alert(data.Descripcion);
            }
        },
        complete: function () {
            console.log('LOGINUSER');
        }
    });
}

function OcultarValidacion() {
    document.getElementById('validacion').style.display = 'none';
    $('#password').val('');
    $('#password2').val('');
}

function NavePage(page) {
    window.location.href = page;
}