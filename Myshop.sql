USE [master]
GO
/****** Object:  Database [Myshop]    Script Date: 02-02-2018 18:59:48 ******/
CREATE DATABASE [Myshop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Myshop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Myshop.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Myshop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Myshop_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Myshop] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Myshop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Myshop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Myshop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Myshop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Myshop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Myshop] SET ARITHABORT OFF 
GO
ALTER DATABASE [Myshop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Myshop] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Myshop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Myshop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Myshop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Myshop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Myshop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Myshop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Myshop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Myshop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Myshop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Myshop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Myshop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Myshop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Myshop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Myshop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Myshop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Myshop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Myshop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Myshop] SET  MULTI_USER 
GO
ALTER DATABASE [Myshop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Myshop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Myshop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Myshop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Myshop]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Controller] [nvarchar](50) NULL,
	[Action] [nvarchar](50) NULL,
	[Area] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NULL,
	[OuterException] [nvarchar](max) NULL,
	[InnerException] [nvarchar](max) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsResolved] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_AppDowntime]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_AppDowntime](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DownTimeStart] [datetime] NOT NULL,
	[DownTimeEnd] [datetime] NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[ShopId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Gbl_AppDowntime] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_AppModule]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gbl_Master_AppModule](
	[ModuleId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_AppModule] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gbl_Master_Bank]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Bank](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[BankName] [nvarchar](50) NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_Gbl_Master_Bank] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_BankAccount]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_BankAccount](
	[BankAccId] [int] IDENTITY(1,1) NOT NULL,
	[BankId] [int] NOT NULL,
	[AccTypeId] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
	[AccountName] [nvarchar](50) NOT NULL,
	[AccountNo] [nvarchar](20) NOT NULL,
	[BranchName] [nvarchar](50) NOT NULL,
	[BranchIFSC] [nvarchar](15) NULL,
	[BranchAddress] [nvarchar](100) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[AccHolderName] [nvarchar](50) NOT NULL,
 CONSTRAINT [_PK_Gbl_Master_BankAccount] PRIMARY KEY CLUSTERED 
(
	[BankAccId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_BankAccountType]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_BankAccountType](
	[AccountTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AccountType] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_BankAccountType] PRIMARY KEY CLUSTERED 
(
	[AccountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_BankCheque]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_BankCheque](
	[ChequeId] [int] IDENTITY(1,1) NOT NULL,
	[BankAccId] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
	[PageSize] [int] NOT NULL,
	[PageStartNo] [int] NOT NULL,
	[PageEndNo] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[Description] [nvarchar](150) NULL,
 CONSTRAINT [PK_Gbl_Master_BankCheque] PRIMARY KEY CLUSTERED 
(
	[ChequeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_BankChequeDetails]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_BankChequeDetails](
	[ChequePageId] [int] IDENTITY(1,1) NOT NULL,
	[ChequeBookId] [int] NOT NULL,
	[ChequeNo] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ShopId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[Desciption] [nvarchar](500) NULL,
	[IsUsed] [bit] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_BankChequeDetails] PRIMARY KEY CLUSTERED 
(
	[ChequePageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_Page]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gbl_Master_Page](
	[PageId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[PageName] [varchar](150) NOT NULL,
	[Url] [varchar](150) NOT NULL,
	[Description] [varchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_Page] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gbl_Master_PayMode]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_PayMode](
	[PayModeId] [int] IDENTITY(1,1) NOT NULL,
	[PayMode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Pay_Master_Mode] PRIMARY KEY CLUSTERED 
(
	[PayModeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Login]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoginDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsReset] [bit] NULL,
	[GUID] [uniqueidentifier] NULL,
	[ReserExpireTime] [datetime] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[CreationBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[OTPid] [varchar](100) NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MasterBrand]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterBrand](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[ShopId] [int] NOT NULL,
	[BrandName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MasterBrand] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterCategory]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterCategory](
	[CatId] [int] IDENTITY(1,1) NOT NULL,
	[ShopId] [int] NOT NULL,
	[CatName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MasterCategory] PRIMARY KEY CLUSTERED 
(
	[CatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterProduct]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterProduct](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[SubCatId] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[MinQuantity] [decimal](18, 4) NOT NULL,
	[ProductCode] [nvarchar](50) NULL,
	[UnitId] [int] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MasterProduct] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterSubCategory]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterSubCategory](
	[SubCatId] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
	[SubCatName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MasterSubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterUnit]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterUnit](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[ShopId] [int] NOT NULL,
	[UnitName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[IsSync] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MasterUnit] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterVendor]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterVendor](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[ShopId] [int] NOT NULL,
	[VendorName] [nvarchar](150) NOT NULL,
	[VendorMobile] [nvarchar](15) NOT NULL,
	[VendorAddress] [nvarchar](250) NULL,
	[Description] [nvarchar](200) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_MasterVendor] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shop]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Shop](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Address] [varchar](300) NOT NULL,
	[Mobile] [varchar](13) NOT NULL,
	[Distict] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Owner] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[CreationBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stk_Tr_Entry]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stk_Tr_Entry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
	[PayModeId] [int] NOT NULL,
	[DebitAccount] [int] NULL,
	[VendorReceiptNo] [nvarchar](10) NULL,
	[ShopReceiptEntryNo] [nvarchar](20) NULL,
	[ChequePageId] [int] NULL,
	[TotalAmt] [decimal](18, 4) NOT NULL,
	[AdditionalAmt] [decimal](18, 4) NULL,
	[PaidAmt] [decimal](18, 4) NOT NULL,
	[RemainingAmt] [decimal](18, 4) NOT NULL,
	[ReceiptDate] [datetime] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Stk_Tr_Entry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StockEntry]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockEntry](
	[StockTrId] [int] IDENTITY(1,1) NOT NULL,
	[StockMstId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
	[BrandId] [int] NOT NULL,
	[SellPrice] [decimal](18, 4) NOT NULL,
	[PurchasePrice] [decimal](18, 4) NOT NULL,
	[Qty] [decimal](18, 4) NOT NULL,
	[Color] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_StockEntry] PRIMARY KEY CLUSTERED 
(
	[StockTrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](250) NULL,
	[Name] [varchar](50) NULL,
	[Mobile] [varchar](13) NULL,
	[Photo] [varbinary](max) NULL,
	[UserType] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[CreationBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NULL,
	[IsBlocked] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[IsBlockAccess] [bit] NOT NULL,
	[Read] [bit] NOT NULL,
	[Write] [bit] NOT NULL,
	[Delete] [bit] NOT NULL,
	[Update] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[CreationBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserPermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserShopMapper]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserShopMapper](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[CreationBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserShopMapper] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserType]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[ss]    Script Date: 02-02-2018 18:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[ss](@id int)
returns table 
as
return select * from Gbl_Master_Bank;

GO
SET IDENTITY_INSERT [dbo].[ErrorLog] ON 

INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1, N'Admin', N'GetErrorLog', NULL, N'c:\Users\satish.sonkar\Documents\Visual Studio 2015\Projects\Myshop\Myshop\Areas\Global\Views\Admin\GetErrorLog.cshtml(71): error CS1525: Invalid expression term ''=''', N'c:\Users\satish.sonkar\Documents\Visual Studio 2015\Projects\Myshop\Myshop\Areas\Global\Views\Admin\GetErrorLog.cshtml(71): error CS1525: Invalid expression term ''=''', N'', 0, 0, 1, CAST(0x0000A831011DCD8D AS DateTime), CAST(0x0000A8320128E1F0 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (2, N'Admin', N'GetErrorLog', NULL, N'c:\Users\satish.sonkar\Documents\Visual Studio 2015\Projects\Myshop\Myshop\Areas\Global\Views\Admin\GetErrorLog.cshtml(71): error CS1525: Invalid expression term ''=''', N'c:\Users\satish.sonkar\Documents\Visual Studio 2015\Projects\Myshop\Myshop\Areas\Global\Views\Admin\GetErrorLog.cshtml(71): error CS1525: Invalid expression term ''=''', N'', 0, 0, 1, CAST(0x0000A831011DE1DA AS DateTime), CAST(0x0000A8320129A8F3 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (3, N'Admin', N'GetErrorLog', NULL, N'The parameters dictionary contains a null entry for parameter ''isAllLog'' of non-nullable type ''System.Boolean'' for method ''System.Web.Mvc.ActionResult GetErrorLog(Boolean)'' in ''Myshop.Areas.Global.Controllers.AdminController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'The parameters dictionary contains a null entry for parameter ''isAllLog'' of non-nullable type ''System.Boolean'' for method ''System.Web.Mvc.ActionResult GetErrorLog(Boolean)'' in ''Myshop.Areas.Global.Controllers.AdminController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'', 0, 0, 1, CAST(0x0000A831012BE85C AS DateTime), CAST(0x0000A832012AA0AC AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (4, N'Admin', N'GetErrorLog', NULL, N'The parameters dictionary contains a null entry for parameter ''isAllLog'' of non-nullable type ''System.Boolean'' for method ''System.Web.Mvc.ActionResult GetErrorLog(Boolean)'' in ''Myshop.Areas.Global.Controllers.AdminController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'The parameters dictionary contains a null entry for parameter ''isAllLog'' of non-nullable type ''System.Boolean'' for method ''System.Web.Mvc.ActionResult GetErrorLog(Boolean)'' in ''Myshop.Areas.Global.Controllers.AdminController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'', 0, 0, 1, CAST(0x0000A831012BF4AA AS DateTime), CAST(0x0000A8320129FF92 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (5, N'Menu', N'GetAppPageJson', NULL, N'The parameters dictionary contains a null entry for parameter ''moduleid'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.JsonResult GetAppPageJson(Int32)'' in ''Myshop.Areas.Global.Controllers.MenuController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'The parameters dictionary contains a null entry for parameter ''moduleid'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.JsonResult GetAppPageJson(Int32)'' in ''Myshop.Areas.Global.Controllers.MenuController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'', 0, 0, 0, CAST(0x0000A832012D389E AS DateTime), CAST(0x0000A832012D389E AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (6, N'Menu', N'GetAppPageJson', NULL, N'The parameters dictionary contains a null entry for parameter ''moduleid'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.JsonResult GetAppPageJson(Int32)'' in ''Myshop.Areas.Global.Controllers.MenuController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'The parameters dictionary contains a null entry for parameter ''moduleid'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.JsonResult GetAppPageJson(Int32)'' in ''Myshop.Areas.Global.Controllers.MenuController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'', 0, 0, 0, CAST(0x0000A832012D69B6 AS DateTime), CAST(0x0000A832012D69B6 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (7, N'Home', N'dashboard', NULL, N'Attempted to divide by zero.', N'Attempted to divide by zero.', N'No Inner exception', 0, 0, 0, CAST(0x0000A83400DEBD3B AS DateTime), CAST(0x0000A83400DEBD3B AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (8, N'Home', N'dashboard', NULL, N'Attempted to divide by zero.', N'Attempted to divide by zero.', N'No Inner exception', 0, 0, 0, CAST(0x0000A83400DF4739 AS DateTime), CAST(0x0000A83400DF4739 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (9, N'Home', N'dashboard', NULL, N'Attempted to divide by zero.', N'Attempted to divide by zero.', N'No Inner exception', 0, 0, 0, CAST(0x0000A83400E0386F AS DateTime), CAST(0x0000A83400E0386F AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (10, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A83800F41F90 AS DateTime), CAST(0x0000A83800F41F90 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1002, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A83A010FF2B0 AS DateTime), CAST(0x0000A83A010FF2B0 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1003, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A83A01104431 AS DateTime), CAST(0x0000A83A01104431 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1004, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A83A011167D3 AS DateTime), CAST(0x0000A83A011167D3 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1005, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A83A01189430 AS DateTime), CAST(0x0000A83A01189430 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1006, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A83C00A15A90 AS DateTime), CAST(0x0000A83C00A15A90 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1007, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A84001105589 AS DateTime), CAST(0x0000A84001105589 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1008, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A84001115911 AS DateTime), CAST(0x0000A84001115911 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1009, N'Admin', N'GetErrorLog', NULL, N'Index and length must refer to a location within the string.
Parameter name: length', N'Index and length must refer to a location within the string.
Parameter name: length', N'No Inner exception', 0, 0, 0, CAST(0x0000A840011205F4 AS DateTime), CAST(0x0000A840011205F4 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1010, N'Menu', N'GetAppModuleJson', NULL, N'Object reference not set to an instance of an object.', N'Object reference not set to an instance of an object.', N'No Inner exception', 0, 0, 0, CAST(0x0000A84A0110273C AS DateTime), CAST(0x0000A84A0110273C AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1011, N'Setting', N'SetDowntime', NULL, N'An error occurred while updating the entries. See the inner exception for details.', N'An error occurred while updating the entries. See the inner exception for details.', N'An error occurred while updating the entries. See the inner exception for details.', 0, 0, 0, CAST(0x0000A84A01138814 AS DateTime), CAST(0x0000A84A01138814 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (1012, N'login', N'getLogin', NULL, N'String reference not set to an instance of a String.
Parameter name: s', N'String reference not set to an instance of a String.
Parameter name: s', N'No Inner exception', 0, 0, 0, CAST(0x0000A85E0114B479 AS DateTime), CAST(0x0000A85E0114B479 AS DateTime))
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate]) VALUES (2012, N'Stock', N'GetSubCatListJosn', NULL, N'The parameters dictionary contains a null entry for parameter ''CatId'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.JsonResult GetSubCatListJosn(Int32)'' in ''Myshop.Controllers.CommonController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'The parameters dictionary contains a null entry for parameter ''CatId'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.JsonResult GetSubCatListJosn(Int32)'' in ''Myshop.Controllers.CommonController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'No Inner exception', 0, 0, 0, CAST(0x0000A879012232FC AS DateTime), CAST(0x0000A879012232FC AS DateTime))
SET IDENTITY_INSERT [dbo].[ErrorLog] OFF
SET IDENTITY_INSERT [dbo].[Gbl_AppDowntime] ON 

INSERT [dbo].[Gbl_AppDowntime] ([Id], [DownTimeStart], [DownTimeEnd], [Message], [ShopId], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (0, CAST(0x0000A84A011494B0 AS DateTime), CAST(0x0000A84A01250F70 AS DateTime), N'dd', 0, CAST(0x0000A84A0114C22C AS DateTime), CAST(0x0000A84A0114C25D AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[Gbl_AppDowntime] ([Id], [DownTimeStart], [DownTimeEnd], [Message], [ShopId], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1, CAST(0x0000A85B00DE7920 AS DateTime), CAST(0x0000A85B00EEF3E0 AS DateTime), N'Due to database issue we are planned activity', 0, CAST(0x0000A85D00DE949B AS DateTime), CAST(0x0000A85D00DED110 AS DateTime), 2, 2, 0, 0)
SET IDENTITY_INSERT [dbo].[Gbl_AppDowntime] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_AppModule] ON 

INSERT [dbo].[Gbl_Master_AppModule] ([ModuleId], [ModuleName], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (1, N'Global', N'Application level Settings', 2, 2, CAST(0x0000A82A00000000 AS DateTime), CAST(0x0000A82B00B9DA26 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_AppModule] ([ModuleId], [ModuleName], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (2, N'Stock', N'Stock Management', 2, 2, CAST(0x0000A82B00B962EA AS DateTime), CAST(0x0000A82B00B962EA AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_AppModule] ([ModuleId], [ModuleName], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (3, N'Main App', N'Main Application', 2, 2, CAST(0x0000A82B00B98EEF AS DateTime), CAST(0x0000A82B00B99F97 AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[Gbl_Master_AppModule] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Bank] ON 

INSERT [dbo].[Gbl_Master_Bank] ([BankId], [BankName], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate], [Description]) VALUES (1, N'SBI', 0, 0, CAST(0x0000A823010BB023 AS DateTime), 2, 2, CAST(0x0000A823010C508C AS DateTime), N'State Bank of India')
INSERT [dbo].[Gbl_Master_Bank] ([BankId], [BankName], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate], [Description]) VALUES (2, N'PNB', 0, 0, CAST(0x0000A823010CB686 AS DateTime), 2, 2, CAST(0x0000A823010CB686 AS DateTime), N'Panjab Natioal Bank')
INSERT [dbo].[Gbl_Master_Bank] ([BankId], [BankName], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate], [Description]) VALUES (3, N'Bank of Baroda', 0, 0, CAST(0x0000A82401241B52 AS DateTime), 2, 2, CAST(0x0000A879012607A6 AS DateTime), N'BOB')
SET IDENTITY_INSERT [dbo].[Gbl_Master_Bank] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankAccount] ON 

INSERT [dbo].[Gbl_Master_BankAccount] ([BankAccId], [BankId], [AccTypeId], [ShopId], [AccountName], [AccountNo], [BranchName], [BranchIFSC], [BranchAddress], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate], [AccHolderName]) VALUES (1, 1, 1, 2, N'Satish Kumar Sonker', N'31240694185', N'Jhusi', N'SBIN0005440', N'Jhusi Allahabad', 0, 0, CAST(0x0000A824011F5EFB AS DateTime), 2, 2, CAST(0x0000A82401253D50 AS DateTime), N'Satish Kumar Sonker - Sam')
INSERT [dbo].[Gbl_Master_BankAccount] ([BankAccId], [BankId], [AccTypeId], [ShopId], [AccountName], [AccountNo], [BranchName], [BranchIFSC], [BranchAddress], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate], [AccHolderName]) VALUES (2, 3, 1, 2, N'Satish Kumar Sonker', N'10000245444', N'Jhusi', N'BOB0R0005440', N'Jhusi Allahabad', 0, 0, CAST(0x0000A824012518CE AS DateTime), 2, 2, CAST(0x0000A824012518CE AS DateTime), N'Satish Kumar Sonker')
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankAccount] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankAccountType] ON 

INSERT [dbo].[Gbl_Master_BankAccountType] ([AccountTypeId], [AccountType], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (1, N'Saving', N'Saving use only', 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A823011DBADE AS DateTime))
INSERT [dbo].[Gbl_Master_BankAccountType] ([AccountTypeId], [AccountType], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (2, N'Current', N'Commercial use only', 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A823011D5966 AS DateTime))
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankAccountType] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankCheque] ON 

INSERT [dbo].[Gbl_Master_BankCheque] ([ChequeId], [BankAccId], [ShopId], [PageSize], [PageStartNo], [PageEndNo], [IssueDate], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate], [Description]) VALUES (1007, 2, 2, 36, 124526, 124562, CAST(0x0000A86D00000000 AS DateTime), 0, 0, CAST(0x0000A86D01386C6B AS DateTime), 2, 2, CAST(0x0000A86D01386C6B AS DateTime), N'Testing')
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankCheque] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankChequeDetails] ON 

INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (60, 1007, 124526, 2, CAST(0x0000A86D01386C6F AS DateTime), 2, CAST(0x0000A86D0138E39B AS DateTime), 2, 0, 0, N'Auto Generated till now', 1)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (61, 1007, 124527, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D0139E3C6 AS DateTime), 2, 0, 0, N'This cheque has been issued.', 1)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (62, 1007, 124528, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (63, 1007, 124529, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (64, 1007, 124530, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (65, 1007, 124531, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (66, 1007, 124532, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (67, 1007, 124533, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (68, 1007, 124534, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (69, 1007, 124535, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (70, 1007, 124536, 2, CAST(0x0000A86D01386C70 AS DateTime), 2, CAST(0x0000A86D01386C70 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (71, 1007, 124537, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A86D01386C71 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (72, 1007, 124538, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A86D01386C71 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (73, 1007, 124539, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A86D01386C71 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (74, 1007, 124540, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A86D01386C71 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (75, 1007, 124541, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A86D01386C71 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (76, 1007, 124542, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A86D01386C71 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (77, 1007, 124543, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A86D01386C71 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (78, 1007, 124544, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A86D01386C71 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (79, 1007, 124545, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A87A00DF0978 AS DateTime), 2, 0, 0, N'This cheque has been issued.', 1)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (80, 1007, 124546, 2, CAST(0x0000A86D01386C71 AS DateTime), 2, CAST(0x0000A87B01380B40 AS DateTime), 2, 0, 0, N'This cheque has been issued.', 1)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (81, 1007, 124547, 2, CAST(0x0000A86D01386C72 AS DateTime), 2, CAST(0x0000A86D01386C72 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (82, 1007, 124548, 2, CAST(0x0000A86D01386C72 AS DateTime), 2, CAST(0x0000A86D01386C72 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (83, 1007, 124549, 2, CAST(0x0000A86D01386C72 AS DateTime), 2, CAST(0x0000A86D01386C72 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (84, 1007, 124550, 2, CAST(0x0000A86D01386C72 AS DateTime), 2, CAST(0x0000A86D01386C72 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (85, 1007, 124551, 2, CAST(0x0000A86D01386C72 AS DateTime), 2, CAST(0x0000A86D01386C72 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (86, 1007, 124552, 2, CAST(0x0000A86D01386C72 AS DateTime), 2, CAST(0x0000A86D01386C72 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (87, 1007, 124553, 2, CAST(0x0000A86D01386C73 AS DateTime), 2, CAST(0x0000A86D01386C73 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (88, 1007, 124554, 2, CAST(0x0000A86D01386C73 AS DateTime), 2, CAST(0x0000A86D01386C73 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (89, 1007, 124555, 2, CAST(0x0000A86D01386C73 AS DateTime), 2, CAST(0x0000A86D01386C73 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (90, 1007, 124556, 2, CAST(0x0000A86D01386C73 AS DateTime), 2, CAST(0x0000A86D01386C73 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (91, 1007, 124557, 2, CAST(0x0000A86D01386C73 AS DateTime), 2, CAST(0x0000A86D01386C73 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (92, 1007, 124558, 2, CAST(0x0000A86D01386C74 AS DateTime), 2, CAST(0x0000A86D01386C74 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (93, 1007, 124559, 2, CAST(0x0000A86D01386C74 AS DateTime), 2, CAST(0x0000A86D01386C74 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (94, 1007, 124560, 2, CAST(0x0000A86D01386C74 AS DateTime), 2, CAST(0x0000A86D01386C74 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (95, 1007, 124561, 2, CAST(0x0000A86D01386C74 AS DateTime), 2, CAST(0x0000A86D01386C74 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
INSERT [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId], [ChequeBookId], [ChequeNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [ShopId], [IsDeleted], [IsSync], [Desciption], [IsUsed]) VALUES (96, 1007, 124562, 2, CAST(0x0000A86D01386C74 AS DateTime), 2, CAST(0x0000A86D01386C74 AS DateTime), 2, 0, 0, N'Auto Generated till now', 0)
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankChequeDetails] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Page] ON 

INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (1, 1, 0, N'Masters', N'/Global/Master', N'Master Menu', 2, 2, CAST(0x0000A82A00000000 AS DateTime), CAST(0x0000A8320132183B AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (2, 1, 1, N'Add App Module', N'/Global/Menu/AddAppModule', NULL, 2, 2, CAST(0x0000A82A00000000 AS DateTime), CAST(0x0000A83201322B83 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (3, 1, 1, N'Add App Page', N'/global/menu/addapppage', NULL, 2, 2, CAST(0x0000A82A00000000 AS DateTime), CAST(0x0000A82A00000000 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (4, 3, 0, N'Dashboard', N'/Home/Dashboard', N'Main App Home Page', 2, 2, CAST(0x0000A82B012AB97F AS DateTime), CAST(0x0000A82C00EDBFE0 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (5, 1, 0, N'Users Main Menu', N'/Globle/Menu/Users', N'Users Main Menu', 2, 2, CAST(0x0000A82C00F5EA7E AS DateTime), CAST(0x0000A82C00F5EA7E AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (6, 1, 5, N'User Permission', N'/Global/User/GetPermission', N'Set Permission on URL', 2, 2, CAST(0x0000A82C00F64747 AS DateTime), CAST(0x0000A82C00F64747 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (7, 1, 0, N'Admin', N'/Global/Admin', N'Admin Controls', 2, 2, CAST(0x0000A832012D36C1 AS DateTime), CAST(0x0000A832012D36C1 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (8, 1, 7, N'ErrorLog', N'/Global/Admin/GetErrorLog', N'See All Error ', 2, 2, CAST(0x0000A8320130DDD7 AS DateTime), CAST(0x0000A8320130DDD7 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (9, 1, 0, N'Main Page', N'/global/main/home', N'Global Dashboard', 2, 2, CAST(0x0000A83200000000 AS DateTime), CAST(0x0000A83200000000 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (11, 1, 1, N'Add Bank', N'/Global/Masters/getBank', N'Add New Bank', 2, 2, CAST(0x0000A838011CBF5C AS DateTime), CAST(0x0000A838011CBF5C AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (12, 1, 1, N'Add Bank Account', N'/Global/Masters/GetBankAccount', N'Add Bank Account', 2, 2, CAST(0x0000A838012063EC AS DateTime), CAST(0x0000A838012063EC AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (13, 1, 1, N'Account Type', N'/Global/Masters/GetAccountType', N'Add Account Type', 2, 2, CAST(0x0000A83801215629 AS DateTime), CAST(0x0000A83801215774 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (14, 1, 1, N'Add Payment mode', N'/Global/Masters/GetPayMode', N'Add Payment mode', 2, 2, CAST(0x0000A83801223480 AS DateTime), CAST(0x0000A83801223480 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (15, 1, 0, N'Setting', N'/globle/setting', N'Application settings', 2, 2, CAST(0x0000A83801235382 AS DateTime), CAST(0x0000A83801235382 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (16, 1, 15, N'Change Shop', N'/Global/Setting/GetShop', N'Change Shop', 2, 2, CAST(0x0000A83801239CD6 AS DateTime), CAST(0x0000A83801239CD6 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (17, 1, 1, N'Add Cheque Book', N'/Global/Masters/GetCheque', N'Add Cheque Book', 2, 2, CAST(0x0000A83900E32370 AS DateTime), CAST(0x0000A83900E32370 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (1007, 2, 0, N'Stock Home', N'/StockManagement/Stock/Main', N'Stock Home', 2, 2, CAST(0x0000A83C00A1F288 AS DateTime), CAST(0x0000A83C00A1F288 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_Page] ([PageId], [ModuleId], [ParentId], [PageName], [Url], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (1008, 1, 15, N'Add Downtime', N'/Global/Setting/GetDowntime', N'Add Application downtime', 2, 2, CAST(0x0000A83801239CD6 AS DateTime), CAST(0x0000A83801239CD6 AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[Gbl_Master_Page] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_PayMode] ON 

INSERT [dbo].[Gbl_Master_PayMode] ([PayModeId], [PayMode], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (1, N'Cash', NULL, 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A82200000000 AS DateTime))
INSERT [dbo].[Gbl_Master_PayMode] ([PayModeId], [PayMode], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (2, N'Debit Card', NULL, 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A82200000000 AS DateTime))
INSERT [dbo].[Gbl_Master_PayMode] ([PayModeId], [PayMode], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (3, N'Credit Card', NULL, 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A82200000000 AS DateTime))
INSERT [dbo].[Gbl_Master_PayMode] ([PayModeId], [PayMode], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (4, N'Online Transfer', NULL, 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A82200000000 AS DateTime))
INSERT [dbo].[Gbl_Master_PayMode] ([PayModeId], [PayMode], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (5, N'Cheque', N'Cheque No.', 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A82400C6ABEA AS DateTime))
SET IDENTITY_INSERT [dbo].[Gbl_Master_PayMode] OFF
SET IDENTITY_INSERT [dbo].[Login] ON 

INSERT [dbo].[Login] ([Id], [UserId], [LoginDate], [IsDeleted], [IsSync], [IsReset], [GUID], [ReserExpireTime], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [OTPid]) VALUES (1, 2, CAST(0x0000A87B01327284 AS DateTime), 0, 0, 1, N'b0536433-ad9e-4d42-a424-a5f3b80aa4f2', CAST(0x0000A81900C1308F AS DateTime), CAST(0x0000A81601139D3D AS DateTime), CAST(0x0000A87B01327284 AS DateTime), 2, 2, N'')
INSERT [dbo].[Login] ([Id], [UserId], [LoginDate], [IsDeleted], [IsSync], [IsReset], [GUID], [ReserExpireTime], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [OTPid]) VALUES (2, 5, CAST(0x0000A83201317215 AS DateTime), 0, 0, NULL, NULL, NULL, CAST(0x0000A82501296260 AS DateTime), CAST(0x0000A83201317215 AS DateTime), 5, 5, NULL)
SET IDENTITY_INSERT [dbo].[Login] OFF
SET IDENTITY_INSERT [dbo].[MasterBrand] ON 

INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 2, N'Outlaw', N'French cut 1', 0, 0, 2, 2, CAST(0x0000A81C00FED5ED AS DateTime), CAST(0x0000A82000FA1386 AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 2, N'Levies', N'Levies Jeans', 0, 0, 2, 2, CAST(0x0000A81C00FF3D2A AS DateTime), CAST(0x0000A81C00FF3D2A AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (3, 2, N'Cobbs Italy', N'Cobbs Italy Shirts', 0, 0, 2, 2, CAST(0x0000A81C00FFA17F AS DateTime), CAST(0x0000A81C00FFA17F AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (4, 2, N'Reebok', N'Printed Flower 1', 0, 0, 2, 2, CAST(0x0000A81C0101DCD4 AS DateTime), CAST(0x0000A82000F8205B AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (5, 2, N'Sam', N'Testing', 0, 0, 2, 2, CAST(0x0000A81C010383F6 AS DateTime), CAST(0x0000A81F00E268A6 AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (6, 2, N'Sam & Nick', NULL, 0, 0, 2, 2, CAST(0x0000A81C01039C3C AS DateTime), CAST(0x0000A81C01039C3C AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (7, 3, N'Outlaw', NULL, 0, 0, 2, 2, CAST(0x0000A81C010588FC AS DateTime), CAST(0x0000A81C010588FC AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (8, 2, N'Amul', N'MACHO 1', 0, 1, 2, 2, CAST(0x0000A81F00D82436 AS DateTime), CAST(0x0000A81F00DDDC76 AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (9, 2, N'Amul', N'MACHO 1', 0, 0, 2, 2, CAST(0x0000A81F00DDF4A6 AS DateTime), CAST(0x0000A81F00DDF4A6 AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (10, 2, N'Samsung', N'Samsung Electronics', 0, 0, 2, 2, CAST(0x0000A87B00C2ED42 AS DateTime), CAST(0x0000A87B00C2ED42 AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (11, 2, N'Surya', N'Surya', 0, 0, 2, 2, CAST(0x0000A87B00C31E0D AS DateTime), CAST(0x0000A87B00C31E0D AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (12, 2, N'Wipro', N'Wipro', 0, 0, 2, 2, CAST(0x0000A87B00C32B6E AS DateTime), CAST(0x0000A87B00C32B6E AS DateTime))
INSERT [dbo].[MasterBrand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (13, 2, N'Phillips', N'Phillips', 0, 0, 2, 2, CAST(0x0000A87B00C3497F AS DateTime), CAST(0x0000A87B00C3497F AS DateTime))
SET IDENTITY_INSERT [dbo].[MasterBrand] OFF
SET IDENTITY_INSERT [dbo].[MasterCategory] ON 

INSERT [dbo].[MasterCategory] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 2, N'Electronic', N'Electronic Goods', 0, 0, 2, 2, CAST(0x0000A81C011201BB AS DateTime), CAST(0x0000A81F00FD623D AS DateTime))
INSERT [dbo].[MasterCategory] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 2, N'Bulb', N'Bajaj Bulb', 0, 0, 2, 2, CAST(0x0000A81D00EC37AB AS DateTime), CAST(0x0000A81F012B6E70 AS DateTime))
INSERT [dbo].[MasterCategory] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (3, 2, N'Cloths', N'Cloths Sample', 0, 0, 2, 2, CAST(0x0000A81D010FC192 AS DateTime), CAST(0x0000A81F01213DA3 AS DateTime))
INSERT [dbo].[MasterCategory] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (4, 2, N'Cosmetic', N'Beauty Product', 0, 0, 2, 2, CAST(0x0000A81F00FC8AE5 AS DateTime), CAST(0x0000A81F012B7801 AS DateTime))
INSERT [dbo].[MasterCategory] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (5, 2, N'Bulb', N'Bajaj Bulb', 0, 1, 2, 2, CAST(0x0000A81F011F7399 AS DateTime), CAST(0x0000A81F011F7B09 AS DateTime))
INSERT [dbo].[MasterCategory] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (6, 2, N'Bulb', N'Bajaj Bulb', 0, 1, 2, 2, CAST(0x0000A81F011FF975 AS DateTime), CAST(0x0000A81F01203017 AS DateTime))
INSERT [dbo].[MasterCategory] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (7, 2, N'Bulb', N'Bajaj Bulb', 0, 1, 2, 2, CAST(0x0000A81F012033B0 AS DateTime), CAST(0x0000A81F01203D31 AS DateTime))
SET IDENTITY_INSERT [dbo].[MasterCategory] OFF
SET IDENTITY_INSERT [dbo].[MasterProduct] ON 

INSERT [dbo].[MasterProduct] ([ProductId], [SubCatId], [ShopId], [ProductName], [Description], [MinQuantity], [ProductCode], [UnitId], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 7, 2, N'Under wear', N'French cut', CAST(5.0000 AS Decimal(18, 4)), N'UW001', 1, 0, 0, 2, 2, CAST(0x0000A81D0123D763 AS DateTime), CAST(0x0000A81D0123D931 AS DateTime))
INSERT [dbo].[MasterProduct] ([ProductId], [SubCatId], [ShopId], [ProductName], [Description], [MinQuantity], [ProductCode], [UnitId], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (3, 1, 2, N'100 Watt LED', N'Red LED', CAST(5.0000 AS Decimal(18, 4)), N'LEDBULD002', 1, 0, 0, 2, 2, CAST(0x0000A81E00F3CE34 AS DateTime), CAST(0x0000A81E00F3CE34 AS DateTime))
INSERT [dbo].[MasterProduct] ([ProductId], [SubCatId], [ShopId], [ProductName], [Description], [MinQuantity], [ProductCode], [UnitId], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (4, 7, 2, N'Panty', N'Printed Flower', CAST(6.0000 AS Decimal(18, 4)), N'UW002', 1, 0, 0, 2, 2, CAST(0x0000A81E00F6D50F AS DateTime), CAST(0x0000A820012EDFD6 AS DateTime))
INSERT [dbo].[MasterProduct] ([ProductId], [SubCatId], [ShopId], [ProductName], [Description], [MinQuantity], [ProductCode], [UnitId], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (5, 4, 2, N'Bra', N'French cut', CAST(56.0000 AS Decimal(18, 4)), N'BR001', 1, 0, 0, 2, 2, CAST(0x0000A824011AEA0F AS DateTime), CAST(0x0000A824011C10D9 AS DateTime))
INSERT [dbo].[MasterProduct] ([ProductId], [SubCatId], [ShopId], [ProductName], [Description], [MinQuantity], [ProductCode], [UnitId], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (6, 2, 2, N'Samsung 42" LED ', N'Samsung 42" LED FHD SoundTrack', CAST(2.0000 AS Decimal(18, 4)), N'TV00120', 1, 0, 0, 2, 2, CAST(0x0000A87B00C2B982 AS DateTime), CAST(0x0000A87B00C2B982 AS DateTime))
SET IDENTITY_INSERT [dbo].[MasterProduct] OFF
SET IDENTITY_INSERT [dbo].[MasterSubCategory] ON 

INSERT [dbo].[MasterSubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 1, 2, N'Bulb', N'Bajaj Bulb', 0, 0, 2, 2, CAST(0x0000A81D00EC99C7 AS DateTime), CAST(0x0000A81D00EC99C7 AS DateTime))
INSERT [dbo].[MasterSubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 1, 2, N'Television', N'LG TV', 0, 0, 2, 2, CAST(0x0000A81D00ECAE8F AS DateTime), CAST(0x0000A81D00ECAE8F AS DateTime))
INSERT [dbo].[MasterSubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (3, 3, 2, N'Mens', NULL, 0, 0, 2, 2, CAST(0x0000A81D010FDAA3 AS DateTime), CAST(0x0000A81D010FDAA3 AS DateTime))
INSERT [dbo].[MasterSubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (4, 3, 2, N'Womans', N'W', 0, 0, 2, 2, CAST(0x0000A81D010FE470 AS DateTime), CAST(0x0000A82000D6C2E0 AS DateTime))
INSERT [dbo].[MasterSubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (5, 3, 2, N'Kids', NULL, 0, 0, 2, 2, CAST(0x0000A81D010FF043 AS DateTime), CAST(0x0000A81D010FF043 AS DateTime))
INSERT [dbo].[MasterSubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (6, 3, 2, N'Saree', NULL, 0, 0, 2, 2, CAST(0x0000A81D010FFD7C AS DateTime), CAST(0x0000A81D010FFD7C AS DateTime))
INSERT [dbo].[MasterSubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (7, 3, 2, N'Under garments', NULL, 0, 0, 2, 2, CAST(0x0000A81D01100D97 AS DateTime), CAST(0x0000A81D01100D97 AS DateTime))
INSERT [dbo].[MasterSubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (8, 1, 2, N'Radio', N'Old Radio', 0, 0, 2, 2, CAST(0x0000A81F012C7DCE AS DateTime), CAST(0x0000A81F012C7DCE AS DateTime))
SET IDENTITY_INSERT [dbo].[MasterSubCategory] OFF
SET IDENTITY_INSERT [dbo].[MasterUnit] ON 

INSERT [dbo].[MasterUnit] ([UnitId], [ShopId], [UnitName], [Description], [IsSync], [IsActive], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 2, N'Pcs', N'1 Piece', 0, 0, 0, 2, 2, CAST(0x0000A81D00CCA207 AS DateTime), CAST(0x0000A82000DDEC30 AS DateTime))
INSERT [dbo].[MasterUnit] ([UnitId], [ShopId], [UnitName], [Description], [IsSync], [IsActive], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 2, N'Packet', N'1 Packet', 0, 0, 0, 2, 2, CAST(0x0000A81D00CD6D71 AS DateTime), CAST(0x0000A81D00CD6D71 AS DateTime))
SET IDENTITY_INSERT [dbo].[MasterUnit] OFF
SET IDENTITY_INSERT [dbo].[MasterVendor] ON 

INSERT [dbo].[MasterVendor] ([VendorId], [ShopId], [VendorName], [VendorMobile], [VendorAddress], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (1, 2, N'Sam & Sam', N'9990614499', N'Pur. Haweliya Jhusi Allahabad', NULL, 0, 0, CAST(0x0000A82300B948BF AS DateTime), 2, CAST(0x0000A82300F7F43B AS DateTime), 2)
INSERT [dbo].[MasterVendor] ([VendorId], [ShopId], [VendorName], [VendorMobile], [VendorAddress], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (2, 2, N'Pooja Garments', N'9682396133', N'U-717-8 Group floor, U Block, DLF phase 3,', N'Sam', 0, 0, CAST(0x0000A82300F8731E AS DateTime), 2, CAST(0x0000A82300F8731E AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[MasterVendor] OFF
SET IDENTITY_INSERT [dbo].[Shop] ON 

INSERT [dbo].[Shop] ([Id], [Name], [Address], [Mobile], [Distict], [State], [Owner], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (2, N'Sonkers Garment Shop', N'Bus Stop, Jagdeeshpur road, Jais', N'9990614499', N'Amethi', N'Utter Pradesh', 2, CAST(0x0000A81600000000 AS DateTime), CAST(0x0000A81600000000 AS DateTime), 1, 1, 0, 0)
INSERT [dbo].[Shop] ([Id], [Name], [Address], [Mobile], [Distict], [State], [Owner], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (3, N'Satish Sam Shop', N'Bus Stop, Jagdeeshpur road, Jais', N'9990614499', N'Amethi', N'Utter Pradesh', 2, CAST(0x0000A81600000000 AS DateTime), CAST(0x0000A81600000000 AS DateTime), 1, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Shop] OFF
SET IDENTITY_INSERT [dbo].[Stk_Tr_Entry] ON 

INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 2, 2, 5, 2, N'14255', N'32323', 60, CAST(0.0000 AS Decimal(18, 4)), CAST(2.0000 AS Decimal(18, 4)), CAST(14.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0x0000A86D00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A86D0138E378 AS DateTime), CAST(0x0000A86D0138E378 AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 2, 2, 5, 2, N'qwee10', N'32323', 61, CAST(0.0000 AS Decimal(18, 4)), CAST(1.0000 AS Decimal(18, 4)), CAST(2.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0x0000A86D00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A86D0139E3B6 AS DateTime), CAST(0x0000A86D0139E3B6 AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1002, 2, 2, 5, 2, N'qwee10', N'eerte', 79, CAST(0.0000 AS Decimal(18, 4)), CAST(1.0000 AS Decimal(18, 4)), CAST(1.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0x0000A87A00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A87A00DF08C7 AS DateTime), CAST(0x0000A87A00DF08C7 AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1003, 2, 2, 5, 2, N'qwee10', N'eerte', 75, CAST(0.0000 AS Decimal(18, 4)), CAST(10.0000 AS Decimal(18, 4)), CAST(1010.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0x0000A87A00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A87A010225EB AS DateTime), CAST(0x0000A87A0102279B AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1004, 2, 2, 5, 2, N'255', N'5522', 62, CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0x0000A87B00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A87B010FFBB9 AS DateTime), CAST(0x0000A87B010FFBB9 AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1007, 2, 2, 1, 0, N'14255', N'32323', 0, CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0x0000A87B00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A87B012F96AC AS DateTime), CAST(0x0000A87B012F982C AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1008, 2, 2, 1, 0, N'22222', N'5525', 0, CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(50000.0000 AS Decimal(18, 4)), CAST(904.0000 AS Decimal(18, 4)), CAST(0x0000A87B00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A87B013061F9 AS DateTime), CAST(0x0000A87B013061F9 AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1009, 2, 2, 5, 2, N'14255', N'5522', 62, CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(4520.0000 AS Decimal(18, 4)), CAST(0x0000A87B00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A87B01313474 AS DateTime), CAST(0x0000A87B01313474 AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1010, 2, 2, 3, 2, N'14255', N'32323', 0, CAST(2500.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(2500.0000 AS Decimal(18, 4)), CAST(0x0000A87B00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A87B01346B2E AS DateTime), CAST(0x0000A87B01346B2E AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1011, 2, 2, 5, 2, N'2018020201', N'32323', 80, CAST(15000.0000 AS Decimal(18, 4)), CAST(120.0000 AS Decimal(18, 4)), CAST(15020.0000 AS Decimal(18, 4)), CAST(100.0000 AS Decimal(18, 4)), CAST(0x0000A87B00000000 AS DateTime), 0, 0, 2, 2, CAST(0x0000A87B01380B32 AS DateTime), CAST(0x0000A87B01380B32 AS DateTime))
SET IDENTITY_INSERT [dbo].[Stk_Tr_Entry] OFF
SET IDENTITY_INSERT [dbo].[StockEntry] ON 

INSERT [dbo].[StockEntry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (1, 1003, 3, 2, 9, CAST(145.0000 AS Decimal(18, 4)), CAST(125.0000 AS Decimal(18, 4)), CAST(20.0000 AS Decimal(18, 4)), N'#000000', N'ddd', 0, 0, CAST(0x0000A87A01023363 AS DateTime), 2, CAST(0x0000A87A01023319 AS DateTime), 2)
INSERT [dbo].[StockEntry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (2, 1003, 2, 2, 5, CAST(65.0000 AS Decimal(18, 4)), CAST(60.0000 AS Decimal(18, 4)), CAST(25.0000 AS Decimal(18, 4)), N'#000000', N'WWWE', 0, 0, CAST(0x0000A87A01026797 AS DateTime), 2, CAST(0x0000A87A01026719 AS DateTime), 2)
INSERT [dbo].[StockEntry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (4, 1007, 6, 2, 10, CAST(45852.0000 AS Decimal(18, 4)), CAST(45852.0000 AS Decimal(18, 4)), CAST(2.0000 AS Decimal(18, 4)), N'#000000', NULL, 0, 0, CAST(0x0000A87B012FA54D AS DateTime), 2, CAST(0x0000A87B012FA532 AS DateTime), 2)
INSERT [dbo].[StockEntry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (5, 1008, 6, 2, 10, CAST(25452.0000 AS Decimal(18, 4)), CAST(25452.0000 AS Decimal(18, 4)), CAST(2.0000 AS Decimal(18, 4)), N'#000000', NULL, 0, 0, CAST(0x0000A87B013061FA AS DateTime), 2, CAST(0x0000A87B013061FA AS DateTime), 2)
INSERT [dbo].[StockEntry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (6, 1009, 4, 2, 2, CAST(500.0000 AS Decimal(18, 4)), CAST(452.0000 AS Decimal(18, 4)), CAST(10.0000 AS Decimal(18, 4)), N'#000000', NULL, 0, 0, CAST(0x0000A87B01313475 AS DateTime), 2, CAST(0x0000A87B01313475 AS DateTime), 2)
INSERT [dbo].[StockEntry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (7, 1010, 2, 2, 5, CAST(150.0000 AS Decimal(18, 4)), CAST(125.0000 AS Decimal(18, 4)), CAST(20.0000 AS Decimal(18, 4)), N'#000000', NULL, 0, 0, CAST(0x0000A87B01346B33 AS DateTime), 2, CAST(0x0000A87B01346B33 AS DateTime), 2)
INSERT [dbo].[StockEntry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (8, 1011, 5, 2, 1, CAST(120.0000 AS Decimal(18, 4)), CAST(120.0000 AS Decimal(18, 4)), CAST(125.0000 AS Decimal(18, 4)), N'#000000', NULL, 0, 0, CAST(0x0000A87B01380B33 AS DateTime), 2, CAST(0x0000A87B01380B33 AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[StockEntry] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Username], [Password], [Name], [Mobile], [Photo], [UserType], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted], [IsActive], [IsBlocked]) VALUES (2, N'btech.csit@gmail.com', N'1AD6CA41BBB894B2D3719C118259CDF8', N'Satish', N'9990614499', NULL, 1, CAST(0x0000A81600000000 AS DateTime), CAST(0x0000A83800E322D9 AS DateTime), 1, 2, 0, 0, 1, 0)
INSERT [dbo].[User] ([Id], [Username], [Password], [Name], [Mobile], [Photo], [UserType], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted], [IsActive], [IsBlocked]) VALUES (5, N'Ramnaresh.sonkar@gmail.com', N'870DA6596C3F6CA4CAB8108BDDE98953', N'Ram naresh', N'9682396133', NULL, 2, CAST(0x0000A8250129539E AS DateTime), CAST(0x0000A8250129539E AS DateTime), 2, 2, 0, 0, 1, 0)
INSERT [dbo].[User] ([Id], [Username], [Password], [Name], [Mobile], [Photo], [UserType], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted], [IsActive], [IsBlocked]) VALUES (6, N'rishi.sonkar@gmail.com', N'96E79218965EB72C92A549DD5A330112', N'Rishi', N'9999061457', NULL, 2, CAST(0x0000A825012DB922 AS DateTime), CAST(0x0000A82D01049A39 AS DateTime), 2, 2, 0, 0, 1, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserPermission] ON 

INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1, 2, 2, 0, 1, 1, 1, 1, CAST(0x0000A82A00000000 AS DateTime), CAST(0x0000A82A00000000 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (2, 2, 3, 0, 1, 1, 1, 1, CAST(0x0000A82A00000000 AS DateTime), CAST(0x0000A82A00000000 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (3, 2, 6, 0, 1, 1, 1, 1, CAST(0x0000A82C00000000 AS DateTime), CAST(0x0000A82C00000000 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (10, 2, 4, 0, 1, 1, 1, 1, CAST(0x0000A82D00B70A0A AS DateTime), CAST(0x0000A82D010274CC AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (13, 2, 7, 0, 1, 1, 1, 1, CAST(0x0000A83201310C9A AS DateTime), CAST(0x0000A83201311458 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (14, 2, 8, 0, 1, 1, 1, 1, CAST(0x0000A83201312300 AS DateTime), CAST(0x0000A83201312300 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (15, 2, 9, 0, 1, 1, 1, 1, CAST(0x0000A83200000000 AS DateTime), CAST(0x0000A83200000000 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (16, 2, 11, 0, 1, 1, 1, 1, CAST(0x0000A838011F1A1F AS DateTime), CAST(0x0000A838011F1A1F AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (17, 2, 12, 0, 1, 1, 1, 1, CAST(0x0000A83801208D53 AS DateTime), CAST(0x0000A83801208D53 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (18, 2, 14, 0, 1, 1, 1, 1, CAST(0x0000A8380122B18B AS DateTime), CAST(0x0000A8380122B18B AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (19, 2, 13, 0, 1, 1, 1, 1, CAST(0x0000A8380122D1CC AS DateTime), CAST(0x0000A8380122D1CC AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (20, 2, 17, 0, 1, 1, 1, 1, CAST(0x0000A83900E338BB AS DateTime), CAST(0x0000A83A010C5FE1 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserPermission] ([Id], [UserId], [PageId], [IsBlockAccess], [Read], [Write], [Delete], [Update], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1013, 2, 1008, 0, 1, 1, 1, 1, CAST(0x0000A84A00000000 AS DateTime), CAST(0x0000A84A00000000 AS DateTime), 2, 2, 0, 0)
SET IDENTITY_INSERT [dbo].[UserPermission] OFF
SET IDENTITY_INSERT [dbo].[UserShopMapper] ON 

INSERT [dbo].[UserShopMapper] ([Id], [UserId], [ShopId], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (2, 2, 2, CAST(0x0000A81900000000 AS DateTime), CAST(0x0000A81900000000 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserShopMapper] ([Id], [UserId], [ShopId], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1002, 2, 3, CAST(0x0000A81900000000 AS DateTime), CAST(0x0000A81900000000 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserShopMapper] ([Id], [UserId], [ShopId], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1003, 5, 3, CAST(0x0000A82A00F8F38A AS DateTime), CAST(0x0000A82A00F8F38A AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserShopMapper] ([Id], [UserId], [ShopId], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1004, 5, 2, CAST(0x0000A82A00F8FE65 AS DateTime), CAST(0x0000A82A00F8FE65 AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserShopMapper] ([Id], [UserId], [ShopId], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1005, 6, 3, CAST(0x0000A82A00F9180B AS DateTime), CAST(0x0000A82A00F9180B AS DateTime), 2, 2, 0, 0)
INSERT [dbo].[UserShopMapper] ([Id], [UserId], [ShopId], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1006, 6, 2, CAST(0x0000A82A00F9946C AS DateTime), CAST(0x0000A82A00F9946C AS DateTime), 2, 2, 0, 0)
SET IDENTITY_INSERT [dbo].[UserShopMapper] OFF
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([Id], [Type], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (1, N'SuperAdmin', N'All rights reserved', 1, 1, CAST(0x0000A81600000000 AS DateTime), CAST(0x0000A81600000000 AS DateTime), 0, 0)
INSERT [dbo].[UserType] ([Id], [Type], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (2, N'Admin', NULL, 1, 1, CAST(0x0000A81600000000 AS DateTime), CAST(0x0000A81600000000 AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[UserType] OFF
ALTER TABLE [dbo].[Gbl_AppDowntime]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_AppDowntime_User_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Gbl_AppDowntime] CHECK CONSTRAINT [FK_Gbl_AppDowntime_User_CreatedBy]
GO
ALTER TABLE [dbo].[Gbl_AppDowntime]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_AppDowntime_User_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Gbl_AppDowntime] CHECK CONSTRAINT [FK_Gbl_AppDowntime_User_ModifiedBy]
GO
ALTER TABLE [dbo].[Gbl_Master_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_BankAccount_Gbl_Master_Bank] FOREIGN KEY([BankId])
REFERENCES [dbo].[Gbl_Master_Bank] ([BankId])
GO
ALTER TABLE [dbo].[Gbl_Master_BankAccount] CHECK CONSTRAINT [FK_Gbl_Master_BankAccount_Gbl_Master_Bank]
GO
ALTER TABLE [dbo].[Gbl_Master_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_BankAccount_Gbl_Master_BankAccount] FOREIGN KEY([AccTypeId])
REFERENCES [dbo].[Gbl_Master_BankAccountType] ([AccountTypeId])
GO
ALTER TABLE [dbo].[Gbl_Master_BankAccount] CHECK CONSTRAINT [FK_Gbl_Master_BankAccount_Gbl_Master_BankAccount]
GO
ALTER TABLE [dbo].[Gbl_Master_BankAccount]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_BankAccount_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[Gbl_Master_BankAccount] CHECK CONSTRAINT [FK_Gbl_Master_BankAccount_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_BankCheque]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_BankCheque_Gbl_Master_BankAccount] FOREIGN KEY([BankAccId])
REFERENCES [dbo].[Gbl_Master_BankAccount] ([BankAccId])
GO
ALTER TABLE [dbo].[Gbl_Master_BankCheque] CHECK CONSTRAINT [FK_Gbl_Master_BankCheque_Gbl_Master_BankAccount]
GO
ALTER TABLE [dbo].[Gbl_Master_BankChequeDetails]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_BankChequeDetails_Gbl_Master_BankCheque] FOREIGN KEY([ChequeBookId])
REFERENCES [dbo].[Gbl_Master_BankCheque] ([ChequeId])
GO
ALTER TABLE [dbo].[Gbl_Master_BankChequeDetails] CHECK CONSTRAINT [FK_Gbl_Master_BankChequeDetails_Gbl_Master_BankCheque]
GO
ALTER TABLE [dbo].[Gbl_Master_BankChequeDetails]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_BankChequeDetails_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[Gbl_Master_BankChequeDetails] CHECK CONSTRAINT [FK_Gbl_Master_BankChequeDetails_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_Page]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Page_Gbl_Master_AppModule] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Gbl_Master_AppModule] ([ModuleId])
GO
ALTER TABLE [dbo].[Gbl_Master_Page] CHECK CONSTRAINT [FK_Gbl_Master_Page_Gbl_Master_AppModule]
GO
ALTER TABLE [dbo].[Login]  WITH CHECK ADD  CONSTRAINT [FK_Login_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Login] CHECK CONSTRAINT [FK_Login_User]
GO
ALTER TABLE [dbo].[MasterBrand]  WITH CHECK ADD  CONSTRAINT [FK_MasterBrand_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[MasterBrand] CHECK CONSTRAINT [FK_MasterBrand_Shop]
GO
ALTER TABLE [dbo].[MasterCategory]  WITH CHECK ADD  CONSTRAINT [FK_MasterCategory_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[MasterCategory] CHECK CONSTRAINT [FK_MasterCategory_Shop]
GO
ALTER TABLE [dbo].[MasterProduct]  WITH CHECK ADD  CONSTRAINT [FK_MasterProduct_MasterProduct] FOREIGN KEY([UnitId])
REFERENCES [dbo].[MasterUnit] ([UnitId])
GO
ALTER TABLE [dbo].[MasterProduct] CHECK CONSTRAINT [FK_MasterProduct_MasterProduct]
GO
ALTER TABLE [dbo].[MasterProduct]  WITH CHECK ADD  CONSTRAINT [FK_MasterProduct_MasterSubCategory] FOREIGN KEY([SubCatId])
REFERENCES [dbo].[MasterSubCategory] ([SubCatId])
GO
ALTER TABLE [dbo].[MasterProduct] CHECK CONSTRAINT [FK_MasterProduct_MasterSubCategory]
GO
ALTER TABLE [dbo].[MasterProduct]  WITH CHECK ADD  CONSTRAINT [FK_MasterProduct_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[MasterProduct] CHECK CONSTRAINT [FK_MasterProduct_Shop]
GO
ALTER TABLE [dbo].[MasterSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_MasterSubCategory_MasterCategory] FOREIGN KEY([CatId])
REFERENCES [dbo].[MasterCategory] ([CatId])
GO
ALTER TABLE [dbo].[MasterSubCategory] CHECK CONSTRAINT [FK_MasterSubCategory_MasterCategory]
GO
ALTER TABLE [dbo].[MasterSubCategory]  WITH CHECK ADD  CONSTRAINT [FK_MasterSubCategory_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[MasterSubCategory] CHECK CONSTRAINT [FK_MasterSubCategory_Shop]
GO
ALTER TABLE [dbo].[MasterVendor]  WITH CHECK ADD  CONSTRAINT [FK_MasterVendor_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[MasterVendor] CHECK CONSTRAINT [FK_MasterVendor_Shop]
GO
ALTER TABLE [dbo].[Shop]  WITH CHECK ADD  CONSTRAINT [FK_Shop_User] FOREIGN KEY([Owner])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Shop] CHECK CONSTRAINT [FK_Shop_User]
GO
ALTER TABLE [dbo].[Stk_Tr_Entry]  WITH NOCHECK ADD  CONSTRAINT [FK_Stk_Tr_Entry_Gbl_Master_BankAccount] FOREIGN KEY([DebitAccount])
REFERENCES [dbo].[Gbl_Master_BankAccount] ([BankAccId])
GO
ALTER TABLE [dbo].[Stk_Tr_Entry] NOCHECK CONSTRAINT [FK_Stk_Tr_Entry_Gbl_Master_BankAccount]
GO
ALTER TABLE [dbo].[Stk_Tr_Entry]  WITH NOCHECK ADD  CONSTRAINT [FK_Stk_Tr_Entry_Gbl_Master_BankChequeDetails] FOREIGN KEY([ChequePageId])
REFERENCES [dbo].[Gbl_Master_BankChequeDetails] ([ChequePageId])
GO
ALTER TABLE [dbo].[Stk_Tr_Entry] NOCHECK CONSTRAINT [FK_Stk_Tr_Entry_Gbl_Master_BankChequeDetails]
GO
ALTER TABLE [dbo].[Stk_Tr_Entry]  WITH CHECK ADD  CONSTRAINT [FK_Stk_Tr_Entry_Gbl_Master_PayMode] FOREIGN KEY([PayModeId])
REFERENCES [dbo].[Gbl_Master_PayMode] ([PayModeId])
GO
ALTER TABLE [dbo].[Stk_Tr_Entry] CHECK CONSTRAINT [FK_Stk_Tr_Entry_Gbl_Master_PayMode]
GO
ALTER TABLE [dbo].[Stk_Tr_Entry]  WITH CHECK ADD  CONSTRAINT [FK_Stk_Tr_Entry_MasterVendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[MasterVendor] ([VendorId])
GO
ALTER TABLE [dbo].[Stk_Tr_Entry] CHECK CONSTRAINT [FK_Stk_Tr_Entry_MasterVendor]
GO
ALTER TABLE [dbo].[Stk_Tr_Entry]  WITH CHECK ADD  CONSTRAINT [FK_Stk_Tr_Entry_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[Stk_Tr_Entry] CHECK CONSTRAINT [FK_Stk_Tr_Entry_Shop]
GO
ALTER TABLE [dbo].[StockEntry]  WITH CHECK ADD  CONSTRAINT [FK_StockEntry_MasterBrand] FOREIGN KEY([StockMstId])
REFERENCES [dbo].[Stk_Tr_Entry] ([Id])
GO
ALTER TABLE [dbo].[StockEntry] CHECK CONSTRAINT [FK_StockEntry_MasterBrand]
GO
ALTER TABLE [dbo].[StockEntry]  WITH CHECK ADD  CONSTRAINT [FK_StockEntry_MasterProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[MasterProduct] ([ProductId])
GO
ALTER TABLE [dbo].[StockEntry] CHECK CONSTRAINT [FK_StockEntry_MasterProduct]
GO
ALTER TABLE [dbo].[StockEntry]  WITH CHECK ADD  CONSTRAINT [FK_StockEntry_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[StockEntry] CHECK CONSTRAINT [FK_StockEntry_Shop]
GO
ALTER TABLE [dbo].[StockEntry]  WITH CHECK ADD  CONSTRAINT [FK_StockEntry_Stk_Tr_Entry] FOREIGN KEY([StockMstId])
REFERENCES [dbo].[Stk_Tr_Entry] ([Id])
GO
ALTER TABLE [dbo].[StockEntry] CHECK CONSTRAINT [FK_StockEntry_Stk_Tr_Entry]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([UserType])
REFERENCES [dbo].[UserType] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserType]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Gbl_Master_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Gbl_Master_Page] ([PageId])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_Gbl_Master_Page]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_User]
GO
ALTER TABLE [dbo].[UserShopMapper]  WITH CHECK ADD  CONSTRAINT [FK_UserShopMapper_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Shop] ([Id])
GO
ALTER TABLE [dbo].[UserShopMapper] CHECK CONSTRAINT [FK_UserShopMapper_Shop]
GO
ALTER TABLE [dbo].[UserShopMapper]  WITH CHECK ADD  CONSTRAINT [FK_UserShopMapper_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserShopMapper] CHECK CONSTRAINT [FK_UserShopMapper_User]
GO
USE [master]
GO
ALTER DATABASE [Myshop] SET  READ_WRITE 
GO
