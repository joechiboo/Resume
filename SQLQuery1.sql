use [proy27_married]

select * from Members with(nolock)
select * from ChatLogs with(nolock)
--select * from Devices with(nolock)
select * from Information with(nolock)

select * from __MigrationHistory with(nolock)

delete __MigrationHistory

drop table Members
drop table ChatLogs
drop table Devices
drop table Information

truncate table Members
truncate table ChatLogs

insert Members values(N'¬ö§B³ì', newid())
insert Members values(N'³¯¥É¶²')
insert Information values('6E9063CF-2BF0-4193-ABF5-9EC8324474CB', 1, 1, 1, 1, 0, 0, null, 0, 0)