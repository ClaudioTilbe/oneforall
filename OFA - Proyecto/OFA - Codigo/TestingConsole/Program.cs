
using EntidadesCompartidas;
using Logica;






Administrador adminLog = (Administrador)FabricaLogica.GetLogicaUsuario().Logueo("administrador", "administrador123");

List<Dispositivo> dispositivos = FabricaLogica.GetLogicaDispositivo().ListadoDispositivosXUsuario(adminLog);

Dispositivo dispositivoEncontrado = dispositivos.FirstOrDefault(d => d.IP == "192.168.5.20");



List<MensajeVisor> lista = FabricaLogica.GetLogicaMensajeVisor().ListarMensajeVisorXDispositivoUltimaH(dispositivoEncontrado, adminLog);



//Console.WriteLine("Hello, World!");
