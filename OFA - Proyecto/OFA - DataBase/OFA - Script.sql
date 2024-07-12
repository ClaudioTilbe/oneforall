

--Creo carpeta SQLData sino aun no lo esta ------------------------------------------------------------

--Cambiar rutas estaticas si corresponde

-- Habilitar xp_cmdshell
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'xp_cmdshell', 1;
RECONFIGURE;

-- Crear la carpeta si no existe 
DECLARE @cmd NVARCHAR(4000);
SET @cmd = 'IF NOT EXIST "C:\SQLData" mkdir C:\SQLData';
EXEC xp_cmdshell @cmd;

-- Deshabilitar xp_cmdshell por seguridad
EXEC sp_configure 'xp_cmdshell', 0;
RECONFIGURE;
EXEC sp_configure 'show advanced options', 0;
RECONFIGURE;


use master
go

if exists(Select * FROM SysDataBases WHERE name='OFA')
BEGIN
	DROP DATABASE OFA
END
go


--creo la base de datos---------------------------------------------------------------------
CREATE DATABASE OFA
ON(
	NAME=OFA,
	FILENAME='C:\SQLData\OFA.mdf'
)
go




--pone en uso la bd-------------------------------------------------------------------------
USE OFA
go



Create Table Mail
(
	Correo varchar(100) Not null Primary Key Check (Correo like '%@%.%'),
	Contraseña varchar(30) Not null Check (LEN(Contraseña) >= 8),
	HostServidor varchar(50) Not null Check (LEN(HostServidor) >= 4),
    Puerto int Not null Check (Puerto > 0 AND Puerto <= 65535)
)
Go


Create Table Usuario
(
	UsuarioID varchar(20) not null Primary Key Check (LEN(UsuarioID) >= 4),
	Contraseña varchar(20) not null Check (LEN(Contraseña) >= 12),
	Correo varchar(100) not null Foreign Key References Mail(Correo)
)
Go


Create Table Administrador 
(
	UsuarioID varchar (20) not null Foreign Key References Usuario(UsuarioID) Check (LEN(UsuarioID) >= 4),
	Nombre varchar (50) not null Check (LEN(Nombre) >= 2),
	NumeroFuncionario int not null Check (NumeroFuncionario > 0 AND NumeroFuncionario <= 99999999),
	Cargo varchar (50) not null Check (LEN(Cargo) >= 4),
	Primary Key (UsuarioID)
)
Go


Create Table Operador 
(
	UsuarioID varchar(20) not null Foreign Key References Usuario(UsuarioID) Check (LEN(UsuarioID) >= 4),
	NumeroFuncionarioSupervisor int not null Check (NumeroFuncionarioSupervisor > 0 AND NumeroFuncionarioSupervisor <= 99999999),
	NombreSupervisor varchar(50) not null Check (LEN(NombreSupervisor) >= 2),
	Primary Key (UsuarioID)
)
Go


Create Table Sucursal
(
	NumeroSucursal int Not Null Primary Key Check (NumeroSucursal >= 0 AND NumeroSucursal <= 9999),
	Tipo varchar(50) not null Check (LEN(Tipo) >= 4),
	Departamento varchar(50) not null Check (LEN(Departamento) >= 2),
    Calle varchar(50) not null Check (LEN(Calle) >= 4),
    NumeroLocal int not null Check (NumeroLocal >= 0 AND NumeroLocal <= 99999999)
)
Go


Create Table SucursalXOperador
(
	UsuarioID varchar(20) not null Foreign Key References Usuario(UsuarioID),
	NumeroSucursal int not null Foreign Key References Sucursal(NumeroSucursal),
	Primary Key(UsuarioID, NumeroSucursal)
)
Go


Create Table Subred
(
	Rango varchar(11) Not Null Primary Key Check (Rango like '%.%.%'),
	NumeroSucursal int not null Foreign Key References Sucursal(NumeroSucursal)
)
Go


Create Table Dispositivo
(
    IP varchar(20) Not Null Primary Key Check (IP like '%.%.%.%'),
    Nombre varchar(50) not null Check (LEN(Nombre) >= 2),
	Tipo varchar(30) not null Check (LEN(Tipo) >= 2),
	Conectado bit not null,
	Accesible bit not null,
	Sector varchar(50) not null Check (LEN(Sector) >= 2),
	NumeroSucursal int not null Foreign Key References Sucursal(NumeroSucursal),
	Prioridad varchar(30) not null Check (Prioridad = 'alta' OR Prioridad = 'media' OR Prioridad = 'baja' OR Prioridad = 'indefinida'), --Alta, Media, Baja, Indefinida
	Permanencia bit not null, --Siempre Conectado = True o Puede Desconectarse = False
	UltimaConexion DateTime not null,
	UltimaNotificacion DateTime
)
Go


Create Table Grupo
(
	Codigo int Not Null Primary Key Identity(1,1),
	NombreGrupo varchar (30) not Null Check (LEN(NombreGrupo) >= 2),
	Descripcion varchar(100) not null Check (LEN(Descripcion) >= 10),
	UsuarioID varchar (20) not null Foreign Key References Usuario(UsuarioID)
)
Go


Create Table DispositivoEnGrupo 
(
	CodigoGrupo int Not Null Foreign Key References Grupo(Codigo),
	DispositivoIP varchar (20) not Null Foreign Key References Dispositivo(IP),
	Primary Key (codigoGrupo, DispositivoIP)
)
Go


Create Table Reporte
(
	Codigo int Not Null Primary Key Identity(1,1),
	Correo varchar(100) not null Foreign Key References Mail(Correo),
    DispositivoIP varchar(20) Not Null Foreign Key References Dispositivo(IP),
	Asunto varchar(50) Not Null Check (LEN(Asunto) >= 10),
	Destino varchar(100) Not Null Check (Destino like '%@%.%'),
	Mensaje varchar(500) not null Check (LEN(Mensaje) >= 10),
	
	CONSTRAINT UQ_Correo_DispositivoIP UNIQUE (Correo, DispositivoIP) --Correo y DispositivoIP Unicos juntos
)
Go


Create Table MensajeVisor
(
	Id int Not Null Primary Key Identity(1,1),
	DispositivoIP varchar(20) Not Null Foreign Key References Dispositivo(IP),
	FechaGenerado DateTime not null,
    Contenido varchar(200) not null Check (LEN(Contenido) >= 10),
    UsuarioID varchar(20) not null Foreign Key References Usuario(UsuarioID)
)
Go


Create Table EstadoMotor
(
	IDEstado int Not Null Primary Key Identity(1,1),
	Activo bit Not Null,
	UltimaModificacion DateTime not null,
    UsuarioID varchar(20) not null Check (LEN(UsuarioID) >= 4)
)
Go


Create Table AnalisisRed 
(
	IdAnalisis int Not null Primary Key Identity(1,1),
	RangoSubred varchar(11) not null Check (RangoSubred like '%.%.%'),
	Razon varchar(200) not null Check (LEN(Razon) >= 5), 
	Estado varchar(20) not null Check (Estado = 'pendiente' OR Estado = 'cancelado' OR Estado = 'finalizado'), --Pendiente, Ejecutandose, Cancelado, Finalizado
	Prioridad varchar(20) Not null Check (Prioridad = 'alta' OR Prioridad = 'media' OR Prioridad = 'baja'), --Baja, Media, Alta
	NuevosDispositivos int,
	FechaGenerado DateTime not null,
	FechaFinalizado DateTime,
	UsuarioID varchar(20) not null Foreign Key References Administrador(UsuarioID)
)
Go


Create Table EscaneoPuertos 
(
	IdEscaneo int Not null Primary Key Identity(1,1),
	DispositivoIP varchar(20) Not Null Foreign Key References Dispositivo(IP),
	Razon varchar(200) not null check (LEN(Razon) >= 5),
	Estado varchar(20) not null Check (Estado = 'pendiente' OR Estado = 'ejecutandose' OR Estado = 'cancelado' OR Estado = 'finalizado'), --Pendiente, Ejecutandose, Cancelado, Finalizado
	Prioridad varchar(20) Not null Check (Prioridad = 'alta' OR Prioridad = 'media' OR Prioridad = 'baja'), --Baja, Media, Alta
	CadenaSalida varchar(3000),
	FechaGenerado DateTime not null,
	FechaFinalizado DateTime,
	UsuarioID varchar(20) not null Foreign Key References Administrador(UsuarioID)
)
Go


Create Table DiagramaRed 
(
	NumeroSucursal int not null Foreign Key References Sucursal(NumeroSucursal),
	Nombre varchar(50) not null Check (LEN(Nombre) >= 4),
	FechaSubida DateTime not null,
	DiagramaImagen VARBINARY(MAX) not null,
	Primary Key(NumeroSucursal)
)
Go


--Visor de Errores de la API (Motor)
Create Table MensajeMotor
(
	IdMensaje int Not null Primary Key Identity(1,1),
	Excepcion varchar(5000) Not null Check (LEN(Excepcion) >= 10),
	Mensaje varchar(300) Not null Check (LEN(Mensaje) >= 10),
	MetodoOrigen varchar(300) Not null Check (LEN(MetodoOrigen) >= 2),
	EstadoVariables varchar (1000) Not null Check (LEN(EstadoVariables) >= 5),
	FechaGenerado DateTime not null,
    Tipo varchar(30) not null Check (Tipo = 'informativo' OR Tipo = 'advertencia' OR Tipo = 'critico' OR Tipo = 'no identificado') --Informativo, Advertencia, Critico, No identificado (Exception)
)
Go





--------------------------------------Creo roles-----------------------------------------------

--Todos (Rol para Administrador)
CREATE ROLE SPAdministrador
GO  

--Solo algunos SP (Rol para Operador)
CREATE ROLE SPOperador
GO  

--Creo Usuario APPPOOL para uso publico
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS 
GO

USE OFA
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO



-------------------------------- Procedimientos ---------------------------------------------------

----------------------------- Logueo de Usuarios --------------------------------------------------


Create PROCEDURE LogueoAdministrador @UsuID VARCHAR(20), @Contraseña VARCHAR(20)
AS
BEGIN
    SELECT U.UsuarioID, U.Contraseña, U.Correo, Adm.Nombre, Adm.NumeroFuncionario, Adm.Cargo, M.Contraseña AS ContraseñaMail, M.HostServidor, M.Puerto
    FROM Usuario U
    INNER JOIN Administrador Adm ON U.UsuarioID = Adm.UsuarioID
    INNER JOIN Mail M ON U.Correo = M.Correo
    WHERE Adm.UsuarioID = @UsuID AND U.Contraseña = @Contraseña;
END
GO


Create PROCEDURE LogueoOperador @UsuID VARCHAR(20), @Contraseña VARCHAR(20)
AS
BEGIN
    SELECT U.UsuarioID, U.Contraseña, U.Correo, Ope.NumeroFuncionarioSupervisor, Ope.NombreSupervisor, M.Contraseña AS ContraseñaMail, M.HostServidor, M.Puerto
    FROM Usuario U
    INNER JOIN Operador Ope ON U.UsuarioID = Ope.UsuarioID
    INNER JOIN Mail M ON U.Correo = M.Correo
    WHERE Ope.UsuarioID = @UsuID AND U.Contraseña = @Contraseña;
END
GO




----------------------------- Administrador -----------------------------------------------------------

--Solo puede cambiar su password en caso que el usuario logueado sea el mismo que pretende modificar

Create Procedure AdministradorAlta @UsuID varchar(20), @Contraseña varchar(20), 
@Correo varchar(100), @Nombre varchar(50), @NumeroFuncionario int, @Cargo varchar(50) As
Begin
        Declare @VarSentencia varchar(200)

	    If Exists (select * from Usuario where UsuarioID = @UsuID)
			return -1
		
		If Exists (select * from Mail where Correo = @Correo)
	        return -2

		Begin Transaction

		--Agrego creacion de Mail
		Insert Mail (Correo, Contraseña, HostServidor, Puerto)
		values (@Correo, 'DefaultPassword', 'outlook.live.com', 25)

		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -3
		End


		--Creo el usuario de logueo (Usuario SQL) -------------------------------------------------------------
		
		Set @VarSentencia = 'CREATE LOGIN [' +  @UsuID + '] WITH PASSWORD = ' + QUOTENAME(@Contraseña, '''')
		Exec (@VarSentencia)

		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -4
		End



		--Creo el usuario de BD -------------------------------------------------------------
		Set @VarSentencia = 'Create User [' +  @UsuID + '] From Login [' + @UsuID + ']'
		Exec (@VarSentencia)

			if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -5
		End
		
	
		Insert Usuario (UsuarioID, Contraseña, Correo) Values (@UsuID, @Contraseña , @Correo) 
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -6
		End

		Insert Administrador(UsuarioID, Nombre, NumeroFuncionario, Cargo) 
		Values(@UsuID, @Nombre, @NumeroFuncionario, @Cargo)
		if (@@ERROR<>0)
		Begin
			RollBack Transaction
			return -7
		End

		Commit Transaction

		--Doy Rol -------------------------------------------------------------
		Exec sp_addsrvrolemember @loginame = @UsuID, @rolename='securityAdmin'	
		Exec sp_addrolemember @rolename = 'SPAdministrador', @membername = @UsuID
		
		if (@@ERROR<>0)
		Begin
			return -8
		End
		
		return 1
	
End
go




Create Procedure AdministradorBaja @UsuID varchar(20) As
Begin
        Declare @VarSentencia varchar(200)

        If Not Exists (select * from Administrador where UsuarioID = @UsuID)
			return -1

		Begin Transaction

			Set @VarSentencia = 'Drop Login [' + @UsuID + ']'
			exec (@VarSentencia)

			if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -2
			End

	     
			Set @VarSentencia = 'Drop User [' + @UsuID + ']'
			exec (@VarSentencia)

			if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -3
			End


			Delete From MensajeVisor Where UsuarioID = @UsuID
			if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -4
			End
			
	        
			Delete From AnalisisRed Where UsuarioID = @UsuID
			if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -5
			End
			
			Delete From EscaneoPuertos Where UsuarioID = @UsuID
			if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -6
			End
			
			
			DELETE FROM DispositivoEnGrupo WHERE CodigoGrupo IN (
            SELECT Codigo FROM Grupo WHERE UsuarioID = @UsuID)
            if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -9
			End
		
			
			Delete From Grupo Where UsuarioID = @UsuID
			if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -10
			End
			
			
			Delete From Administrador Where UsuarioID = @UsuID
			if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -7
			End


			Delete From Usuario Where UsuarioID = @UsuID
			if (@@ERROR <> 0)
			Begin
				RollBack Transaction
				return -8
			End

		Commit Transaction
		return 1
End
go




Create Procedure AdministradorModificar @UsuarioID varchar(20), @Contraseña varchar(20),
@Correo varchar(100), @Nombre varchar(30), @NumeroFuncionario int, @Cargo varchar(50) As
Begin
        Declare @VarSentencia varchar(200)
 	    
		if Not Exists(Select * From Administrador Where UsuarioID = @UsuarioID)
			return -1
			
		if (CURRENT_USER != @UsuarioID) --Solo el Propio Usuario puede modificar sus Datos
			Begin
			
			     Update Administrador Set Nombre = @Nombre, NumeroFuncionario = @NumeroFuncionario,
				 Cargo = @Cargo Where UsuarioID = @UsuarioID
		
				 if (@@ERROR <> 0)
				 Begin
					RollBack Transaction
					return -2
	   			 End
							
				 return 1
			End

		Else
		Begin
			Begin Transaction
		
				 --Cambio pass Usuario SQL
	   			 Set @VarSentencia = 'Alter LOGIN [' + @UsuarioID + '] WITH PASSWORD = ' + QUOTENAME(@Contraseña, '''')
				 exec (@VarSentencia)
				 if (@@ERROR <> 0)
				 Begin
				      RollBack Transaction
					  return -3
				 End
		
		
				 Update Usuario Set Contraseña = @Contraseña
				 Where UsuarioID = @UsuarioID
		
				 if (@@ERROR <> 0)
				 Begin
					 RollBack Transaction
				 	 return -4
				 End
				
				
				 Update Administrador Set Nombre = @Nombre, NumeroFuncionario = @NumeroFuncionario,
				 Cargo = @Cargo Where UsuarioID = @UsuarioID
		
				 if (@@ERROR <> 0)
				 Begin
					RollBack Transaction
					return -5
			   	 End
					
			Commit Transaction
			return 1	
		end			
End
go




Create Procedure BuscarAdministrador @UsuID varchar(20) As
Begin
	Select * 
	From Usuario U Inner Join Administrador A on U.UsuarioID = A.UsuarioID 
	Where A.UsuarioID = @UsuID
End
go




Create Procedure ListadoAdministradores As
Begin
	Select * 
	From Usuario U Inner Join Administrador A on U.UsuarioID = A.UsuarioID
End
go



----------------------------- Operador -----------------------------------------------------------

Create Procedure OperadorAlta @UsuID varchar(20), @Contraseña varchar(20), @Correo varchar(100),
@NumeroFuncionarioSupervisor int, @NombreSupervisor varchar(50) As
Begin
        Declare @VarSentencia varchar(200)

	    If Exists (select * from Usuario where UsuarioID = @UsuID)
			return -1
		
		If Exists (select * From Mail Where Correo = @Correo)
			return -2

		Begin Transaction

		--Creo el Mail que va a necesitar el usuario
		Insert Mail (Correo, Contraseña, HostServidor, Puerto)
		values (@Correo, 'DefaultPassword', 'outlook.live.com', 25)

		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -3
		End


		--Creo el usuario de logueo (Usuario SQL) -------------------------------------------------------------
		
		Set @VarSentencia = 'CREATE LOGIN [' +  @UsuID + '] WITH PASSWORD = ' + QUOTENAME(@Contraseña, '''')
		Exec (@VarSentencia)

		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -4
		End



		--Creo el usuario de BD -------------------------------------------------------------
		Set @VarSentencia = 'Create User [' +  @UsuID + '] From Login [' + @UsuID + ']'
		Exec (@VarSentencia)

			if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -5
		End
		
	
		Insert Usuario (UsuarioID, Contraseña, Correo) Values (@UsuID, @Contraseña , @Correo)
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -6
		End
	
		Insert Operador(UsuarioID, NumeroFuncionarioSupervisor, NombreSupervisor)
		Values(@UsuID, @NumeroFuncionarioSupervisor, @NombreSupervisor)
		
		if (@@ERROR<>0)
		Begin
			RollBack Transaction
			return -7
		End

		Commit Transaction

		Exec sp_addrolemember @rolename = 'SPOperador', @membername = @UsuID
		
		
		if (@@ERROR<>0)
		Begin
			return -8
		End
		
		return 1
End
go




Create Procedure OperadorBaja @UsuID varchar(20) As
Begin
       Declare @VarSentencia varchar(200)

        If Not Exists (select * from Operador where UsuarioID = @UsuID)
			return -1

        --Revisar si es necesario verificaciones pero creo que en Empleado no
		Begin Transaction

	    Set @VarSentencia = 'Drop Login [' + @UsuID + ']'
	    exec (@VarSentencia)

		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -2
		End

     
	    Set @VarSentencia = 'Drop User [' + @UsuID + ']'
	    exec (@VarSentencia)

		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -3
		End

		
		Delete From SucursalXOperador Where UsuarioID = @UsuID
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -4
		End
        
		Delete From Operador Where UsuarioID = @UsuID
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -5
		End


		Delete From Usuario Where UsuarioID = @UsuID
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -6
		End

		Commit Transaction
		return 1
End
go




Create Procedure OperadorModificar @UsuarioID varchar(20), @Contraseña varchar(20), @Correo varchar(100),
@NumeroFuncionarioSupervisor int, @NombreSupervisor varchar(50) As
Begin
        Declare @VarSentencia varchar(200)
 	    
		if Not Exists(Select * From Operador Where UsuarioID = @UsuarioID)
			return -1
			
		Begin Transaction
		
				 --Cambio pass Usuario SQL
	   			 Set @VarSentencia = 'Alter LOGIN [' + @UsuarioID + '] WITH PASSWORD = ' + QUOTENAME(@Contraseña, '''')
				 exec (@VarSentencia)
				 if (@@ERROR <> 0)
				 Begin
				      RollBack Transaction
					  return -2
				 End
		
		
				 Update Usuario Set Contraseña = @Contraseña, Correo = @Correo
				 Where UsuarioID = @UsuarioID
		
				 if (@@ERROR <> 0)
				 Begin
					 RollBack Transaction
				 	 return -3
				 End
				
				
				 Update Operador Set NumeroFuncionarioSupervisor = @NumeroFuncionarioSupervisor, NombreSupervisor = @NombreSupervisor
				 Where UsuarioID = @UsuarioID
		
				 if (@@ERROR <> 0)
				 Begin
					RollBack Transaction
					return -4
			   	 End
					
        Commit Transaction
		return 1	
End
go




Create Procedure BuscarOperador @UsuID varchar(20) As
Begin
	Select * 
	From Usuario U Inner Join Operador O on U.UsuarioID = O.UsuarioID 
	Where O.UsuarioID = @UsuID
End
go




Create Procedure ListadoOperadores As
Begin
	Select * 
	From Usuario U Inner Join Operador O on U.UsuarioID = O.UsuarioID
End
go



-------------------------------- Dispositivos ------------------------------------------
	
Create Procedure DispositivoAlta @IP varchar(20), @Nombre varchar(50), @Tipo varchar(30), @Conectado bit,
@Accesible bit, @Sector varchar(50), @NroSucursal int, @Prioridad varchar(30), @Permanencia bit,
@UltimaConexion DateTime, @UltimaNotificacion DateTime
As
Begin

	   If Exists (select * from Dispositivo where IP = @IP)
	        return -1
				
		Insert Dispositivo (IP, Nombre, Tipo, Conectado, Accesible, Sector, NumeroSucursal, Prioridad,
		 Permanencia, UltimaConexion, UltimaNotificacion) 
		 
		values (@IP, @Nombre, @Tipo, @Conectado, @Accesible, @Sector, @NroSucursal, @Prioridad,
		 @Permanencia, @UltimaConexion, @UltimaNotificacion)

		If @@ERROR = 0
			return 1
		else
			return -2
End
go





Create Procedure DispositivoBaja @IP varchar(20) As
Begin

		if not Exists (Select * From Dispositivo Where IP = @IP)
			return -1
			
		Begin Transaction
		
				Delete From MensajeVisor Where DispositivoIP = @IP
				if (@@ERROR <> 0)
				Begin
					RollBack Transaction
					return -2
				End
				
				Delete From EscaneoPuertos Where DispositivoIP = @IP
				if (@@ERROR <> 0)
				Begin
					RollBack Transaction
					return -3
				End
				
				Delete From Reporte Where DispositivoIP = @IP 
				if (@@ERROR <> 0)
				Begin
					RollBack Transaction
					return -4
				End
				
				Delete From DispositivoEnGrupo Where DispositivoIP = @IP 
				if (@@ERROR <> 0)
				Begin
					RollBack Transaction
					return -5
				End
				
				Delete From Dispositivo Where IP = @IP

				if (@@ERROR <> 0)
				Begin
					RollBack Transaction
					return -6
				End

		Commit Transaction
		return 1
End
go




Create Procedure DispositivoModificar @IP varchar(20), @Nombre varchar(50), @Tipo varchar(30), @Conectado bit,
@Accesible bit, @Sector varchar(50), @NroSucursal int, @Prioridad varchar(30), @Permanencia bit
As
Begin
		if Not Exists (Select * From Dispositivo Where IP = @IP)
			return -1
			
		if Not Exists (Select * From Sucursal Where NumeroSucursal = @NroSucursal)
			return -2
			
		Update Dispositivo Set Nombre = @Nombre, Tipo = @Tipo, Conectado = @Conectado, Accesible = @Accesible,
		Sector = @Sector, NumeroSucursal = @NroSucursal, Prioridad = @Prioridad, Permanencia = @Permanencia
		Where IP = @IP

		If (@@ERROR = 0)
			return 1
		Else
			return -3		
End
go




Create Procedure DispositivoActualizarEstadoConexion @IP varchar(20), @Conectado bit,
@UltimaConexion DateTime
As
Begin
		if Not Exists (Select * From Dispositivo Where IP = @IP)
			return -1
			
		Update Dispositivo Set Conectado = @Conectado, UltimaConexion = @UltimaConexion
		Where IP = @IP

		If (@@ERROR = 0)
			return 1
		Else
			return -2		
End
go



Create Procedure DispositivoActualizarEstadoNotificacion @IP varchar(20), @Conectado bit, 
@UltimaNotificacion DateTime
As
Begin
		if Not Exists (Select * From Dispositivo Where IP = @IP)
			return -1
			
		Update Dispositivo Set Conectado = @Conectado, UltimaNotificacion = @UltimaNotificacion
		Where IP = @IP

		If (@@ERROR = 0)
			return 1
		Else
			return -2		
End
go




Create Procedure DispositivoBuscar @IP varchar(20) As
Begin 
	Select * 
	From Dispositivo
	Where IP = @IP
End
GO



Create Procedure ListadoDispositivos As
Begin 
	Select * 
	From Dispositivo
End
GO



Create Procedure ListadoDispositivosXSucursal @NumeroSucursal int As 
Begin 
	Select * 
	From Dispositivo
	Where NumeroSucursal = @NumeroSucursal
End
GO




Create Procedure ListadoDispositivosXSubred @Subred varchar(11) As
Begin
      -- Seleccionar los dispositivos que coincidan con la subred
    SELECT *
    FROM Dispositivo
    WHERE IP LIKE '%' + @Subred + '%'

End
go



Create Procedure ListadoDispositivosAccesibles @Accesible bit As
Begin 
	Select * 
	From Dispositivo
	Where Accesible = @Accesible
End
GO




Create Procedure ListadoDispositivosXGrupo @CodigoGrupo int As
Begin 
	Select D.IP, D.Nombre, D.NumeroSucursal, D.Sector, D.Tipo, D.Conectado, D.Accesible,
	       D.Prioridad, D.Permanencia, D.Ultimaconexion, D.UltimaNotificacion
	       
	From DispositivoEnGrupo DG Inner Join Dispositivo D on DG.DispositivoIP = D.IP
	Where DG.CodigoGrupo = @CodigoGrupo
End
GO




-------------------------------- Sucursal ---------------------------------

Create Procedure SucursalAlta @NroSucursal int, @Tipo varchar(50), @Departamento varchar(50),
@Calle varchar(50), @NumeroLocal int 
As
Begin

	   If Exists (select * from Sucursal where NumeroSucursal = @NroSucursal)
	        return -1
				
		Insert Sucursal (NumeroSucursal, Tipo, Departamento, Calle, NumeroLocal)
		values (@NroSucursal, @Tipo, @Departamento, @Calle, @NumeroLocal)

		If @@ERROR = 0
			return 1
		else
			return -2
End
go




Create Procedure SucursalBaja @NroSucursal int As
Begin
	if not Exists (Select * From Sucursal Where NumeroSucursal = @NroSucursal)
		return -1

    Begin Transaction

		Delete From DiagramaRed Where NumeroSucursal = @NroSucursal
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -2
		End
		
		
		Delete From DispositivoEnGrupo
		WHERE DispositivoIP IN (SELECT IP From Dispositivo Where NumeroSucursal = @NroSucursal)
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -3
		End
		
		
		Delete From Reporte
		WHERE DispositivoIP IN (SELECT IP From Dispositivo Where NumeroSucursal = @NroSucursal)
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -4
		End
		
		
	
		Delete From EscaneoPuertos
		WHERE DispositivoIP IN (SELECT IP From Dispositivo Where NumeroSucursal = @NroSucursal)
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -5
		End
		
		

		Delete From MensajeVisor
		WHERE DispositivoIP IN (SELECT IP From Dispositivo Where NumeroSucursal = @NroSucursal)
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -6
		End
		
		
		Delete From Dispositivo Where NumeroSucursal = @NroSucursal
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -7
		End
		
		
		Delete From SucursalXOperador Where NumeroSucursal = @NroSucursal
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -8
		End
		
		
		Delete From Sucursal Where NumeroSucursal = @NroSucursal
		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -9
		End
		
		Commit Transaction
		return 1
End
go




Create Procedure SucursalModificar @NroSucursal int, @Tipo varchar(50), @Departamento varchar(50),
@Calle varchar(50), @NumeroLocal int
As
Begin
		if Not Exists(Select * From Sucursal Where NumeroSucursal = @NroSucursal)
			return -1
			
		Update Sucursal Set Tipo = @Tipo, Departamento = @Departamento, Calle = @Calle, NumeroLocal = @NumeroLocal
		Where NumeroSucursal = @NroSucursal

		If (@@ERROR = 0)
			return 1
		Else
			return -2		
End
go





Create Procedure AsignarSucursalAOperador @NroSucursal int, @UsuarioID varchar(20)
As
Begin
		If not exists (select * from Sucursal where NumeroSucursal = @NroSucursal)
	        return -1
	    If exists (select * from SucursalXOperador where NumeroSucursal = @NroSucursal and UsuarioID = @UsuarioID)
	        return -2
				
		Insert SucursalXOperador (UsuarioID, NumeroSucursal)
		values (@UsuarioID, @NroSucursal)

		If @@ERROR = 0
			return 1
		else
			return -3
End
go




Create Procedure BajaSucursalesOperador @UsuarioID varchar(20) As
Begin
 
		if not Exists (Select * From SucursalXOperador Where UsuarioID = @UsuarioID)
			return -1

		Delete From SucursalXOperador Where UsuarioID = @UsuarioID

		If @@ERROR = 0
				return 1
		else
				return -2
End
go





Create Procedure SucursalBuscar @NroSucursal int As
Begin 
	Select * 
	From Sucursal
	Where NumeroSucursal = @NroSucursal
End
GO





Create Procedure SucursalBuscarXRango @Rango varchar(11) As
Begin
    Select S.NumeroSucursal, S.Tipo, S.Departamento, S.Calle, S.NumeroLocal
    From Sucursal S Inner Join Subred SR
    On S.NumeroSucursal = SR.NumeroSucursal
    Where SR.Rango = @Rango
End
Go



Create Procedure ListadoSucursales As
Begin 
	Select * 
	From Sucursal
End
GO



Create Procedure ListadoSucursalesXUsuario @UsuarioID varchar(20) As
Begin 
	Select S.NumeroSucursal, S.Calle, S.Departamento, S.NumeroLocal, S.Tipo
	From SucursalXOperador SO Inner Join Sucursal S on SO.NumeroSucursal = S.NumeroSucursal
	Where SO.UsuarioID = @UsuarioID
End
GO




-------------------------------- Subred ---------------------------------

Create Procedure SubredAlta @NumeroSucursal int, @Rango varchar(11)
As
Begin

	   If Not Exists (select * from Sucursal where NumeroSucursal = @NumeroSucursal)
	        return -1
	        
	   If Exists (select * from Subred where Rango = @Rango)
	        return -2
	        
				
		Insert Subred (NumeroSucursal, Rango)
		values (@NumeroSucursal, @Rango)

		If @@ERROR = 0
			return 1
		else
			return -3
End
go




Create Procedure SubredBaja @NumeroSucursal int, @Rango varchar(11) As
Begin
 
		if not Exists (Select * From Subred Where Rango = @Rango and NumeroSucursal = @NumeroSucursal)
			return -1
		
		Delete From Subred
		Where Rango = @Rango and NumeroSucursal = @NumeroSucursal

		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -3
		End					
End
go





Create Procedure SubredBajaXSucursal @NumeroSucursal int As
Begin
		if not Exists (Select * From Sucursal Where NumeroSucursal = @NumeroSucursal)
			return -1
		
		Delete From Subred
		Where NumeroSucursal = @NumeroSucursal

		if (@@ERROR <> 0)
		Begin
			RollBack Transaction
			return -2
		End	
End
go



Create Procedure SubredBuscar @Rango varchar(11), @NumeroSucursal int As
Begin 
	Select * 
	From Subred
	Where Rango = @Rango and NumeroSucursal = @NumeroSucursal
End
GO




Create Procedure ListadoSubredXSucursal @NumeroSucursal int As 
Begin 
	Select * 
	From Subred
	Where NumeroSucursal = @NumeroSucursal
End
GO



Create PROCEDURE ListadoSubredes
AS
BEGIN
    Select * 
    From Subred
END
go


----------------------------------- Grupos ------------------------------------------------

Create Procedure GrupoAlta @NombreGrupo varchar(30), @Descripcion varchar(100), @UsuarioID varchar(20),
@Codigo int output
As
Begin
		if not Exists(Select * From Usuario Where UsuarioID = @UsuarioID)
			return -1	
				
		Insert Grupo (NombreGrupo, Descripcion, UsuarioID) values (@NombreGrupo, @Descripcion, @UsuarioID)

		SET @Codigo = SCOPE_IDENTITY()
		
		If @@ERROR = 0
			return 1
		else 
			return -2
End
go



Create Procedure GrupoBaja @Codigo int As
Begin
     
	    if not Exists (Select * From Grupo Where Codigo = @Codigo)
			return -1
			
		Delete From Grupo Where Codigo = @Codigo

		If @@ERROR = 0
				return 1
		else
				return -2
End
go



Create Procedure GrupoModificar @Codigo int, @NombreGrupo varchar(30), @Descripcion varchar(100), 
@UsuarioID varchar(20) As
Begin
		if Not Exists(Select * From Grupo Where Codigo = @Codigo)
			return -1
			
		Update Grupo Set NombreGrupo = @NombreGrupo, Descripcion = @Descripcion Where Codigo = @Codigo

		If (@@ERROR = 0)
			return 1
		Else
			return -2	
End
go




Create Procedure ListarGruposXUsuario @UsuarioID varchar(20) As
Begin 
	Select * 
	From Grupo
	Where UsuarioID = @UsuarioID
End
GO




------------------------------------------------ Grupo Asignado -----------------------------------------------

Create Procedure AsignoDispositivoAGrupo @CodigoGrupo int, @DispositivoIP varchar(20) As
Begin
		if not Exists (Select * From Dispositivo Where IP = @DispositivoIP) 
		    return -1
		if not Exists (Select * From Grupo Where Codigo = @CodigoGrupo)
		    return -2
		if Exists (select * from DispositivoEnGrupo where CodigoGrupo = @CodigoGrupo and DispositivoIP = @DispositivoIP)
		    return -3

		
		Insert DispositivoEnGrupo(CodigoGrupo, DispositivoIP) values (@CodigoGrupo, @DispositivoIP)

		If @@ERROR = 0
			return 1
		else
			return -4
End
go



--Quito individual
Create Procedure QuitoDispositivoEnGrupo @CodigoGrupo int, @DispositivoIP varchar (20) As
Begin
     
		if not Exists (Select * From DispositivoEnGrupo
					   Where CodigoGrupo = @CodigoGrupo and DispositivoIP = @DispositivoIP)
			return -1
			
		Delete From DispositivoEnGrupo Where CodigoGrupo = @CodigoGrupo and DispositivoIP = @DispositivoIP

		If @@ERROR = 0
				return 1
		else
				return -2
End
go



--Quito Todos
Create Procedure QuitoDispositivosEnGrupo @Codigo int As
Begin
		--Debe existir un Grupo con ese Codigo y el usuario Logeado debe ser quien creo ese grupo
		if not Exists (Select * From Grupo Where Codigo = @Codigo) 
			return -1
				
		Delete From DispositivoEnGrupo Where CodigoGrupo = @Codigo

		If @@ERROR = 0
				return 1
		else
				return -2
End
go


Create Procedure DispositivoEnGrupoBuscar @CodigoGrupo int, @DispositivoIP varchar (20) As
Begin 
	Select * 
	From DispositivoEnGrupo
	Where CodigoGrupo = @CodigoGrupo and DispositivoIP = @DispositivoIP
End
GO



------------------------------------------------ Mail ---------------------------------------------------------

Create Procedure MailAlta @Correo varchar(100), @Contraseña varchar(20),
 @HostServidor varchar(50), @Puerto int
As
Begin

	   If Exists (select * from Mail where Correo = @Correo)
	        return -1
				
		Insert Mail (Correo, Contraseña, HostServidor, Puerto)
		values (@Correo, @Contraseña, @HostServidor, @Puerto)

		If @@ERROR = 0
			return 1
		else
			return -2
End
go




Create Procedure MailBaja @Correo varchar(100) As 
Begin
        If Not Exists (select * from Mail where Correo = @Correo)
			return -1

		Begin Transaction
		
				Delete From Reporte Where Correo = @Correo
				if (@@ERROR <> 0)
				Begin
					RollBack Transaction
					return -2
				End
				
				
				Delete From Mail Where Correo = @Correo
				if (@@ERROR <> 0)
				Begin
					RollBack Transaction
					return -3
				End

		Commit Transaction
		return 1
End
go




Create Procedure MailModificar @Correo varchar(100), @Contraseña varchar(20),
 @HostServidor varchar(50), @Puerto int
As
Begin
		if Not Exists (Select * From Mail Where Correo = @Correo)
			return -1
			
		Update Mail Set Contraseña = @Contraseña, HostServidor = @HostServidor, Puerto = @Puerto 
		Where Correo = @Correo

		If (@@ERROR = 0)
			return 1
		Else
			return -2		
End
go




Create Procedure MailBuscar @Correo varchar(100) As
Begin 
	Select * 
	From Mail
	Where Correo = @Correo
End
GO




Create Procedure ListadoMails As
Begin 
	Select * 
	From Mail
End
GO

 
 
------------------------------------------------ Reporte ---------------------------------------------------------

Create Procedure ReporteAlta @Correo varchar(100), @DispositivoIP varchar(20), @Asunto varchar(30), 
@Destino varchar(100), @Mensaje varchar(500)
As
Begin

	   If Exists (select * from Reporte where Correo = @Correo and DispositivoIP = @DispositivoIP)
	        return -1
	   If not Exists (Select * From Mail where Correo = @Correo)
			return -2
	   If not Exists (Select * From Dispositivo where IP = @DispositivoIP)
			return -3
				
		Insert Reporte (Correo, DispositivoIP, Asunto, Destino, Mensaje)
		values (@Correo, @DispositivoIP, @Asunto, @Destino, @Mensaje)

		If @@ERROR = 0
			return 1
		else
			return -4
End
go



Create Procedure ReporteModificar @Codigo int,@Correo varchar(100), @DispositivoIP varchar(20), @Asunto varchar(30), 
@Destino varchar(100), @Mensaje varchar(500) 
As
Begin
		if not exists (Select * From Reporte Where Codigo = @Codigo)
			return -1
		if not exists (Select * From Dispositivo Where IP = @DispositivoIP)
			return -2
		If not exists (Select * From Mail where Correo = @Correo)
			return -3

			
		Update Reporte Set Correo = @Correo, DispositivoIP = @DispositivoIP, Asunto = @Asunto,
		Destino = @Destino, Mensaje = @Mensaje
		Where Codigo = @Codigo

		If (@@ERROR = 0)
			return 1
		Else
			return -4	
End
go




Create Procedure ReporteBaja @Codigo int As
Begin
	    if not Exists (Select * From Reporte Where Codigo = @Codigo)
			return -1
			
		Delete From Reporte Where Codigo = @Codigo

		If @@ERROR = 0
				return 1
		else
				return -2
End
go



Create Procedure ReporteBuscar @Codigo int As
Begin 
	Select * 
	From Reporte
	Where Codigo = @Codigo
End
GO





Create Procedure ListadoReportesXCorreo @Correo varchar(100) As
Begin 
	Select * 
	From Reporte
	Where Correo = @Correo
End
GO




Create Procedure ListadoReportesXIP @IP varchar(20) As
Begin 
	Select * 
	From Reporte
	Where DispositivoIP = @IP
End
GO




Create Procedure ListadoReportesTodos As
Begin 
	Select * 
	From Reporte
End
GO



------------------------------------------------ MensajeVisor -------------------------------------------------------
    
Create Procedure MensajeVisorAlta @DispositivoIP varchar(20), @FechaGenerado DateTime, @Contenido varchar(200), 
@UsuarioID varchar(20)
As
Begin

	   If Not Exists (select * from Dispositivo where IP = @DispositivoIP)
	        return -1
	        
	   If Not Exists (select * from Usuario where UsuarioID = @UsuarioID)
	        return -2
				
		Insert MensajeVisor (DispositivoIP, FechaGenerado, Contenido, UsuarioID)
		values (@DispositivoIP, @FechaGenerado, @Contenido, @UsuarioID)

		If @@ERROR = 0
			return 1
		else
			return -3
End
go
    



Create Procedure MensajeVisorBajaXDispositivo @DispositivoIP varchar(20) As
Begin 
	    if not Exists (Select * From MensajeVisor Where DispositivoIP = @DispositivoIP)
			return -1
			
		Delete From MensajeVisor Where DispositivoIP = @DispositivoIP

		If @@ERROR = 0
			return 1
		else
			return -2
End
go



Create Procedure MensajeVisorBajaXTodos As
Begin 
		Delete From MensajeVisor

		If @@ERROR = 0
				return 1
		else
				return -2
End
go




    

Create Procedure ListarMensajeVisorXDispositivo @DispositivoIP varchar(20) As
Begin 
	Select * 
	From MensajeVisor
	Where DispositivoIP = @DispositivoIP
End
GO 





Create Procedure ListarMensajeVisorXUsuario @DispositivoIP varchar(20), @UsuarioID varchar(20) As
Begin 
	Select * 
	From MensajeVisor
	Where UsuarioID = @UsuarioID
End
GO 



Create Procedure ListarMensajeVisorXDispositivoUltimaH @DispositivoIP varchar(20) As
Begin 
	Select * 
	From MensajeVisor
	Where DispositivoIP = @DispositivoIP  and FechaGenerado >= DATEADD(MINUTE, -60, GETDATE())
End
GO 
    



Create Procedure ListarMensajeVisorXUsuarioUltimaH @UsuarioID varchar(20) As
Begin 
	Select * 
	From MensajeVisor
	Where UsuarioID = @UsuarioID  and FechaGenerado >= DATEADD(MINUTE, -60, GETDATE())
End
GO 




Create Procedure MensajeVisorBaja As
Begin
		Delete From MensajeVisor
		If @@ERROR = 0
			return 1
		else
			return -2
End
go



------------------------------------------------ EstadoMotor -------------------------------------------------------
    

Create Procedure EstadoMotorAlta @Activo bit, @UltimaModificacion DateTime, @UsuarioID varchar(20)
As
Begin

	    If Not Exists (select * from Administrador where UsuarioID = @UsuarioID)
	        return -1
				
		Insert EstadoMotor(Activo, UltimaModificacion, UsuarioID)
		values (@Activo, @UltimaModificacion, @UsuarioID)

		If @@ERROR = 0
			return 1
		else
			return -2
End
go


    
    
Create Procedure BuscarUltimoEstadoMotor As
Begin 
	Select TOP 1 * 
	From EstadoMotor
	ORDER BY IDEstado DESC
End
GO 




-------------------------------------- MensajeMotor --------------------------------------

Create Procedure MensajeMotorAlta @Excepcion varchar(5000), @Mensaje varchar(300),
@MetodoOrigen varchar(300), @EstadoVariables varchar(1000), @FechaGenerado DateTime,
@Tipo Varchar(30)
As
Begin				
		Insert MensajeMotor (Excepcion, Mensaje, MetodoOrigen, EstadoVariables, FechaGenerado, Tipo)
		values (@Excepcion, @Mensaje, @MetodoOrigen, @EstadoVariables, @FechaGenerado, @Tipo)

		If @@ERROR = 0
			return 1
		else
			return -1
End
Go




Create Procedure MensajeMotorBaja @IdMensaje int As
Begin
	    if Not Exists (Select * From MensajeMotor Where IdMensaje = @IdMensaje)
			return -1
			
		Delete From MensajeMotor Where IdMensaje = @IdMensaje

		If (@@ERROR = 0)
			return 1
		Else
			return -2	
End
go




Create Procedure MensajeMotorListarTodos
As
Begin 
	Select * 
	From MensajeMotor
End
GO





-------------------------------------- DiagramaRed --------------------------------------

Create Procedure DiagramaRedAlta @NumeroSucursal int, @Nombre NVARCHAR(50),
@DiagramaImagen VARBINARY(MAX), @FechaSubida DateTime
As
Begin
		If not Exists (Select * From Sucursal where NumeroSucursal = @NumeroSucursal)
			return -1
			
		If Exists (Select * From DiagramaRed where NumeroSucursal = @NumeroSucursal)
			return -2

    	Insert DiagramaRed (NumeroSucursal, Nombre, FechaSubida, DiagramaImagen)
		values (@NumeroSucursal, @Nombre, @FechaSubida, @DiagramaImagen)
		
		If @@ERROR = 0
			return 1
		else
			return -3
End
Go



Create Procedure DiagramaRedBaja @NumeroSucursal int As
Begin
		if not Exists (Select * From DiagramaRed Where NumeroSucursal = @NumeroSucursal)
			return -1

		Delete From DiagramaRed Where NumeroSucursal = @NumeroSucursal

		If @@ERROR = 0
				return 1
		else
				return -2
End
go



Create Procedure DiagramaRedBuscar @NumeroSucursal int As
Begin 
	Select * 
	From DiagramaRed
	Where NumeroSucursal = @NumeroSucursal
End
GO



Create Procedure ListarDiagramaRed As
Begin 
	Select * 
	From DiagramaRed
End
GO




-------------------------------------- AnalisisRed --------------------------------------

Create Procedure AnalisisRedAlta @RangoSubred varchar(11), @Razon varchar(200), @Estado varchar(20), 
@Prioridad varchar(20), @NuevosDispositivos int, @UsuarioID Varchar(20), @FechaGenerado DateTime
As
Begin
	   If not Exists (Select * From Administrador where UsuarioID = @UsuarioID)
			return -1
			
	   If not Exists (Select * From Subred where Rango = @RangoSubred)
			return -2
				
		Insert AnalisisRed (RangoSubred, Razon, Estado, Prioridad, NuevosDispositivos, FechaGenerado, UsuarioID)
		values (@RangoSubred, @Razon, @Estado, @Prioridad, @NuevosDispositivos, @FechaGenerado, @UsuarioID)

		If @@ERROR = 0
			return 1
		else
			return -3
End
go




Create Procedure AnalisisRedActualizar @IdAnalisis int, @Estado varchar(20), @NuevosDispositivos int, 
@FechaFinalizado Datetime
As
Begin
		if Not Exists (Select * From AnalisisRed Where IdAnalisis = @IdAnalisis)
			return -1
		if Exists (Select * From AnalisisRed Where IdAnalisis = @IdAnalisis and Estado = 'cancelado')
			return -2
			
		Update AnalisisRed Set Estado = @Estado, NuevosDispositivos = @NuevosDispositivos,
							   FechaFinalizado = @FechaFinalizado
		Where IdAnalisis = @IdAnalisis

		If (@@ERROR = 0)
			return 1
		Else
			return -3		
End
go



Create Procedure AnalisisRedCancelar @IdAnalisis int, @FechaFinalizado DateTime As
Begin
	    if Not Exists (Select * From AnalisisRed Where IdAnalisis = @IdAnalisis)
			return -1
			
		Update AnalisisRed Set Estado = 'Cancelado', FechaFinalizado = @FechaFinalizado
		Where IdAnalisis = @IdAnalisis

		If (@@ERROR = 0)
			return 1
		Else
			return -2	
End
go





Create Procedure AnalisisBuscar @IdAnalisis int As
Begin 
	Select * 
	From AnalisisRed
	Where IdAnalisis = @IdAnalisis
End
GO





Create Procedure ListadoAnalisisRedTodos
As
Begin 
	Select * 
	From AnalisisRed
End
GO





Create Procedure ListadoAnalisisRedPendientes
As
Begin 
	Select * 
	From AnalisisRed
	Where Estado = 'Pendiente'
End
GO




-------------------------------------- EscaneoPuertos --------------------------------------

Create Procedure EscaneoPuertosAlta @DispositivoIP varchar(20), @Razon varchar(200), @Estado varchar(20), 
@Prioridad varchar(20), @CadenaSalida varchar(3000),@UsuarioID Varchar(20), @FechaGenerado DateTime
As
Begin
	    If not Exists (Select * From Administrador where UsuarioID = @UsuarioID)
			return -1
			
	    If not Exists (Select * From Dispositivo where IP = @DispositivoIP)
			return -2
				
		Insert EscaneoPuertos (DispositivoIP, Razon, Estado, Prioridad, FechaGenerado, CadenaSalida, UsuarioID)
		values (@DispositivoIP, @Razon, @Estado, @Prioridad, @FechaGenerado, @CadenaSalida, @UsuarioID)

		If @@ERROR = 0
			return 1
		else
			return -3
End
go




Create Procedure EscaneoPuertosActualizar @IdEscaneo int, @Estado varchar(20),
@CadenaSalida varchar(3000), @FechaFinalizado DateTime
As
Begin
		if Not Exists (Select * From EscaneoPuertos Where IdEscaneo = @IdEscaneo)
			return -1
			
		if Exists (Select * From EscaneoPuertos Where IdEscaneo = @IdEscaneo and Estado = 'cancelado')
			return -2
			
		Update EscaneoPuertos Set Estado = @Estado, CadenaSalida = @CadenaSalida,
							      FechaFinalizado = @FechaFinalizado
		Where IdEscaneo = @IdEscaneo

		If (@@ERROR = 0)
			return 1
		Else
			return -3		
End
go



Create Procedure EscaneoPuertosCancelar @IdEscaneo int, @FechaFinalizado DateTime As
Begin
	    if Not Exists (Select * From EscaneoPuertos Where IdEscaneo = @IdEscaneo)
			return -1
			
		Update EscaneoPuertos Set Estado = 'Cancelado', FechaFinalizado = @FechaFinalizado
		Where IdEscaneo = @IdEscaneo

		If (@@ERROR = 0)
			return 1
		Else
			return -2	
End
go



Create Procedure EscaneoPuertosBuscar @IdEscaneo int As
Begin 
	Select * 
	From EscaneoPuertos
	Where IdEscaneo = @IdEscaneo
End
GO




Create Procedure ListadoEscaneoPuertosTodos
As
Begin 
	Select * 
	From EscaneoPuertos
End
GO





Create Procedure ListadoEscaneoPuertosPendientes
As
Begin 
	Select * 
	From EscaneoPuertos
	Where Estado = 'Pendiente'
End
GO





    
------------------------------------------------ Asigno permisos ------------------------------------------------
--Permisos de ejecucion (Administrador)
GRANT EXEC TO SPAdministrador;
GO


--Permisos sobre SP (Para Operador)
GRANT EXEC ON ListadoSucursalesXUsuario TO SPOperador;
GRANT EXEC ON ListadoSubredXSucursal TO SPOperador;
GRANT EXEC ON ListadoDispositivos TO SPOperador;
GRANT EXEC ON SucursalBuscar TO SPOperador;
GRANT EXEC ON MailBuscar TO SPOperador;
GRANT EXEC ON ListadoDispositivosXGrupo TO SPOperador;
GRANT EXEC ON ListarGruposXUsuario TO SPOperador;
GRANT EXEC ON ListarMensajeVisorXDispositivo TO SPOperador;
GRANT EXEC ON ListarMensajeVisorXDispositivoUltimaH TO SPOperador;
GRANT EXEC ON ListadoDispositivosXSucursal TO SPOperador;
GRANT EXEC ON DispositivoBuscar TO SPOperador;
GRANT EXEC ON DispositivoModificar TO SPOperador;
GRANT EXEC ON DispositivoBaja TO SPOperador;
GRANT EXEC ON DispositivoAlta TO SPOperador;
GRANT EXEC ON MailModificar TO SPOperador;
GRANT EXEC ON BuscarOperador TO SPOperador;
GRANT EXEC ON ListarMensajeVisorXUsuarioUltimaH TO SPOperador;
GRANT EXEC ON ListadoReportesXCorreo TO SPOperador;
GRANT EXEC ON ReporteAlta TO SPOperador;
GRANT EXEC ON ReporteBaja TO SPOperador;
GRANT EXEC ON ListarDiagramaRed TO SPOperador;
GRANT EXEC ON GrupoAlta TO SPOperador;
GRANT EXEC ON AsignoDispositivoAGrupo TO SPOperador;
GRANT EXEC ON QuitoDispositivosEnGrupo TO SPOperador;
GRANT EXEC ON GrupoBaja TO SPOperador;
GRANT EXEC ON GrupoModificar TO SPOperador;
GRANT EXEC ON ReporteModificar TO SPOperador;
GRANT EXEC ON DiagramaRedBaja TO SPOperador;
GRANT EXEC ON DiagramaRedAlta TO SPOperador;
GO


GRANT EXEC ON LogueoAdministrador TO [IIS APPPOOL\DefaultAppPool]
GRANT EXEC ON LogueoOperador TO [IIS APPPOOL\DefaultAppPool]
GRANT EXEC ON MailBuscar TO [IIS APPPOOL\DefaultAppPool] --Utiliza este SP en el Logueo para traer el mail al Usuario
GO


GRANT CREATE SCHEMA TO SPAdministrador
GRANT ALTER ANY USER TO  SPAdministrador
GRANT ALTER ANY ROLE TO  SPAdministrador
Go




--------------------------------------------------------------------------------------------------------
------------------------------- Insercion de Datos (Test 1 - Datos) ------------------------------------
--------------------------------------------------------------------------------------------------------

-- Se retiran datos de insercion para subir a Github

 
--Administrador ========================================================================================
Exec AdministradorAlta 'administrador', 'administrador123', 'administrador123@hotmail.com', 'Claudio Tilbe', 42545, 'Administrador'
Go

