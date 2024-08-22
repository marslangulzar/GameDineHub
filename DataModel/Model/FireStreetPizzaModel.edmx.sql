
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/19/2024 23:59:01
-- Generated from EDMX file: C:\Users\arslan\Downloads\Game Dine Hub(Mixtape)\Game Dine Hub(Mixtape)\Development\DataModel\Model\FireStreetPizzaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FireStreetPizza];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Fk_SiteProprity_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SiteProprity] DROP CONSTRAINT [Fk_SiteProprity_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[fk_Team_VotingRound]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Team] DROP CONSTRAINT [fk_Team_VotingRound];
GO
IF OBJECT_ID(N'[dbo].[fk_TeamSong_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamSongs] DROP CONSTRAINT [fk_TeamSong_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfo_Lst_Gender]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_UserInfo_Lst_Gender];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfo_Lst_State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_UserInfo_Lst_State];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLoginHistory_To_Users_fk_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLoginHistory] DROP CONSTRAINT [FK_UserLoginHistory_To_Users_fk_UserID];
GO
IF OBJECT_ID(N'[dbo].[FK_UserToRole_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserToRole_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_UserToRole_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserToRole_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[fk_VotiingResult_TeamBy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VotingResult] DROP CONSTRAINT [fk_VotiingResult_TeamBy];
GO
IF OBJECT_ID(N'[dbo].[fk_VotiingResult_TeamTo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VotingResult] DROP CONSTRAINT [fk_VotiingResult_TeamTo];
GO
IF OBJECT_ID(N'[dbo].[Fk_VotingResult_VotingSong]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VotingResult] DROP CONSTRAINT [Fk_VotingResult_VotingSong];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AppSetting]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AppSetting];
GO
IF OBJECT_ID(N'[dbo].[Lst_Gender]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lst_Gender];
GO
IF OBJECT_ID(N'[dbo].[Lst_State]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lst_State];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[SiteProprity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SiteProprity];
GO
IF OBJECT_ID(N'[dbo].[Team]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Team];
GO
IF OBJECT_ID(N'[dbo].[TeamSongs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamSongs];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[UserLoginHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLoginHistory];
GO
IF OBJECT_ID(N'[dbo].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRole];
GO
IF OBJECT_ID(N'[dbo].[VotingResult]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VotingResult];
GO
IF OBJECT_ID(N'[dbo].[VotingRound]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VotingRound];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AppSettings'
CREATE TABLE [dbo].[AppSettings] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Value] nvarchar(3500)  NOT NULL,
    [Label] nvarchar(255)  NULL,
    [Description] nvarchar(500)  NULL
);
GO

-- Creating table 'Lst_Gender'
CREATE TABLE [dbo].[Lst_Gender] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Lst_State'
CREATE TABLE [dbo].[Lst_State] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Abbreviation] nchar(2)  NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int  NOT NULL,
    [RoleName] nvarchar(200)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [Active] bit  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NOT NULL,
    [fk_VotingRoundID] int  NOT NULL
);
GO

-- Creating table 'UserInfoes'
CREATE TABLE [dbo].[UserInfoes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [fk_GenderID] int  NULL,
    [Email] nvarchar(80)  NOT NULL,
    [PasswordHash] nvarchar(255)  NULL,
    [FirstName] varchar(80)  NULL,
    [LastName] varchar(80)  NULL,
    [MiddleName] nvarchar(50)  NULL,
    [PasswordRequestHash] nvarchar(500)  NULL,
    [PasswordRequestDate] datetime  NULL,
    [LoginAttempts] int  NOT NULL,
    [LastPasswordChange] datetime  NULL,
    [MobileNo] nvarchar(20)  NULL,
    [PhoneNo] nvarchar(20)  NULL,
    [PhoneExt] nvarchar(10)  NULL,
    [Fax] nvarchar(20)  NULL,
    [StateID] int  NULL,
    [DOB] datetime  NULL,
    [CreatedBy] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedBy] int  NULL,
    [ModifiedDate] datetime  NOT NULL,
    [Active] bit  NOT NULL,
    [IsNewsLetter] bit  NULL
);
GO

-- Creating table 'UserLoginHistories'
CREATE TABLE [dbo].[UserLoginHistories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [fk_UserId] int  NOT NULL,
    [LoginDateTime] datetime  NOT NULL,
    [LogoutDateTime] datetime  NULL,
    [UserIP] nvarchar(50)  NULL,
    [BrowserName] nvarchar(50)  NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [fk_UserID] int  NOT NULL,
    [fk_RoleID] int  NOT NULL
);
GO

-- Creating table 'VotingResults'
CREATE TABLE [dbo].[VotingResults] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [fk_VotingByTeamID] int  NOT NULL,
    [fk_VotingToTeamID] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [fk_TeamSongID] int  NOT NULL
);
GO

-- Creating table 'VotingRounds'
CREATE TABLE [dbo].[VotingRounds] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetime  NULL,
    [VoatingStart] bit  NULL,
    [VoatingDone] bit  NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'TeamSongs'
CREATE TABLE [dbo].[TeamSongs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [fk_TeamID] int  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SongURL] nvarchar(250)  NOT NULL,
    [VotingDone] bit  NULL,
    [CreatedDate] datetime  NOT NULL,
    [SpotifySongUrl] nvarchar(500)  NULL
);
GO

-- Creating table 'SiteProprities'
CREATE TABLE [dbo].[SiteProprities] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(500)  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'AppSettings'
ALTER TABLE [dbo].[AppSettings]
ADD CONSTRAINT [PK_AppSettings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Lst_Gender'
ALTER TABLE [dbo].[Lst_Gender]
ADD CONSTRAINT [PK_Lst_Gender]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Lst_State'
ALTER TABLE [dbo].[Lst_State]
ADD CONSTRAINT [PK_Lst_State]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [ID] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [PK_Teams]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [PK_UserInfoes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserLoginHistories'
ALTER TABLE [dbo].[UserLoginHistories]
ADD CONSTRAINT [PK_UserLoginHistories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'VotingResults'
ALTER TABLE [dbo].[VotingResults]
ADD CONSTRAINT [PK_VotingResults]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'VotingRounds'
ALTER TABLE [dbo].[VotingRounds]
ADD CONSTRAINT [PK_VotingRounds]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TeamSongs'
ALTER TABLE [dbo].[TeamSongs]
ADD CONSTRAINT [PK_TeamSongs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SiteProprities'
ALTER TABLE [dbo].[SiteProprities]
ADD CONSTRAINT [PK_SiteProprities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [fk_GenderID] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [FK_UserInfo_Lst_Gender]
    FOREIGN KEY ([fk_GenderID])
    REFERENCES [dbo].[Lst_Gender]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfo_Lst_Gender'
CREATE INDEX [IX_FK_UserInfo_Lst_Gender]
ON [dbo].[UserInfoes]
    ([fk_GenderID]);
GO

-- Creating foreign key on [StateID] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [FK_UserInfo_Lst_State]
    FOREIGN KEY ([StateID])
    REFERENCES [dbo].[Lst_State]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfo_Lst_State'
CREATE INDEX [IX_FK_UserInfo_Lst_State]
ON [dbo].[UserInfoes]
    ([StateID]);
GO

-- Creating foreign key on [fk_RoleID] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserToRole_Roles]
    FOREIGN KEY ([fk_RoleID])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserToRole_Roles'
CREATE INDEX [IX_FK_UserToRole_Roles]
ON [dbo].[UserRoles]
    ([fk_RoleID]);
GO

-- Creating foreign key on [fk_VotingRoundID] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [fk_Team_VotingRound]
    FOREIGN KEY ([fk_VotingRoundID])
    REFERENCES [dbo].[VotingRounds]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Team_VotingRound'
CREATE INDEX [IX_fk_Team_VotingRound]
ON [dbo].[Teams]
    ([fk_VotingRoundID]);
GO

-- Creating foreign key on [fk_VotingByTeamID] in table 'VotingResults'
ALTER TABLE [dbo].[VotingResults]
ADD CONSTRAINT [fk_VotiingResult_TeamBy]
    FOREIGN KEY ([fk_VotingByTeamID])
    REFERENCES [dbo].[Teams]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_VotiingResult_TeamBy'
CREATE INDEX [IX_fk_VotiingResult_TeamBy]
ON [dbo].[VotingResults]
    ([fk_VotingByTeamID]);
GO

-- Creating foreign key on [fk_VotingToTeamID] in table 'VotingResults'
ALTER TABLE [dbo].[VotingResults]
ADD CONSTRAINT [fk_VotiingResult_TeamTo]
    FOREIGN KEY ([fk_VotingToTeamID])
    REFERENCES [dbo].[Teams]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_VotiingResult_TeamTo'
CREATE INDEX [IX_fk_VotiingResult_TeamTo]
ON [dbo].[VotingResults]
    ([fk_VotingToTeamID]);
GO

-- Creating foreign key on [fk_UserId] in table 'UserLoginHistories'
ALTER TABLE [dbo].[UserLoginHistories]
ADD CONSTRAINT [FK_UserLoginHistory_To_Users_fk_UserID]
    FOREIGN KEY ([fk_UserId])
    REFERENCES [dbo].[UserInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLoginHistory_To_Users_fk_UserID'
CREATE INDEX [IX_FK_UserLoginHistory_To_Users_fk_UserID]
ON [dbo].[UserLoginHistories]
    ([fk_UserId]);
GO

-- Creating foreign key on [fk_UserID] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserToRole_UserInfo]
    FOREIGN KEY ([fk_UserID])
    REFERENCES [dbo].[UserInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserToRole_UserInfo'
CREATE INDEX [IX_FK_UserToRole_UserInfo]
ON [dbo].[UserRoles]
    ([fk_UserID]);
GO

-- Creating foreign key on [fk_TeamID] in table 'TeamSongs'
ALTER TABLE [dbo].[TeamSongs]
ADD CONSTRAINT [fk_TeamSong_Team]
    FOREIGN KEY ([fk_TeamID])
    REFERENCES [dbo].[Teams]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_TeamSong_Team'
CREATE INDEX [IX_fk_TeamSong_Team]
ON [dbo].[TeamSongs]
    ([fk_TeamID]);
GO

-- Creating foreign key on [fk_TeamSongID] in table 'VotingResults'
ALTER TABLE [dbo].[VotingResults]
ADD CONSTRAINT [Fk_VotingResult_VotingSong]
    FOREIGN KEY ([fk_TeamSongID])
    REFERENCES [dbo].[TeamSongs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'Fk_VotingResult_VotingSong'
CREATE INDEX [IX_Fk_VotingResult_VotingSong]
ON [dbo].[VotingResults]
    ([fk_TeamSongID]);
GO

-- Creating foreign key on [CreatedBy] in table 'SiteProprities'
ALTER TABLE [dbo].[SiteProprities]
ADD CONSTRAINT [Fk_SiteProprity_UserInfo]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[UserInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'Fk_SiteProprity_UserInfo'
CREATE INDEX [IX_Fk_SiteProprity_UserInfo]
ON [dbo].[SiteProprities]
    ([CreatedBy]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------