CREATE TABLE [dbo].[Professor]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [Title] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Professor] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Professor_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [UK_Professor_UserId] UNIQUE([UserId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Lector/Dr/Prof/Ing/Etc',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Professor',
    @level2type = N'COLUMN',
    @level2name = N'Title'