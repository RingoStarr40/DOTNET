CREATE TABLE [dbo].[File] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NOT NULL,
    [Date]   DATE          NOT NULL,
    [UserId] INT           NOT NULL,
    [Type]   NVARCHAR (10)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_File_ToUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK611DA7F81D8052CB] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

