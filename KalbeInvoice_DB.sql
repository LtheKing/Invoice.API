USE [master]
GO
/****** Object:  Database [KalbeInvoice_DB]    Script Date: 3/10/2021 3:08:30 AM ******/
CREATE DATABASE [KalbeInvoice_DB] ON  PRIMARY 
( NAME = N'KalbeInvoice_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\KalbeInvoice_DB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'KalbeInvoice_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\KalbeInvoice_DB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [KalbeInvoice_DB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KalbeInvoice_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KalbeInvoice_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KalbeInvoice_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KalbeInvoice_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KalbeInvoice_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KalbeInvoice_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KalbeInvoice_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [KalbeInvoice_DB] SET  MULTI_USER 
GO
ALTER DATABASE [KalbeInvoice_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KalbeInvoice_DB] SET DB_CHAINING OFF 
GO
USE [KalbeInvoice_DB]
GO
/****** Object:  Table [dbo].[KLBCurrency]    Script Date: 3/10/2021 3:08:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KLBCurrency](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Currency] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KLBCustomer]    Script Date: 3/10/2021 3:08:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KLBCustomer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Address] [text] NULL,
	[LogoURL] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KLBCustomerPO]    Script Date: 3/10/2021 3:08:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KLBCustomerPO](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[PONumber] [varchar](50) NULL,
	[Description] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KLBInvoice]    Script Date: 3/10/2021 3:08:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KLBInvoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoInvoice] [varchar](50) NULL,
	[Sender] [text] NULL,
	[Customer] [varchar](255) NULL,
	[CustomerAddress] [text] NULL,
	[Date] [date] NULL,
	[DueDate] [date] NULL,
	[PONumber] [varchar](120) NULL,
	[SubTotal] [decimal](20, 2) NULL,
	[DiscountName] [varchar](120) NULL,
	[DiscountPersentation] [int] NULL,
	[Discount] [decimal](20, 2) NULL,
	[Total] [decimal](20, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KLBInvoiceDetail]    Script Date: 3/10/2021 3:08:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KLBInvoiceDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NULL,
	[ContentName] [text] NULL,
	[Quantity] [decimal](20, 2) NULL,
	[Rate] [decimal](20, 2) NULL,
	[Amount] [decimal](20, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KLBLanguage]    Script Date: 3/10/2021 3:08:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KLBLanguage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Language] [varchar](255) NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [KalbeInvoice_DB] SET  READ_WRITE 
GO
