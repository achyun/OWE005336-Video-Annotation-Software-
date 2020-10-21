CREATE TABLE [dbo].[bbox_labels]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [image_id] INT NOT NULL, 
    [label_id] INT NOT NULL DEFAULT -1, 
    [location_x] INT NOT NULL, 
    [location_y] INT NOT NULL, 
    [size_width] INT NOT NULL, 
    [size_height] INT NOT NULL, 
    [truncated] BIT NOT NULL DEFAULT 0, 
    [occluded] BIT NOT NULL DEFAULT 0, 
    [out_of_focus] BIT NOT NULL DEFAULT 0
)
