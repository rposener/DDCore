CREATE TABLE [Product].[Reviews]
(
	[ReviewId] BIGINT NOT NULL DEFAULT NEXT VALUE FOR [Product].[ReviewsSequence],
	[ProductId] BIGINT NOT NULL, 
    [Reviewer] NVARCHAR(50) NULL, 
    [ReviewText] NVARCHAR(MAX) NULL, 
    [ReviewDate] DATE NOT NULL, 
    [Rating] INT NOT NULL, 
    CONSTRAINT [FK_Reviews_ProductDetails] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Details]([ProductId]), 
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([ReviewId]) 
)
