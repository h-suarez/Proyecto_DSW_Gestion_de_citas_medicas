use SISTEMACONSULTASMEDICAS

use master

drop database if exists SISTEMACONSULTASMEDICAS

create database SISTEMACONSULTASMEDICAS

use SISTEMACONSULTASMEDICAS

--select * from INFORMATION_SCHEMA.TABLES

create table tb_sexo(
idsexo int primary key identity(1,1),
descripcion varchar(25) not null unique
)
insert into tb_sexo values('Masculino')
insert into tb_sexo values('Femenino')
go

-- truncate table tb_sexo
create proc usp_listar_sexo
as
select*from tb_sexo order by idsexo asc
go

create table tb_distrito(
iddistrito int primary key identity(1,1),
descripcion varchar(45) not null unique
)
insert into tb_distrito values('Ancón')
insert into tb_distrito values('Ate Vitarte')
insert into tb_distrito values('Barranco')
insert into tb_distrito values('Breña')
insert into tb_distrito values('Carabayllo')
insert into tb_distrito values('Chaclacayo')
insert into tb_distrito values('Chorrillos')
insert into tb_distrito values('Cieneguilla')
insert into tb_distrito values('Comas')
insert into tb_distrito values('El Agustino')
insert into tb_distrito values('Independencia')
insert into tb_distrito values('Jesús María')
insert into tb_distrito values('La Molina')
insert into tb_distrito values('La Victoria')
insert into tb_distrito values('Lima')
insert into tb_distrito values('Lince')
insert into tb_distrito values('Los Olivos')
insert into tb_distrito values('Lurigancho')
insert into tb_distrito values('Lurín')
insert into tb_distrito values('Magdalena del Mar')
insert into tb_distrito values('Miraflores')
insert into tb_distrito values('Pachacamac')
insert into tb_distrito values('Pucusana')
insert into tb_distrito values('Pueblo Libre')
insert into tb_distrito values('Puente Piedra')
insert into tb_distrito values('Punta Hermosa')
insert into tb_distrito values('Punta Negra')
insert into tb_distrito values('Rímac')
insert into tb_distrito values('San Bartolo')
insert into tb_distrito values('San Borja')
insert into tb_distrito values('San Isidro')
insert into tb_distrito values('San Juan de Lurigancho')
insert into tb_distrito values('San Juan de Miraflores')
insert into tb_distrito values('San Luis')
insert into tb_distrito values('San Martín de Porres')
insert into tb_distrito values('San Miguel')
insert into tb_distrito values('Santa Anita')
insert into tb_distrito values('Santa María del Mar')
insert into tb_distrito values('Santa Rosa')
insert into tb_distrito values('Santiago de Surco')
insert into tb_distrito values('Surquillo')
insert into tb_distrito values('Villa El Salvador')
insert into tb_distrito values('Villa María del Triunfo')
go

create proc usp_listar_distrito
as
select*from tb_distrito order by iddistrito asc
go

CREATE TABLE tb_estado(
idestado int identity(1,1) primary key not null,
descripcion varchar(25) not null unique
)
-- Cuenta de usuario
insert into tb_estado values('Activa')
insert into tb_estado values('Bloqueada')
-- Estado de proveedores
insert into tb_estado values('Activo')
insert into tb_estado values('Inoperable')
-- Estado de los farmaceuticos
insert into tb_estado values('Disponible')
insert into tb_estado values('Vacio')
-- Estado de la cita
insert into tb_estado values('Pendiente')
insert into tb_estado values('Atendida')
insert into tb_estado values('Cancelada')
go
--truncate table tb_estado

create proc usp_listar_estado_usuario
as
select*from tb_estado 
where idestado in(1,2)
order by idestado asc
go

create proc usp_listar_estado_proveedor
as
select*from tb_estado 
where idestado in(3,4)
order by idestado asc
go

create proc usp_listar_estado_farmaceutico
as
select*from tb_estado 
where idestado in(5,6)
order by idestado asc
go

create proc usp_listar_estado_cita
as
select*from tb_estado 
where idestado in(7,8,9)
order by idestado asc
go

select*from tb_estado order by idestado asc

CREATE TABLE tb_tipoCuenta(
idtipo int identity(1,1) primary key not null,
descripcion varchar(25) not null unique
)
insert into tb_tipoCuenta values('Administrador');
insert into tb_tipoCuenta values('Paciente');
insert into tb_tipoCuenta values('Medico');
insert into tb_tipoCuenta values('Vendedor');
go

create proc usp_listar_tipocuenta
as
select * from tb_tipoCuenta order by idtipo asc
go

create table tb_especialidad(
idespecialidad int primary key identity(1,1),
fotoesp varchar(255) not null,
nombreesp varchar(50) not null unique,
descripcionesp varchar(150) not null
)
insert into tb_especialidad values('foto','Alergología','Su función')
insert into tb_especialidad values('foto','Cardiología','Su función')
insert into tb_especialidad values('foto','Dermatología','Su función')
insert into tb_especialidad values('foto','Ecografía','Su función')
insert into tb_especialidad values('foto','Endocrinología','Su función')
insert into tb_especialidad values('foto','Gastroenterología','Su función')
insert into tb_especialidad values('foto','Geriatría','Su función')
insert into tb_especialidad values('foto','Ginecología Oncológica','Su función')
insert into tb_especialidad values('foto','Hematología','Su función')
insert into tb_especialidad values('foto','Mastología','Su función')
insert into tb_especialidad values('foto','Medicina Física y Rehabilitación','Su función')
insert into tb_especialidad values('foto','Medicina General','Su función')
insert into tb_especialidad values('foto','Medicina Interna','Su función')
insert into tb_especialidad values('foto','Nutrición','Su función')
go

create proc usp_listar_especialidad
as
select*from tb_especialidad order by idespecialidad asc
go

create proc usp_listar_especialidad_combo
as
select idespecialidad,nombreesp from tb_especialidad order by nombreesp asc
go

create proc usp_agregar_especialidad
@img varchar(255),
@nom varchar(50),
@funcion varchar(150)
as
insert into tb_especialidad values(@img,@nom,@funcion)
go

create proc usp_editar_especialidad
@idesp int,
@img varchar(255),
@nom varchar(50),
@funcion varchar(150)
as
update tb_especialidad set fotoesp = @img, nombreesp = @nom, descripcionesp = @funcion
where idespecialidad = @idesp
go

create table tb_usuario(
idusuario int primary key identity(1,1),
dniusu int not null check(len(dniusu) = 8) unique,
nombreusu varchar(200) not null,
idsexo int not null foreign key(idsexo) references tb_sexo(idsexo),
iddistrito int not null foreign key(iddistrito) references tb_distrito(iddistrito),
celularusu varchar(15) not null,
email varchar(60) not null unique,
clave varchar(40) not null,
fechaRegusu date not null default GETDATE(),
idtipo int not null default 2,
idestado int not null default 1,
foreign key(idtipo) references tb_tipoCuenta(idtipo),
foreign key(idestado) references tb_estado(idestado)
)
-- administradores
insert into tb_usuario values(12345678,'admin1',1,1,'123-123-123','ad1@admin.com','123',default,1,default)
insert into tb_usuario values(68426546,'admin2',1,1,'123-123-123','ad2@admin.com','123',default,1,default)
-- clientes
insert into tb_usuario values(64864887,'paciente 1',1,1,'123-123-123','cliente1@mail.com','123',default,default,default)
insert into tb_usuario values(16843546,'paciente 2',2,8,'123-123-123','cliente2@mail.com','123',default,default,default)
-- medicos
insert into tb_usuario values(51651652,'doctor 1',1,10,'123-123-123','doctor1@mail.com','123',default,3,default)
insert into tb_usuario values(13446888,'doctor 2',1,4,'123-123-123','doctor2@mail.com','123',default,3,default)
-- vendedor
insert into tb_usuario values(75341351,'vendedor 1',2,7,'123-123-123','vendedor1@mail.com','123',default,4,default)
insert into tb_usuario values(65451888,'vendedor 1',1,5,'123-123-123','vendedor2@mail.com','123',default,4,default)
go
--truncate table tb_usuario

/******************************LISTARES********************************/
-- SP PARA ADMIN
create proc usp_listar_usuarios
as
select*from tb_usuario order by idusuario asc
go

create proc usp_listar_man_usuarios
as
select u.idusuario,u.nombreusu,s.descripcion,u.email,t.descripcion,e.descripcion
from tb_usuario u
inner join tb_sexo s
on u.idsexo = s.idsexo
inner join tb_tipoCuenta t
on u.idtipo = t.idtipo
inner join tb_estado e
on u.idestado = e.idestado
where u.idtipo != 1
order by idusuario asc
go

-- SP PARA ADMIN
/*create proc usp_listar_usuarios_combo
as
select idusuario,nombreusu from tb_usuario order by nombreusu asc
go*/

create proc usp_validar_usuario
@email varchar(60),
@clave varchar(40)
as
select * from tb_usuario where email = @email and clave = @clave
go
-- SP PARA ADMIN
/*create proc usp_listar_usuarios_x_sexo
@sexo int
as
select*from tb_usuario
where idsexo = @sexo
order by idusuario asc
go*/

-- SP PARA ADMIN
/*create proc usp_listar_usuarios_x_distrito
@distrito int
as
select*from tb_usuario
where iddistrito = @distrito
order by idusuario asc
go*/
-- SP PARA ADMIN
/*create proc usp_listar_usuarios_x_tipo
@tipo int
as
select*from tb_usuario
where idtipo = @tipo
order by idusuario asc
go*/
-- SP PARA ADMIN
/*create proc usp_listar_usuarios_x_estado
@est int
as
select*from tb_usuario
where idestado = @est
order by idusuario asc
go*/

/******************************CRUDs********************************/
-- CREATEs
create proc usp_agregar_cliente
@dni int,
@nom varchar(200),
@sexo int,
@distrito int,
@cel varchar(15),
@email varchar(60),
@clave varchar(40)
as
insert into tb_usuario values(@dni,@nom,@sexo,@distrito,@cel,@email,@clave,default,default,default)
go

create proc usp_agregar_empleado
@dni int,
@nom varchar(200),
@sexo int,
@distrito int,
@cel varchar(15),
@email varchar(60),
@clave varchar(40),
@tipo int,
@estado int
as
insert into tb_usuario values(@dni,@nom,@sexo,@distrito,@cel,@email,@clave,default,@tipo,@estado)
go

-- UPDATEs
create proc usp_editar_cliente
@idcli int,
@dni int,
@nom varchar(200),
@sexo int,
@distrito int,
@cel varchar(15),
@email varchar(60),
@clave varchar(40)
as
update tb_usuario set dniusu = @dni, nombreusu = @nom, idsexo = @sexo, iddistrito = @distrito,celularusu = @cel,
email = @email, clave = @clave where idusuario = @idcli
go

create proc usp_editar_empleado
@idcli int,
@dni int,
@nom varchar(200),
@sexo int,
@distrito int,
@cel varchar(15),
@email varchar(60),
@clave varchar(40),
@tipo int,
@estado int
as
update tb_usuario set dniusu = @dni, nombreusu = @nom, idsexo = @sexo, iddistrito = @distrito,celularusu = @cel,
email = @email, clave = @clave, idtipo = @tipo, idestado = @estado where idusuario = @idcli
go

/*************************************************************************************************************/
create table tb_medico(
idmedico int primary key identity(1,1),
fotomed varchar(255) not null,
idespecialidad int not null references tb_especialidad(idespecialidad),
--horaini int not null,
--tipotiempoini varchar(2) not null,
--horafin int not null,
--tipotiempofin varchar(2) not null,
horaini time not null,
horafin time not null,
idcuenta int not null references tb_usuario(idusuario)
)
go
--truncate table tb_medico

insert into tb_medico values('img',1,'8:00 AM','6:00 PM',5)
insert into tb_medico values('img',3,'9:00 AM','7:00 PM',6)
go

create proc usp_listar_medicos_x_nombre_y_especialidad
@nombre varchar(200),
@especialidad int
as
select m.idmedico,m.fotomed,u.nombreusu,e.nombreesp from tb_medico m
inner join tb_usuario u
on m.idcuenta = u.idusuario
inner join tb_especialidad e
on m.idespecialidad = e.idespecialidad
where u.nombreusu like '%'+@nombre+'%' or m.idespecialidad = @especialidad
order by idusuario asc
go

create proc usp_listar_medicos
as
select*from tb_medico
go

create proc usp_listar_man_medicos
as
select m.idmedico,m.fotomed,e.nombreesp,m.horaini,m.horafin from tb_medico m
inner join tb_especialidad e
on m.idespecialidad = e.idespecialidad
go
/*create proc usp_listar_cuentas_tipo_medico
as
select*from tb_usuario
where idtipo = 3
order by idusuario asc
go*/

create proc usp_listar_medicos_x_especiliadad
@especialidad int
as
select m.idmedico,u.nombreusu,m.fotomed,e.nombreesp from tb_medico m
inner join tb_usuario u
on m.idcuenta = u.idusuario
inner join tb_especialidad e
on m.idespecialidad = e.idespecialidad
where m.idespecialidad = @especialidad
order by idmedico asc
go

/*create proc usp_listar_medicos_combo
as
select m.idmedico,u.nombreusu from tb_medico m
inner join tb_usuario u
on m.idcuenta = u.idusuario
order by u.nombreusu asc
go*/

create proc usp_agregar_medico
@img varchar(255),
@especialidad int,
@horaini time,
@horafin time,
@cuenta int
as
insert into tb_medico values(@img,@especialidad,@horaini,@horafin,@cuenta)
go

create proc usp_editar_medico
@idmed int,
@especialidad int,
@img varchar(255),
@horaini time,
@horafin time,
@cuenta int
as
update tb_medico set fotomed = @img, idespecialidad = @especialidad, horaini = @horaini, horafin = @horafin, idcuenta = @cuenta
where idmedico = @idmed
go

/*************************************************************************************************************/

create table tb_cita(
idcita int primary key identity(1,1),
idmedico int not null,
idusuario int not null foreign key(idusuario) references tb_usuario(idusuario),
fechacita date not null,
horacita time not null,
--horacita int not null default datepart(hour,GETDATE()),
--minutocita int not null,
preciocita decimal(11,2) not null default 50.00 check(preciocita >= 00.00),
pagocita decimal(11,2) not null default 0.00,
observaciones varchar(350) default null,
prescripcion varchar(350) default null,
idestado int not null default 7,
fechaReg date not null default GETDATE(),
foreign key(idmedico) references tb_medico(idmedico),
foreign key(idestado) references tb_estado(idestado)
)
--truncate table tb_cita
--insert into tb_cita values('estoy mal',3,GETDATE(),datepart(hour,GETDATE()),'','',default,default)
insert into tb_cita values(1,4,'5-6-2021','2:50',default,default,'ninguna observación','ninguna prescripción',default,default)
insert into tb_cita values(2,3,'5-7-2021','5:50 PM',default,default,'ninguna observación','ninguna prescripción',default,default)
insert into tb_cita values(1,4,'5-12-2021','3:50 AM',default,default,'ninguna observación','ninguna prescripción',default,default)
go

/******************************LISTARES********************************/
-- SP para usuario
create proc usp_listar_citas_de_paciente
@paciente int
as
select*from tb_cita
where idusuario = @paciente
order by idcita asc
go

/*Listares para administrador*/
create proc usp_listar_citas
as
select*from tb_cita order by idcita asc
go

create proc usp_listar_man_citas
@idmed int
as
select c.idcita,u.nombreusu,c.fechacita,c.horacita,e.descripcion from tb_cita c
inner join tb_usuario u
on c.idusuario = u.idusuario
inner join tb_estado e
on c.idestado = e.idestado
inner join tb_medico m
on c.idmedico = m.idmedico
where m.idcuenta = @idmed
order by idcita asc
go
--exec usp_listar_man_citas 6

create proc usp_listar_reporte_citas_atendidas
as
select c.idcita,e.nombreesp,c.preciocita,c.pagocita,es.descripcion,c.fechacita from tb_cita c
inner join tb_medico m
on c.idmedico = m.idmedico
inner join tb_especialidad e
on m.idespecialidad = e.idespecialidad
inner join tb_estado es
on c.idestado = es.idestado
order by 1 asc
go

/*create proc usp_listar_citas_x_id
@idcita int
as
select*from tb_cita
where idcita = @idcita
go*/

/*create proc usp_listar_citas_x_medico_o_estado
@estado int,
@medico int
as
select*from tb_cita
where idestado = @estado or idmedico = @medico
order by idcita asc
go*/

-- SP el medico
/*create proc usp_listar_citas_x_paciente_medico
@medico int,
@paciente int
as
select*from tb_cita
where idmedico = @medico and idusuario = @paciente
order by idcita asc
go*/

/*create proc usp_listar_citas_x_paciente
@nombre int
as
select*from tb_cita
where nompaciente like @nombre+'%'
order by idcita asc
go*/
/*create proc usp_listar_citas_x_medico_estado
@medico int,
@estado int
as
select*from tb_cita
where idmedico = @medico and idestado = @estado
order by idcita asc
go*/

/************************************CRUDs***************************************/
-- SP para pacientes
create proc usp_agregar_cita
@medico int,
@paciente int,
@fecha date,
@hora time
as
insert into tb_cita values(@medico,@paciente,@fecha,@hora,default,default,default,default,default,default)
go

-- SP para medicos
create proc usp_editar_cita
@idcita int,
@observaciones varchar(350),
@prescripcion varchar(350),
@pago decimal(11,2),
@estado int
as
update tb_cita set observaciones = @observaciones, 
prescripcion = @prescripcion, pagocita = @pago, idestado = @estado where idcita = @idcita
go

--exec usp_editar_cita 2,'ninguna ob','ninguna pre',5.00,7
--select*from tb_cita

/***********************************************ECOMMERCE***************************************************/
CREATE TABLE tb_proveedor(
idproveedor int identity(1,1) primary key,
rucprov varchar(11) not null unique,
nombreprov varchar(40) not null unique,
telefonoprov varchar(15) not null check(LEN(telefonoprov) >= 9 and LEN(telefonoprov) <= 15),
razonprov varchar(60) not null unique,
iddistrito int not null references tb_distrito(iddistrito),
direccionprov varchar(60) not null,
fechaRegprov date default getdate() not null,
idestado int default 3 not null
foreign key(idestado) references tb_estado(idestado)
);
-- truncate table tb_proveedor
insert into tb_proveedor values('RUC-01','Panadol','132456789','RZ-01',2,'Algun lugar',default,default)
insert into tb_proveedor values('RUC-02','Ciberfarma','132456789','RZ-02',8,'Algun lugar',default,default)
insert into tb_proveedor values('RUC-03','Panasonic','132456789','RZ-03',13,'Algun lugar',default,default)
go

create proc usp_listar_proveedores
as
select*from tb_proveedor order by idproveedor asc
go

create proc usp_listar_man_proveedores
as
select p.idproveedor,p.nombreprov,p.telefonoprov,d.descripcion,e.descripcion
from tb_proveedor p
inner join tb_distrito d
on p.iddistrito = d.iddistrito
inner join tb_estado e
on p.idestado = e.idestado
order by idproveedor asc
go

/*create proc usp_listar_proveedores_x_id
@idprov int
as
select*from tb_proveedor
where idproveedor = @idprov
go*/

create proc usp_agregar_proveedor
@ruc varchar(11),
@nom varchar(40),
@tel varchar(15),
@rz varchar(60),
@distrito int,
@direccion varchar(60)
as
insert into tb_proveedor values(@ruc,@nom,@tel,@rz,@distrito,@direccion,default,default)
go

create proc usp_editar_proveedor
@idprov int,
@ruc varchar(11),
@nom varchar(40),
@tel varchar(15),
@rz varchar(60),
@distrito int,
@direccion varchar(60),
@estado int
as
update tb_proveedor set rucprov = @ruc, nombreprov = @nom, telefonoprov = @tel, razonprov = @rz, iddistrito = @distrito,
direccionprov = @direccion, idestado = @estado where idproveedor = @idprov
go

create table tb_farmaceutico(
idfarmaceutico int primary key identity(1,1),
fotofarm varchar(255) not null,
nombrefarm varchar(200) not null,
stockfarm int not null check(stockfarm >= 0),
preciofarm decimal(11,2) not null check(preciofarm > 00.00),
descripcionfarm varchar(300) not null,
idproveedor int references tb_proveedor(idproveedor),
idestado int not null default 5,
fechaReg date default getdate() not null,
foreign key(idestado) references tb_estado(idestado)
)
-- truncate table tb_farmaceutico
insert into tb_farmaceutico values('foto','paracetamol',20,5.00,'caja de pastillas',2,default,default)
insert into tb_farmaceutico values('foto','panadol',30,8.00,'caja de pastillas',1,default,default)
insert into tb_farmaceutico values('foto','cocaina',60,7.00,'caja de pastillas',3,default,default)
go

create proc usp_listar_farmaceutico
as
select*from tb_farmaceutico order by idfarmaceutico asc
go

/*create proc usp_listar_farmaceutico_x_id
@idfarm int
as
select*from tb_farmaceutico
where idfarmaceutico = @idfarm
go*/

/*create proc usp_listar_farmaceutico_x_proveedor
@prov int
as
select * from tb_farmaceutico
where idproveedor = @prov
order by idfarmaceutico asc
go*/

/*create proc usp_listar_farmaceutico_x_estado
@estado int
as
select * from tb_farmaceutico
where idestado = @estado
order by idfarmaceutico asc
go*/

create proc usp_agregar_farmaceutico
@foto varchar(255),
@nom varchar(200),
@stock int,
@precio decimal(11,2),
@desc varchar(300),
@proveedor int
as
insert into tb_farmaceutico values(@foto,@nom,@stock,@precio,@desc,@proveedor,default,default)
go

create proc usp_editar_farmaceutico
@idfarm int,
@foto varchar(255),
@nom varchar(200),
@stock int,
@precio decimal(11,2),
@desc varchar(300),
@proveedor int,
@estado int
as
update tb_farmaceutico set fotofarm = @foto, nombrefarm = @nom, stockfarm = @stock, 
preciofarm = @precio, descripcionfarm = @desc,
idproveedor = @proveedor, idestado = @estado
where idfarmaceutico = @idfarm
go

create table tb_venta(
idventa int primary key identity(1,1),
idpaciente int references tb_usuario(idusuario),
--idvendedor int references tb_usuario(idusuario),
--cantidadtot int not null,
--preciotot decimal(11,2) not null default 0.00,
fechaReg date default getdate()
)
--truncate table tb_venta
insert into tb_venta values(3,default)
insert into tb_venta values(4,default)
go

create proc usp_listar_venta
as
select*from tb_venta order by idventa asc
go

/*create proc usp_listar_venta_x_vendedor
@vend int
as
select * from tb_venta
where idvendedor = @vend
order by idventa asc
go*/

create proc usp_agregar_venta
@paciente int,
--@cant int,
--@prec decimal(11,2),
@n int output -- Variable de salida
as
begin
insert into tb_venta values(@paciente,default)
select @n = SCOPE_IDENTITY() -- SCOPE_IDENTITY() => devuelve el último ID creado en la misma conexión
return @n
end
go

create table tb_detalleVenta(
idventa int not null,
idfarmaceutico int not null,
cantidaddet int not null check(cantidaddet >= 1),
precioUnidaddet decimal(11,2) not null check(precioUnidaddet > 0.00),
primary key(idventa,idfarmaceutico),
foreign key(idventa) references tb_venta(idventa),
foreign key(idfarmaceutico) references tb_farmaceutico(idfarmaceutico)
);
--truncate table tb_detalleVenta
insert into tb_detalleVenta values(1,1,3,12.00)
insert into tb_detalleVenta values(1,2,2,20.00)
go

create proc usp_listar_detalle_venta
as
select d.idventa,f.nombrefarm,d.cantidaddet,d.precioUnidaddet from tb_detalleVenta d
inner join tb_farmaceutico f
on d.idfarmaceutico = f.idfarmaceutico
order by d.idventa asc
go

create proc usp_agregar_detalle_venta
@idventa int,
@farm int,
@cant int,
@preunit decimal(11,2)
as
insert into tb_detalleVenta values(@idventa,@farm,@cant,@preunit)
go

create proc usp_actualiza_unidades
@idfarm int,
@cantidad int
as
update tb_farmaceutico set stockfarm -= @cantidad
where idfarmaceutico = @idfarm
go

/***********************************************SP PARA PANEL DE CONTROL***************************************************/
/*select count(idmedico) from tb_medico
select count(idusuario) from tb_usuario
select count(idcita) from tb_cita where idestado = 8*/

-- VENTAS
-- este mes
create proc usp_ventas_este_mes
as
select * from tb_venta
where Month(fechaReg) = Month(getdate())
go
-- mes pasado
create proc usp_ventas_este_mes_pasado
as
select * from tb_venta
where Month(fechaReg) = month(dateadd(month, -1, GETDATE()))
go

--USUARIOS
-- este mes
create proc usp_usuarios_este_mes
as
select * from tb_usuario
where Month(fechaRegusu) = Month(getdate())
go
-- mes pasado
create proc usp_usuarios_este_mes_pasado
as
select * from tb_usuario
where Month(fechaRegusu) = month(dateadd(month, -1, GETDATE()))
go

--INGRESOS VENTAS
-- este mes
create proc usp_ingresos_este_mes
as
--select ISNULL(sum(d.precioUnidaddet),0.00) from tb_detalleVenta d
select * from tb_detalleVenta d
inner join tb_venta v
on d.idventa = v.idventa
where MONTH(v.fechaReg) = MONTH(getdate())
go
-- mes pasado
create proc usp_ingresos_este_mes_pasado
as
select * from tb_detalleVenta d
--select ISNULL(sum(d.precioUnidaddet),0.00) from tb_detalleVenta d
inner join tb_venta v
on d.idventa = v.idventa
where Month(fechaReg) = month(dateadd(month, -1, GETDATE()))
go