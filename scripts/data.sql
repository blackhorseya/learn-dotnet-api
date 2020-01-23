-- meta data
insert into `Groups` (`Name`) values ('pls'), ('wfbss'), ('cas');
insert into `Roles` (`Name`) values ('admin'), ('wfbss_manager'), ('wfbss_director'), ('ddi_manager'), ('tippingpoint_manager');
insert into `Modules` (`Name`) values ('key'), ('management');
insert into `Services` (`Name`) values ('ddi'), ('wfbss'), ('tippingpoint');

-- relationship
insert into `GroupRoleMap` (`GroupId`,`RoleId`) values ('2', '2'), ('5', '5'), ('5', '8'), ('8', '11');
insert into `RoleModuleMap` (`RoleId`, `ModuleId`) values ('2', '5'), ('2', '2'), ('5', '2'), ('11', '2'), ('14', '2');
insert into `RoleServiceMap` (`RoleId`, `ServiceId`) values ('5', '5'), ('8', '5'), ('11', '2'), ('14', '8');
