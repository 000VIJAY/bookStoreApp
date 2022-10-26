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

create table Book
(
bookId int primary key identity,
bookimg varchar(600) ,
Rating int ,
RatingCount int, 
bookName varchar(60) not null,
[Description] varchar(2000),
AuthorName varchar(60) not null,
bookOriginalPrice money not null,
bookDiscountedPrice money not null,
bookQuantity int
)

create procedure spAddBook 
@bookimg varchar(600) ,
@Rating int ,
@RatingCount int, 
@bookName varchar(60) ,
@Description varchar(2000),
@AuthorName varchar(60),
@bookOriginalPrice money,
@bookDiscountedPrice money,
@bookQuantity int
as 
begin
insert into dbo.Book(bookimg,Rating,RatingCount,bookName,Description,AuthorName,bookOriginalPrice,bookDiscountedPrice,bookQuantity) 
values(@bookimg,@Rating,@RatingCount,@bookName,@Description,@AuthorName,@bookOriginalPrice,@bookDiscountedPrice,@bookQuantity)
end

create procedure spDeleteBook 
@bookId int
as 
begin
delete from dbo.Book  where bookId = @bookId
end

select * from Book 

select * from Admin 

exec spDeleteBook 3
drop proc spUpdateBook
create procedure spUpdateBook 
@bookId int,
@bookimg varchar(600),
@Rating int,
@RatingCount int, 
@bookName varchar(60) ,
@Description varchar(2000),
@AuthorName varchar(60),
@bookOriginalPrice money,
@bookDiscountedPrice money,
@bookQuantity int
as 
begin
update dbo.Book
set bookimg=@bookimg,Rating=@Rating,RatingCount=@RatingCount,bookName =@bookName,Description=@Description,AuthorName=@AuthorName,
bookOriginalPrice=@bookOriginalPrice,bookDiscountedPrice=@bookDiscountedPrice,bookQuantity = @bookQuantity where bookId = @bookId

end
--(bookimg,Rating,RatingCount,bookName,Description,AuthorName,bookOriginalPrice,bookDiscountedPrice,bookQuantity) 

exec spUpdateBook 2,'' 