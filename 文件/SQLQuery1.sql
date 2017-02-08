
--select * from Members with(nolock)
--select * from ChatLogs with(nolock)
--select * from Information with(nolock) order by memberid
--select * from [Tables] with(nolock)

--update [Tables] set Name = N'恩師' where id = 24
--update Information set tableid = 31 where tableid = 29
--update Members set Valid = 1 where id = 52

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
select Sum(number) as N'賓客總數', Sum(vegetable) as N'素食' from Information as i with(nolock) 
	inner join Members as m with(nolock) on i.memberid = m.id
where m.Valid = 1

--- number per table
select i.tableid, Sum(i.number) from Members as m
	inner join Information as i on m.id = i.memberid
where m.Valid = 1
group by i.tableid

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
order by i.tableid

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

--- talk members
select 
	--c.memberid, 
	distinct i.tableid 
from ChatLogs as c with(nolock)
	inner join Members as m with(nolock) on c.memberid = m.id
	inner join Information as i with(nolock) on m.id = i.memberid
where m.Valid = 1
	and tableid is not null