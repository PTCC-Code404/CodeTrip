create database Codetrip;

use Codetrip;

CREATE TABLE Usuario (
Id_Usuario INT AUTO_INCREMENT,
Nome_Usuario VARCHAR(100),
Email_Usuario VARCHAR(255) UNIQUE,
Senha_Usuario VARCHAR(255),
PRIMARY KEY (Id_Usuario));

CREATE TABLE Estado (
UF_Estado CHAR(2),
Nome_Estado VARCHAR(50),
PRIMARY KEY (UF_Estado));

CREATE TABLE Cidade (
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
PRIMARY KEY (Cidade_Nome, UF_Estado),
FOREIGN KEY (UF_Estado) REFERENCES Estado(UF_Estado));

CREATE TABLE Cliente (
Nome_Cli VARCHAR(100),
Email_Cli VARCHAR(255),
Data_Nasc_Cli DATE,
CPF_Cli CHAR(11),
Telefone_Cli VARCHAR(20),
Logradouro_Cli VARCHAR(100),
Numero_Cli VARCHAR(10),
Bairro_Cli VARCHAR(60),
Complemento_Cli VARCHAR(60),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
PRIMARY KEY (CPF_Cli),
FOREIGN KEY (UF_Estado) REFERENCES Estado (UF_Estado),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade (Cidade_Nome));

CREATE TABLE Transporte (
Id_Transp INT AUTO_INCREMENT,
Tipo_Transp VARCHAR(100),
UF_Estado CHAR(2),
PRIMARY KEY (Id_Transp),
FOREIGN KEY (UF_Estado) REFERENCES Estado(UF_Estado));

CREATE TABLE Tipo_Hospedagem (
Id_Tipo_Hospedagem INT AUTO_INCREMENT,
Desc_Hospedagem VARCHAR(50),
Capacidade_Hospedagem int,
Valor_Hospedagem DECIMAL(10, 2),
PRIMARY KEY (Id_Tipo_Hospedagem));

CREATE TABLE Tipo_Pensao (
Id_Pensao INT AUTO_INCREMENT,
Desc_Pensao VARCHAR(50),
Valor_Pensao DECIMAL(10, 2),
PRIMARY KEY (Id_Pensao));

CREATE TABLE Origem_Viagem (
Id_Origem INT AUTO_INCREMENT,
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
PRIMARY KEY (Id_Origem),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade(Cidade_Nome),
FOREIGN KEY (UF_Estado) REFERENCES Estado(UF_Estado));

CREATE TABLE Destino_Viagem (
Id_Destino INT AUTO_INCREMENT,
Desc_Destino VARCHAR(50),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
Valor_Destino DECIMAL(10, 2),
PRIMARY KEY (Id_Destino),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade (Cidade_Nome),
FOREIGN KEY (UF_Estado) REFERENCES Estado (UF_Estado));

CREATE TABLE Pagamento (
Id_Pagamento INT AUTO_INCREMENT,
Desc_Pagamento VARCHAR(30),
PRIMARY KEY (Id_Pagamento));

CREATE TABLE End_Transporte (
Id_End_Transporte INT AUTO_INCREMENT,
Id_Transp INT,
Logradouro_End_Transporte VARCHAR(100),
Numero_End_Transporte VARCHAR(10),
Bairro_End_Transporte VARCHAR(60),
Complemento_End_Transporte VARCHAR(60),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
PRIMARY KEY (Id_End_Transporte),
FOREIGN KEY (Id_Transp) REFERENCES Transporte(Id_Transp));

CREATE TABLE Hospedagem (
Id_Hospedagem INT AUTO_INCREMENT,
Nome_Hospedagem VARCHAR(50),
Id_Tipo_Hospedagem int,
Id_Pensao int,
Logradouro_Endereço_Hospedagem VARCHAR(100),
Numero_Endereço_Hospedagem VARCHAR(10),
Bairro_Endereço_Hospedagem VARCHAR(60),
Complemento_Endereço_Hospedagem VARCHAR(60),
Cidade_Nome VARCHAR(50),
UF_Estado CHAR(2),
PRIMARY KEY (Id_Hospedagem),
FOREIGN KEY (Id_Tipo_Hospedagem) REFERENCES Tipo_Hospedagem (Id_Tipo_Hospedagem),
FOREIGN KEY (Id_Pensao) REFERENCES Tipo_Pensao (Id_Pensao),
FOREIGN KEY (Cidade_Nome) REFERENCES Cidade (Cidade_Nome),
FOREIGN KEY (UF_Estado) REFERENCES Estado (UF_Estado));

CREATE TABLE Pedido (
Id_Pedido INT AUTO_INCREMENT,
Id_Usuario INT NOT NULL,
CPF_Cli CHAR(11) NOT NULL,
Data_Inicio DATE NOT NULL,
Data_Fim DATE NOT NULL,
Id_Transp INT NOT NULL,
Id_End_Transporte INT NOT NULL,
Id_Hospedagem INT NOT NULL,
Id_Pagamento INT NOT NULL,
PRIMARY KEY (Id_Pedido),
FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id_Usuario),
FOREIGN KEY (CPF_Cli) REFERENCES Cliente(CPF_Cli),
FOREIGN KEY (Id_End_Transporte) REFERENCES End_Transporte(Id_End_Transporte),
FOREIGN KEY (Id_Transp) REFERENCES Transporte(Id_Transp),
FOREIGN KEY (Id_Hospedagem) REFERENCES Hospedagem(Id_Hospedagem),
FOREIGN KEY (Id_Pagamento) REFERENCES Pagamento(Id_Pagamento));
