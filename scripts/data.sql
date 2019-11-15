-- meta data
insert into `Groups` (`Name`) values ('pls'), ('wfbss'), ('cas');
insert into `Roles` (`Name`) values ('admin'), ('wfbss_manager'), ('wfbss_director'), ('ddi_manager'), ('tippingpoint_manager');
insert into `Modules` (`Name`) values ('key'), ('management');
insert into `Services` (`Name`) values ('ddi'), ('wfbss'), ('tippingpoint');

-- relationship
insert into `GroupRoleMap` (`GroupId`,`RoleId`) values ('1', '1'), ('2', '2'), ('2', '3'), ('3', '4');
insert into `RoleModuleMap` (`RoleId`, `ModuleId`) values ('1', '2'), ('2', '1'), ('4', '1'), ('5', '1');
insert into `RoleServiceMap` (`RoleId`, `ServiceId`) values ('2', '2'), ('3', '2'), ('4', '1'), ('5', '3');
