USE [master]
GO
/****** Object:  Database [TikTokDB]    Script Date: 8/13/2024 18:20:48 ******/
CREATE DATABASE [TikTokDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TikTokDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SONHA\MSSQL\DATA\TikTokDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TikTokDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SONHA\MSSQL\DATA\TikTokDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TikTokDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TikTokDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TikTokDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TikTokDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TikTokDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TikTokDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TikTokDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TikTokDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TikTokDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TikTokDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TikTokDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TikTokDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TikTokDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TikTokDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TikTokDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TikTokDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TikTokDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TikTokDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TikTokDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TikTokDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TikTokDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TikTokDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TikTokDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TikTokDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TikTokDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TikTokDB] SET  MULTI_USER 
GO
ALTER DATABASE [TikTokDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TikTokDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TikTokDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TikTokDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TikTokDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TikTokDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TikTokDB', N'ON'
GO
ALTER DATABASE [TikTokDB] SET QUERY_STORE = OFF
GO
USE [TikTokDB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 8/13/2024 18:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullName] [nvarchar](50) NULL,
	[email] [varchar](100) NULL,
	[password] [varchar](100) NULL,
	[liked] [int] NULL,
	[followed] [int] NULL,
	[avatar] [varchar](255) NULL,
	[contact] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Video]    Script Date: 8/13/2024 18:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Video](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NULL,
	[liked] [int] NULL,
	[commented] [int] NULL,
	[shared] [int] NULL,
	[srcVideo] [varchar](255) NULL,
	[idAccount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([id], [fullName], [email], [password], [liked], [followed], [avatar], [contact]) VALUES (1, N'Hoàng Sơn Hà', N'hoangsonha@gmail.com', N'12345', 1500000, 1000000, N'no avatar', N'No contact')
INSERT [dbo].[Account] ([id], [fullName], [email], [password], [liked], [followed], [avatar], [contact]) VALUES (2, N'Nguyễn Thị Kim Ngan', N'nguyenthikimngan@gmail.com', N'12345', 569000, 159000, N'no avatar', N'contact for work')
INSERT [dbo].[Account] ([id], [fullName], [email], [password], [liked], [followed], [avatar], [contact]) VALUES (3, N'Châu Quỳnh Phương', N'chauquynhphuong@gmail.com', N'12345', 1320000, 750000, N'no', N'no')
INSERT [dbo].[Account] ([id], [fullName], [email], [password], [liked], [followed], [avatar], [contact]) VALUES (4, N'HiHiNe', N'duavuithoi@gmail.com', N'12345', 0, 0, N'https://firebasestorage.googleapis.com/v0/b/swp391-f046d.appspot.com/o/Tiktok_BE%2Fadf3e10d-d2b9-4bfe-b84e-a5ce803b33d0.jpg?alt=media', N'So 1 ')
INSERT [dbo].[Account] ([id], [fullName], [email], [password], [liked], [followed], [avatar], [contact]) VALUES (5, NULL, N'nguoisaohoa@gmail.com', N'12345', 0, 0, N'https://firebasestorage.googleapis.com/v0/b/swp391-f046d.appspot.com/o/Tiktok_BE%2Ffcc90d14-d4cf-4230-8c9f-d452c35d05f0.png?alt=media', NULL)
INSERT [dbo].[Account] ([id], [fullName], [email], [password], [liked], [followed], [avatar], [contact]) VALUES (6, NULL, N'nguyenvanb@gmail.com', N'12345', 0, 0, N'https://firebasestorage.googleapis.com/v0/b/swp391-f046d.appspot.com/o/Tiktok_BE%2F85cfd2ba-4b1d-4558-8f16-0c5f20b7e698.png?alt=media', NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Video] ON 

INSERT [dbo].[Video] ([id], [title], [liked], [commented], [shared], [srcVideo], [idAccount]) VALUES (1, N'Video VuiVe', 45, 12, 7, N'no', 1)
INSERT [dbo].[Video] ([id], [title], [liked], [commented], [shared], [srcVideo], [idAccount]) VALUES (2, N'Funny', 565, 123, 69, N'no', 2)
INSERT [dbo].[Video] ([id], [title], [liked], [commented], [shared], [srcVideo], [idAccount]) VALUES (3, N'Huong Dan choi Game', 1964, 45, 644, N'no', 1)
SET IDENTITY_INSERT [dbo].[Video] OFF
GO
ALTER TABLE [dbo].[Video]  WITH CHECK ADD  CONSTRAINT [PK_IDACCOUNT] FOREIGN KEY([idAccount])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Video] CHECK CONSTRAINT [PK_IDACCOUNT]
GO
USE [master]
GO
ALTER DATABASE [TikTokDB] SET  READ_WRITE 
GO
