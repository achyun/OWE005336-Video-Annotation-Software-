CREATE TABLE [dbo].[images]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [filepath] CHAR(100) NOT NULL, 
    [labelling_complete] BIT NOT NULL DEFAULT 0, 
    [sensor_type] INT NOT NULL DEFAULT -1, 
    [label_id] INT NOT NULL DEFAULT -1, 
    [tags] NVARCHAR(100) NOT NULL DEFAULT '', 
    [created_by] CHAR(30) NOT NULL DEFAULT CURRENT_USER, 
    [created_date] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [modified_by] CHAR(30) NOT NULL DEFAULT CURRENT_USER, 
    [modified_date] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [completed_by] CHAR(30) NULL, 
    [completed_date] DATETIME NULL 
)

GO
