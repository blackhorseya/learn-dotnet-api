insert into `Roles` (`Name`) values ('admin'), ('qa'), ('dev'), ('op');
insert into `Groups` (`Name`) values ('user1'), ('user2'), ('user3'), ('user4');
insert into `GroupRole` (`GroupId`,`RoleId`) value ('1', '1'), ('2', '2'), ('3', '3'), ('3', '4');