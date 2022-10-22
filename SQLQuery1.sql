create database bookStore

use bookStore

create table userRegistration
(userId int Primary key identity,
[Name] varchar(50) not null,
email varchar(50) not null unique,
[password] varchar(50) not null,
PhoneNumber bigint not null unique
)
select * from userRegistration

create procedure spRegisterUser
@Name varchar(50),
@email varchar(50),
@password varchar(50),
@PhoneNumber bigint
as 
begin
insert into dbo.userRegistration(Name,email,password,PhoneNumber) values(@Name,@email,@password,@PhoneNumber)
end
exec spRegisterUser 'Rahul','Vijay@gmail.com','Vijay@123',9988776655

create procedure spLoginUser
@email varchar(50),
@password varchar(50)
as 
begin 
begin try
	select u.email,u.password from userRegistration u where u.email =@email and u.password = @password
end try
begin catch
print ERROR_MESSAGE()
end catch
end
exec spLoginUser 'Vija@gmail.com' ,'Vijay@123' 
select userId from dbo.userRegistration where email ='nitish@gmail.com'

create procedure spForgotPasswordUser
@email varchar(50)
as 
begin 
begin try
	select u.email from userRegistration u where u.email =@email 
end try
begin catch
print ERROR_MESSAGE()
end catch
end

create procedure spResetPasswordUser
@password varchar(50),
@userId int
as 
begin 
begin try
	update userRegistration set password = @password where userId = @userId 
end try
begin catch
print ERROR_MESSAGE()
end catch
end
