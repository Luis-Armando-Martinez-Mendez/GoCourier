Create Database GoCourierdb;

Use GoCourierdb;


CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100),
    Email NVARCHAR(255),
    Contraseña NVARCHAR(255),
    Direccion NVARCHAR(255),
    Pais NVARCHAR(100)
);

CREATE TABLE Envios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT,
    Direccion NVARCHAR(255),
    Descripcion NVARCHAR(500),
    Estado NVARCHAR(50),
    Fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

CREATE TABLE Notificaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT,
    Mensaje NVARCHAR(500),
    Fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);