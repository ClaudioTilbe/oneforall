

if (typeof enableScript !== 'undefined' && enableScript === false)
{
    console.log("Script Estandar.JS no se ejecutara en esta pagina");
}
else
{

    function formatEstado(estado) {

        return $.extend(estado, {

            TipoEstado: estado.item1,
            Cantidad: estado.item2.toString()
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



    function openUpdatePanel() {

        // Llamar al método del servidor
        connection.invoke("OpenPanelEstado").then(function () {
        }).catch(function (error) {
            console.error("Error al llamar al método del servidor: " + error);
        });
    }



    //window.addEventListener('beforeunload', function (event) {
    //    // Llama al método del hub para notificar que el usuario se está desconectando
    //    connection.invoke("ClosePanelEstado").catch(function (err) {
    //        console.error(err.toString());
    //    });

    //});



    //Inicio, se ejecuta luego de Cargar el DOM (Document Object Model)
    $(function () {

        //Variables para carga de Estado
        $estadoCantidadConectados = $('#cantidadConectados'),
            $estadoCantidadAlerta = $('#cantidadAlerta'),
            $estadoCantidadCriticos = $('#cantidadCriticos'),
            $estadoCantidadDesconocidos = $('#cantidadDesconocidos'),
            $estadoCantidadTodos = $('#cantidadTodos')
            ;
    });



    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/monitoreoredhub")
        .configureLogging(signalR.LogLevel.Information)
        .build();



    connection.start()
        .then(cargoPanelEstado)
        .then(openUpdatePanel)
        .catch(function (err) {
            return console.error(err.toString());
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

}