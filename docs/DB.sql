CREATE DATABASE IF NOT EXISTS PruebaTecnica;

USE PruebaTecnica;

CREATE TABLE Usuario(
	id INT PRIMARY KEY AUTO_INCREMENT,
	usuario VARCHAR(100) NOT NULL UNIQUE,
	hash_contrasena VARCHAR(100) NOT NULL,
	salt_contrasena VARCHAR(100) NOT NULL
  
);

CREATE TABLE Producto (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    categoria VARCHAR(100) NOT NULL,

    CONSTRAINT CK_Producto_precio CHECK (precio >= 0)

);

INSERT INTO Usuario (usuario, hash_contrasena, salt_contrasena) VALUES ('usuario', 'tdaaGx3ortHtYpDknLWkcQLA1kbH95Exd+0d6NuK+gY=', '+rJ1VR2sdLNflp0rgXmRXSgTs5aDYsGQUvYewE5iqn8My4zR1C8Jh2fpEZ9t4+wrP8lXtpVonabIyWzA+4MmCQ==');
