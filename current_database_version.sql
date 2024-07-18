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

CREATE TABLE [Projects] (
    [ProjectId] int NOT NULL IDENTITY,
    [ProjectName] nvarchar(100) NOT NULL,
    [ProjectDescription] nvarchar(255) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([ProjectId])
);
GO

CREATE TABLE [Roles] (
    [RoleId] int NOT NULL IDENTITY,
    [RoleName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
);
GO

CREATE TABLE [Tasks] (
    [TaskId] int NOT NULL IDENTITY,
    [TaskName] nvarchar(100) NOT NULL,
    [TaskDescription] nvarchar(255) NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [ProjectId] int NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY ([TaskId]),
    CONSTRAINT [FK_Tasks_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Username] nvarchar(100) NOT NULL,
    [Password] nvarchar(100) NOT NULL,
    [FullName] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProjectUser] (
    [ProjectId] int NOT NULL,
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_ProjectUser] PRIMARY KEY ([ProjectId], [UserId], [RoleId]),
    CONSTRAINT [FK_ProjectUser_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectUser_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectUser_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [TaskUser] (
    [TaskId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_TaskUser] PRIMARY KEY ([TaskId], [UserId]),
    CONSTRAINT [FK_TaskUser_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([TaskId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TaskUser_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_ProjectUser_RoleId] ON [ProjectUser] ([RoleId]);
GO

CREATE INDEX [IX_ProjectUser_UserId] ON [ProjectUser] ([UserId]);
GO

CREATE INDEX [IX_Tasks_ProjectId] ON [Tasks] ([ProjectId]);
GO

CREATE INDEX [IX_TaskUser_UserId] ON [TaskUser] ([UserId]);
GO

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240711092515_InitialCreate_V0', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240711164155_CreateDatabase_V1', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Projects] ADD [StatusProject] nvarchar(max) NOT NULL DEFAULT N'Đang thực hiện';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240715151928_AddStatusProjectToProject', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [ImageUrl] nvarchar(255) NOT NULL DEFAULT N'E:/LTWebNC_Ki3/BTL/PojectManagement_11_07/PojectManagement_11_07/wwwroot/image/default-avater.png';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240717053305_AddImageUrlToUsers', N'6.0.8');
GO

COMMIT;
GO

