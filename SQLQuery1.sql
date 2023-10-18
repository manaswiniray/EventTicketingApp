create database EventTicketingApp;

create table EventTickets
(
	 EventId int identity primary key,
	 EventName varchar(80),
	 Price decimal
);


select * from EventTickets;

insert into EventTickets(EventName,Price)
	values('AnubhavComedy',6900),('ShreyaNights',7500);

