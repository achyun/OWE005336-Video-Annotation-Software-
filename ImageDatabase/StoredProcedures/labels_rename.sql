CREATE PROCEDURE [dbo].[labels_rename]
	@param1 int,
	@param2 char
AS
	UPDATE label_trees SET name = @param2 WHERE id = @param1
RETURN 0
