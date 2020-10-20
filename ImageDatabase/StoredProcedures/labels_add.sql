CREATE PROCEDURE [dbo].[labels_add]
	@param1 char
AS
	INSERT INTO label_trees (label_trees.name) VALUES (@param1)
RETURN 0
