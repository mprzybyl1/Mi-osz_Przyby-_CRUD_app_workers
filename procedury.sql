
USE school
GO


CREATE PROCEDURE ADD_P 
	@id int, @f_name varchar(30), @l_name varchar(30), @city  varchar(50), @dep  varchar(60),
	@done int output
AS
begin
	declare @err int = 0

	begin transaction
		if exists (select * from Workers where id = @id )
		begin
			set @err = 1
		end
		else
		begin
			INSERT INTO Workers VALUES (@id, @f_name, @l_name, @city, @dep)
			set @err += @@error
		end

		if @err = 0
		begin
			commit transaction
			set @done = 1
		end
		else
		begin
			rollback transaction
			set @done = 0
		end
end
GO

CREATE PROCEDURE UPDATE_P
	@id int, @f_name varchar(30), @l_name varchar(30), @city  varchar(50), @dep  varchar(60),
	@done int output
AS
begin
	declare @err int = 0

	begin transaction
		if not exists (select * from Workers where id = @id )
		begin
			set @err = 1
		end
		else
		begin
			UPDATE Workers SET firstName = @f_name, lastName = @l_name, city = @city, department = @dep
				WHERE id = @id
			set @err += @@error
		end

		if @err = 0
		begin
			commit transaction
			set @done = 1
		end
		else
		begin
			rollback transaction
			set @done = 0
		end
end
GO

CREATE PROCEDURE DELETE_P
	@id int,
	@done int output
AS
begin
	declare @err int = 0

	begin transaction
		if not exists (select * from Workers where id = @id )
		begin
			set @err = 1
		end
		else
		begin
			DELETE FROM Workers WHERE id = @id;
			set @err += @@error
		end

		if @err = 0
		begin
			commit transaction
			set @done = 1
		end
		else
		begin
			rollback transaction
			set @done = 0
		end
end
GO


select * from Workers
