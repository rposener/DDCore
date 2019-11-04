CREATE TABLE [Product].[Reviews]
(
	[ReviewId] INT IDENTITY NOT NULL PRIMARY KEY,
	[ProductId] INT NOT NULL, 
    [Reviewer] NVARCHAR(50) NULL, 
    [ReviewText] NVARCHAR(MAX) NULL, 
    [ReviewDate] DATE NOT NULL, 
    [Rating] INT NOT NULL, 
    CONSTRAINT [FK_Reviews_ProductDetails] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Details]([ProductId]) 
)
