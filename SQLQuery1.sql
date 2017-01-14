use [marry]

select * from Members with(nolock)
select * from ChatLogs with(nolock)
select * from Information with(nolock)


--- message log
select m.Name, c.[message] from ChatLogs as c with(nolock)
inner join Members as m on c.MemberId = m.id
where m.Valid = 1 or m.Valid is null

--- total comming per member 
select 
	m.Name,
	[read],
	number
from Information as i with(nolock)
inner join Members as m with(nolock) on i.memberid = m.id
where m.Valid = 1 or m.Valid is null
order by m.id

--- total comming
select Sum(number) from Information as i with(nolock) 
inner join Members as m with(nolock) on i.memberid = m.id
where m.Valid = 1