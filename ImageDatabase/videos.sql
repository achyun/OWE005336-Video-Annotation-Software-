CREATE TABLE [dbo].[videos]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [filepath] CHAR(100) NOT NULL, 
    [labelling_complete] BIT NOT NULL DEFAULT 0, 
    [sensor_type] INT NOT NULL DEFAULT -1, 
    [label_id] INT NOT NULL DEFAULT -1, 
    [tags] NVARCHAR(100) NOT NULL DEFAULT ''
)
