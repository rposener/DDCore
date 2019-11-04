CREATE PROCEDURE [Product].[GetReviews]
	@ProductId int
AS
	SELECT [ReviewId],[Reviewer],[ReviewText],[ReviewDate],[Rating]
	FROM [Product].[Reviews]
	WHERE [ProductId] = @ProductId

RETURN 0
