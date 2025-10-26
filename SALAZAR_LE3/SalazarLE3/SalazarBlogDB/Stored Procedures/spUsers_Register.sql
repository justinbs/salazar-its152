CREATE PROCEDURE dbo.spUsers_Register
    @Username  NVARCHAR(16),
    @Password  NVARCHAR(16),
    @FirstName NVARCHAR(50),
    @LastName  NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Users (Username, Password, FirstName, LastName)
    VALUES (@Username, @Password, @FirstName, @LastName);

    SELECT SCOPE_IDENTITY() AS NewId;
END
GO
