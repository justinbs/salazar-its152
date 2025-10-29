CREATE PROCEDURE dbo.spPosts_Get @Id INT
AS
BEGIN
  SET NOCOUNT ON;
  SELECT TOP(1) Id, UserId, Title, Body, DateCreated
  FROM dbo.Posts WHERE Id = @Id;
END
GO
