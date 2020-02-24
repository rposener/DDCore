﻿CREATE TABLE [Cart].[Details]
(
	[CartId] BIGINT NOT NULL  DEFAULT NEXT VALUE FOR [Cart].[CartSequence], 
    [Created] DATETIME2 NOT NULL DEFAULT (SYSUTCDATETIME()), 
    [CartIdentifier] NVARCHAR(50) NOT NULL, 
    [RowVer] ROWVERSION NOT NULL, 
    CONSTRAINT [PK_Details] PRIMARY KEY ([CartId])
)
