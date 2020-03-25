

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
            } else if (data.Descripcion === "Autentificacion Fallida"){
                alert(data.Descripcion);
            }
        },
        complete: function () {
            console.log('LOGINUSER');
        }
    });
    return false;
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
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Index'); }
            $('#grado').empty();
            $('#grado').append('<option selected disable value="-1"> Seleccione grado...</option>');

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
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Index'); }
            $('#grupo').empty();
            $('#grupo').append('<option selected disable value="-1"> Seleccione grupo...</option>');

            $.each(data, function (index, item) {
                $('#grupo').append("<option value=\"" + item.Id + "\">" + item.NombreGrupo + "</option>");
            });
        },
        complete: function () {
            console.log('GetGrupo');
        }
    });
}

/*$('#grado').change(function () {
   
}); 

$('#grupo').change(function () {

}); */



//**************************ASISTENCIA *************************************************************************

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
          try { data = JSON.parse(data); } catch{ NavePage('../Home/Index'); }
            CrearTabla(data);
        },
        complete: function () {
            TablaPlus();
            console.log('GetAsistencia');
        }
    });
    return false;
}


function CrearTabla(emp) {
    $('#tableAsistencia tbody tr').remove();
        var estado = null;
        $.each(emp, function (index, item) {
            if (item.Status === true) estado = 'Asistente'; else estado = 'Inasistente';
            let tr = `<tr> 
                      <td style="text-align: center;"> ${index + 1} </td>
                      <td style="text-align: justify;"> ${item.Nombre} </td>
                      <td style="text-align: justify;"> ${item.Apellido} </td>
                      <td style="text-align: justify;"> ${item.Email} </td>
                      <td style="text-align: justify;"> ${item.Materia} </td>
                      <td style="text-align: justify;"> ${estado} </td>
                      <td style="text-align: center;"> <input type="button" value="Editar" class="btn btn-primary" style="width:80px;" onclick="PreventEdit('${item.Id}','${item.Dni}','${item.Materia}','${item.Status}','${item.Email}','${item.Foto}','${item.DniAdm}');"> </td>
                      </tr>`;
            $('#tableAsistencia tbody').append(tr);
        });
}

function TablaPlus() {
    var initDataTable = $('#initDataTable').val();
    if (initDataTable === 'yes') return false;

    $('#tableAsistencia').DataTable({
        language: {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: "true",
        dom: 'Bfrtilp',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel"></i> ',
                titleAttr: 'Exportar a Excel',
                className: 'btn btn-success',
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf"></i> ',
                titleAttr: 'Exportar a PDF',
                className: 'btn btn-danger'
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i> ',
                titleAttr: 'Imprimir',
                className: 'btn btn-info'
            },

        ],

    });
    $('#initDataTable').val('yes');
}

//**************************CONSOLIDADO *************************************************************************

function GetConsolidado() {

    var grado = $("#grado option:selected").text();
    var grupo = $("#grupo option:selected").text();

    if (grado === '' || grupo === '') {
        alert('Seleccione fecha, grado y grupo');
        return false;
    }
    console.log(grado + ' ' + grupo);

    $.ajax({
        type: "POST",
        url: "/Procesor/BuscarPersonaGrado",
        datatype: "json",
        data: {grado: grado, grupo: grupo },
        success: function (data) {
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Index'); }
            console.log(data);
            CrearTablaConsolidado(data);
        },
        complete: function () {
            TablaPlusConsolidado();
            console.log('GetConsolidado');
        }
    });
    return false;
}


function CrearTablaConsolidado(emp) {
    $('#tableConsolidado tbody tr').remove();
    $.each(emp, function (index, item) {
        let tr = `<tr> 
                      <td style="text-align: center;"> ${index + 1} </td>
                      <td style="text-align: justify;"> ${item.Nombre} </td>
                      <td style="text-align: justify;"> ${item.Apellido} </td>
                      <td style="text-align: justify;"> ${item.Dni} </td>
                      <td style="text-align: center;"> <input type="button" value="Historia" class="btn btn-primary" style="width:80px;" onclick="MostrarInformacion('${item.Dni}','${item.Email}');"> </td>
                      <td style="text-align: center;"> <input type="button" value="Email" class="btn btn-primary" style="width:80px;" onclick="EnviarEmail('${item.Dni}','${item.Email}');"> </td>
                      </tr>`;
        $('#tableConsolidado tbody').append(tr);
    });
}

function TablaPlusConsolidado() {
    var initDataTable = $('#initDataTable').val();
    if (initDataTable === 'yes') return false;

    $('#tableConsolidado').DataTable({
        language: {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        },
        responsive: "true",
        dom: 'Bfrtilp',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel"></i> ',
                titleAttr: 'Exportar a Excel',
                className: 'btn btn-success',
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf"></i> ',
                titleAttr: 'Exportar a PDF',
                className: 'btn btn-danger'
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i> ',
                titleAttr: 'Imprimir',
                className: 'btn btn-info'
            },

        ],

    });
    $('#initDataTable').val('yes');
}


function MostrarModal() {
    var modal = document.getElementById('myModal');
    modal.style.display = 'block';
}

function CerrarModal() {
    var modal = document.getElementById('myModal');
    modal.style.display = "none";
}

function CerrarModalUpdate() {
    var modal = document.getElementById('myModal');
    modal.style.display = "none";
    $('#observacion').val('');
}

function PreventEdit(idAsistencia, dni, materia, status, email, foto,dniAdm) {
    $('#idAsistencia').val(idAsistencia);
    $('#dniAdm').val(dniAdm);
    $('#dni').val(dni);
    $('#materia').val(materia);
    $('#email').val(email);


  
    if (status === 'true') {  
        console.log(status);
        $('#asistencia option').eq(0).prop('selected', true);
    } 
    else if (status === 'false') {
        console.log(status);
        $('#asistencia option').eq(1).prop('selected', true);
    }
      
    document.getElementById('alumno').setAttribute('src', 'data:image/jpg;base64,' + foto);
    MostrarModal();
    return false;
}

function EditAtending() {

    var idAsistencia = $('#idAsistencia').val();
    var dni = $('#dni').val();
    var dniAdm = $('#dniAdm').val();
    var email = $('#email').val();
    var status = $("select#asistencia option:checked").val();
    var materia = $('#materia').val();
    var observacion = $('textarea#observacion').val();

    if (status === "1")
        status = true;
    else if (status === "0")
        status = false;

    console.log(observacion.length);
    if (observacion.length < 5) {
        alert('La observacion es requerida');
    }

    $.ajax({
        type: "POST",
        url: "/Procesor/EditAtending",
        datatype: "json",
        data: {idAsistencia: idAsistencia,dni: dni,status: status,materia: materia,observacion: observacion ,dniAdm: dniAdm},
        success: function (data) {
            if (data.Descripcion === "Transaccion Exitosa") {
                GetAsistencia();
            } else if (data.Descripcion === "Transaccion Fallida") {
                alert(data.Descripcion);
            }
        },
        complete: function () {
            console.log('EditAtending');
            CerrarModalUpdate();
        }
    });
    return false;
}

function GetDate (object) {
    var today = new Date();
    var fecha = today.toISOString().substr(0, 10);
    $(object).val(fecha);
}


