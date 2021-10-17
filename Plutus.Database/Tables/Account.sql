CREATE TABLE [plutus].[Transaction]
(
    [Id]                UNIQUEIDENTIFIER   NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [Title]             NVARCHAR(140)      NOT NULL,
    [Description]       NVARCHAR(500)      NOT NULL,
    [Balance]           DECIMAL            NOT NULL,

    
    [Created]           DATETIME           NOT NULL,
    [CreatedBy]         NVARCHAR(25)       NOT NULL,
    [LastModified]      DATETIME           NOT NULL,
    [LastModifiedBy]    NVARCHAR(25)       NOT NULL
);