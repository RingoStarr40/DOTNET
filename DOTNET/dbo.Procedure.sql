CREATE PROCEDURE [dbo].[AddFile]
    @Name nvarchar(50),
    @Date Date,
	@UserId int,
	@Type nvarchar(10)
AS
    INSERT INTO File (name, date, userid, type)
    VALUES (@name, @age)
  
    SELECT SCOPE_IDENTITY()
GO
