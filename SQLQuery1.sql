use [proy27_married]

select * from Members with(nolock)
select * from ChatLogs with(nolock)
select * from Information with(nolock)

select * from __MigrationHistory with(nolock)

delete Information where hash = '00000000-0000-0000-0000-000000000000'
delete Information where hash = '4945F081-48F8-411B-904F-07E6BBF87B6B'


delete __MigrationHistory

drop table Members
drop table ChatLogs
drop table Information

truncate table Members
truncate table ChatLogs

insert Members values(N'紀伯喬', newid())
insert Members values(N'陳玉雯')
insert Information values('6E9063CF-2BF0-4193-ABF5-9EC8324474CB', 1, 1, 1, 1, 0, 0, null, 0, 0)

update Information set address = N'台北市景華街22巷6號5樓', bus = 1, HSR = 1, write = 'test1', [read] = 'test2' where memberid = 1