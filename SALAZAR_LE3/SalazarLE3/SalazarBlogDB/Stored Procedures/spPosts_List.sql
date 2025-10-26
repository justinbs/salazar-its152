CREATE PROCEDURE dbo.spPosts_List
AS
BEGIN
  SET NOCOUNT ON;
  SELECT p.Id, p.Title, u.Username AS Author, p.DateCreated
  FROM dbo.Posts p
  JOIN dbo.Users u ON u.Id = p.UserId
  ORDER BY p.DateCreated DESC;
END
GO
