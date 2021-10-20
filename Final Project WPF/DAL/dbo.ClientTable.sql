CREATE TABLE [dbo].[Table_Client]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [f_Name] NCHAR(50) NOT NULL, 
    [l_Name] NCHAR(50) NOT NULL, 
    [Birth] DATETIME NOT NULL, 
    [is_male] BIT NOT NULL, 
    [phone] NCHAR(10) NOT NULL
)
