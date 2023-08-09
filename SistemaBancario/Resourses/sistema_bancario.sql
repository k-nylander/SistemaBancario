-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Tempo de geração: 09-Ago-2023 às 21:36
-- Versão do servidor: 8.0.33
-- versão do PHP: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `sistema_bancario`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `agencia`
--

DROP TABLE IF EXISTS `agencia`;
CREATE TABLE IF NOT EXISTS `agencia` (
  `Id` int UNSIGNED NOT NULL AUTO_INCREMENT,
  `Endereco` varchar(70) NOT NULL,
  `Gerente` varchar(70) NOT NULL,
  `ContasCadastradas` int DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `agencia`
--

INSERT INTO `agencia` (`Id`, `Endereco`, `Gerente`, `ContasCadastradas`) VALUES
(1, 'Av. Martins Francisco, 2001 - Camilópolis, Santo André/SP', 'Marco Contessoto', 1),
(2, 'Av. Teste, 0019 - Centro, São Bernardo/SP', 'Marcola P. C. Coelo', -10);

-- --------------------------------------------------------

--
-- Estrutura da tabela `conta`
--

DROP TABLE IF EXISTS `conta`;
CREATE TABLE IF NOT EXISTS `conta` (
  `Numero` int UNSIGNED NOT NULL AUTO_INCREMENT,
  `AgenciaID` int UNSIGNED NOT NULL,
  `Dono` varchar(14) NOT NULL,
  `Saldo` double DEFAULT '0',
  `ValorRendimento` double DEFAULT '0',
  `Rendimento` float DEFAULT '1',
  `Variacao` float DEFAULT '0',
  `DataCriacao` date NOT NULL,
  `Senha` varchar(100) NOT NULL,
  PRIMARY KEY (`Numero`,`AgenciaID`) USING BTREE,
  KEY `Dono_idx` (`Dono`),
  KEY `Agencia_idx` (`AgenciaID`)
) ENGINE=InnoDB AUTO_INCREMENT=9856171 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `conta`
--

INSERT INTO `conta` (`Numero`, `AgenciaID`, `Dono`, `Saldo`, `ValorRendimento`, `Rendimento`, `Variacao`, `DataCriacao`, `Senha`) VALUES
(6512814, 1, '12312312301', 0, 0, 1, 0, '0001-01-01', '$2b$10$LQn50AM2Nv0HeBvPvRaUPO9rdNQdrsybxJl5e7Eo0eQnD1kUHYg.e');

--
-- Acionadores `conta`
--
DROP TRIGGER IF EXISTS `conta_after_insert`;
DELIMITER $$
CREATE TRIGGER `conta_after_insert` AFTER INSERT ON `conta` FOR EACH ROW BEGIN
    UPDATE Agencia
    SET ContasCadastradas = ContasCadastradas + 1
    WHERE Id = NEW.AgenciaID;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE IF NOT EXISTS `usuario` (
  `CPF` varchar(14) NOT NULL,
  `Nome` varchar(70) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Senha` varchar(100) NOT NULL,
  `Endereco` varchar(50) DEFAULT NULL,
  `Telefone` varchar(17) DEFAULT NULL,
  PRIMARY KEY (`CPF`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Extraindo dados da tabela `usuario`
--

INSERT INTO `usuario` (`CPF`, `Nome`, `Email`, `Senha`, `Endereco`, `Telefone`) VALUES
('00000000000', 'Jose', 'email@email.com', '$2a$11$UmVb6RKe198CyeodVNFJzeOa4X1UP.iVNGKk/5vsfszGtebwdljSy', 'Av. 1, 500', '4002-8922'),
('12312312301', 'Mario', 'novoEmailMario@email.com', '$2b$10$MHT43Mm.gGwWM0Js7g2s2uHUxi0hxZwPKm9MNyynjh7eWKkebTs3O', 'Casa do Mario', '40028922');

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `conta`
--
ALTER TABLE `conta`
  ADD CONSTRAINT `Agencia` FOREIGN KEY (`AgenciaID`) REFERENCES `agencia` (`Id`),
  ADD CONSTRAINT `Dono` FOREIGN KEY (`Dono`) REFERENCES `usuario` (`CPF`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
