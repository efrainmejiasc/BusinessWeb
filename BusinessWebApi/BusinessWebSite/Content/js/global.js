

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

function GetGrado() {
    $.ajax({
        type:"POST",
        url: "/Procesor/GetGrados",
        datatype: "json",
        success: function (data) {
            data = JSON.parse(data);
            $('#grado').empty();
            $('#grado').append('<option selected disable value="-1"> Seleccione...</option>');

            $.each(data, function (index, item) {
                $('#grado').append("<option value=\"" + item.Id + "\">" + item.NombreGrado + "</option>");
            });     
        },
        complete: function () {
            console.log('GetGrado');
        }
    });
}

function GetGrupo() {
    $.ajax({
        type: "POST",
        url: "/Procesor/GetGrupos",
        datatype: "json",
        success: function (data) {
            data = JSON.parse(data);
            $('#grupo').empty();
            $('#grupo').append('<option selected disable value="-1"> Seleccione...</option>');

            $.each(data, function (index, item) {
                $('#grupo').append("<option value=\"" + item.Id + "\">" + item.NombreGrupo + "</option>");
            });
        },
        complete: function () {
            console.log('GetGrupo');
        }
    });
}

$('#grado').on('change', function (e) {

});

$('#grupo').on('change', function (e) {

});


function GetAsistencia() {
    var fecha = $('#fecha').val();
    var grado = $("#grado option:selected").text();
    var grupo = $("#grupo option:selected").text();

    console.log(fecha + grado + grupo);
    if (fecha ==='' || grado === '' || grupo === '') {
        alert('Seleccione fecha, grado y grupo');
        return false;
    }


    $.ajax({
        type: "POST",
        url: "/Procesor/GetAsistencia",
        datatype: "json",
        data: {fecha: fecha, grado: grado, grupo: grupo},
        success: function (data) {
            data = JSON.parse(data);
            console.log(data);
            CrearTabla(data);
        },
        complete: function () {
            console.log('GetAsistencia');
        }
    });
}


function CrearTabla(emp) {
    var estado = null;
    $('#tableAsistencia tbody tr').remove();
    $.each(emp, function (index, item) {
        if (item.Status === true) estado = 'Asistente'; else  estado = 'Inasistente';
        let tr = `<tr> 
                      <td style="text-align: center;"> ${index + 1} </td>
                      <td style="text-align: justify;"> ${item.Nombre} </td>
                      <td style="text-align: justify;"> ${item.Apellido} </td>
                      <td style="text-align: justify;"> ${item.Email} </td>
                      <td style="text-align: justify;"> ${item.Materia} </td>
                      <td style="text-align: justify;"> ${estado} </td>
                      <td style="text-align: center;"> <input type="button" value="Editar" class="btn btn-primary" style="width:80px;" onclick="PreventEdit('${item.Dni}' ,'${item.CreateDate}','${item.Id}','${item.Email}');"> </td>
                      <td style="text-align: center;"> <input type="button" value="Email" class="btn btn-success" style="width:80px;" onclick="ShowModal('${item.Dni}' ,'${item.CreateDate}','${item.Id}','${item.Email}');"> </td>
                        </tr >`;
        $('#tableAsistencia tbody').append(tr);
    });
}


