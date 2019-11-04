CREATE PROCEDURE [ProductSummary].[Search]
	@Search nvarchar(256)
AS

	;with Rating_CTE as
	(
		SELECT [ProductId], AVG([Rating]) AS Rating
		FROM [Product].[Reviews]
		GROUP BY [ProductId]
	)

	SELECT [Details].[ProductId], [Name], [Description], [Price], [Rating_CTE].[Rating]
	FROM [Product].[Details] 
	LEFT JOIN [Rating_CTE] ON [Details].[ProductId] = [Rating_CTE].[ProductId]
	WHERE [Name] LIKE Concat('%',@Search,'%') OR [Description] LIKE Concat('%',@Search,'%')

RETURN 0
