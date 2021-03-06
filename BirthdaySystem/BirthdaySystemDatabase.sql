/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [BirthdayPresentVotingSystem]    Script Date: 31-May-18 12:30:49 PM ******/
CREATE DATABASE [BirthdayPresentVotingSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BirthdayPresentVotingSystem', FILENAME = N'C:\Programs\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\BirthdayPresentVotingSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BirthdayPresentVotingSystem_log', FILENAME = N'C:\Programs\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\BirthdayPresentVotingSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BirthdayPresentVotingSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET  MULTI_USER 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET QUERY_STORE = OFF
GO
USE [BirthdayPresentVotingSystem]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [BirthdayPresentVotingSystem]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 31-May-18 12:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[PasswordHash] [nchar](64) NOT NULL,
	[PasswordSalt] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Presents]    Script Date: 31-May-18 12:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Presents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Presents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VotingPresents]    Script Date: 31-May-18 12:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VotingPresents](
	[VotingId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PresentId] [int] NOT NULL,
 CONSTRAINT [PK_VotingPresents] PRIMARY KEY CLUSTERED 
(
	[VotingId] ASC,
	[EmployeeId] ASC,
	[PresentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Votings]    Script Date: 31-May-18 12:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Votings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatorId] [int] NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[PresentId] [int] NULL,
	[ClosingDate] [date] NOT NULL,
	[IsClosed] [bit] NOT NULL,
 CONSTRAINT [PK_Votings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [Username], [Name], [BirthDate], [PasswordHash], [PasswordSalt]) VALUES (1, N'sherlock', N'Sherlock Holmes', CAST(N'1992-06-05' AS Date), N'b8fe18df23194e299dbaf57f3b34748e41870eae36b843381567ec06216b9bea', N'HZa1oMgqQB')
INSERT [dbo].[Employees] ([Id], [Username], [Name], [BirthDate], [PasswordHash], [PasswordSalt]) VALUES (2, N'holmes', N'Sherlock Holmes', CAST(N'1992-07-10' AS Date), N'934735d54622fa19468705cc055098f1201b0d1d39bb4663c06befa2da329e68', N'jnWBCrmU4K')
INSERT [dbo].[Employees] ([Id], [Username], [Name], [BirthDate], [PasswordHash], [PasswordSalt]) VALUES (3, N'doctor', N'Doctor Watson', CAST(N'1992-08-15' AS Date), N'86acc3a35c2030e6f8b43e5164e942583e238b17e8f9ea73e56262d960443e43', N'AsnsH2HK5G')
INSERT [dbo].[Employees] ([Id], [Username], [Name], [BirthDate], [PasswordHash], [PasswordSalt]) VALUES (4, N'watson', N'Doctor Watson', CAST(N'1992-09-20' AS Date), N'8ae2d3e776fdd3a816f53efb802e9150daeaaf71829444675b3e13704fc8c7b3', N'aiUW4M8f1O')
INSERT [dbo].[Employees] ([Id], [Username], [Name], [BirthDate], [PasswordHash], [PasswordSalt]) VALUES (5, N'irene', N'Irene Adler', CAST(N'1992-10-25' AS Date), N'b6af8bf053aa36474934a48d66510ed6ffdd0b2f2f991732df225dcdf4f7dbd9', N'lUb4Ga8Jbr')
INSERT [dbo].[Employees] ([Id], [Username], [Name], [BirthDate], [PasswordHash], [PasswordSalt]) VALUES (6, N'adler', N'Irene Adler', CAST(N'1992-11-30' AS Date), N'5b654dc6adfb1e98702817c0d25d5e4daea8aaaaa1e4bd391c85a2796b354c4c', N'l1T4Hk8smR')
INSERT [dbo].[Employees] ([Id], [Username], [Name], [BirthDate], [PasswordHash], [PasswordSalt]) VALUES (7, N'wolk2', N'Wolk Wolk', CAST(N'1992-12-12' AS Date), N'd1dd86e3730507008b96fea8a538fb522eb7ce61740a7b49cc0a526b4975343d', N'Ijs7Yf5Hte')
INSERT [dbo].[Employees] ([Id], [Username], [Name], [BirthDate], [PasswordHash], [PasswordSalt]) VALUES (12, N'wolk1', N'wolk wolk', CAST(N'2010-12-31' AS Date), N'dd859125b6dcc7d638f372d100aa9e176bc66706bc18274362447c8c7a28cb94', N'O9YyOx6MQw')
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[Presents] ON 

INSERT [dbo].[Presents] ([Id], [Name], [Description]) VALUES (1, N'Bike', NULL)
INSERT [dbo].[Presents] ([Id], [Name], [Description]) VALUES (2, N'Playstation', N'Very nice for playing video games.')
INSERT [dbo].[Presents] ([Id], [Name], [Description]) VALUES (3, N'Laptop', N'Useful for work and for playing video games.')
INSERT [dbo].[Presents] ([Id], [Name], [Description]) VALUES (4, N'Jeans', N'From new collection.')
INSERT [dbo].[Presents] ([Id], [Name], [Description]) VALUES (5, N'Watch', N'Very fancy.')
INSERT [dbo].[Presents] ([Id], [Name], [Description]) VALUES (6, N'Book', NULL)
SET IDENTITY_INSERT [dbo].[Presents] OFF
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (1, 1, 4)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (1, 2, 4)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (1, 3, 4)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (1, 4, 6)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (1, 7, 6)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (2, 4, 5)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (2, 7, 4)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (3, 4, 6)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (3, 7, 1)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (3, 12, 1)
INSERT [dbo].[VotingPresents] ([VotingId], [EmployeeId], [PresentId]) VALUES (4, 12, 6)
SET IDENTITY_INSERT [dbo].[Votings] ON 

INSERT [dbo].[Votings] ([Id], [CreatorId], [ReceiverId], [PresentId], [ClosingDate], [IsClosed]) VALUES (1, 1, 6, NULL, CAST(N'2018-11-02' AS Date), 0)
INSERT [dbo].[Votings] ([Id], [CreatorId], [ReceiverId], [PresentId], [ClosingDate], [IsClosed]) VALUES (2, 7, 5, NULL, CAST(N'2018-10-23' AS Date), 0)
INSERT [dbo].[Votings] ([Id], [CreatorId], [ReceiverId], [PresentId], [ClosingDate], [IsClosed]) VALUES (3, 12, 1, 1, CAST(N'2018-05-31' AS Date), 1)
INSERT [dbo].[Votings] ([Id], [CreatorId], [ReceiverId], [PresentId], [ClosingDate], [IsClosed]) VALUES (4, 12, 3, NULL, CAST(N'2018-08-13' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Votings] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Employees_Username]    Script Date: 31-May-18 12:30:49 PM ******/
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [UQ_Employees_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Presents_Name]    Script Date: 31-May-18 12:30:49 PM ******/
ALTER TABLE [dbo].[Presents] ADD  CONSTRAINT [UQ_Presents_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VotingPresents]  WITH CHECK ADD  CONSTRAINT [FK_VotingPresents_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[VotingPresents] CHECK CONSTRAINT [FK_VotingPresents_Employees]
GO
ALTER TABLE [dbo].[VotingPresents]  WITH CHECK ADD  CONSTRAINT [FK_VotingPresents_Presents] FOREIGN KEY([PresentId])
REFERENCES [dbo].[Presents] ([Id])
GO
ALTER TABLE [dbo].[VotingPresents] CHECK CONSTRAINT [FK_VotingPresents_Presents]
GO
ALTER TABLE [dbo].[VotingPresents]  WITH CHECK ADD  CONSTRAINT [FK_VotingPresents_Votings] FOREIGN KEY([VotingId])
REFERENCES [dbo].[Votings] ([Id])
GO
ALTER TABLE [dbo].[VotingPresents] CHECK CONSTRAINT [FK_VotingPresents_Votings]
GO
ALTER TABLE [dbo].[Votings]  WITH CHECK ADD  CONSTRAINT [FK_Votings_Employees_Creator] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Votings] CHECK CONSTRAINT [FK_Votings_Employees_Creator]
GO
ALTER TABLE [dbo].[Votings]  WITH CHECK ADD  CONSTRAINT [FK_Votings_Employees_Receiver] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Votings] CHECK CONSTRAINT [FK_Votings_Employees_Receiver]
GO
ALTER TABLE [dbo].[Votings]  WITH CHECK ADD  CONSTRAINT [FK_Votings_Presents] FOREIGN KEY([PresentId])
REFERENCES [dbo].[Presents] ([Id])
GO
ALTER TABLE [dbo].[Votings] CHECK CONSTRAINT [FK_Votings_Presents]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [CK_Employees_Username] CHECK  ((len([Username])>=(5) AND len([Username])<=(50)))
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [CK_Employees_Username]
GO
ALTER TABLE [dbo].[Presents]  WITH CHECK ADD  CONSTRAINT [CK_Presents_Name] CHECK  ((len([Name])>=(2)))
GO
ALTER TABLE [dbo].[Presents] CHECK CONSTRAINT [CK_Presents_Name]
GO
USE [master]
GO
ALTER DATABASE [BirthdayPresentVotingSystem] SET  READ_WRITE 
GO
