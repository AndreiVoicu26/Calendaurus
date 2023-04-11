CREATE TABLE [dbo].[User]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(320) NOT NULL, 
    [Address] NVARCHAR(500) NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]), 
)
