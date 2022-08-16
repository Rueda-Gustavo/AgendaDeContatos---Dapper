IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categorias] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] varchar(50) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Contatos] (
    [Id] int NOT NULL IDENTITY,
    [Celular] varchar(20) NULL,
    [Telefone] varchar(20) NULL,
    [Nome] varchar(50) NULL,
    [Endereco] varchar(50) NULL,
    [NumeroCasa] varchar(20) NULL,
    [Email] varchar(50) NULL,
    [Profissao] nvarchar(max) NULL,
    [DataCadastro] datetime2 NULL,
    [Descricao] nvarchar(max) NULL,
    [IdCategoria] int NOT NULL,
    CONSTRAINT [PK_Contatos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Contatos_Categorias_IdCategoria] FOREIGN KEY ([IdCategoria]) REFERENCES [Categorias] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Contatos_IdCategoria] ON [Contatos] ([IdCategoria]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220604121931_Initial-database', N'6.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Contatos]') AND [c].[name] = N'Descricao');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Contatos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Contatos] DROP COLUMN [Descricao];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Contatos]') AND [c].[name] = N'Profissao');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Contatos] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Contatos] ALTER COLUMN [Profissao] varchar(20) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220604122527_AjustandoCampoContatos', N'6.0.5');
GO

COMMIT;
GO

