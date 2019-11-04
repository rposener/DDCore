CREATE TABLE [Cart].[Contents]
(
	[CartId] INT NOT NULL , 
    [ProductId] INT NOT NULL , 
    [Price] MONEY NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT (1),
	CONSTRAINT [FK_Contents_CartDetails] FOREIGN KEY ([CartId]) REFERENCES [Cart].[Details]([CartId]), 
	CONSTRAINT [FK_Contents_ProductDetails] FOREIGN KEY ([ProductId]) REFERENCES [Product].[Details]([ProductId]), 
    CONSTRAINT [PK_CartContents] PRIMARY KEY ([CartId],[ProductId]) 
)
