
CREATE DATABASE Parcial2Mevy;
GO


USE Parcial2Mevy;
GO


USE [master];
GO
CREATE LOGIN [usrparcial2] WITH PASSWORD=N'12345678',
    DEFAULT_DATABASE = Parcial2Mevy,
    CHECK_EXPIRATION = OFF,
    CHECK_POLICY = ON;
GO
USE Parcial2Mevy;
GO
CREATE USER [usrparcial2] FOR LOGIN [usrparcial2];
GO
ALTER ROLE [db_owner] ADD MEMBER [usrparcial2];
GO


CREATE TABLE Serie (
    id INT IDENTITY(1,1) PRIMARY KEY,
    titulo VARCHAR(250),
    sinopsis VARCHAR(5000),
    director VARCHAR(100),
    episodios INT,
    fecha_estreno DATE,
    estado SMALLINT
);
GO

ALTER TABLE Serie ADD estado SMALLINT NOT NULL DEFAULT 1; 
GO


CREATE PROCEDURE paSerieListar 
    @parametro VARCHAR(50)
AS
BEGIN
    SELECT id, titulo, sinopsis, director, episodios, fecha_estreno, estado
    FROM Serie
    WHERE estado <> -1 AND titulo LIKE '%' + REPLACE(@parametro, ' ', '%') + '%';
END
GO

INSERT INTO Serie (titulo, sinopsis, director, episodios, fecha_estreno, estado)
VALUES
('Jeffrey Dahmer', 'Moustro, asesino en serie mato a mas de 17 jovenes', 'Ryan Murphy, Ian Brennan', 8, '2022-07-01', 1),
('Breaking Bad' , 'Un profesor de química se convierte en fabricante de metanfetamina', 'Vince Gilligan', 62, '2008-01-20', 1),
('Ni una mas' , 'un retrato de la generación Z y su relación con el mundo adulto', 'Eduard Cortes', 8, '2024-05-22', 1),
('Merlina' , 'Inteligente, Sarcastica, un poco muerta por dentro', 'Tiim Burton', 8, '2022-01-01', 1)
