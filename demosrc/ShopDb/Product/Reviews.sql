CREATE TABLE [Product].[Reviews]
(
	[ReviewId] BIGINT NOT NULL PRIMARY KEY,
	[ProductId] BIGINT NOT NULL, 
    [Reviewer] NVARCHAR(50) NULL, 
    [ReviewText] NVARCHAR(MAX) NULL, 
    [ReviewDate] DATE NOT NULL, 
    [Rating] INT NOT NULL, 
    CONSTRAINT [FK_Reviews_ProductDetails] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Details]([ProductId]) 
)
