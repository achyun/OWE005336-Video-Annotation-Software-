CREATE TABLE [dbo].[label_trees]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [name] NVARCHAR(50) NOT NULL, 
    [parent_id] INT NOT NULL, 
    [lft] INT NOT NULL DEFAULT 0, 
    [rgt] INT NOT NULL DEFAULT 0
)
