CREATE TABLE [Product].[Details]
(
	[ProductId] BIGINT NOT NULL , 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] MONEY NOT NULL, 
    CONSTRAINT [PK_ProductDetails] PRIMARY KEY ([ProductId])
)
