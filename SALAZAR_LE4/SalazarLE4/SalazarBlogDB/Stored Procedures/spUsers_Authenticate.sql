CREATE PROCEDURE [dbo].[spUsers_Authenticate]
    @Username NVARCHAR(16),
    @Password NVARCHAR(16)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1) [Id], [Username], [Password], [FirstName], [LastName]
    FROM [dbo].[Users]
    WHERE [Username] = @Username AND [Password] = @Password;
END
GO
