CREATE PROCEDURE dbo.spPosts_Create
 @UserId INT, @Title NVARCHAR(150), @Body NVARCHAR(MAX), @DateCreated DATETIME2
AS
BEGIN
  SET NOCOUNT ON;
  INSERT INTO dbo.Posts(UserId, Title, Body, DateCreated)
  VALUES(@UserId, @Title, @Body, @DateCreated);
END
GO
