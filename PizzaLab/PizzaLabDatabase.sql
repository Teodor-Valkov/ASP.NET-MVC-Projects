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
/****** Object:  Database [PizzaLab]    Script Date: 31-May-18 10:16:07 AM ******/
CREATE DATABASE [PizzaLab]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PizzaLab', FILENAME = N'C:\Programs\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\PizzaLab.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PizzaLab_log', FILENAME = N'C:\Programs\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\PizzaLab_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PizzaLab] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PizzaLab].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PizzaLab] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PizzaLab] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PizzaLab] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PizzaLab] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PizzaLab] SET ARITHABORT OFF 
GO
ALTER DATABASE [PizzaLab] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PizzaLab] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PizzaLab] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PizzaLab] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PizzaLab] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PizzaLab] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PizzaLab] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PizzaLab] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PizzaLab] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PizzaLab] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PizzaLab] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PizzaLab] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PizzaLab] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PizzaLab] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PizzaLab] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PizzaLab] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PizzaLab] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PizzaLab] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PizzaLab] SET  MULTI_USER 
GO
ALTER DATABASE [PizzaLab] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PizzaLab] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PizzaLab] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PizzaLab] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PizzaLab] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PizzaLab] SET QUERY_STORE = OFF
GO
USE [PizzaLab]
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
USE [PizzaLab]
GO
/****** Object:  Table [dbo].[DoughTypes]    Script Date: 31-May-18 10:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoughTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Price] [decimal](12, 6) NOT NULL,
 CONSTRAINT [PK_DoughTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 31-May-18 10:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Price] [decimal](12, 6) NOT NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 31-May-18 10:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TotalPrice] [decimal](12, 6) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PizzaOrders]    Script Date: 31-May-18 10:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PizzaOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PizzaId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[DoughTypeId] [int] NOT NULL,
	[SizeId] [int] NOT NULL,
 CONSTRAINT [PK_PizzaOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pizzas]    Script Date: 31-May-18 10:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pizzas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PicturePath] [nvarchar](500) NULL,
 CONSTRAINT [PK_Pizzas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pizzas_Ingredients]    Script Date: 31-May-18 10:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pizzas_Ingredients](
	[PizzaId] [int] NOT NULL,
	[IngredientId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Pizzas_Ingredients] PRIMARY KEY CLUSTERED 
(
	[PizzaId] ASC,
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sizes]    Script Date: 31-May-18 10:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sizes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Price] [decimal](12, 6) NOT NULL,
 CONSTRAINT [PK_Sizes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31-May-18 10:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](1000) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[PasswordHash] [nchar](64) NOT NULL,
	[PasswordSalt] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DoughTypes] ON 

INSERT [dbo].[DoughTypes] ([Id], [Name], [Price]) VALUES (1, N'Traditional', CAST(1.500000 AS Decimal(12, 6)))
INSERT [dbo].[DoughTypes] ([Id], [Name], [Price]) VALUES (2, N'Italian', CAST(2.500000 AS Decimal(12, 6)))
INSERT [dbo].[DoughTypes] ([Id], [Name], [Price]) VALUES (3, N'Thin', CAST(3.500000 AS Decimal(12, 6)))
INSERT [dbo].[DoughTypes] ([Id], [Name], [Price]) VALUES (4, N'Creamy', CAST(4.500000 AS Decimal(12, 6)))
SET IDENTITY_INSERT [dbo].[DoughTypes] OFF
SET IDENTITY_INSERT [dbo].[Ingredients] ON 

INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (1, N'Chicken', CAST(4.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (2, N'Pork', CAST(5.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (3, N'Cheese', CAST(2.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (4, N'Mozarella', CAST(2.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (5, N'Parmezan', CAST(2.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (6, N'Feta', CAST(2.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (7, N'Emmental', CAST(2.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (8, N'Cheddar', CAST(2.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (9, N'Peperoni', CAST(3.500000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (10, N'Onion', CAST(1.000000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (11, N'Mushroom', CAST(2.000000 AS Decimal(12, 6)))
INSERT [dbo].[Ingredients] ([Id], [Name], [Price]) VALUES (12, N'Ham', CAST(3.500000 AS Decimal(12, 6)))
SET IDENTITY_INSERT [dbo].[Ingredients] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [UserId], [TotalPrice]) VALUES (1, 1, CAST(25.000000 AS Decimal(12, 6)))
INSERT [dbo].[Orders] ([Id], [UserId], [TotalPrice]) VALUES (2, 2, CAST(35.000000 AS Decimal(12, 6)))
INSERT [dbo].[Orders] ([Id], [UserId], [TotalPrice]) VALUES (3, 3, CAST(47.000000 AS Decimal(12, 6)))
INSERT [dbo].[Orders] ([Id], [UserId], [TotalPrice]) VALUES (4, 4, CAST(63.000000 AS Decimal(12, 6)))
INSERT [dbo].[Orders] ([Id], [UserId], [TotalPrice]) VALUES (5, 5, CAST(77.000000 AS Decimal(12, 6)))
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[PizzaOrders] ON 

INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (1, 1, 1, 1, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (2, 1, 1, 2, 2)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (3, 1, 1, 1, 4)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (4, 1, 1, 2, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (5, 1, 1, 2, 2)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (6, 1, 1, 1, 4)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (7, 2, 1, 1, 3)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (8, 2, 1, 3, 4)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (9, 2, 1, 3, 3)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (10, 2, 2, 2, 3)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (11, 2, 1, 2, 2)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (12, 2, 2, 3, 2)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (13, 3, 2, 4, 4)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (14, 3, 2, 4, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (15, 3, 2, 1, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (16, 3, 2, 2, 3)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (17, 4, 2, 4, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (18, 4, 2, 1, 2)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (19, 4, 2, 4, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (20, 4, 2, 3, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (21, 4, 3, 2, 3)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (22, 1, 3, 1, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (23, 1, 4, 4, 4)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (24, 1, 4, 1, 1)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (25, 1, 5, 2, 3)
INSERT [dbo].[PizzaOrders] ([Id], [PizzaId], [OrderId], [DoughTypeId], [SizeId]) VALUES (26, 2, 5, 3, 4)
SET IDENTITY_INSERT [dbo].[PizzaOrders] OFF
SET IDENTITY_INSERT [dbo].[Pizzas] ON 

INSERT [dbo].[Pizzas] ([Id], [Name], [Description], [PicturePath]) VALUES (1, N'Margaritta', N'Classic', N'https://www.dominos.bg/gallery/fmobile/1265medium.png')
INSERT [dbo].[Pizzas] ([Id], [Name], [Description], [PicturePath]) VALUES (2, N'Alfredo', N'The Italian past reimagined', N'https://www.dominos.bg/gallery/fmobile/1352large.png')
INSERT [dbo].[Pizzas] ([Id], [Name], [Description], [PicturePath]) VALUES (3, N'American hot', N'It''s American, and it''s HOT!', N'https://www.dominos.bg/gallery/fmobile/1291large.png')
INSERT [dbo].[Pizzas] ([Id], [Name], [Description], [PicturePath]) VALUES (4, N'Meat Mania', N'A lot of meat', N'https://www.dominos.bg/gallery/fmobile/1364large.png')
SET IDENTITY_INSERT [dbo].[Pizzas] OFF
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (1, 3, 2)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (1, 4, 1)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (1, 5, 1)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (1, 9, 1)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (1, 10, 2)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (1, 12, 2)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (2, 1, 3)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (2, 2, 3)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (2, 5, 3)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (2, 6, 1)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (2, 7, 2)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (2, 8, 2)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (3, 2, 2)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (3, 4, 1)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (3, 9, 1)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (3, 10, 2)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (4, 1, 3)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (4, 3, 2)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (4, 5, 1)
INSERT [dbo].[Pizzas_Ingredients] ([PizzaId], [IngredientId], [Quantity]) VALUES (4, 6, 1)
SET IDENTITY_INSERT [dbo].[Sizes] ON 

INSERT [dbo].[Sizes] ([Id], [Name], [Price]) VALUES (1, N'Smal', CAST(5.500000 AS Decimal(12, 6)))
INSERT [dbo].[Sizes] ([Id], [Name], [Price]) VALUES (2, N'Medium', CAST(6.500000 AS Decimal(12, 6)))
INSERT [dbo].[Sizes] ([Id], [Name], [Price]) VALUES (3, N'Large', CAST(7.500000 AS Decimal(12, 6)))
INSERT [dbo].[Sizes] ([Id], [Name], [Price]) VALUES (4, N'Extra Large', CAST(8.500000 AS Decimal(12, 6)))
SET IDENTITY_INSERT [dbo].[Sizes] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Address], [Phone], [PasswordHash], [PasswordSalt]) VALUES (1, N'admin', N'101, Al. Stamboliyski Blvd.', N'1234567890', N'2bb28071ee31f4834c9d46db4355623b48111bb63463d5dc108cebe71c5f9d32', N'zdooUYtRCE')
INSERT [dbo].[Users] ([Id], [Username], [Address], [Phone], [PasswordHash], [PasswordSalt]) VALUES (2, N'user', N'The second garbage bin on the right', N'1234567890', N'18603752c1b7bc38be82c3f8ed0601ef009257e4b9eb98139862ff3bf15f0ab6', N'm2dsN5Ro6J')
INSERT [dbo].[Users] ([Id], [Username], [Address], [Phone], [PasswordHash], [PasswordSalt]) VALUES (3, N'wolk2', N'Sofia', N'123123123', N'76a86b200ad32cf003c4d7e935df4d626568e9b9e0a3aa264e935d63c565591f', N'1dgDnOCErK')
INSERT [dbo].[Users] ([Id], [Username], [Address], [Phone], [PasswordHash], [PasswordSalt]) VALUES (4, N'wolk3', N'address', N'123123123', N'9cf63a0b278502fc4831ef7ef3c84c719073502b355e5c373ed065314a4296df', N'g8i700Ttv1')
INSERT [dbo].[Users] ([Id], [Username], [Address], [Phone], [PasswordHash], [PasswordSalt]) VALUES (5, N'wolk1', N'address', N'123123123', N'c99e1cacb2f81f27668c27b099820d2df65569a1bafd21d52f3098bb7c09d9a3', N'HDLk0FnLJp')
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Username]    Script Date: 31-May-18 10:16:07 AM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[PizzaOrders]  WITH CHECK ADD  CONSTRAINT [FK_PizzaOrders_DoughTypes] FOREIGN KEY([DoughTypeId])
REFERENCES [dbo].[DoughTypes] ([Id])
GO
ALTER TABLE [dbo].[PizzaOrders] CHECK CONSTRAINT [FK_PizzaOrders_DoughTypes]
GO
ALTER TABLE [dbo].[PizzaOrders]  WITH CHECK ADD  CONSTRAINT [FK_PizzaOrders_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[PizzaOrders] CHECK CONSTRAINT [FK_PizzaOrders_Orders]
GO
ALTER TABLE [dbo].[PizzaOrders]  WITH CHECK ADD  CONSTRAINT [FK_PizzaOrders_Pizzas] FOREIGN KEY([PizzaId])
REFERENCES [dbo].[Pizzas] ([Id])
GO
ALTER TABLE [dbo].[PizzaOrders] CHECK CONSTRAINT [FK_PizzaOrders_Pizzas]
GO
ALTER TABLE [dbo].[PizzaOrders]  WITH CHECK ADD  CONSTRAINT [FK_PizzaOrders_Sizes] FOREIGN KEY([SizeId])
REFERENCES [dbo].[Sizes] ([Id])
GO
ALTER TABLE [dbo].[PizzaOrders] CHECK CONSTRAINT [FK_PizzaOrders_Sizes]
GO
ALTER TABLE [dbo].[Pizzas_Ingredients]  WITH CHECK ADD  CONSTRAINT [FK_Pizzas_Ingredients_Ingredients] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredients] ([Id])
GO
ALTER TABLE [dbo].[Pizzas_Ingredients] CHECK CONSTRAINT [FK_Pizzas_Ingredients_Ingredients]
GO
ALTER TABLE [dbo].[Pizzas_Ingredients]  WITH CHECK ADD  CONSTRAINT [FK_Pizzas_Ingredients_Pizzas] FOREIGN KEY([PizzaId])
REFERENCES [dbo].[Pizzas] ([Id])
GO
ALTER TABLE [dbo].[Pizzas_Ingredients] CHECK CONSTRAINT [FK_Pizzas_Ingredients_Pizzas]
GO
USE [master]
GO
ALTER DATABASE [PizzaLab] SET  READ_WRITE 
GO
