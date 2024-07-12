

//Codigo =================================================================================================

// Crockford's supplant method (poor man's templating)
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}



// Asigna formato a la fila a insertar o actualizar
function formatDispositivo(dispositivo) {

    // Agregar la clase y modificar el valor de "Conectado" según su estado
    var conectadoClass = dispositivo.conectado ? 'text-success' : 'text-danger';
    var conectado = dispositivo.conectado ? 'Online' : 'Offline';

    // Modificar el valor de "Accesible" según su estado y agregar la clase correspondiente
    var accesible = dispositivo.accesible ? 'Sí' : 'No';
    var accesibleClass = dispositivo.accesible ? 'text-success' : 'text-danger';

    //debugger;

    dispositivo.conectadoClass = conectadoClass;
    dispositivo.Conectado = conectado;

    dispositivo.accesibleClass = accesibleClass;
    dispositivo.Accesible = accesible;

    return $.extend(dispositivo, {
        IP: dispositivo.IP,
        Nombre: dispositivo.Nombre,
        Tipo: dispositivo.Tipo,
        Conectado: dispositivo.Conectado + '',
        Accesible: dispositivo.Accesible + '',
        Sector: dispositivo.Sector,
        conectadoClass: dispositivo.conectadoClass,
        accesibleClass: dispositivo.accesibleClass
    });
}



function formatEstado(estado) {

    return $.extend(estado, {

        TipoEstado: estado.item1,
        Cantidad: estado.item2.toString()
    });
}



function formatMensaje(mensaje) {

    // Formatear la fecha al formato deseado
    var fecha = new Date(mensaje.fechaGenerado);

    var dia = ('0' + fecha.getDate()).slice(-2);
    var mes = ('0' + (fecha.getMonth() + 1)).slice(-2);
    var año = fecha.getFullYear();
    var hora = ('0' + fecha.getHours()).slice(-2);
    var minutos = ('0' + fecha.getMinutes()).slice(-2);
    var segundos = ('0' + fecha.getSeconds()).slice(-2);

    var fechaFormateada = dia + '-' + mes + '-' + año + ' - ' + hora + ':' + minutos + ':' + segundos;


    return $.extend(mensaje, {
        Id: mensaje.id.toString(),
        FechaGenerado: fechaFormateada,
        Contenido: mensaje.contenido,
        DispositivoIP: mensaje.provieneDispositivo.ip
    });
}



function cargoPanelEstado() {

    return connection.invoke("cargoPanelEstado").then(function (listaEstados) {

        $.each(listaEstados, function () {

            var estado = formatEstado(this);

            if (estado.TipoEstado == "Conectados") {
                $estadoCantidadConectados.empty();
                $estadoCantidadConectados.append(estado.Cantidad);

            }
            else if (estado.TipoEstado == "Alerta") {
                $estadoCantidadAlerta.empty();
                $estadoCantidadAlerta.append(estado.Cantidad);
            }
            else if (estado.TipoEstado == "Criticos") {
                $estadoCantidadCriticos.empty();
                $estadoCantidadCriticos.append(estado.Cantidad);
            }
            else if (estado.TipoEstado == "Desconocidos") {
                $estadoCantidadDesconocidos.empty();
                $estadoCantidadDesconocidos.append(estado.Cantidad);
            }
            else if (estado.TipoEstado == "Todos") {
                $estadoCantidadTodos.empty();
                $estadoCantidadTodos.append(estado.Cantidad);
            }

        });
    });
}



function cargoVisorMensajes() {
    return connection.invoke("cargoMensajesVisor").then(function (mensajes) {
        $visorMensajesUl.empty();

        if (mensajes.length === 0) {
            $visorMensajesUl.append('<li>No hay mensajes recientes...</li>');
        }
        else {
            $.each(mensajes, function () {
                var mensaje = formatMensaje(this);
                $visorMensajesUl.append(liTemplateVisor.supplant(mensaje));
            });
        }
    });
}



function openUpdatePanel() {

    // Llamar al método del servidor
    connection.invoke("OpenPanelEstado").then(function () {
    }).catch(function (error) {
        console.error("Error al llamar al método del servidor: " + error);
    });
}



function actualizoDispositivosXGrupo() {

    var ddlprueba = document.getElementById("DDLGrupos");

    //Si estoy fuera de la pagina no ejecuto la busqueda por grupo ni actualizo
    if (ddlprueba != null) {

        var ddlValor = ddlprueba.options[ddlprueba.selectedIndex].value;

        return connection.invoke("actualizoDispositivosXGrupo", ddlValor).then(function (dispositivos) {

            $sondeoTableBody.empty();
            $.each(dispositivos, function () {
                var dispositivo = formatDispositivo(this);
                $sondeoTableBody.append(rowTemplate.supplant(dispositivo));
            });
        });
    }
}




$(function () {
    $sondeoTable = $('#sondeoTable'),
        $sondeoTableBody = $sondeoTable.find('tbody'),
        rowTemplate = '<tr data-ip="{ip}"><td class="text-center">{ip}</td><td class="text-center">{nombre}</td><td class="text-center">{tipo}</td><td class="{conectadoClass} text-center">{Conectado}</td><td class="{accesibleClass} text-center">{Accesible}</td><td class="text-center">{sector}</td></tr>',

        liTemplate = '<li data-ip="{ip}"><span class="ip">{ip}</span> <span class="nombre">{nombre}</span> <span class="tipo">{tipo}</span> <span class="Conectado">{Conectado}</span> <span class="Accesible">{Accesible}</span> <span class="sector">{sector}</span> </li>',

        //Agrego Prueba Visor
        $visorMensajes = $('#visorMensajes'),
        $visorMensajesUl = $visorMensajes.find('ul'),
        liTemplateVisor = '<li data-ip="{Id}"><span class="id"></span>  <span class="contenido">{Contenido}</span>  <span class="fechaGenerado text-primary"><strong>[{FechaGenerado}]</strong></span> </li>',

        //Variables para carga de Estado
        $estadoCantidadConectados = $('#cantidadConectados'),
        $estadoCantidadAlerta = $('#cantidadAlerta'),
        $estadoCantidadCriticos = $('#cantidadCriticos'),
        $estadoCantidadDesconocidos = $('#cantidadDesconocidos'),
        $estadoCantidadTodos = $('#cantidadTodos')
        ;
});




function parpadeo(elemento, color, duracion) {

    var originalColor = elemento.style.backgroundColor;

    if (color == '255,148,148') {
        elemento.style.animation = `parpadeoRojoAnimation ${duracion / 1000}s`;
    }
    else {
        elemento.style.animation = `parpadeoVerdeAnimation ${duracion / 1000}s`;
    }

    setTimeout(function () {

        elemento.style.backgroundColor = originalColor;
        elemento.style.animation = '';

    }, duracion);
}



const connection = new signalR.HubConnectionBuilder()
    .withUrl("/monitoreoredhub")
    .configureLogging(signalR.LogLevel.Information)
    .build();


connection.start()
    .then(cargoPanelEstado)
    .then(cargoVisorMensajes)
    .then(openUpdatePanel)
    .catch(function (err) {
        return console.error(err.toString());
    });


document.getElementById("open").addEventListener("click", function () {

    connection.invoke("OpenSondeo").then(function () {
    }).catch(function (error) {
        console.error("Error al llamar al método del servidor: " + error);
    });
});



document.getElementById("close").addEventListener("click", function () {

    connection.invoke("CloseSondeo").then(function () {
    }).catch(function (error) {
        console.error("Error al llamar al método del servidor: " + error);
    });
});



$("#DDLGrupos").change(function () {
    actualizoDispositivosXGrupo();
});




window.addEventListener('beforeunload', function (event) {
    // Llama al método del hub para notificar que el usuario se está desconectando
    connection.invoke("CloseSondeo").catch(function (err) {
        console.error(err.toString());
    });

    connection.invoke("ClosePanelEstadoPanel").catch(function (err) {
        console.error(err.toString());
    });

});






// Métodos del lado del cliente que el servidor llamará

connection.on('monitoreoRedOpened', function () {
    $("#open").prop("disabled", true);
    $("#close").prop("disabled", false);
});



connection.on('monitoreoRedClosed', function () {
    $("#open").prop("disabled", false);
    $("#close").prop("disabled", true);
});



connection.on('updateMonitoreoRed', function (dispositivo) {

    // Obtener la URL actual
    var urlActual = window.location.href;

    if (urlActual.includes('IndexMonitoreo') == true) {

        var displaySondeo = formatDispositivo(dispositivo),

            $row = $(rowTemplate.supplant(displaySondeo)),
            $li = $(liTemplate.supplant(displaySondeo)),

            bg = dispositivo.Conectado == "Offline"
                ? '255,148,148' // red
                : '154,240,117'; // green

        $sondeoTableBody.find('tr[data-ip="' + dispositivo.ip + '"]')
            .replaceWith($row);

        parpadeo($row[0], bg, 1000);
        parpadeo($li[0], bg, 1000);
    }
    else
    {
        console.log("Metodo updateMonitoreoRed no se ejecuta. Fuera de IndexMonitoreo.")
    }
});



connection.on('actualizoPanelEstado', function (listaEstados) {

    $.each(listaEstados, function () {

        var estado = formatEstado(this);

        if (estado.TipoEstado == "Conectados") {
            $estadoCantidadConectados.empty();
            $estadoCantidadConectados.append(estado.Cantidad);
        }
        else if (estado.TipoEstado == "Alerta") {
            $estadoCantidadAlerta.empty();
            $estadoCantidadAlerta.append(estado.Cantidad);
        }
        else if (estado.TipoEstado == "Criticos") {
            $estadoCantidadCriticos.empty();
            $estadoCantidadCriticos.append(estado.Cantidad);
        }
        else if (estado.TipoEstado == "Desconocidos") {
            $estadoCantidadDesconocidos.empty();
            $estadoCantidadDesconocidos.append(estado.Cantidad);
        }
        else if (estado.TipoEstado == "Todos") {
            $estadoCantidadTodos.empty();
            $estadoCantidadTodos.append(estado.Cantidad);
        }
    });
});



connection.on('actualizoVisorMensajes', function (mensajes) {

    // Obtener la URL actual
    var urlActual = window.location.href;

    if (urlActual.includes('IndexMonitoreo') == true) {

        $visorMensajesUl.empty();

        if (mensajes.length === 0) {
            $visorMensajesUl.append('<li>No hay mensajes recientes...</li>');
        }
        else {

            $.each(mensajes, function () {

                var mensaje = formatMensaje(this);

                $visorMensajesUl.append(liTemplateVisor.supplant(mensaje));
            });
        }
    }
    else
    {
        console.log("Metodo updateMonitoreoRed no se ejecuta. Fuera de IndexMonitoreo.")
    }
});





