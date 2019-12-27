ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [FK_PostGLDetail_PostGLAccount_GLAccountID];

GO

ALTER TABLE [ACC].[PostGLHeader] DROP CONSTRAINT [FK_PostGLHeader_MasterCenter_BatchTypeID];

GO

EXEC sp_rename N'[ACC].[PostGLHeader].[BatchTypeMasterCenterID]', N'PostGLDocumentTypeMasterCenterID', N'COLUMN';

GO

EXEC sp_rename N'[ACC].[PostGLHeader].[BatchTypeID]', N'PostGLDocumentTypeID', N'COLUMN';

GO

EXEC sp_rename N'[ACC].[PostGLHeader].[BatchID]', N'DocumentNo', N'COLUMN';

GO

EXEC sp_rename N'[ACC].[PostGLHeader].[IX_PostGLHeader_BatchTypeID]', N'IX_PostGLHeader_PostGLDocumentTypeID', N'INDEX';

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLHeader]') AND [c].[name] = N'ReferentType');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLHeader] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ACC].[PostGLHeader] ALTER COLUMN [ReferentType] nvarchar(50) NULL;

GO

ALTER TABLE [ACC].[PostGLHeader] ADD [Amount] decimal(18,2) NOT NULL DEFAULT 0.0;

GO

ALTER TABLE [ACC].[PostGLHeader] ADD [DeleteReason] nvarchar(1000) NULL;

GO

ALTER TABLE [ACC].[PostGLHeader] ADD [ExportedTimes] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [ACC].[PostGLHeader] ADD [Fee] decimal(18,2) NOT NULL DEFAULT 0.0;

GO

ALTER TABLE [ACC].[PostGLHeader] ADD [LastExportedDate] int NOT NULL DEFAULT 0;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'WBSNumber');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [WBSNumber] nvarchar(50) NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'UnitNo');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [UnitNo] nvarchar(50) NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'Unit');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [Unit] nvarchar(50) NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'TaxCode');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [TaxCode] nvarchar(50) NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'Street');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [Street] nvarchar(100) NULL;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'Quantity');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [Quantity] nvarchar(50) NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'ProjectNo');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [ProjectNo] nvarchar(50) NULL;

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'ProfitCenter');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [ProfitCenter] nvarchar(50) NULL;

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'PostingKey');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [PostingKey] nvarchar(50) NULL;

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'PostCode');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [PostCode] nvarchar(50) NULL;

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'ObjectNumber');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [ObjectNumber] nvarchar(50) NULL;

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'CustomerName');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [CustomerName] nvarchar(100) NULL;

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'Country');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [Country] nvarchar(100) NULL;

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'CostCenter');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [CostCenter] nvarchar(50) NULL;

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'City');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [City] nvarchar(100) NULL;

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'Assignment');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [Assignment] nvarchar(50) NULL;

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ACC].[PostGLDetail]') AND [c].[name] = N'AccountCode');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [ACC].[PostGLDetail] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [ACC].[PostGLDetail] ALTER COLUMN [AccountCode] nvarchar(50) NULL;

GO

ALTER TABLE [ACC].[PostGLDetail] ADD [PostGLType] nvarchar(50) NULL;

GO

ALTER TABLE [ACC].[PostGLDetail] ADD CONSTRAINT [FK_PostGLDetail_BankAccount_GLAccountID] FOREIGN KEY ([GLAccountID]) REFERENCES [MST].[BankAccount] ([ID]) ON DELETE NO ACTION;

GO

ALTER TABLE [ACC].[PostGLHeader] ADD CONSTRAINT [FK_PostGLHeader_MasterCenter_PostGLDocumentTypeID] FOREIGN KEY ([PostGLDocumentTypeID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191217121607_alter_tb_PostGLHeader_non_kim', N'2.2.2-servicing-10034');

GO

