CREATE VIEW [Product].[Summaries]
	AS 
	
	SELECT P.[ProductId], P.[Name], P.[Description], P.[Price], AVG(CAST(R.[Rating] as decimal(3,2))) AS Rating
	FROM [Product].[Details] P 
	LEFT JOIN [Product].[Reviews] R ON P.[ProductId] = R.[ProductId]
	GROUP BY P.[ProductId], P.[Name], P.[Description], P.[Price]