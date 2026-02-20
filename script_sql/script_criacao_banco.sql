CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Herois` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `NomeHeroi` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
    `DataNascimento` datetime(6) NULL,
    `Altura` float NOT NULL,
    `Peso` float NOT NULL,
    `CriadoEm` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `AtualizadoEm` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT `PK_Herois` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Superpoderes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `SuperPoder` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Descricao` varchar(250) CHARACTER SET utf8mb4 NULL,
    `CriadoEm` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `AtualizadoEm` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT `PK_Superpoderes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `HeroiSuperpoderes` (
    `HeroiId` int NOT NULL,
    `SuperpoderId` int NOT NULL,
    CONSTRAINT `PK_HeroiSuperpoderes` PRIMARY KEY (`HeroiId`, `SuperpoderId`),
    CONSTRAINT `FK_HeroiSuperpoderes_Herois_HeroiId` FOREIGN KEY (`HeroiId`) REFERENCES `Herois` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_HeroiSuperpoderes_Superpoderes_SuperpoderId` FOREIGN KEY (`SuperpoderId`) REFERENCES `Superpoderes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_HeroiSuperpoderes_SuperpoderId` ON `HeroiSuperpoderes` (`SuperpoderId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260218203039_Initial', '9.0.0');

COMMIT;

