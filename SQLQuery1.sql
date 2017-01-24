
--select * from Members with(nolock)
--select * from ChatLogs with(nolock)
--select * from Information with(nolock) order by memberid
select * from [Tables] with(nolock)

update [Tables] set Name = N'303®à' where id = 33

update Information set tableid = 26 where memberid in (17, 20)
update Information set tableid = 27 where memberid in (22)
update Information set tableid = 30 where memberid in (23)
update Members set Valid = 1 where id in (33)

--- message log
select m.Name, c.[message] from ChatLogs as c with(nolock)
inner join Members as m on c.MemberId = m.id
where m.Valid = 1 or m.Valid is null

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
select Sum(number) as N'»««ÈÁ`¼Æ', Sum(vegetable) as N'¯À­¹' from Information as i with(nolock) 
inner join Members as m with(nolock) on i.memberid = m.id
where m.Valid = 1

--- number per table
select i.tableid, Sum(i.number) from Members as m
	inner join Information as i on m.id = i.memberid
where m.Valid = 1
group by i.tableid

--- talk members
select 
	--c.memberid, 
	distinct i.tableid 
from ChatLogs as c with(nolock)
	inner join Members as m with(nolock) on c.memberid = m.id
	inner join Information as i with(nolock) on m.id = i.memberid
where m.Valid = 1
	and tableid is not null
