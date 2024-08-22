USE [master]
GO
/****** Object:  Database [FireStreetLive]    Script Date: 26/08/2021 3:14:04 pm ******/
CREATE DATABASE [FireStreetLive]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FireStreetLive', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\FireStreetLive.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FireStreetLive_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\FireStreetLive_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [FireStreetLive] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FireStreetLive].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FireStreetLive] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FireStreetLive] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FireStreetLive] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FireStreetLive] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FireStreetLive] SET ARITHABORT OFF 
GO
ALTER DATABASE [FireStreetLive] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FireStreetLive] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FireStreetLive] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FireStreetLive] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FireStreetLive] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FireStreetLive] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FireStreetLive] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FireStreetLive] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FireStreetLive] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FireStreetLive] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FireStreetLive] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FireStreetLive] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FireStreetLive] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FireStreetLive] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FireStreetLive] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FireStreetLive] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FireStreetLive] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FireStreetLive] SET RECOVERY FULL 
GO
ALTER DATABASE [FireStreetLive] SET  MULTI_USER 
GO
ALTER DATABASE [FireStreetLive] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FireStreetLive] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FireStreetLive] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FireStreetLive] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FireStreetLive] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'FireStreetLive', N'ON'
GO
ALTER DATABASE [FireStreetLive] SET QUERY_STORE = OFF
GO
USE [FireStreetLive]
GO
/****** Object:  Table [dbo].[AppSetting]    Script Date: 26/08/2021 3:14:09 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](3500) NOT NULL,
	[Label] [nvarchar](255) NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_AppSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lst_Gender]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lst_Gender](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Lst_Gend__3214EC27FD7C9F80] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lst_State]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lst_State](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Abbreviation] [nchar](2) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK__Lst_Stat__3214EC2759001E32] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__Role__8AFACE1AE228B88A] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteProprity]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteProprity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[fk_VotingRoundID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeamSongs]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamSongs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_TeamID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[SongURL] [nvarchar](250) NOT NULL,
	[VotingDone] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[SpotifySongUrl] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_GenderID] [int] NULL,
	[Email] [nvarchar](80) NOT NULL,
	[PasswordHash] [nvarchar](255) NULL,
	[FirstName] [varchar](80) NULL,
	[LastName] [varchar](80) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[PasswordRequestHash] [nvarchar](500) NULL,
	[PasswordRequestDate] [datetime] NULL,
	[LoginAttempts] [int] NOT NULL,
	[LastPasswordChange] [datetime] NULL,
	[MobileNo] [nvarchar](20) NULL,
	[PhoneNo] [nvarchar](20) NULL,
	[PhoneExt] [nvarchar](10) NULL,
	[Fax] [nvarchar](20) NULL,
	[StateID] [int] NULL,
	[DOB] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[IsNewsLetter] [bit] NULL,
 CONSTRAINT [PK__UserInfo__3214EC270AFC0CD3] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UniqueEmail] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginHistory]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_UserId] [int] NOT NULL,
	[LoginDateTime] [datetime] NOT NULL,
	[LogoutDateTime] [datetime] NULL,
	[UserIP] [nvarchar](50) NULL,
	[BrowserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserLoginHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_UserID] [int] NOT NULL,
	[fk_RoleID] [int] NOT NULL,
 CONSTRAINT [PK__UserRole__FA1051FFAA6FB562] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VotingResult]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VotingResult](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_VotingByTeamID] [int] NOT NULL,
	[fk_VotingToTeamID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[fk_TeamSongID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VotingRound]    Script Date: 26/08/2021 3:14:10 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VotingRound](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NULL,
	[VoatingStart] [bit] NULL,
	[VoatingDone] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[SiteProprity]  WITH CHECK ADD  CONSTRAINT [Fk_SiteProprity_UserInfo] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[UserInfo] ([ID])
GO
ALTER TABLE [dbo].[SiteProprity] CHECK CONSTRAINT [Fk_SiteProprity_UserInfo]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [fk_Team_VotingRound] FOREIGN KEY([fk_VotingRoundID])
REFERENCES [dbo].[VotingRound] ([ID])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [fk_Team_VotingRound]
GO
ALTER TABLE [dbo].[TeamSongs]  WITH CHECK ADD  CONSTRAINT [fk_TeamSong_Team] FOREIGN KEY([fk_TeamID])
REFERENCES [dbo].[Team] ([ID])
GO
ALTER TABLE [dbo].[TeamSongs] CHECK CONSTRAINT [fk_TeamSong_Team]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_Lst_Gender] FOREIGN KEY([fk_GenderID])
REFERENCES [dbo].[Lst_Gender] ([ID])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_Lst_Gender]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_Lst_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[Lst_State] ([ID])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_Lst_State]
GO
ALTER TABLE [dbo].[UserLoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginHistory_To_Users_fk_UserID] FOREIGN KEY([fk_UserId])
REFERENCES [dbo].[UserInfo] ([ID])
GO
ALTER TABLE [dbo].[UserLoginHistory] CHECK CONSTRAINT [FK_UserLoginHistory_To_Users_fk_UserID]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserToRole_Roles] FOREIGN KEY([fk_RoleID])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserToRole_Roles]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserToRole_UserInfo] FOREIGN KEY([fk_UserID])
REFERENCES [dbo].[UserInfo] ([ID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserToRole_UserInfo]
GO
ALTER TABLE [dbo].[VotingResult]  WITH CHECK ADD  CONSTRAINT [fk_VotiingResult_TeamBy] FOREIGN KEY([fk_VotingByTeamID])
REFERENCES [dbo].[Team] ([ID])
GO
ALTER TABLE [dbo].[VotingResult] CHECK CONSTRAINT [fk_VotiingResult_TeamBy]
GO
ALTER TABLE [dbo].[VotingResult]  WITH CHECK ADD  CONSTRAINT [fk_VotiingResult_TeamTo] FOREIGN KEY([fk_VotingToTeamID])
REFERENCES [dbo].[Team] ([ID])
GO
ALTER TABLE [dbo].[VotingResult] CHECK CONSTRAINT [fk_VotiingResult_TeamTo]
GO
ALTER TABLE [dbo].[VotingResult]  WITH CHECK ADD  CONSTRAINT [Fk_VotingResult_VotingSong] FOREIGN KEY([fk_TeamSongID])
REFERENCES [dbo].[TeamSongs] ([ID])
GO
ALTER TABLE [dbo].[VotingResult] CHECK CONSTRAINT [Fk_VotingResult_VotingSong]
GO
USE [master]
GO
ALTER DATABASE [FireStreetLive] SET  READ_WRITE 
GO
