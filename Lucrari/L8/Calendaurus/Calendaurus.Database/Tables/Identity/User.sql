CREATE TABLE [dbo].[User]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [TelephoneNumber] VARCHAR(29) NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]), 
    CONSTRAINT [UK_User_Email] UNIQUE ([Email]) 
)
