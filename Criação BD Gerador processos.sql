CREATE DATABASE GeradorDeProcessos
GO

USE GeradorDeProcessos
GO

CREATE TABLE Usuarios
(
	IDUsuario INT IDENTITY(1,1) NOT NULL,
	TipoUsuario INT NOT NULL,
	Nome VARCHAR(80) NOT NULL,
    Email VARCHAR(100) NOT NULL,
	EmailConfirmado BIT DEFAULT 0 NOT NULL,
	Senha VARCHAR(100) NOT NULL,
    IDEmpresa INT NOT NULL,

	PRIMARY KEY(IDUsuario)
);
GO

CREATE TABLE Clientes
(
	IDCliente INT IDENTITY(1,1) NOT NULL,
	TipoPessoa INT NOT NULL,
	Nome VARCHAR(80) NOT NULL,
	CpfCnpj VARCHAR(20) NOT NULL,
	Sexo VARCHAR(20) NULL,
	Profissao VARCHAR(60) NULL,
	DataNascimento DATE NULL,
	Renda VARCHAR(20) NULL,
    EstadoCivil VARCHAR(80) NULL,
    RegimeCasamento VARCHAR(80) NULL,
    Conjuge_Cpf VARCHAR(30) NULL,
	Conjuge_Nome VARCHAR(80) NULL,
	IDUsuario INT NOT NULL,

	PRIMARY KEY(IDCliente)
);
GO

CREATE TABLE Empresas
(
	IDEmpresa INT IDENTITY(1,1) NOT NULL,
	Nome VARCHAR(80) NOT NULL,
	Responsavel VARCHAR(80) NOT NULL,
	Responsavel_Email VARCHAR(80) NOT NULL,
	Responsavel_Telefone VARCHAR(20) NOT NULL,

	PRIMARY KEY(IDEmpresa)
);
GO

CREATE TABLE Empreendimentos
(
	IDEmpreendimento INT IDENTITY(1,1) NOT NULL,
	Nome VARCHAR(100) NOT NULL,
    DataEntrega DATE NOT NULL,
	Produto VARCHAR(50) NOT NULL,
	Campanha VARCHAR(50) NOT NULL,
    IDEmpresa INT NOT NULL,

	PRIMARY KEY(IDEmpreendimento)
);
GO
CREATE TABLE Unidades
(
	IDUnidade INT IDENTITY(1,1) NOT NULL,
	Numero VARCHAR(50) NOT NULL,
	IDEmpreendimento INT NOT NULL,
	UnidadeStatus INT NOT NULL,
    Tipo VARCHAR(50) NOT NULL,
	UnidadeObservacao VARCHAR(100),

	PRIMARY KEY(IDUnidade)
);
GO

CREATE TABLE Analises
(
	IDAnalise INT IDENTITY(1,1) NOT NULL,
    DataEntrega DATE NOT NULL,
	ValorFinanciamento DECIMAL NOT NULL,
    ValorTotal DECIMAL NOT NULL,
	SaldoDevedor DECIMAL NOT NULL,
    Observacao VARCHAR(300) NULL,
	TipoAnalise VARCHAR(50) NOT NULL,
    IDCliente INT NOT NULL,
    IDUnidade INT NOT NULL,
	IDUsuario INT NOT NULL,

	PRIMARY KEY(IDAnalise)
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

ALTER TABLE Analises
ADD CONSTRAINT fk_analises_clientes
FOREIGN KEY(IDCliente)
REFERENCES Clientes(IDCliente)
GO
ALTER TABLE Clientes
ADD CONSTRAINT fk_clientes_usuarios
FOREIGN KEY(IDUsuario)
REFERENCES Usuarios(IDUsuario)
GO

ALTER TABLE Analises
ADD CONSTRAINT fk_analises_unidades
FOREIGN KEY(IDUnidade)
REFERENCES Unidades(IDUnidade)
GO

ALTER TABLE Analises
ADD CONSTRAINT fk_analises_usuarios
FOREIGN KEY(IDUsuario)
REFERENCES Usuarios(IDUsuario)
GO


INSERT INTO Empresas (Nome, Responsavel, Responsavel_Email, Responsavel_Telefone) VALUES ('Fibra','Fulano','fulano@fibra.com.br','1112341234')
INSERT INTO Empresas (Nome, Responsavel, Responsavel_Email, Responsavel_Telefone) VALUES ('Odebrecht','Fulano','fulano@odebrecht.com.br','1112341234')

GO

Insert Into Usuarios (Nome, TipoUsuario, Email, Senha, IDEmpresa) Values ('Administrador','0','admin@anapro.com.br', '123', '1')
