CREATE TABLE [dbo].[Year]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [AcademicPromotionName] NCHAR(9) NOT NULL, 
    [FacultyName] NVARCHAR(200) NOT NULL, 
    [Year] TINYINT NOT NULL, 
    CONSTRAINT [UK_Year_NameFacultyNameYear] UNIQUE ([AcademicPromotionName],[FacultyName],[Year]), 
    CONSTRAINT [PK_Year] PRIMARY KEY ([Id]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The academic years like 2018-2022',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Year',
    @level2type = N'COLUMN',
    @level2name = 'AcademicPromotionName'