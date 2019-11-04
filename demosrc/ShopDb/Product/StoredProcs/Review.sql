CREATE PROCEDURE [Product].[Review]
	@ProductId int,
	@Reviewer NVARCHAR(50), 
    @ReviewText NVARCHAR(MAX), 
    @ReviewDate DATE, 
    @Rating INT,
	@ReviewId INT OUTPUT
AS

SET @ReviewId = (
	SELECT TOP 1 [ReviewId] 
	FROM [Product].[Reviews] 
	WHERE [ProductId] = @ProductId AND [Reviewer] = @Reviewer AND [ReviewDate] = @ReviewDate);

IF @ReviewId <> NULL
	BEGIN
	UPDATE [Product].[Reviews]
	SET [ReviewText] = @ReviewText, [Rating] = @Rating
	WHERE [ProductId] = @ProductId AND [Reviewer] = @Reviewer AND [ReviewDate] = @ReviewDate
	END
ELSE
	BEGIN
	INSERT INTO [Product].[Reviews] ([ProductId],[Reviewer],[ReviewText],[ReviewDate],[Rating])
	VALUES (@ProductId,@Reviewer,@ReviewText,@ReviewDate,@Rating)
	SET @ReviewId = SCOPE_IDENTITY();
	END
RETURN 0
