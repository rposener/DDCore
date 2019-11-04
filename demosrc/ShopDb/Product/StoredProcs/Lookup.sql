CREATE PROCEDURE [Product].[Lookup]
	@ProductId int
AS

	SELECT [ProductId], [Name], [Description], [Price] 
	FROM [Product].[Details]
	WHERE [ProductId] = @ProductId

RETURN 0
