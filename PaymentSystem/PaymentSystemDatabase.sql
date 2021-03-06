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
/****** Object:  Database [PaymentSystem]    Script Date: 31-May-18 09:45:30 AM ******/
CREATE DATABASE [PaymentSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PaymentSystem', FILENAME = N'C:\Programs\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\PaymentSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PaymentSystem_log', FILENAME = N'C:\Programs\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\PaymentSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PaymentSystem] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PaymentSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PaymentSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PaymentSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PaymentSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PaymentSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PaymentSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [PaymentSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PaymentSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PaymentSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PaymentSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PaymentSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PaymentSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PaymentSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PaymentSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PaymentSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PaymentSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PaymentSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PaymentSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PaymentSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PaymentSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PaymentSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PaymentSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PaymentSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PaymentSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PaymentSystem] SET  MULTI_USER 
GO
ALTER DATABASE [PaymentSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PaymentSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PaymentSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PaymentSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PaymentSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PaymentSystem] SET QUERY_STORE = OFF
GO
USE [PaymentSystem]
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
USE [PaymentSystem]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 31-May-18 09:45:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IBAN] [nchar](22) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CreatorId] [int] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountUsers]    Script Date: 31-May-18 09:45:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountUsers](
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_AccountUsers] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 31-May-18 09:45:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[PaymentIBAN] [nchar](22) NOT NULL,
	[PaymentAmount] [decimal](18, 2) NOT NULL,
	[PaymentReason] [nvarchar](32) NOT NULL,
	[PaymentDate] [date] NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 31-May-18 09:45:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31-May-18 09:45:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[PasswordHash] [nchar](64) NOT NULL,
	[PasswordSalt] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [IBAN], [Amount], [CreatorId]) VALUES (1, N'BG70UNCR96601451876602', CAST(8888488140.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Accounts] ([Id], [IBAN], [Amount], [CreatorId]) VALUES (2, N'BG82UNCR96601051876613', CAST(99487272.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Accounts] ([Id], [IBAN], [Amount], [CreatorId]) VALUES (3, N'BG32UNCR96601051876640', CAST(100000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Accounts] ([Id], [IBAN], [Amount], [CreatorId]) VALUES (4, N'BG38UNCR96601051876629', CAST(500.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (1, 1)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (1, 2)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (1, 3)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (1, 4)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (1, 7)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (2, 1)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (2, 2)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (2, 3)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (2, 7)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (3, 1)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (3, 3)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (4, 1)
INSERT [dbo].[AccountUsers] ([AccountId], [UserId]) VALUES (4, 4)
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (1, 1, 1, 2, N'BG38UNCR96601051850315', CAST(1000.00 AS Decimal(18, 2)), N'Захранване на сметка', CAST(N'2018-05-05' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (2, 1, 1, 2, N'BG38UNCR96601012477901', CAST(2500.00 AS Decimal(18, 2)), N'Плащане на лаптоп', CAST(N'2018-05-08' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (3, 2, 1, 3, N'BG38UNCR96601043675967', CAST(350.00 AS Decimal(18, 2)), N'Плащане към топлофикация', CAST(N'2018-05-11' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (4, 1, 2, 1, N'BG38UNCR96601057651482', CAST(150.00 AS Decimal(18, 2)), N'Payment for watch', CAST(N'2018-05-14' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (5, 1, 2, 2, N'BG38UNCR96601051876045', CAST(50.00 AS Decimal(18, 2)), N'Payment for glasses', CAST(N'2018-05-17' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (6, 2, 2, 3, N'BG38UNCR96601051873469', CAST(7500.00 AS Decimal(18, 2)), N'Payment for car', CAST(N'2018-05-20' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (7, 1, 3, 2, N'BG38UNCR96601051871256', CAST(250.00 AS Decimal(18, 2)), N'Payment for medicine', CAST(N'2018-05-05' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (8, 3, 3, 3, N'BG38UNCR96601051871256', CAST(250.00 AS Decimal(18, 2)), N'Payment for cat', CAST(N'2018-05-07' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (9, 4, 4, 3, N'BG38UNCR96601051871256', CAST(250.00 AS Decimal(18, 2)), N'Payment for dog', CAST(N'2018-05-09' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (11, 3, 3, 3, N'BG38UNCR96601051871256', CAST(100000000000.00 AS Decimal(18, 2)), N'Payment for house', CAST(N'2018-05-11' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (12, 1, 1, 1, N'RANDOMIBAN112345Random', CAST(250.00 AS Decimal(18, 2)), N'First Payment', CAST(N'2018-05-13' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (13, 4, 1, 3, N'RANDOMIBAN112345Random', CAST(10000.00 AS Decimal(18, 2)), N'Very Expensive To Process', CAST(N'2018-05-15' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (14, 4, 4, 1, N'BG38374289AJSDHHASDJH2', CAST(20000.00 AS Decimal(18, 2)), N'Courses', CAST(N'2018-05-17' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (15, 4, 4, 3, N'BG38374289AJSDHHASDJH2', CAST(20000.00 AS Decimal(18, 2)), N'Подарък за кучето', CAST(N'2018-05-19' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (16, 1, 4, 2, N'BG38374289AJSDHHASDJH2', CAST(200000.00 AS Decimal(18, 2)), N'Girlfriends gift', CAST(N'2018-05-21' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (17, 1, 4, 3, N'BG38374289KJJIJHASDJH2', CAST(1500.00 AS Decimal(18, 2)), N'Laptop', CAST(N'2018-05-23' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (18, 4, 1, 2, N'1234568900987654321123', CAST(500.00 AS Decimal(18, 2)), N'Gift', CAST(N'2018-05-25' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (19, 1, 7, 2, N'BGASDjkjaksdjjs11231JJ', CAST(555555555.00 AS Decimal(18, 2)), N'test', CAST(N'2018-05-30' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (20, 2, 7, 2, N'BGASDjkjaksdjjs11231JJ', CAST(512555.00 AS Decimal(18, 2)), N'Payment', CAST(N'2018-05-30' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (21, 4, 1, 1, N'BGASDjkjaksdjjs11231JJ', CAST(45555.00 AS Decimal(18, 2)), N'no payment', CAST(N'2018-05-30' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (22, 2, 7, 3, N'BGASDjkjaksdjjs11231JJ', CAST(250.00 AS Decimal(18, 2)), N'Test', CAST(N'2018-05-31' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (23, 2, 7, 2, N'BGASDjkjaksdjjs11231JJ', CAST(50.00 AS Decimal(18, 2)), N'Test', CAST(N'2018-05-31' AS Date))
INSERT [dbo].[Payments] ([Id], [AccountId], [UserId], [StatusId], [PaymentIBAN], [PaymentAmount], [PaymentReason], [PaymentDate]) VALUES (24, 2, 7, 2, N'BGASDjkjaksdjjs11231JJ', CAST(123.00 AS Decimal(18, 2)), N'Payment', CAST(N'2018-05-31' AS Date))
SET IDENTITY_INSERT [dbo].[Payments] OFF
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([Id], [Status]) VALUES (1, N'ИЗЧАКВА')
INSERT [dbo].[Statuses] ([Id], [Status]) VALUES (2, N'ОБРАБОТЕН')
INSERT [dbo].[Statuses] ([Id], [Status]) VALUES (3, N'ОТКАЗАН')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt]) VALUES (1, N'wolk2', N'Wolk Wolk', N'd1dd86e3730507008b96fea8a538fb522eb7ce61740a7b49cc0a526b4975343d', N'Ijs7Yf5Hte')
INSERT [dbo].[Users] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt]) VALUES (2, N'теодор', N'Теодор Теодоров', N'e9f7756388fb6e0540978478264920fe81feeb9b2e1e294a71e7f046499e52d1', N'8urHjGtD5h')
INSERT [dbo].[Users] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt]) VALUES (3, N'sherlock', N'Sherlock Holmes', N'b8fe18df23194e299dbaf57f3b34748e41870eae36b843381567ec06216b9bea', N'HZa1oMgqQB')
INSERT [dbo].[Users] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt]) VALUES (4, N'watson', N'Doctor Watson', N'8ae2d3e776fdd3a816f53efb802e9150daeaaf71829444675b3e13704fc8c7b3', N'aiUW4M8f1O')
INSERT [dbo].[Users] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt]) VALUES (5, N'holmes', N'Sherlock Holmes', N'934735d54622fa19468705cc055098f1201b0d1d39bb4663c06befa2da329e68', N'jnWBCrmU4K')
INSERT [dbo].[Users] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt]) VALUES (6, N'doctor', N'Doctor Watson', N'86acc3a35c2030e6f8b43e5164e942583e238b17e8f9ea73e56262d960443e43', N'AsnsH2HK5G')
INSERT [dbo].[Users] ([Id], [Username], [Name], [PasswordHash], [PasswordSalt]) VALUES (7, N'wolk3', N'wolk wolk', N'86f1da640f128649ae189539bfce03277192c0caf387319ad0b0f475f2ddddfc', N'kNtnq9DDtu')
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_IBAN]    Script Date: 31-May-18 09:45:30 AM ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [UQ_IBAN] UNIQUE NONCLUSTERED 
(
	[IBAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Status]    Script Date: 31-May-18 09:45:30 AM ******/
ALTER TABLE [dbo].[Statuses] ADD  CONSTRAINT [UQ_Status] UNIQUE NONCLUSTERED 
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Username]    Script Date: 31-May-18 09:45:30 AM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Users] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Users]
GO
ALTER TABLE [dbo].[AccountUsers]  WITH CHECK ADD  CONSTRAINT [FK_AccountsUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AccountUsers] CHECK CONSTRAINT [FK_AccountsUsers_Users]
GO
ALTER TABLE [dbo].[AccountUsers]  WITH CHECK ADD  CONSTRAINT [FK_AccountUsers_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[AccountUsers] CHECK CONSTRAINT [FK_AccountUsers_Accounts]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Accounts]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Statuses]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Users]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [CK_Amount] CHECK  (([Amount]>(0)))
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [CK_Amount]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [CK_IBAN] CHECK  ((len([IBAN])=(22)))
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [CK_IBAN]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [CK_PaymentAmount] CHECK  (([PaymentAmount]>(0)))
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [CK_PaymentAmount]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [CK_PaymentIBAN] CHECK  ((len([PaymentIBAN])=(22)))
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [CK_PaymentIBAN]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [CK_PaymentReason] CHECK  ((len([PaymentReason])>=(3) AND len([PaymentReason])<=(32)))
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [CK_PaymentReason]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Username] CHECK  ((len([Username])>=(5) AND len([Username])<=(50)))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Username]
GO
USE [master]
GO
ALTER DATABASE [PaymentSystem] SET  READ_WRITE 
GO
