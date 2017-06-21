USE [master]
GO
/****** Object:  Database [charity_test]    Script Date: 6/20/2017 5:55:52 PM ******/
CREATE DATABASE [charity_test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'charity', FILENAME = N'C:\Users\epicodus\charity_test.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'charity_log', FILENAME = N'C:\Users\epicodus\charity_test_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [charity_test] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [charity_test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [charity_test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [charity_test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [charity_test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [charity_test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [charity_test] SET ARITHABORT OFF 
GO
ALTER DATABASE [charity_test] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [charity_test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [charity_test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [charity_test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [charity_test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [charity_test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [charity_test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [charity_test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [charity_test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [charity_test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [charity_test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [charity_test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [charity_test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [charity_test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [charity_test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [charity_test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [charity_test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [charity_test] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [charity_test] SET  MULTI_USER 
GO
ALTER DATABASE [charity_test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [charity_test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [charity_test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [charity_test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [charity_test] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [charity_test] SET QUERY_STORE = OFF
GO
USE [charity_test]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [charity_test]
GO
/****** Object:  Table [dbo].[campaigns]    Script Date: 6/20/2017 5:55:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[campaigns](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [varchar](max) NULL,
	[goal_amt] [int] NULL,
	[current_amt] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[category_id] [int] NULL,
	[owner_id] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categories]    Script Date: 6/20/2017 5:55:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[donations]    Script Date: 6/20/2017 5:55:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[donations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[campaign_id] [int] NULL,
	[donation_amount] [int] NULL,
	[donation_date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[roles]    Script Date: 6/20/2017 5:55:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role] [varchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 6/20/2017 5:55:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NULL,
	[name] [varchar](255) NULL,
	[login] [varchar](255) NULL,
	[password] [varchar](100) NULL,
	[address] [varchar](255) NULL,
	[phone_number] [varchar](50) NULL,
	[email] [varchar](50) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[campaigns] ON 

INSERT [dbo].[campaigns] ([id], [name], [description], [goal_amt], [current_amt], [start_date], [end_date], [category_id], [owner_id]) VALUES (1, N'Jun''s Broken Foot', N'Jun broke his foot', 50, 101, CAST(N'2017-06-20T00:00:00.000' AS DateTime), CAST(N'2017-06-25T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[campaigns] ([id], [name], [description], [goal_amt], [current_amt], [start_date], [end_date], [category_id], [owner_id]) VALUES (2, N'Epicodus Course', N'I need a new mac book!', 5000, 0, CAST(N'2017-06-20T00:00:00.000' AS DateTime), CAST(N'2017-06-25T00:00:00.000' AS DateTime), 2, 2)
INSERT [dbo].[campaigns] ([id], [name], [description], [goal_amt], [current_amt], [start_date], [end_date], [category_id], [owner_id]) VALUES (3, N'My Dog Needs New Teeth', N'a;skfjaskdjfasdklfnalksdf;lkansdkl;faklsdf;lakshdfklnaskldfnlakshdfklasdfasdfasdfasdfasdfa;skfjaskdjfasdklfnalksdf;lkansdkl;faklsdf;lakshdfklnaskldfnlakshdfklasdfasdfasdfasdfasdfa;skfjaskdjfasdklfnalksdf;lkansdkl;faklsdf;lakshdfklnaskldfnlakshdfklasdfasdfasdfasdfasdfa;skfjaskdjfasdklfnalksdf;lkansdkl;faklsdf;lakshdfklnaskldfnlakshdfklasdfasdfasdfasdfasdfa;skfjaskdjfasdklfnalksdf;lkansdkl;faklsdf;lakshdfklnaskldfnlakshdfklasdfasdfasdfasdfasdfa;skfjaskdjfasdklfnalksdf;lkansdkl;faklsdf;lakshdfklnaskldfnlakshdfklasdfasdfasdfasdfasdfa;skfjaskdjfasdklfnalksdf;lkansdkl;faklsdf;lakshdfklnaskldfnlakshdfklasdfasdfasdfasdfasdf', 1000, 1, CAST(N'2017-06-20T00:00:00.000' AS DateTime), CAST(N'2017-06-21T00:00:00.000' AS DateTime), 3, 9)
SET IDENTITY_INSERT [dbo].[campaigns] OFF
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [name]) VALUES (1, N'medical')
INSERT [dbo].[categories] ([id], [name]) VALUES (2, N'education')
INSERT [dbo].[categories] ([id], [name]) VALUES (3, N'animals')
INSERT [dbo].[categories] ([id], [name]) VALUES (4, N'emergencies')
INSERT [dbo].[categories] ([id], [name]) VALUES (5, N'art')
INSERT [dbo].[categories] ([id], [name]) VALUES (6, N'charity')
SET IDENTITY_INSERT [dbo].[categories] OFF
SET IDENTITY_INSERT [dbo].[donations] ON 

INSERT [dbo].[donations] ([id], [user_id], [campaign_id], [donation_amount], [donation_date]) VALUES (1, 1, 1, 50, CAST(N'2017-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[donations] ([id], [user_id], [campaign_id], [donation_amount], [donation_date]) VALUES (2, 2, 1, 123, CAST(N'2017-06-20T16:24:32.477' AS DateTime))
INSERT [dbo].[donations] ([id], [user_id], [campaign_id], [donation_amount], [donation_date]) VALUES (3, 2, 1, 134142, CAST(N'2017-06-20T17:09:03.907' AS DateTime))
INSERT [dbo].[donations] ([id], [user_id], [campaign_id], [donation_amount], [donation_date]) VALUES (4, 2, 1, 100, CAST(N'2017-06-20T17:13:22.773' AS DateTime))
INSERT [dbo].[donations] ([id], [user_id], [campaign_id], [donation_amount], [donation_date]) VALUES (6, 9, 3, 1, CAST(N'2017-06-20T17:49:24.293' AS DateTime))
INSERT [dbo].[donations] ([id], [user_id], [campaign_id], [donation_amount], [donation_date]) VALUES (5, 2, 1, 1, CAST(N'2017-06-20T17:20:29.163' AS DateTime))
SET IDENTITY_INSERT [dbo].[donations] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (1, 1, N'a', N'a', N'b', N'a', N'asdf', N'asdf')
INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (2, 1, N'a', N'a', N'b', N'a', N'a', N'a')
INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (3, 1, N'Lena', N'lena123', N'123', N'asdfasdf', N'123123', N'asdfsdaf')
INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (4, 1, N'daniela', N'd123', N'123', N'asdf', N'asdfasd', N'afsdf')
INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (5, 1, N'asdf', N'asdf', N'asdf', N'asdf', N'asdf', N'asdf')
INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (6, 1, N'sfds', N'asdf', N'asdf', N'asdf', N'asdf', N'asdf')
INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (7, 1, N'asdf', N'asdfasdf', N'asdf', N'asdf', N'asdf', N'asdf')
INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (8, 1, N'Lena', N'b', N'a', N'asdfsa', N'asdfs', N'asdfsdf')
INSERT [dbo].[users] ([id], [role_id], [name], [login], [password], [address], [phone_number], [email]) VALUES (9, 1, N'asdfasd', N'a', N'b', N'asdf', N'asdf', N'asdf')
SET IDENTITY_INSERT [dbo].[users] OFF
USE [master]
GO
ALTER DATABASE [charity_test] SET  READ_WRITE 
GO
