CREATE TABLE [Product].[Details]
(
	[ProductId] BIGINT NOT NULL DEFAULT NEXT VALUE FOR [Product].[DetailsSequence], 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] MONEY NOT NULL, 
    CONSTRAINT [PK_ProductDetails] PRIMARY KEY ([ProductId])
)
