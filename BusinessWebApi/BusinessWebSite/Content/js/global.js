

function LoginUser() {
    var user = $('#user').val();
    var password = $('#password').val();
    var flag = $('#check:checked').val();
    console.log(flag);
    if (user === '' || password === '' || flag !== 'on') {
        alert('Todos los campos son obligatorios');
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Home/LoginUser",
        data: { user: user, password: password },
        datatype: "json",
        success: function (data) {
            if (data.Descripcion === "Autentificacion Exitosa") {
                alert(data.Descripcion);
            } else if (data.Descripcion === "Autentificacion Fallida") {
                alert(data.Descripcion);
            }
        },
        complete: function () {
            console.log('LOGINUSER');
        }
    });
    return false;
}

function GetGrado() {
    $.ajax({
        type: "POST",
        url: "/Procesor/GetGrados",
        datatype: "json",
        success: function (data) {
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Autentication'); }

            $('#grado').empty();
            $('#grado').append('<option selected disabled value=""> Seleccione grado...</option>');

            $.each(data, function (index, item) {
                $('#grado').append("<option value=\"" + item.Id + "\">" + item.NombreGrado + "</option>");
            });
        },
        complete: function () {
            console.log('GetGrado');
        }
    });
    return false;
}

function GetGrupo() {
    $.ajax({
        type: "POST",
        url: "/Procesor/GetGrupos",
        datatype: "json",
        success: function (data) {
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Autentication'); }

            $('#grupo').empty();
            $('#grupo').append('<option selected disabled value=""> Seleccione grupo...</option>');

            $.each(data, function (index, item) {
                $('#grupo').append("<option value=\"" + item.Id + "\">" + item.NombreGrupo + "</option>");
            });
        },
        complete: function () {
            GetTurno();
            console.log('GetGrupo');
        }
    });
    return false;
}


function GetTurno() {
    $('#turno').empty();
    $('#turno').append('<option selected disabled value=""> Seleccione turno...</option>');
    $('#turno').append('<option  value="1">MAÑANA</option>');
    $('#turno').append('<option  value="2">TARDE</option>');
    $('#turno').append('<option  value="3">NOCHE</option>');
}

/*function GetTurno() {
    $.ajax({
        type: "POST",
        url: "/Procesor/GetTurnos",
        datatype: "json",
        success: function (data) {
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Autentication'); }

            $('#grupo').empty();
            $('#grupo').append('<option selected disabled value=""> Seleccione turno...</option>');

            $.each(data, function (index, item) {
                $('#grupo').append("<option value=\"" + item.Id + "\">" + item.NombreTurno + "</option>");
            });
        },
        complete: function () {
            console.log('GetTurno');
        }
    });
    return false;
}*/

//**************************ASISTENCIA *************************************************************************

function GetAsistencia() {
    var fecha = $('#fecha').val();
    var grado = $("#grado option:selected").text();
    var grupo = $("#grupo option:selected").text();
    var nameTurno = $("#turno option:selected").text();
    var turno = 0;

    if (nameTurno === 'MAÑANA')
        turno = 1;
    else if (nameTurno === 'TARDE')
        turno = 2;
    else if (nameTurno === 'NOCHE')
        turno = 3;

    console.log(fecha + grado + grupo + turno);
    if (fecha === '' || grado === '' || grupo === '' || turno === 0) {
        alert('Seleccione fecha, grado y grupo');
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Procesor/GetAsistencia",
        datatype: "json",
        data: { fecha: fecha, grado: grado, grupo: grupo, turno: turno },
        success: function (data) {
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Autentication'); }
            console.log(data);
            CrearTabla(data);
        },
        complete: function () {
            TablaPlus();
            console.log('GetAsistencia');
            $('#tituloBusqueda1').val('ASISTENCIAS - ' + fecha + ' Grado: ' + grado + ' Grupo: ' + grupo + ' Turno: ' + nameTurno);
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
                      <td style="text-align: center;"> <input type="button" value="Editar" class="btn btn-primary" style="width:80px;" onclick="PreventEdit('${item.Id}','${item.Dni}','${item.Materia}','${item.Status}','${item.Email}','${item.Foto}','${item.DniAdm}','${item.NombreProfesor}');"> </td>
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
                title: function TituloAsistencia() {
                    return $('#tituloBusqueda1').val();
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf"></i> ',
                titleAttr: 'Exportar a PDF',
                className: 'btn btn-danger',
                title: function TituloAsistencia() {
                    return $('#tituloBusqueda1').val();
                }
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i> ',
                titleAttr: 'Imprimir',
                className: 'btn btn-info',
                title: function TituloAsistencia() {
                    return $('#tituloBusqueda1').val();
                }
            }

        ]

    });
    $('#initDataTable').val('yes');
}



//**************************CONSOLIDADO *************************************************************************

function GetConsolidado() {

    var grado = $("#grado option:selected").text();
    var grupo = $("#grupo option:selected").text();
    var nameTurno = $("#turno option:selected").text();
    var turno = 0;
    var fecha = EstablecerFecha();

    if (nameTurno === 'MAÑANA')
        turno = 1;
    else if (nameTurno === 'TARDE')
        turno = 2;
    else if (nameTurno === 'NOCHE')
        turno = 3;

    if (grado === '' || grupo === '' || turno === 0) {
        alert('Seleccione fecha, grado y grupo');
        return false;
    }
    console.log(grado + ' ' + grupo + '' + turno);

    $.ajax({
        type: "POST",
        url: "/Procesor/BuscarPersonaGrado",
        datatype: "json",
        data: { grado: grado, grupo: grupo, turno: turno },
        success: function (data) {
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Autentication'); }
            console.log(data);
            CrearTablaConsolidado(data);
        },
        complete: function () {
            TablaPlusConsolidado();
            console.log('GetConsolidado');
            $('#tituloBusqueda2').val('CONSOLIDADO - ' + fecha + ' Grado: ' + grado + ' Grupo: ' + grupo + ' Turno: ' + nameTurno);
        }
    });
    return false;
}

function EstablecerFecha() {
    var today = new Date();
    var fecha = today.toISOString().substr(0, 10);
    return fecha;
}


function CrearTablaConsolidado(emp) {
    $('#tableConsolidado tbody tr').remove();

    $.each(emp, function (index, item) {
        let tr = `<tr> 
                      <td style="text-align: center;"> ${index + 1} </td>
                      <td style="text-align: justify;"> ${item.Nombre} </td>
                      <td style="text-align: justify;"> ${item.Apellido} </td>
                      <td style="text-align: justify;"> ${item.Dni} </td>
                      <td style="text-align: center;"> <input type="button" value="Historia" class="btn btn-primary" style="width:80px;" onclick="MostrarHistoria('${item.Dni}','${item.Email}','${item.Foto}');"> </td>
                      <td style="text-align: center;"> <input type="button" value="Email" class="btn btn-success" style="width:80px;" onclick="PreventEnviarEmail('${item.Dni}','${item.Email}','${item.Nombre}','${item.Apellido}');"> </td>
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
                title: function TituloConsolidado() {
                    return $('#tituloBusqueda2').val();
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf"></i> ',
                titleAttr: 'Exportar a PDF',
                className: 'btn btn-danger',
                title: function TituloConsolidado() {
                    return $('#tituloBusqueda2').val();
                }
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i> ',
                titleAttr: 'Imprimir',
                className: 'btn btn-info',
                title: function TituloConsolidado() {
                    return $('#tituloBusqueda2').val();
                }
            }

        ]

    });
    $('#initDataTable').val('yes');
}



// **************************************************HISTORIAL DE ASISTENCIAS ******************************************************************

function MostrarHistoria(dni, email, foto) {

    var inMagen = 'data:image/jpg;base64,'.concat(foto);
    console.log(inMagen);
    $('#imgAlumno').attr('src', inMagen);
    $('#dniEstudiante').val(dni);
    console.log(dni);
    $.ajax({
        type: "POST",
        url: "/Procesor/GetHistoriaAsistenciaPerson",
        datatype: "json",
        data: { dni: dni },
        success: function (data) {
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Autentication'); }
            CrearTablaHistoria(data);
        },
        complete: function () {
            console.log('MostrarHistoria');
        }
    });
    MostrarModal();
    return false;
}

function CrearTablaHistoria(emp, inMagen) {
    var strTotal = '&nbsp;&nbsp; Total Inasistencias: ';
    var total = 0;
    $('#tableHistoria tbody tr').remove();
    if (emp.length > 0) {
        $.each(emp, function (index, item) {
            total = total + item.NumeroInasistencia;
            let tr = `<tr> 
                      <td style="text-align: center;"> ${index + 1} </td>
                      <td style="text-align: justify;"> ${item.Materia} </td>
                      <td style="text-align: center;"> ${item.NumeroInasistencia} </td>
                      <td style="text-align: center;"> <input type="button" value="Detalle" class="btn btn-success" style="width:80px;" onclick="InasistenciaDetail('${item.Materia}', '${item.DniAdm}');"> </td>
                      </tr>`;
            $('#tableHistoria tbody').append(tr);
            $('#total').html(strTotal + total + ' &nbsp;&nbsp;');
        });
    } else {
        let tr = `<tr> 
                      <td colspan="4"> No posee inasistencia </td>
                      </tr>`;
        $('#tableHistoria tbody').append(tr);
        $('#total').html(strTotal + 0 + ' &nbsp;&nbsp;');
    }

    return false;
}

//*************************************DETALLES INASISTENCIA **********************************************************
function InasistenciaDetail(materia, dniAdm) {
    var dni = $('#dniEstudiante').val();
    $.ajax({
        type: "POST",
        url: "/Procesor/GetHistoriaAsistenciaMateria",
        datatype: "json",
        data: { dni: dni, materia: materia, dniAdm: dniAdm },
        success: function (data) {
            try { data = JSON.parse(data); } catch{ NavePage('../Home/Autentication'); }
            CrearTablaInasistenciaDetail(data);
        },
        complete: function () {
            console.log('MostrarHistoria');
        }
    });
    MostrarModalDetail();
    return false;
}

function CrearTablaInasistenciaDetail(emp) {
    $('#tableInasistenciaDetail tbody tr').remove();
    if (emp.length > 0) {
        $.each(emp, function (index, item) {

            let tr = `<tr> 
                      <td style="text-align: center;"> ${index + 1} </td>
                      <td style="text-align: justify;"> ${item.NombreProfesor} </td>
                      <td style="text-align: justify;"> ${item.Materia} </td>
                      <td style="text-align: justify;"> ${item.CreateDate.substr(0, 10)} </td>
                      </tr>`;
            $('#tableInasistenciaDetail tbody').append(tr);
        });
    }
    else {
        let tr = `<tr> 
                      <td colspan="4"> No posee inasistencia </td>
                      </tr>`;
        $('#tableInasistenciaDetail tbody').append(tr);

    }
}

//*********************************ENVIAR_EMAIL*************************************************

function PreventEnviarEmail(dni, email, nombre, apellido) {
    $('#cce').html(email);
    $('#di').val(dni);
    $('#name').val(nombre);
    $('#lastName').val(apellido);
    MostrarModalEmail();
}

function EnviarEmail() {
    var email = $('#cce').html();
    var dni = $('#di').val();
    var nombre = $('#name').val();
    var apellido = $('#lastName').val();
    var chx = $('#check:checked').val();
    if (chx === 'on')
        chx = true;
    else
        chx = false;

    console.log(chx);
    var asunto = $('#asunto').val();
    var mensaje = $('#mensaje').val();

    if (asunto === '' || mensaje === '') {
        alert('El asunto y el cuerpo del mensaje son requeridos');
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Procesor/EnviarEmail",
        datatype: "json",
        data: { dni: dni, nombre: nombre, apellido: apellido, email: email, asunto: asunto, mensaje: mensaje, resumen: chx },
        success: function (data) {
            if (data.Descripcion === "Email enviado")
                alert(data.Descripcion);
            else if (data.Descripcion === "Fallo el envio")
                alert(data.Descripcion);
            else
                NavePage('../Home/Autentication');

        },
        complete: function () {
            console.log('EnviarEmail');
            CerrarModalEmail();
        }
    });
    return false;
}

//**********************ACTUALIZACION DE ASISTENCIAS *************************************************

function PreventEdit(idAsistencia, dni, materia, status, email, foto, dniAdm, nombreProfesor) {
    $('#idAsistencia').val(idAsistencia);
    $('#dniAdm').val(dniAdm);
    $('#dni').val(dni);
    $('#materia').val(materia);
    $('#email').val(email);
    $('#profesor').val(nombreProfesor);

    var inMagen = 'data:image/jpg;base64,'.concat(foto);
    console.log(inMagen);
    $('#imgEstudiante').attr('src', inMagen);


    if (status === 'true') {
        console.log(status);
        $('#asistencia option').eq(0).prop('selected', true);
    }
    else if (status === 'false') {
        console.log(status);
        $('#asistencia option').eq(1).prop('selected', true);
    }

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
        data: { idAsistencia: idAsistencia, dni: dni, status: status, materia: materia, observacion: observacion, dniAdm: dniAdm },
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

//************HELPERS*******************************************

function OcultarValidacion() {
    document.getElementById('validacion').style.display = 'none';
    $('#password').val('');
    $('#password2').val('');
}

function NavePage(page) {
    window.location.href = page;
}

function GetDate(object) {
    var today = new Date();
    var fecha = today.toISOString().substr(0, 10);
    $(object).val(fecha);
}

function OcultarIcons() {
    $('#soporte').hide();
    $('#soporteImg').hide();
    $('#producto').hide();
    $('#productoImg').hide();
    $('#contacto').hide();
    $('#contactoImg').hide();
}

//*************OPEN_CLOSE_MODAL*******************************

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

function MostrarModalEmail() {
    var modal = document.getElementById('myModalEmail');
    modal.style.display = 'block';
}

function CerrarModalEmail() {
    var modal = document.getElementById('myModalEmail');
    modal.style.display = "none";
}

function MostrarModalDetail() {
    var modal = document.getElementById('myModalDetail');
    modal.style.display = 'block';
}

function CerrarModalDetail() {
    var modal = document.getElementById('myModalDetail');
    modal.style.display = "none";
}

//**************MENU *************************************************************

function MostrarMenu() {
    document.getElementById("sidebar").style.width = "350px";
    //document.getElementById("contenido").style.marginLeft = "350px";
    document.getElementById("abrir").style.display = "none";
    document.getElementById("cerrar").style.display = "inline";
}

function OcultarMenu() {
    document.getElementById("sidebar").style.width = "0";
    // document.getElementById("contenido").style.marginLeft = "0";
    document.getElementById("abrir").style.display = "inline";
    document.getElementById("cerrar").style.display = "none";
}


//**********************************CONTACTO*******************************************************************

function ContactoMail() {
    var email = $('#email').val();
    var asunto = $('#asunto').val();
    var mensaje = $('#mensaje').val();
    var flag = $('#check:checked').val();
    if (email === '' || asunto === '' || mensaje === '' || flag !== 'on') {
        alert('Todos los campos son requeridos');
        return false;
    }
    $.ajax({
        type: "POST",
        url: "/Contact/MensajeContacto",
        data: { email: email, asunto: asunto, mensaje: mensaje },
        datatype: "json",
        success: function (data) {
            alert(data.Descripcion);
            $('#email').val('');
            $('#asunto').val('');
            $('mensaje').val('');
            $('#check').prop("checked", false);
        },
        complete: function () {
            console.log('ContactoMail');
        }
    });
    return false;
}

//**********************************SOPORTE*******************************************************************

function GetTemaSoporte() {
    $('#tema').empty();
    $('#tema').append('<option selected disabled value=""> Seleccione tema...</option>');
    $('#tema').append('<option  value="1">CARNETIZACION</option>');
    $('#tema').append('<option  value="2">BIOMETRIA</option>');
    $('#tema').append('<option  value="3">INVENTARIOS</option>');
    $('#tema').append('<option  value="4">OTRO</option>');
}

function ContactoSoporte() {
    var email = $('#email').val();
    var e = document.getElementById("tema");
    var tema = e.options[e.selectedIndex].text;
    var mensaje = $('#mensaje1').val();
    var flag = $('#check:checked').val();
    if (email === '' || tema === '' || mensaje === '' || flag !== 'on') {
        alert('Todos los campos son requeridos');
        return false;
    }
    $.ajax({
        type: "POST",
        url: "/Contact/MensajeSoporte",
        data: { email: email, tema: tema, mensaje: mensaje },
        datatype: "json",
        success: function (data) {
            alert(data.Descripcion);
            $('#email').val('');
            $('#tema').val('');
            $('mensaje').val('');
            $('#check').prop("checked", false);
        },
        complete: function () {
            console.log('ContactoSoporte');
        }
    });
    return false;
}





