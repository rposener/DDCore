CREATE PROCEDURE [ProductSummary].[List]
AS

	;with Rating_CTE as
	(
		SELECT [ProductId], CAST(AVG(1.0*[Rating]) AS decimal(2,1)) AS Rating
		FROM [Product].[Reviews]
		GROUP BY [ProductId]
	)

	SELECT [Details].[ProductId], [Name], [Description], [Price], [Rating_CTE].[Rating]
	FROM [Product].[Details] 
	LEFT JOIN [Rating_CTE] ON [Details].[ProductId] = [Rating_CTE].[ProductId]
	
RETURN 0
