CREATE DATABASE GeradorDeProcessos
GO

USE GeradorDeProcessos
GO

CREATE TABLE Usuarios
(
	IDUsuario INT IDENTITY(1,1) NOT NULL,
	Nome VARCHAR(80) NOT NULL,
    Email VARCHAR(100) NOT NULL,
	Senha VARCHAR(100) NOT NULL,
    IDEmpresa INT NOT NULL,

	PRIMARY KEY(IDUsuario)
);
GO

CREATE TABLE Clientes
(
	IDCliente INT IDENTITY(1,1) NOT NULL,
	CpfCnpj VARCHAR(30) NOT NULL,
	Nome VARCHAR(80) NOT NULL,
	Sexo VARCHAR(100) NOT NULL,
	Profissao VARCHAR(60) NOT NULL,
	DataNascimento DATE NULL,
	Renda VARCHAR(80) NULL,
    EstadoCivil VARCHAR(80) NULL,
    RegimeCasamento VARCHAR(80) NULL,
	IDUsuario INT NOT NULL,

	PRIMARY KEY(IDCliente)
);
GO

CREATE TABLE Empresas
(
	IDEmpresa INT IDENTITY(1,1) NOT NULL,
	Nome VARCHAR(80) NOT NULL,

	PRIMARY KEY(IDEmpresa)
);
GO

CREATE TABLE Empreendimentos
(
	IDEmpreendimento INT IDENTITY(1,1) NOT NULL,
	Nome VARCHAR(100) NOT NULL,
    IDEmpresa INT NOT NULL,

	PRIMARY KEY(IDEmpreendimento)
);
GO
CREATE TABLE Unidades
(
	IDUnidade INT IDENTITY(1,1) NOT NULL,
	Numero VARCHAR(100) NOT NULL,
	IDEmpreendimento INT NOT NULL,

	PRIMARY KEY(IDUnidade)
);
GO

CREATE TABLE Vendas
(
	IDVenda INT IDENTITY(1,1) NOT NULL,
	Unidades varchar(50) NOT NULL,
	ValorFinanciamento DECIMAL NOT NULL,
    ValorTotal DECIMAL NOT NULL,
    IDCliente INT NOT NULL,
    IDUnidade INT NOT NULL,

	PRIMARY KEY(IDVenda)
);
GO

ALTER TABLE Empreendimentos
ADD CONSTRAINT fk_empreendimentos_empresas
FOREIGN KEY(IDEmpresa)
REFERENCES Empresas(IDEmpresa)
GO

ALTER TABLE Unidades
ADD CONSTRAINT fk_unidades_empreendimentos
FOREIGN KEY(IDEmpreendimento)
REFERENCES Empreendimentos(IDEmpreendimento)
GO

ALTER TABLE Usuarios
ADD CONSTRAINT fk_usuarios_empresas
FOREIGN KEY(IDEmpresa)
REFERENCES Empresas(IDEmpresa)
GO

ALTER TABLE Vendas
ADD CONSTRAINT fk_vendas_clientes
FOREIGN KEY(IDCliente)
REFERENCES Clientes(IDCliente)
GO
ALTER TABLE Clientes
ADD CONSTRAINT fk_clientes_usuarios
FOREIGN KEY(IDUsuario)
REFERENCES Usuarios(IDUsuario)
GO

ALTER TABLE Vendas
ADD CONSTRAINT fk_vendas_unidades
FOREIGN KEY(IDUnidade)
REFERENCES Unidades(IDUnidade)
GO


INSERT INTO Empresas (Nome) VALUES ('Fibra')
INSERT INTO Empresas (Nome) VALUES ('Odebrecht')

GO

Insert Into Usuarios (Nome, Email, Senha, IDEmpresa) Values ('Administrador','admin@anapro.com.br', '123', '1')