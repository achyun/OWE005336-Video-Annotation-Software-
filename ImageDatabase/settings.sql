CREATE TABLE [dbo].[settings]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [setting_name] NVARCHAR(20) NOT NULL, 
    [setting_value] NVARCHAR(100) NOT NULL
)
