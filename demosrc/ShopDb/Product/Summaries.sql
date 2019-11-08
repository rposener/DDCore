CREATE VIEW [Product].[Summaries]
	AS 
	
	SELECT P.[ProductId], P.[Name], P.[Description], P.[Price], AVG(R.[Rating]) AS Rating
	FROM [Product].[Details] P 
	INNER JOIN [Product].[Reviews] R ON P.[ProductId] = R.[ProductId]
	GROUP BY P.[ProductId], P.[Name], P.[Description], P.[Price]