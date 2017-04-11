--select * from Members with(nolock)
--select * from ChatLogs with(nolock)
--select * from Information with(nolock) order by memberid
--select * from [Tables] with(nolock)

--update [Tables] set Name = N'恩師' where id = 24
--update Information set address = N'台北市中山區松江路152號12樓1202E' where memberid = 47
--update Members set Valid = 1 where id in (148)

select * from Information where tableid = 15 order by memberid
update Information set TableId = 1 where memberid in (1, 2, 5)
update Information set TableId = 303 where memberid between 8 and 16
update Information set TableId = 14 where memberid between 134 and 139
update Information set TableId = 13 where memberid between 129 and 133
update Information set TableId = 12 where memberid between 122 and 128
update Information set TableId = 7 where memberid between 121 and 121
update Information set TableId = 6 where memberid between 120 and 120
update Information set TableId = 2 where memberid between 116 and 119
update Information set TableId = 8 where memberid between 98 and 106
update Information set TableId = 3 where memberid between 91 and 97
update Information set TableId = 1 where memberid between 88 and 90

update Information set TableId = 24 where memberid in (17, 20, 36, 37, 42)
update Information set TableId = 26 where memberid in (25, 27, 43, 52, 58)
update Information set TableId = 1 where memberid = 56
update Information set TableId = 28 where memberid between 82 and 87
update Information set TableId = 28 where memberid = 148
update Information set TableId = 16 where memberid in (77, 76, 75, 41)

update Information set TableId = 22 where memberid in (78, 80, 81)
update Information set TableId = 22 where memberid = 66
update Information set TableId = 23 where memberid in (46, 53, 68, 69, 70)
update Information set TableId = 29 where memberid in (63, 65, 71, 72, 73)

select m.name, i.memberid, i.tableid, i.number
--, i.[address], * 
from Information as i with(nolock) 
	inner join Members m with(nolock) on i.memberid = m.id 
where 
memberid not in (3, 6, 7, 19, 28, 59, 54)
and tableid is null
and m.valid = 1
and i.number > 0
order by memberid

--- message log
select m.Name, c.[message] from ChatLogs as c with(nolock)
inner join Members as m on c.MemberId = m.id
where m.Valid = 1 or m.Valid is null
order by LogTime

--- total comming per member 
select 
	m.Name,
	[read],
	number,
	tableId
from Information as i with(nolock)
	inner join Members as m with(nolock) on i.memberid = m.id
where m.Valid = 1 or m.Valid is null
order by m.id

--- total comming
select Sum(number) as N'賓客總數', Sum(vegetable) as N'素食' from Information as i with(nolock) 
	inner join Members as m with(nolock) on i.memberid = m.id
where m.Valid = 1

--- number per table
select i.tableid, Sum(i.number) from Members as m
	inner join Information as i on m.id = i.memberid
where m.Valid = 1
group by i.tableid
order by CONVERT(int, tableid)

--- list roster per table
select 
	i.tableid,
	i.number,
	m.name 
from Members as m with(nolock)
	inner join Information as i with(nolock) on m.id = i.memberid
where m.Valid = 1
	and i.number > 0
	and i.tableid is not null
order by CONVERT(int, i.tableid)

--- have no setting table
select 
	i.tableid,
	m.id,
	m.name,
	i.number
from Members as m
	inner join Information as i on m.id = i.memberid
where m.Valid = 1
	and i.tableid is null
	and i.number > 0
order by CONVERT(int, m.id)

--- talk members
select 
	--c.memberid, 
	distinct i.tableid 
from ChatLogs as c with(nolock)
	inner join Members as m with(nolock) on c.memberid = m.id
	inner join Information as i with(nolock) on m.id = i.memberid
where m.Valid = 1
	and tableid is not null