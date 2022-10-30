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


exec spDeleteBook 3
create procedure spUpdateBook 
@bookId int,
@bookimg varchar(600),
@Rating int,
@RatingCount int, 
@bookDiscountedPrice money,
@bookQuantity int
as 
begin
update dbo.Book
set bookimg=@bookimg,Rating=@Rating,RatingCount=@RatingCount,
bookDiscountedPrice=@bookDiscountedPrice,bookQuantity = @bookQuantity where bookId = @bookId

end
--(bookimg,Rating,RatingCount,bookName,Description,AuthorName,bookOriginalPrice,bookDiscountedPrice,bookQuantity) 

exec spUpdateBook 3,'tbm=isch&sa=X&ved=2ahUKEwjE74Psyf36AhWU7HMBHa1DAiMQ_AUoAXoECAIQAw#imgrc=BD1Mn-7CrhkJeM',4,67,600,200 


create table cart
(cartId int Primary Key Identity,
	Quantity int not null,
	userId int not null Foreign key references dbo.userRegistration(userId),
	bookId int not null Foreign key references dbo.Book(bookId)
)
select * from cart
create procedure spAddCart
@Quantity int,
@userId int,
@bookId int
as
begin
insert into dbo.cart(Quantity,userId,bookId) values ( @Quantity,@userId,@bookId) 
end

create procedure spUpdateCart
@CartId int,
@Quantity int
as
begin
update dbo.cart set Quantity = @Quantity where cartId = @CartId
end



create table WishList
(
wishListId int Primary key Identity,
userId int not null Foreign key references dbo.userRegistration(userId),
bookId int not null Foreign key references dbo.Book(bookId)
)

create proc spAddToWishList
@userId int,
@bookId int
as 
begin 
insert into dbo.WishList(userId,bookId) values(@userId,@bookId);
end


create table AddressType
(typeId int primary key identity,
AddressType varchar(60) not null )

insert into AddressType(AddressType) values ('Home'),('Work'),('Others');

select * from AddressType

create table AddressDetails(
AddressId int primary key identity,
[Address] varchar(500) not null,
City varchar(110) not null,
[State] varchar(110) not null,
typeId int not null Foreign key References AddressType(typeId),
userId int not null Foreign key references dbo.userRegistration(userId)
)

create proc spAddAddress
@Address varchar(max),
@City varchar(110),
@State varchar(110),
@typeId int ,
@userId int
as 
begin 
insert into AddressDetails(Address,City,State,typeId,userId) values (@Address,@City,@State,@typeId,@userId)
end

create proc spUpdateAddress
@AddressId int,
@Address varchar(max),
@City varchar(110),
@State varchar(110),
@typeId int
as 
begin 
update AddressDetails set Address=@Address,City=@City,State=@State,typeId=@typeId where AddressId = @AddressId
end


select * from AddressDetails
select * from dbo.cart
select * from Book 
select * from userRegistration
select * from Admin
select * from dbo.[Order]

create table [Order]
(orderId int Primary key Identity,
orderDate dateTime2 not null,
totalPrice money not null,
AddressId int not null Foreign key references dbo.AddressDetails(AddressId),
cartId int not null Foreign key references dbo.cart(cartId),
bookId int not null Foreign key references dbo.Book(bookId),
userId int not null Foreign key references dbo.userRegistration(userId),
)

select * from dbo.[Order]

create procedure spTotalPriceCart
@cartId int
as 
begin 
select totalPrice=c.Quantity* b.bookDiscountedPrice from dbo.cart c inner join dbo.Book b on c.bookId = b.bookId  where c.cartId = @cartId
end

exec spTotalPriceCart 2

create proc spAddOrder
@orderDate dateTime2,
@totalPrice money,
@AddressId int ,
@cartId int,
@bookId int,
@userId int
as
begin 
insert into  [Order](orderDate,totalPrice,AddressId,cartId,bookId,userId) values (@orderDate,@totalPrice,@AddressId,@cartId,@bookId,@userId)
end

create table feedback(
feedbackId int Primary key identity,
Rating int not null,
comment varchar(2000),
bookId int not null Foreign key references dbo.Book(bookId),
userId int not null Foreign key references dbo.userRegistration(userId)
)

create proc spAddFeedback
@Rating int,
@comment varchar(2000),
@bookId int,
@userId int
as 
begin 
insert into dbo.feedback(Rating,comment,bookId,userId) values(@Rating,@comment,@bookId,@userId)
end

create proc spGetFeedback
@bookId int
as 
begin 
select * from dbo.feedback where bookId = @bookId 
end

create procedure spGetOrder
@userId int
as 
Begin
select o.userId, o.orderId,o.orderDate,o.totalPrice,o.bookId,b.bookImg,b.bookName,
b.bookDiscountedPrice,c.cartId,c.Quantity,a.AddressId,a.Address,a.City,a.State,a.typeId from dbo.[Order] o 
inner join dbo.Book b on o.bookId = b.bookId 
inner join dbo.cart c on o.cartId = c.cartId inner join dbo.AddressDetails a on o.AddressId = a.AddressId where o.userId = @userId
end
exec spGetOrder 1

drop proc spGetOrder

select * from feedback