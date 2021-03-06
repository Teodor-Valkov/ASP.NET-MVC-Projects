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
/****** Object:  Database [LocalPub]    Script Date: 31-May-18 12:33:50 PM ******/
CREATE DATABASE [LocalPub]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LocalPub', FILENAME = N'C:\Programs\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\LocalPub.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LocalPub_log', FILENAME = N'C:\Programs\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\LocalPub_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LocalPub] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LocalPub].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LocalPub] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LocalPub] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LocalPub] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LocalPub] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LocalPub] SET ARITHABORT OFF 
GO
ALTER DATABASE [LocalPub] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [LocalPub] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LocalPub] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LocalPub] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LocalPub] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LocalPub] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LocalPub] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LocalPub] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LocalPub] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LocalPub] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LocalPub] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LocalPub] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LocalPub] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LocalPub] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LocalPub] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LocalPub] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LocalPub] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LocalPub] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LocalPub] SET  MULTI_USER 
GO
ALTER DATABASE [LocalPub] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LocalPub] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LocalPub] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LocalPub] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LocalPub] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LocalPub] SET QUERY_STORE = OFF
GO
USE [LocalPub]
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
USE [LocalPub]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 31-May-18 12:33:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[PasswordHash] [nchar](64) NOT NULL,
	[PasswordSalt] [nchar](10) NOT NULL,
	[ClientTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientTypes]    Script Date: 31-May-18 12:33:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_ClientTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meals]    Script Date: 31-May-18 12:33:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[MealTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Meals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealTypes]    Script Date: 31-May-18 12:33:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_MealTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderMeals]    Script Date: 31-May-18 12:33:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderMeals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[MealId] [int] NOT NULL,
 CONSTRAINT [PK_OrderMeals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 31-May-18 12:33:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[OrderDate] [date] NOT NULL,
	[IsCancelled] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt], [ClientTypeId]) VALUES (1, N'sherlock', N'Sherlock Holmes', N'b8fe18df23194e299dbaf57f3b34748e41870eae36b843381567ec06216b9bea', N'HZa1oMgqQB', 2)
INSERT [dbo].[Clients] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt], [ClientTypeId]) VALUES (2, N'watson', N'Doctor Watson', N'934735d54622fa19468705cc055098f1201b0d1d39bb4663c06befa2da329e68', N'jnWBCrmU4K', 1)
INSERT [dbo].[Clients] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt], [ClientTypeId]) VALUES (3, N'wolk2', N'wolk wolk', N'6aa054c296cb03b08a2a6286d9d936fa5b258ee044b0a4546b4b4da30abe99ed', N'luAjpzkVtg', 1)
INSERT [dbo].[Clients] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt], [ClientTypeId]) VALUES (4, N'wolk3', N'wolk wolk', N'9da964f6cc9afd2e8d873840a150662171a4f66d39c0f3716596c3757fd2c062', N'WvQEVAeHA0', 2)
SET IDENTITY_INSERT [dbo].[Clients] OFF
SET IDENTITY_INSERT [dbo].[ClientTypes] ON 

INSERT [dbo].[ClientTypes] ([Id], [Name]) VALUES (1, N'Обикновен')
INSERT [dbo].[ClientTypes] ([Id], [Name]) VALUES (2, N'Връзкар')
SET IDENTITY_INSERT [dbo].[ClientTypes] OFF
SET IDENTITY_INSERT [dbo].[Meals] ON 

INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (1, N'Задушени картофи с копър', 1)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (2, N'Омлет „Асорти”', 1)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (3, N'Пилешки филенца с корнфлейкс', 1)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (4, N'Пържени картофи', 1)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (5, N'Тиквички с млечен сос', 1)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (6, N'Пилешко филе', 2)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (7, N'Тортила с пилешко', 2)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (8, N'Ризото на рибаря', 2)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (9, N'Спагети с вонголе', 2)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (10, N'Риба меч в тиган с бяло вино', 2)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (11, N'Банофи', 3)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (12, N'Бонбон с моркови, фурми и орехи', 3)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (13, N'Домашен бонбон с боровинки и кокос', 3)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (14, N'Домашен шоколадов трюфел', 3)
INSERT [dbo].[Meals] ([Id], [Name], [MealTypeId]) VALUES (15, N'Сладък салам', 3)
SET IDENTITY_INSERT [dbo].[Meals] OFF
SET IDENTITY_INSERT [dbo].[MealTypes] ON 

INSERT [dbo].[MealTypes] ([Id], [Name]) VALUES (3, N'Десерт')
INSERT [dbo].[MealTypes] ([Id], [Name]) VALUES (2, N'Основно ястие')
INSERT [dbo].[MealTypes] ([Id], [Name]) VALUES (1, N'Предястие')
SET IDENTITY_INSERT [dbo].[MealTypes] OFF
SET IDENTITY_INSERT [dbo].[OrderMeals] ON 

INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (1, 1, 2)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (2, 1, 7)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (3, 1, 14)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (4, 2, 3)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (5, 2, 8)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (6, 2, 13)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (7, 3, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (8, 3, 6)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (9, 3, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (10, 4, 3)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (11, 4, 9)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (12, 4, 15)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (13, 5, 2)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (14, 5, 10)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (15, 5, 14)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (16, 6, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (17, 6, 8)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (18, 6, 13)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (19, 7, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (20, 7, 5)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (21, 7, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (22, 7, 12)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (23, 8, 3)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (24, 8, 8)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (25, 8, 15)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (26, 8, 15)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (27, 9, 2)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (28, 9, 8)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (29, 9, 12)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (30, 9, 12)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (31, 10, 2)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (32, 10, 7)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (33, 10, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (34, 10, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (35, 11, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (36, 11, 6)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (37, 11, 13)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (38, 14, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (39, 15, 13)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (40, 16, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (41, 16, 8)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (42, 16, 15)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (43, 17, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (44, 17, 7)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (45, 17, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (46, 18, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (47, 18, 7)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (48, 18, 13)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (49, 18, 13)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (50, 19, 4)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (51, 19, 8)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (52, 19, 12)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (53, 19, 13)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (54, 20, 3)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (55, 20, 7)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (56, 20, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (57, 21, 2)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (58, 21, 7)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (59, 22, 2)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (60, 22, 8)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (61, 22, 12)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (62, 22, 12)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (63, 23, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (64, 23, 12)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (65, 24, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (66, 24, 6)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (67, 24, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (68, 24, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (69, 25, 1)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (70, 25, 11)
INSERT [dbo].[OrderMeals] ([Id], [OrderId], [MealId]) VALUES (71, 25, 11)
SET IDENTITY_INSERT [dbo].[OrderMeals] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (1, 1, CAST(N'2018-05-22' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (2, 1, CAST(N'2018-05-22' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (3, 1, CAST(N'2018-05-22' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (4, 1, CAST(N'2018-05-23' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (5, 2, CAST(N'2018-05-22' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (6, 2, CAST(N'2018-05-22' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (7, 2, CAST(N'2018-05-22' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (8, 2, CAST(N'2018-05-23' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (9, 1, CAST(N'2017-09-30' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (10, 1, CAST(N'2015-10-30' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (11, 2, CAST(N'2015-10-31' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (12, 1, CAST(N'2018-05-03' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (14, 1, CAST(N'2018-05-18' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (15, 2, CAST(N'2018-05-08' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (16, 2, CAST(N'2018-06-01' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (17, 2, CAST(N'2018-06-01' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (18, 1, CAST(N'2018-06-01' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (19, 1, CAST(N'2018-06-02' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (20, 1, CAST(N'2018-05-24' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (21, 3, CAST(N'2018-05-31' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (22, 4, CAST(N'2018-05-31' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (23, 1, CAST(N'2018-05-31' AS Date), 1)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (24, 4, CAST(N'2018-05-30' AS Date), 0)
INSERT [dbo].[Orders] ([Id], [ClientId], [OrderDate], [IsCancelled]) VALUES (25, 4, CAST(N'2018-06-01' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Clients_Username]    Script Date: 31-May-18 12:33:50 PM ******/
ALTER TABLE [dbo].[Clients] ADD  CONSTRAINT [UQ_Clients_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Meals_Name]    Script Date: 31-May-18 12:33:50 PM ******/
ALTER TABLE [dbo].[Meals] ADD  CONSTRAINT [UQ_Meals_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_MealTypes_Name]    Script Date: 31-May-18 12:33:50 PM ******/
ALTER TABLE [dbo].[MealTypes] ADD  CONSTRAINT [UQ_MealTypes_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_ClientTypes] FOREIGN KEY([ClientTypeId])
REFERENCES [dbo].[ClientTypes] ([Id])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_ClientTypes]
GO
ALTER TABLE [dbo].[Meals]  WITH CHECK ADD  CONSTRAINT [FK_Meals_MealTypes] FOREIGN KEY([MealTypeId])
REFERENCES [dbo].[MealTypes] ([Id])
GO
ALTER TABLE [dbo].[Meals] CHECK CONSTRAINT [FK_Meals_MealTypes]
GO
ALTER TABLE [dbo].[OrderMeals]  WITH CHECK ADD  CONSTRAINT [FK_OrderMeals_Meals] FOREIGN KEY([MealId])
REFERENCES [dbo].[Meals] ([Id])
GO
ALTER TABLE [dbo].[OrderMeals] CHECK CONSTRAINT [FK_OrderMeals_Meals]
GO
ALTER TABLE [dbo].[OrderMeals]  WITH CHECK ADD  CONSTRAINT [FK_OrderMeals_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderMeals] CHECK CONSTRAINT [FK_OrderMeals_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Clients]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [CK_Clients_Username] CHECK  ((len([Username])>=(5)))
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [CK_Clients_Username]
GO
ALTER TABLE [dbo].[Meals]  WITH CHECK ADD  CONSTRAINT [CK_Meals_Name] CHECK  ((len([Name])>=(3)))
GO
ALTER TABLE [dbo].[Meals] CHECK CONSTRAINT [CK_Meals_Name]
GO
ALTER TABLE [dbo].[MealTypes]  WITH CHECK ADD  CONSTRAINT [CK_MealTypes_Name] CHECK  ((len([Name])>=(3)))
GO
ALTER TABLE [dbo].[MealTypes] CHECK CONSTRAINT [CK_MealTypes_Name]
GO
USE [master]
GO
ALTER DATABASE [LocalPub] SET  READ_WRITE 
GO
