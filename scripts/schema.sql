CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Groups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Groups` PRIMARY KEY (`Id`)
);

CREATE TABLE `Modules` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Modules` PRIMARY KEY (`Id`)
);

CREATE TABLE `Roles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Roles` PRIMARY KEY (`Id`)
);

CREATE TABLE `Services` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CreatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Services` PRIMARY KEY (`Id`)
);

CREATE TABLE `GroupRoleMap` (
    `GroupId` int NOT NULL,
    `RoleId` int NOT NULL,
    CONSTRAINT `PK_GroupRoleMap` PRIMARY KEY (`RoleId`, `GroupId`),
    CONSTRAINT `FK_GroupRoleMap_Groups_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `Groups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_GroupRoleMap_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `RoleModuleMap` (
    `RoleId` int NOT NULL,
    `ModuleId` int NOT NULL,
    CONSTRAINT `PK_RoleModuleMap` PRIMARY KEY (`RoleId`, `ModuleId`),
    CONSTRAINT `FK_RoleModuleMap_Modules_ModuleId` FOREIGN KEY (`ModuleId`) REFERENCES `Modules` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_RoleModuleMap_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `RoleServiceMap` (
    `RoleId` int NOT NULL,
    `ServiceId` int NOT NULL,
    CONSTRAINT `PK_RoleServiceMap` PRIMARY KEY (`RoleId`, `ServiceId`),
    CONSTRAINT `FK_RoleServiceMap_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_RoleServiceMap_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `Services` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_GroupRoleMap_GroupId` ON `GroupRoleMap` (`GroupId`);

CREATE INDEX `IX_RoleModuleMap_ModuleId` ON `RoleModuleMap` (`ModuleId`);

CREATE INDEX `IX_RoleServiceMap_ServiceId` ON `RoleServiceMap` (`ServiceId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20191117040344_Init', '3.1.0');


