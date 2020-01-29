CREATE TABLE [Cart].[Contents]
(
	[CartId] BIGINT NOT NULL , 
    [ProductId] BIGINT NOT NULL , 
    [Price] MONEY NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT (1),
	CONSTRAINT [FK_Contents_CartDetails] FOREIGN KEY ([CartId]) REFERENCES [Cart].[Details]([CartId]), 
	CONSTRAINT [FK_Contents_ProductDetails] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Details]([ProductId]), 
    CONSTRAINT [PK_CartContents] PRIMARY KEY ([CartId],[ProductId]) 
)
