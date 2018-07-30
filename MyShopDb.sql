USE [master]
GO
/****** Object:  Database [MyShop]    Script Date: 30-07-2018 18:47:07 ******/
CREATE DATABASE [MyShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MyShop.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MyShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\MyShop_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MyShop] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MyShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyShop] SET  MULTI_USER 
GO
ALTER DATABASE [MyShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [MyShop]
GO
/****** Object:  StoredProcedure [dbo].[SpGetEmpList]    Script Date: 30-07-2018 18:47:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SpGetEmpList]
@PageNo int=1,
@PageSize int=10,
@ShopId int
As
	BEGIN
		;WITH CTE AS(
			select EmpId,
			FirstName,
			LastName,
			FatherName,
			Mobile,
			[Address],
			City,
			Distict,
			[State],
			EmailId,
			AadharNo,
			PANCardNo,
			DOJ,
			DOB,
			DOR,
			Emp.CreatedBy,
			Emp.CreatedDate,
			Emp.ModifiedBy,
			Emp.ModificationDate,
			IsAppAccess,
			PINCode,
			ImageId,
			RoleId,
			Gender,
			TotalExp,
			EmergencyContactNo,
			BloodGroup,
			AddressDocProofId,
			IdentityDocProofId,
			AddressDocProofImageId,
			IdentityDocProofImageId,
			IdentityDocProofNo,
			AddressDocProofNo,
			att.Attachment UserImage,
			ROW_Number() OVER (Order by Emp.EmpId) RowNo
		from Gbl_Master_Employee Emp
		left join Gbl_Attachment att on Emp.ImageId=att.AttachmentId
		where Emp.IsActive=1 AND Emp.IsDeleted=0 AND Emp.shopId=@shopId
	)--End OF CTE
		Select * from CTE
		Where RowNo between ((@PageNo-1)*@PageSize) AND (@PageNo*@PageSize)
	END

GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 30-07-2018 18:47:08 ******/
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
	[Source] [nvarchar](250) NULL,
	[ErrorCode] [int] NULL,
	[Severity] [nvarchar](50) NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_AppDowntime]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Attachment]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gbl_Attachment](
	[AttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](150) NOT NULL,
	[OriginalFileName] [nvarchar](150) NOT NULL,
	[FileExtension] [nvarchar](6) NOT NULL,
	[ModuleName] [nvarchar](50) NOT NULL,
	[Attachment] [varbinary](max) NOT NULL,
	[ShopId] [int] NOT NULL,
	[IsSync] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_Gbl_Attachment] PRIMARY KEY CLUSTERED 
(
	[AttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gbl_Master_AppModule]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Master_Bank]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Master_BankAccount]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Master_BankAccountType]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Master_BankCheque]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Master_BankChequeDetails]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Master_Brand]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Brand](
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
/****** Object:  Table [dbo].[Gbl_Master_Category]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Category](
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
/****** Object:  Table [dbo].[Gbl_Master_City]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NOT NULL,
	[CityName] [nvarchar](80) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModificationDate] [datetime] NULL,
	[IsSync] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Gbl_Master_City] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_Customer]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerTypeId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Mobile] [nvarchar](13) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[District] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[PINCode] [nvarchar](6) NOT NULL,
	[ShopId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_CustomerType]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_CustomerType](
	[CustomerTypeId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerType] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[ShopId] [int] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_CustomerType] PRIMARY KEY CLUSTERED 
(
	[CustomerTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_DocProof]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_DocProof](
	[DocProofId] [int] IDENTITY(1,1) NOT NULL,
	[DocProofTypeId] [int] NOT NULL,
	[DocProof] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_DocProof] PRIMARY KEY CLUSTERED 
(
	[DocProofId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_DocProofType]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_DocProofType](
	[DocProofTypeId] [int] IDENTITY(1,1) NOT NULL,
	[DocProofType] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_DocProofType] PRIMARY KEY CLUSTERED 
(
	[DocProofTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_Employee]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gbl_Master_Employee](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FatherName] [nvarchar](50) NOT NULL,
	[Mobile] [nvarchar](13) NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Distict] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[EmailId] [nvarchar](50) NULL,
	[AadharNo] [nvarchar](12) NULL,
	[PANCardNo] [nvarchar](50) NULL,
	[DOJ] [datetime] NOT NULL,
	[DOB] [datetime] NOT NULL,
	[DOR] [datetime] NULL,
	[ShopId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsAppAccess] [bit] NOT NULL,
	[PINCode] [nvarchar](6) NOT NULL,
	[ImageId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Gender] [char](1) NULL,
	[TotalExp] [int] NULL,
	[EmergencyContactNo] [nvarchar](15) NULL,
	[BloodGroup] [nchar](4) NULL,
	[AddressDocProofId] [int] NULL,
	[IdentityDocProofId] [int] NULL,
	[AddressDocProofImageId] [int] NULL,
	[IdentityDocProofImageId] [int] NULL,
	[IdentityDocProofNo] [nvarchar](50) NULL,
	[AddressDocProofNo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Gbl_Master_Employee] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gbl_Master_Employee_Role]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Employee_Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleType] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ShopId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_Employee_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_Notification]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Notification](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationTypeId] [int] NOT NULL,
	[Message] [nvarchar](200) NOT NULL,
	[UserId] [int] NOT NULL,
	[ShopId] [int] NOT NULL,
	[IsPushed] [bit] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[IsForAll] [bit] NOT NULL,
	[PushedDate] [datetime] NULL,
	[MessageExpireDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsSync] [bit] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_NotificationType]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_NotificationType](
	[NotificationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationType] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[ShopId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsSync] [bit] NOT NULL,
 CONSTRAINT [PK_Gbl_Master_NotificationType] PRIMARY KEY CLUSTERED 
(
	[NotificationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_Page]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Master_PayMode]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[Gbl_Master_Product]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Product](
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
/****** Object:  Table [dbo].[Gbl_Master_Shop]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gbl_Master_Shop](
	[ShopId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Address] [varchar](300) NOT NULL,
	[Mobile] [varchar](13) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Distict] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Owner] [int] NOT NULL,
	[LogoAttachmentId] [int] NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[ShopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gbl_Master_State]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](80) NOT NULL,
	[CountryId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Gbl_Master_State] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gbl_Master_SubCategory]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_SubCategory](
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
/****** Object:  Table [dbo].[Gbl_Master_Unit]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Unit](
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
/****** Object:  Table [dbo].[Gbl_Master_User]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gbl_Master_User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
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
	[ShopId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gbl_Master_User_Permission]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_User_Permission](
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
/****** Object:  Table [dbo].[Gbl_Master_UserType]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gbl_Master_UserType](
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
/****** Object:  Table [dbo].[Gbl_Master_Vendor]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gbl_Master_Vendor](
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
/****** Object:  Table [dbo].[Login]    Script Date: 30-07-2018 18:47:08 ******/
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
	[LoginAttempt] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsSync] [bit] NOT NULL,
	[IsReset] [bit] NULL,
	[IsLoginBlocked] [bit] NOT NULL,
	[GUID] [uniqueidentifier] NULL,
	[OTPid] [varchar](100) NULL,
	[ReserExpireTime] [datetime] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[CreationBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stk_Dtl_Entry]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stk_Dtl_Entry](
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
/****** Object:  Table [dbo].[Stk_Tr_Entry]    Script Date: 30-07-2018 18:47:08 ******/
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
/****** Object:  Table [dbo].[User_ShopMapper]    Script Date: 30-07-2018 18:47:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_ShopMapper](
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
/****** Object:  UserDefinedFunction [dbo].[ss]    Script Date: 30-07-2018 18:47:08 ******/
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

INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (1, N'Master', N'AddCustmerType', NULL, N'The partial view ''_ButtonGroup'' was not found or no view engine supports the searched locations. The following locations were searched:
~/Areas/CustomersManagement/Views/Master/_ButtonGroup.aspx
~/Areas/CustomersManagement/Views/Master/_ButtonGroup.ascx
~/Areas/CustomersManagement/Views/Shared/_ButtonGroup.aspx
~/Areas/CustomersManagement/Views/Shared/_ButtonGroup.ascx
~/Views/Master/_ButtonGroup.aspx
~/Views/Master/_ButtonGroup.ascx
~/Views/Shared/_ButtonGroup.aspx
~/Views/Shared/_ButtonGroup.ascx
~/Areas/CustomersManagement/Views/Master/_ButtonGroup.cshtml
~/Areas/CustomersManagement/Views/Master/_ButtonGroup.vbhtml
~/Areas/CustomersManagement/Views/Shared/_ButtonGroup.cshtml
~/Areas/CustomersManagement/Views/Shared/_ButtonGroup.vbhtml
~/Views/Master/_ButtonGroup.cshtml
~/Views/Master/_ButtonGroup.vbhtml
~/Views/Shared/_ButtonGroup.cshtml
~/Views/Shared/_ButtonGroup.vbhtml', N'The partial view ''_ButtonGroup'' was not found or no view engine supports the searched locations. The following locations were searched:
~/Areas/CustomersManagement/Views/Master/_ButtonGroup.aspx
~/Areas/CustomersManagement/Views/Master/_ButtonGroup.ascx
~/Areas/CustomersManagement/Views/Shared/_ButtonGroup.aspx
~/Areas/CustomersManagement/Views/Shared/_ButtonGroup.ascx
~/Views/Master/_ButtonGroup.aspx
~/Views/Master/_ButtonGroup.ascx
~/Views/Shared/_ButtonGroup.aspx
~/Views/Shared/_ButtonGroup.ascx
~/Areas/CustomersManagement/Views/Master/_ButtonGroup.cshtml
~/Areas/CustomersManagement/Views/Master/_ButtonGroup.vbhtml
~/Areas/CustomersManagement/Views/Shared/_ButtonGroup.cshtml
~/Areas/CustomersManagement/Views/Shared/_ButtonGroup.vbhtml
~/Views/Master/_ButtonGroup.cshtml
~/Views/Master/_ButtonGroup.vbhtml
~/Views/Shared/_ButtonGroup.cshtml
~/Views/Shared/_ButtonGroup.vbhtml', N'No Inner exception', 0, 0, 1, CAST(0x0000A90C01071710 AS DateTime), CAST(0x0000A92A010FFB63 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (2, N'Login', N'ShopSelection', NULL, N'Object reference not set to an instance of an object.', N'Object reference not set to an instance of an object.', N'No Inner exception', 0, 0, 0, CAST(0x0000A911011D383E AS DateTime), CAST(0x0000A911011D383F AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (3, N'Login', N'ShopSelection', NULL, N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''Myshop.App_Start.ShopListModel'' because ''Myshop.App_Start.ShopListModel'' does not contain a public definition for ''GetEnumerator''', N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''Myshop.App_Start.ShopListModel'' because ''Myshop.App_Start.ShopListModel'' does not contain a public definition for ''GetEnumerator''', N'No Inner exception', 0, 0, 0, CAST(0x0000A911011DA482 AS DateTime), CAST(0x0000A911011DA482 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (4, N'Login', N'ShopSelection', NULL, N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''Myshop.App_Start.ShopListModel'' because ''Myshop.App_Start.ShopListModel'' does not contain a public definition for ''GetEnumerator''', N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''Myshop.App_Start.ShopListModel'' because ''Myshop.App_Start.ShopListModel'' does not contain a public definition for ''GetEnumerator''', N'No Inner exception', 0, 0, 0, CAST(0x0000A911011DC39A AS DateTime), CAST(0x0000A911011DC39A AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (5, N'Login', N'ShopSelection', NULL, N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' because ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' does not contain a public definition for ''GetEnumerator''', N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' because ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' does not contain a public definition for ''GetEnumerator''', N'No Inner exception', 0, 0, 0, CAST(0x0000A911011E99CC AS DateTime), CAST(0x0000A911011E99CC AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (6, N'Login', N'ShopSelection', NULL, N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' because ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' does not contain a public definition for ''GetEnumerator''', N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' because ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' does not contain a public definition for ''GetEnumerator''', N'No Inner exception', 0, 0, 0, CAST(0x0000A911011EB5F4 AS DateTime), CAST(0x0000A911011EB5F4 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (7, N'login', N'getLogin', NULL, N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' because ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' does not contain a public definition for ''GetEnumerator''', N'e:\My Data\Projects\MyShop\Myshop\Views\Login\ShopSelection.cshtml(20): error CS1579: foreach statement cannot operate on variables of type ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' because ''System.Collections.Generic.IEnumerator<Myshop.App_Start.ShopListModel>'' does not contain a public definition for ''GetEnumerator''', N'No Inner exception', 0, 0, 0, CAST(0x0000A911011EF5EC AS DateTime), CAST(0x0000A911011EF5EC AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (8, N'Login', N'ShopSelection', NULL, N'Object reference not set to an instance of an object.', N'Object reference not set to an instance of an object.', N'No Inner exception', 0, 0, 0, CAST(0x0000A9110121E77C AS DateTime), CAST(0x0000A9110121E77C AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (9, N'login', N'setshopselection', NULL, N'The parameters dictionary contains a null entry for parameter ''shopid'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.ActionResult SetShopSelection(Int32)'' in ''Myshop.Controllers.LoginController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'The parameters dictionary contains a null entry for parameter ''shopid'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.ActionResult SetShopSelection(Int32)'' in ''Myshop.Controllers.LoginController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'No Inner exception', 0, 0, 0, CAST(0x0000A91101280A58 AS DateTime), CAST(0x0000A91101280A59 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (10, N'login', N'setshopselection', NULL, N'The parameters dictionary contains a null entry for parameter ''shopid'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.ActionResult SetShopSelection(Int32)'' in ''Myshop.Controllers.LoginController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'The parameters dictionary contains a null entry for parameter ''shopid'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.ActionResult SetShopSelection(Int32)'' in ''Myshop.Controllers.LoginController''. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter.
Parameter name: parameters', N'No Inner exception', 0, 0, 0, CAST(0x0000A911012854B7 AS DateTime), CAST(0x0000A911012854B7 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (11, N'Login', N'ShopSelection', NULL, N'Object reference not set to an instance of an object.', N'Object reference not set to an instance of an object.', N'No Inner exception', 0, 0, 0, CAST(0x0000A9110128B401 AS DateTime), CAST(0x0000A9110128B401 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (12, N'login', N'setshopselection', NULL, N'The view ''setshopselection'' or its master was not found or no view engine supports the searched locations. The following locations were searched:
~/Views/login/setshopselection.aspx
~/Views/login/setshopselection.ascx
~/Views/Shared/setshopselection.aspx
~/Views/Shared/setshopselection.ascx
~/Views/login/setshopselection.cshtml
~/Views/login/setshopselection.vbhtml
~/Views/Shared/setshopselection.cshtml
~/Views/Shared/setshopselection.vbhtml', N'The view ''setshopselection'' or its master was not found or no view engine supports the searched locations. The following locations were searched:
~/Views/login/setshopselection.aspx
~/Views/login/setshopselection.ascx
~/Views/Shared/setshopselection.aspx
~/Views/Shared/setshopselection.ascx
~/Views/login/setshopselection.cshtml
~/Views/login/setshopselection.vbhtml
~/Views/Shared/setshopselection.cshtml
~/Views/Shared/setshopselection.vbhtml', N'No Inner exception', 0, 0, 0, CAST(0x0000A9110128C2A1 AS DateTime), CAST(0x0000A9110128C2A1 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (13, N'Login', N'ShopSelection', NULL, N'Object reference not set to an instance of an object.', N'Object reference not set to an instance of an object.', N'No Inner exception', 0, 0, 0, CAST(0x0000A911012B8178 AS DateTime), CAST(0x0000A911012B8178 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (14, N'Masters', N'GetBrand', NULL, N'e:\My Data\Projects\MyShop\Myshop\Areas\StockManagement\Views\Masters\GetBrand.cshtml(48): error CS0128: A local variable named ''ViewDataName'' is already defined in this scope', N'e:\My Data\Projects\MyShop\Myshop\Areas\StockManagement\Views\Masters\GetBrand.cshtml(48): error CS0128: A local variable named ''ViewDataName'' is already defined in this scope', N'No Inner exception', 0, 0, 0, CAST(0x0000A91200F78DA6 AS DateTime), CAST(0x0000A91200F78DA6 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (15, N'Masters', N'GetSubCategory', NULL, N'Value cannot be null.
Parameter name: items', N'Value cannot be null.
Parameter name: items', N'No Inner exception', 0, 0, 0, CAST(0x0000A91200FD64B5 AS DateTime), CAST(0x0000A91200FD64B5 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (16, N'Masters', N'GetUnit', NULL, N'Value cannot be null.
Parameter name: items', N'Value cannot be null.
Parameter name: items', N'No Inner exception', 0, 0, 0, CAST(0x0000A91200FF5064 AS DateTime), CAST(0x0000A91200FF5064 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (17, N'Masters', N'GetVendor', NULL, N'Value cannot be null.
Parameter name: items', N'Value cannot be null.
Parameter name: items', N'No Inner exception', 0, 0, 0, CAST(0x0000A91201042D88 AS DateTime), CAST(0x0000A91201042D88 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (18, N'Masters', N'GetProduct', NULL, N'Value cannot be null.
Parameter name: items', N'Value cannot be null.
Parameter name: items', N'No Inner exception', 0, 0, 0, CAST(0x0000A9120106B90C AS DateTime), CAST(0x0000A9120106B90C AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (19, N'masters', N'setlogo', NULL, N'Index was out of range. Must be non-negative and less than the size of the collection.
Parameter name: index', N'Index was out of range. Must be non-negative and less than the size of the collection.
Parameter name: index', N'No Inner exception', 0, 0, 0, CAST(0x0000A92700BE643A AS DateTime), CAST(0x0000A92700BE643A AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (20, N'masters', N'setlogo', NULL, N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'No Inner exception', 0, 0, 0, CAST(0x0000A92700C91B2B AS DateTime), CAST(0x0000A92700C91B2C AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (21, N'Login', N'ShopSelection', NULL, N'Object reference not set to an instance of an object.', N'Object reference not set to an instance of an object.', N'No Inner exception', 0, 0, 0, CAST(0x0000A92A01380305 AS DateTime), CAST(0x0000A92A01380305 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (22, N'users', N'NotificationList', NULL, N'The partial view ''_ButtonGroup'' was not found or no view engine supports the searched locations. The following locations were searched:
~/Views/users/_ButtonGroup.aspx
~/Views/users/_ButtonGroup.ascx
~/Views/Shared/_ButtonGroup.aspx
~/Views/Shared/_ButtonGroup.ascx
~/Views/users/_ButtonGroup.cshtml
~/Views/users/_ButtonGroup.vbhtml
~/Views/Shared/_ButtonGroup.cshtml
~/Views/Shared/_ButtonGroup.vbhtml', N'The partial view ''_ButtonGroup'' was not found or no view engine supports the searched locations. The following locations were searched:
~/Views/users/_ButtonGroup.aspx
~/Views/users/_ButtonGroup.ascx
~/Views/Shared/_ButtonGroup.aspx
~/Views/Shared/_ButtonGroup.ascx
~/Views/users/_ButtonGroup.cshtml
~/Views/users/_ButtonGroup.vbhtml
~/Views/Shared/_ButtonGroup.cshtml
~/Views/Shared/_ButtonGroup.vbhtml', N'No Inner exception', 0, 0, 0, CAST(0x0000A92D01052B09 AS DateTime), CAST(0x0000A92D01052B09 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (23, N'Users', N'DeleteUserNotificationList', NULL, N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'No Inner exception', 0, 0, 0, CAST(0x0000A92D0132A496 AS DateTime), CAST(0x0000A92D0132A496 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (24, N'Users', N'DeleteUserNotificationList', NULL, N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'No Inner exception', 0, 0, 0, CAST(0x0000A92D0132ADBC AS DateTime), CAST(0x0000A92D0132ADBC AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (25, N'Users', N'DeleteUserNotificationList', NULL, N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'No Inner exception', 0, 0, 0, CAST(0x0000A92D0132E57A AS DateTime), CAST(0x0000A92D0132E57A AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (26, N'Users', N'DeleteUserNotificationList', NULL, N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'No Inner exception', 0, 0, 0, CAST(0x0000A92D013313AE AS DateTime), CAST(0x0000A92D013313AE AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ErrorLog] ([Id], [Controller], [Action], [Area], [Message], [OuterException], [InnerException], [IsSync], [IsDeleted], [IsResolved], [CreatedDate], [ModifiedDate], [Source], [ErrorCode], [Severity]) VALUES (27, N'Users', N'DeleteUserNotificationList', NULL, N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'Type ''System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'' is not supported for serialization/deserialization of a dictionary, keys must be strings or objects.', N'No Inner exception', 0, 0, 0, CAST(0x0000A92D01336488 AS DateTime), CAST(0x0000A92D01336488 AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ErrorLog] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Attachment] ON 

INSERT [dbo].[Gbl_Attachment] ([AttachmentId], [FileName], [OriginalFileName], [FileExtension], [ModuleName], [Attachment], [ShopId], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (1, N'Img_636680294163055283', N'deepa-sign-gif.jpg', N'jpg', N'Upload Shop Logo', 0xFFD8FFE000104A46494600010101000100010000FFDB004300030202020202030202020303030304060404040404080606050609080A0A090809090A0C0F0C0A0B0E0B09090D110D0E0F101011100A0C12131210130F101010FFDB00430103030304030408040408100B090B1010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010FFC0001108003C006403012200021101031101FFC4001C0000020202030000000000000000000000060703040005010208FFC4003910000201030204050204020A030000000001020304051100060712213113224151611432154271815291081617232425264372B162C1F0FFC400160101010100000000000000000000000000000102FFC4001B11010003000301000000000000000000000001112102123141FFDA000C03010002110311003F00625F2AC57DE6E954496FA8AFAA9727F36656208FFBD29B8A9B9378355D170FF86DE18BF5CA3FAAA9AD7FB6DF479E50FF000EED90B9F4563A63CC4B8894062CCDCBEDFBE81F624B4D5179DEDBE6B668A34ACBBFE1914CEC02C54D46A2245CFA65D9CFC93A4630165DC1C4AE175C1A0DDF5E3736DF59901AC684475488C3CCEA57A3F29072A7AF6C69BD47574B57490D550CE93534F189A29D4E43A1EC41D455F68A1B9504D69B8D3F8B4F5072EA7D09F5F823BE7E340DC30ABAFB5D66E0E1D5CA25492D7526AEDEAB265450C8C79797E01EB8F4C9D59D0C232119665E83A8F9F6D579240D19603B303D3D75604B83865E5F2E72755DC9605C6729928A359544ECC646E56059415EFD33EA75D1D91DB90FDAC32DF2352245962E7B05C631DCEA273E56F2F5763DFD0682BB1E5F3004F5381A1CDEF7EABDB5B3AED7FA7815EAA8E12F4D1B1E8F292022FCF53DBE3444E7A194B3673938F5C7FEB411C539567B2DB2D44395AEBA461D463AA471C921073D31955D48D90594F21A8A282670C4CD0452118C1E665048FDB275BEDB3BF37B6CE793FAA7BBAEB6C01C334115416858FCC4D94F7F4F5D0FC5FDCD253C1192DE1431C649F70A064FF2D746665462C5B27BF5D580F2B77F4C5E2CD052AD356DBB6DDD245EBF532C12C4CC3D8AA3F2FEE00FD359A458919BCCA9807AF43ACD5685154CC668D517A0006477C6913C688ABEDFC29DC1B176E4ED4B75BA6E384C61321E58EAE5F13231EA593973E9A7B4A5BC42CC70A338503D34BCE235AE6BC6E6D8D416E5E4B9ADC27AE59460858A1873E71EA819874F7EDAB0C0CB65AEE07D99635DD34E69AF9F410C57084B87293A8C36587427A673EE743FB929A2B1F1336D6E48E4448AE514966AB403CCCEFD60663EDCCBCA344B4759B8220C6E76D82403189295FEE6FF008376193AD171129D6E5B616E5C8AB2DB6AE9ABBCD190E0C720F4EE0F53A0279E5182D8C3679707FEB5A7DC3B8ECDB5ED5537EDC174828682971CF34AD80091F681DC93D7A0D6E6ADA396570016CF6F639D2AA1DAD6EE2A5FAE97CDDD4D1D66DCB6544B68B350B1CC4FE1902A2A9C7E66790145CF654E9DCEA28DF6C6E8B2EF0B0536E3DBB57F554158BCD0C8320B60F5047A6A9EEBDF3B63625B7F1DDE17986DB42F22D3A3C9D4B484FDAA0753EE7D8753AD5F0E36C5A766A6E8B05813C2B7C1796929600C5840648D1DE204FF00093DBD33A1EBB6DBDBBC46BB6F2ACE235B696A36DED42F4144AEE7313A5378955393E8D991547FC40D2B43323757A45959633E2A7302872A54F5041F62307407BF5E9EA3746CDB33C986ADA8AE1C80F7C42B927E002DA23D9C2A4ECCB02CEA44BF85D300AC30C0786319F9E5E5CFEFA536EBDC97ABDF14AC906D848D5A7B6D7D25AAB648C32C47C445ABAD507EE0AA39231D98E4F6D22350E592525C0E50ACDE6C7A1FFE1A01E226F5DC761ADB26DEDA9B7A2BBDE6F93B2F24F23AC1494C301A7919474192001F07B9C0D454505D362EEAB3DB64DC773BC5A371C935132DC5C4B253D5AC4644915F00857E5705318ED8D6FEAEAB6CC7BC6DAB5B76A4A7BEBC32434D4CD30134F0B1C9509EA32B91FA74D3C549B2AF551B9F6D525DA5A4486A58C90D5C4AD958E78DD91C293D4AE57A7C11ACD57E1430976BCF24922A86BADC0A051D397EA64C6B3527D431E60AEB93274439183D49D0B52CF0D7715EB8AA331B158E1A6523AAAC953219180F63C88B9F8D71BCF7E536D9AEB5EDFA2A09EE97DBDF88D436E830ACF1A639A4773D238C13DCF7D2F760713EE569A8DCFBAF776C6BB2D35E2ED23FE2169FF1B4EA908102C6157CFCABC8DE6C609271AD4407A1214F9CE005CA8CFB685F7E4F8DAB3249959EE73D3DBE203AF334B2A8E5FE418FEDAD4C7C73E145429E4DDF4E8EA18B42F0C9E2E07B205C9FDB5A9AD1BE38873536ECB044968B75B5BFC8E0BAD31E79E47CABD7C91020E150911237F1163A506156DD2DD6D592BEE15B0D250D31065A99E411C71A0F52C7E06939C3ADCFB8AFBB51B69F0C451CF5F4D5B546A6FB30E7A3A18A4A8778DD13EE9E5656042E397DCE8BEDBC29A7ACE4B87112FB59BB2AD70C62ABF2D1291F695A75F203F272756371F0A7666E5AC86BDE0ABB5D5A2F862AED354D472151D429E4C061D3A023A698AB9456EDB7C35DA694D5F7914F494E649EAEE17098096AAA646CC93CA4FDCEED9240EDD00E83495A0DE366BD6E2BF597715C96CBB3AAAFBF8F3A57C122545FA361188A28D71910ABC619BD5BCA31A6CD9B83FB16CF5305CA4B754DDEB607F1126BB553D5B237F180E7941FDB44F554B435138ABA8A186691530AD246AC6339F427B76F4D2E80654DE2EDC417929E9696AED5B7092B5759511186A6BD4F5F0A9D7EE8D0F6673D482401EBAD54115155F1A678E9A148976E6D78208D02E162FA89D982AFA7D918D1E573C92E7EE766214123B1231A4C59375D3C3C4AE216FBA5B7DCEEB4B475347B766868632F22186124B88FF30E7E65CF71D4F63ACC69F06FBB552A7736CBA3740645B8CD5E31F9121A77CB1FDE451FA9D005DF6447BBAF3BFEB226FF00565B6E51CF6D662032C0B023D2853DD50957008FCD9CE8B769D3DFAFF7BAEE216E7B7496A926A616DB3DAA620CB47461F9DE5971D3C595F0703B2A81AD9DEB6CC95B75A4DD568BAB5B6F54ABE12D42C61D2A60CE7C09D3F3A67A83DD4F6D5BA1AFE0954CD55C2FB1DCEA03A545C567AD9D70472CB24F2330FE79D66A6E1E505F36BECFB7D82F54713D65278BE23D3CBCD1BF3CAEF95CF5C79F18F8D668AD1EF16BA5838CDB9DA9E3924BEEF8B3D059B684D9E614EEC4C752C07FB6912E6424F738EE4E9F162B2DBB6A59A876F5AB9CD3DB6963A38CF62C88B82C7E49058FC9D710535335647717A689AA61568E39190168C37DDCA7B8CFC6AD38E556C13D41F5F9D2F11523B65B63615B1DB28E394F9448B02061FA1C675232A86386C95520FEB8D4A7AA29FE1208D53A976E4650719C751DC7AE88EAD24BC9CC72189E9ED9ED9D713F2AE01EAC307A7B9F4D712F578D4F60074D5533C8E8C59B249C6A5EAA49E5648C856C903B9F6F7D577F2A75380C7F7F7D435150CB238E45388F1D47A6741FB8F7ADD6D1149F4D052B72441C73AB1EA588F461E8357D04EE1CB31279948191EA0E82F65DA5ED1B9F7ECBF47F4F15C2F30D5C4FCA40939A9539DB3EBE6CE71EBA05BCF1BB78524C63A7A7B620E45C1103123207BB684EF7C7ADFD086481EDF182FCBD29B3807F52748E23D16C0AA303F9FA827E3B6A26979539C8C80A339F4D79969B8C3C46BACDE1CBB9248812147830C6B8CE3FF13EFAEB2EEFDFB34F54ADBFEF8A1092BCB24431D48C7D9DBA6AF4957A756A463393D7BE00D66BCE92D5EEEC44DFDA1EE305E2576C4F1752464FFB7ACD3A8FFFD9, 1, 0, 1, CAST(0x0000A92700C13F41 AS DateTime), 1, CAST(0x0000A92700CAD3DE AS DateTime), 1)
INSERT [dbo].[Gbl_Attachment] ([AttachmentId], [FileName], [OriginalFileName], [FileExtension], [ModuleName], [Attachment], [ShopId], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (2, N'Img_636680314024682739', N'deepa sign.jpg', N'jpg', N'Upload Shop Logo', 0xFFD8FFE107C445786966000049492A00080000000F000001040001000000C00C00000101040001000000900900000201030003000000C20000000601030001000000020000000F01020008000000C80000001001020009000000D00000001201030001000000010000001501030001000000030000001A01050001000000D90000001B01050001000000E1000000280103000100000002000000310102001E000000E900000032010200140000000701000013020300010000000100000069870400010000001C0100002403000008000800080053414D53554E470047542D49393038320080FC0A001027000080FC0A001027000041646F62652050686F746F73686F7020435336202857696E646F77732900323031343A30343A30322031363A33343A313300001E009A820500010000008A0200009D82050001000000920200002288030001000000030000002788030001000000F401000000900700040000003032323003900200140000009A0200000490020014000000AE02000001910700040000000102030001920A0001000000C20200000292050001000000CA02000003920A0001000000D202000004920A0001000000DA0200000592050001000000E20200000792030001000000010000000992030001000000000000000A92050001000000EA0200008692070008000000F202000000A00700040000003031303001A00300010000000100000002A00400010000003200000003A00400010000001E00000005A00400010000001003000001A30700010000000100000002A40300010000000000000003A40300010000000000000004A4050001000000FA02000006A40300010000000000000009A4030001000000000000000AA40300010000000000000020A402000C00000002030000000000005EEA000040420F007467000010270000323031343A30313A30312032333A30313A333400323031343A30313A30312032333A30313A3334003DEF3D0040420F00C66D000010270000F5000000640000000000000006000000C66D0000102700007201000064000000415343494900000001000000010000004F30384230534147443031000000010001000200040000005239380000000000000006000301030001000000060000001A01050001000000720300001B010500010000007A03000028010300010000000200000001020400010000008203000002020400010000003A0400000000000048000000010000004800000001000000FFD8FFED000C41646F62655F434D0001FFEE000E41646F626500648000000001FFDB0084000C08080809080C09090C110B0A0B11150F0C0C0F1518131315131318110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C010D0B0B0D0E0D100E0E10140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4013F0000010501010101010100000000000000030001020405060708090A0B0100010501010101010100000000000000010002030405060708090A0B1000010401030204020507060805030C33010002110304211231054151611322718132061491A1B14223241552C16233347282D14307259253F0E1F163733516A2B283264493546445C2A3743617D255E265F2B384C3D375E3F3462794A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F637475767778797A7B7C7D7E7F711000202010204040304050607070605350100021103213112044151617122130532819114A1B14223C152D1F0332462E1728292435315637334F1250616A2B283072635C2D2449354A317644555367465E2F2B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F62737475767778797A7B7C7FFDA000C03010002110311003F002E6EFB766335E6A392E76FB5A61CDADA3D5BB63BF31EFF00E6F7A1D2D7E1DECA0BDD663DF229DE4B9D5BDA377A3EA1F73ABB19FCDEE4DD5AA75B87686C87BDA6A11F4A2D2DA9FE9FF2FF007517240FB200CDC5F8FB1EC2F10E9A88F73BF94E6A484E78E540CA1E5DB6B4B6BC60D7DF7122ADF218206E75B6EDF77A75FF0027E9AAEFC8CCFB1545ADAFEDD635C2093E887D61CEBAC739BEEF45ADAD24A4CC7BABC5B9ED30F0C3B3E27DAD3F8A235EFACB5CC7B9AF681EF692D331FBCD553A86431B895120CDEEA8068124C96BDD1FF5295B6599153AB6BCE1DC2C6D761DCD258090EFD13FF9B7BECAFF009A494EA7EDAEAFFF00736EE23E976FBBFE9A4B03ED991FBDFE0278FCEFF49FD749253FFFD06CACEC6664D343DF1B2CDF6BB697359B1BBDADB1CD076BBDF5A9E466D1654EAF19E322C7830CA8EF207E7D8E8FA3B1A9BA6328664649A6CF51EF7DA5AD820B07A9FACB1C5DF4DFF69FFC0BD1FF0006AD8D907D3DB13EED91CFF2B6A48685F5F50B9CCB58C1435A1CDF4B7C5CE6BE377E9DA1F4E3BBDBFCB7A85D87937B6A11562D7468DC71B9E1CD236BABBAC6FA7FA3FE457FCE7F855A067550713E04FC3FDA524B9F98F6D36639BACDEFB2E0FB2C7088654D7DA763193B6AABDBEC6215D40CCADD73AB2F63DE5CDADDED7166CF42BB9BBBE85EDFE7E8DEAC5EF68BF1CECB0D81CFF480D9A9D877F367EE283F27201F6E25AEFEDD63FEFE929A1E85DFB96FF41F4BE89FE77F7BFF000C24AEFDAB33FEE1BFFEDCAFFF0024924A7FFFD9FFED0D6450686F746F73686F7020332E30003842494D040400000000002C1C015A00031B25471C0200000200001C0237000832303134303130311C023C000B3233303133342B303030303842494D04250000000000105BC256EA5002190D3DDB9F2CB5C6F15B3842494D043A0000000000E5000000100000000100000000000B7072696E744F7574707574000000050000000050737453626F6F6C0100000000496E7465656E756D00000000496E746500000000436C726D0000000F7072696E745369787465656E426974626F6F6C000000000B7072696E7465724E616D65544558540000000100000000000F7072696E7450726F6F6653657475704F626A630000000C00500072006F006F006600200053006500740075007000000000000A70726F6F6653657475700000000100000000426C746E656E756D0000000C6275696C74696E50726F6F660000000970726F6F66434D594B003842494D043B00000000022D00000010000000010000000000127072696E744F75747075744F7074696F6E7300000017000000004370746E626F6F6C0000000000436C6272626F6F6C00000000005267734D626F6F6C000000000043726E43626F6F6C0000000000436E7443626F6F6C00000000004C626C73626F6F6C00000000004E677476626F6F6C0000000000456D6C44626F6F6C0000000000496E7472626F6F6C000000000042636B674F626A630000000100000000000052474243000000030000000052642020646F7562406FE000000000000000000047726E20646F7562406FE0000000000000000000426C2020646F7562406FE000000000000000000042726454556E744623526C74000000000000000000000000426C6420556E744623526C7400000000000000000000000052736C74556E74462350786C40520000000000000000000A766563746F7244617461626F6F6C010000000050675073656E756D00000000506750730000000050675043000000004C656674556E744623526C74000000000000000000000000546F7020556E744623526C7400000000000000000000000053636C20556E74462350726340590000000000000000001063726F705768656E5072696E74696E67626F6F6C000000000E63726F7052656374426F74746F6D6C6F6E67000000000000000C63726F70526563744C6566746C6F6E67000000000000000D63726F705265637452696768746C6F6E67000000000000000B63726F7052656374546F706C6F6E6700000000003842494D03ED000000000010004800000001000100480000000100013842494D042600000000000E000000000000000000003F8000003842494D040D000000000004FFFFFFC43842494D04190000000000040000001E3842494D03F3000000000009000000000000000001003842494D271000000000000A000100000000000000013842494D03F5000000000048002F66660001006C66660006000000000001002F6666000100A1999A0006000000000001003200000001005A00000006000000000001003500000001002D000000060000000000013842494D03F80000000000700000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800003842494D0408000000000010000000010000024000000240000000003842494D041E000000000004000000003842494D041A0000000003490000000600000000000000000000001E000000320000000A006400650065007000610020007300690067006E0000000100000000000000000000000000000000000000010000000000000000000000320000001E00000000000000000000000000000000010000000000000000000000000000000000000010000000010000000000006E756C6C0000000200000006626F756E64734F626A6300000001000000000000526374310000000400000000546F70206C6F6E6700000000000000004C6566746C6F6E67000000000000000042746F6D6C6F6E670000001E00000000526768746C6F6E670000003200000006736C69636573566C4C73000000014F626A6300000001000000000005736C6963650000001200000007736C69636549446C6F6E67000000000000000767726F757049446C6F6E6700000000000000066F726967696E656E756D0000000C45536C6963654F726967696E0000000D6175746F47656E6572617465640000000054797065656E756D0000000A45536C6963655479706500000000496D672000000006626F756E64734F626A6300000001000000000000526374310000000400000000546F70206C6F6E6700000000000000004C6566746C6F6E67000000000000000042746F6D6C6F6E670000001E00000000526768746C6F6E67000000320000000375726C54455854000000010000000000006E756C6C54455854000000010000000000004D7367655445585400000001000000000006616C74546167544558540000000100000000000E63656C6C54657874497348544D4C626F6F6C010000000863656C6C546578745445585400000001000000000009686F727A416C69676E656E756D0000000F45536C696365486F727A416C69676E0000000764656661756C740000000976657274416C69676E656E756D0000000F45536C69636556657274416C69676E0000000764656661756C740000000B6267436F6C6F7254797065656E756D0000001145536C6963654247436F6C6F7254797065000000004E6F6E6500000009746F704F75747365746C6F6E67000000000000000A6C6566744F75747365746C6F6E67000000000000000C626F74746F6D4F75747365746C6F6E67000000000000000B72696768744F75747365746C6F6E6700000000003842494D042800000000000C000000023FF00000000000003842494D0414000000000004000000013842494D040C00000000045600000001000000320000001E00000098000011D00000043A00180001FFD8FFED000C41646F62655F434D0001FFEE000E41646F626500648000000001FFDB0084000C08080809080C09090C110B0A0B11150F0C0C0F1518131315131318110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C010D0B0B0D0E0D100E0E10140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4013F0000010501010101010100000000000000030001020405060708090A0B0100010501010101010100000000000000010002030405060708090A0B1000010401030204020507060805030C33010002110304211231054151611322718132061491A1B14223241552C16233347282D14307259253F0E1F163733516A2B283264493546445C2A3743617D255E265F2B384C3D375E3F3462794A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F637475767778797A7B7C7D7E7F711000202010204040304050607070605350100021103213112044151617122130532819114A1B14223C152D1F0332462E1728292435315637334F1250616A2B283072635C2D2449354A317644555367465E2F2B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F62737475767778797A7B7C7FFDA000C03010002110311003F002E6EFB766335E6A392E76FB5A61CDADA3D5BB63BF31EFF00E6F7A1D2D7E1DECA0BDD663DF229DE4B9D5BDA377A3EA1F73ABB19FCDEE4DD5AA75B87686C87BDA6A11F4A2D2DA9FE9FF2FF007517240FB200CDC5F8FB1EC2F10E9A88F73BF94E6A484E78E540CA1E5DB6B4B6BC60D7DF7122ADF218206E75B6EDF77A75FF0027E9AAEFC8CCFB1545ADAFEDD635C2093E887D61CEBAC739BEEF45ADAD24A4CC7BABC5B9ED30F0C3B3E27DAD3F8A235EFACB5CC7B9AF681EF692D331FBCD553A86431B895120CDEEA8068124C96BDD1FF5295B6599153AB6BCE1DC2C6D761DCD258090EFD13FF9B7BECAFF009A494EA7EDAEAFFF00736EE23E976FBBFE9A4B03ED991FBDFE0278FCEFF49FD749253FFFD06CACEC6664D343DF1B2CDF6BB697359B1BBDADB1CD076BBDF5A9E466D1654EAF19E322C7830CA8EF207E7D8E8FA3B1A9BA6328664649A6CF51EF7DA5AD820B07A9FACB1C5DF4DFF69FFC0BD1FF0006AD8D907D3DB13EED91CFF2B6A48685F5F50B9CCB58C1435A1CDF4B7C5CE6BE377E9DA1F4E3BBDBFCB7A85D87937B6A11562D7468DC71B9E1CD236BABBAC6FA7FA3FE457FCE7F855A067550713E04FC3FDA524B9F98F6D36639BACDEFB2E0FB2C7088654D7DA763193B6AABDBEC6215D40CCADD73AB2F63DE5CDADDED7166CF42BB9BBBE85EDFE7E8DEAC5EF68BF1CECB0D81CFF480D9A9D877F367EE283F27201F6E25AEFEDD63FEFE929A1E85DFB96FF41F4BE89FE77F7BFF000C24AEFDAB33FEE1BFFEDCAFFF0024924A7FFFD93842494D042100000000005500000001010000000F00410064006F00620065002000500068006F0074006F00730068006F00700000001300410064006F00620065002000500068006F0074006F00730068006F0070002000430053003600000001003842494D0406000000000007FFFC000000010100FFE10DEB687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F003C3F787061636B657420626567696E3D22EFBBBF222069643D2257354D304D7043656869487A7265537A4E54637A6B633964223F3E203C783A786D706D65746120786D6C6E733A783D2261646F62653A6E733A6D6574612F2220783A786D70746B3D2241646F626520584D5020436F726520352E332D633031312036362E3134353636312C20323031322F30322F30362D31343A35363A32372020202020202020223E203C7264663A52444620786D6C6E733A7264663D22687474703A2F2F7777772E77332E6F72672F313939392F30322F32322D7264662D73796E7461782D6E7323223E203C7264663A4465736372697074696F6E207264663A61626F75743D222220786D6C6E733A786D703D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F2220786D6C6E733A70686F746F73686F703D22687474703A2F2F6E732E61646F62652E636F6D2F70686F746F73686F702F312E302F2220786D6C6E733A786D704D4D3D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F6D6D2F2220786D6C6E733A73744576743D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F73547970652F5265736F757263654576656E74232220786D6C6E733A64633D22687474703A2F2F7075726C2E6F72672F64632F656C656D656E74732F312E312F2220786D703A43726561746F72546F6F6C3D224939303832585855424D4B332220786D703A4D6F64696679446174653D22323031342D30342D30325431363A33343A31332B30353A33302220786D703A437265617465446174653D22323031342D30312D30315432333A30313A33342220786D703A4D65746164617461446174653D22323031342D30342D30325431363A33343A31332B30353A3330222070686F746F73686F703A44617465437265617465643D22323031342D30312D30315432333A30313A3334222070686F746F73686F703A436F6C6F724D6F64653D2233222070686F746F73686F703A49434350726F66696C653D22735247422049454336313936362D322E312220786D704D4D3A446F63756D656E7449443D2231393435374633433137394231313931364136304630423539413430394243312220786D704D4D3A496E7374616E636549443D22786D702E6969643A35463032313835333536424145333131393031314535463444333739333537422220786D704D4D3A4F726967696E616C446F63756D656E7449443D223139343537463343313739423131393136413630463042353941343039424331222064633A666F726D61743D22696D6167652F6A706567223E203C786D704D4D3A486973746F72793E203C7264663A5365713E203C7264663A6C692073744576743A616374696F6E3D227361766564222073744576743A696E7374616E636549443D22786D702E6969643A3232323735324346353442414533313139303131453546344433373933353742222073744576743A7768656E3D22323031342D30342D30325431363A32343A30312B30353A3330222073744576743A736F6674776172654167656E743D2241646F62652050686F746F73686F7020435336202857696E646F777329222073744576743A6368616E6765643D222F222F3E203C7264663A6C692073744576743A616374696F6E3D227361766564222073744576743A696E7374616E636549443D22786D702E6969643A3546303231383533353642414533313139303131453546344433373933353742222073744576743A7768656E3D22323031342D30342D30325431363A33343A31332B30353A3330222073744576743A736F6674776172654167656E743D2241646F62652050686F746F73686F7020435336202857696E646F777329222073744576743A6368616E6765643D222F222F3E203C2F7264663A5365713E203C2F786D704D4D3A486973746F72793E203C2F7264663A4465736372697074696F6E3E203C2F7264663A5244463E203C2F783A786D706D6574613E2020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020203C3F787061636B657420656E643D2277223F3EFFE20C584943435F50524F46494C4500010100000C484C696E6F021000006D6E74725247422058595A2007CE00020009000600310000616373704D5346540000000049454320735247420000000000000000000000000000F6D6000100000000D32D4850202000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001163707274000001500000003364657363000001840000006C77747074000001F000000014626B707400000204000000147258595A00000218000000146758595A0000022C000000146258595A0000024000000014646D6E640000025400000070646D6464000002C400000088767565640000034C0000008676696577000003D4000000246C756D69000003F8000000146D6561730000040C0000002474656368000004300000000C725452430000043C0000080C675452430000043C0000080C625452430000043C0000080C7465787400000000436F70797269676874202863292031393938204865776C6574742D5061636B61726420436F6D70616E790000646573630000000000000012735247422049454336313936362D322E31000000000000000000000012735247422049454336313936362D322E31000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000058595A20000000000000F35100010000000116CC58595A200000000000000000000000000000000058595A200000000000006FA2000038F50000039058595A2000000000000062990000B785000018DA58595A2000000000000024A000000F840000B6CF64657363000000000000001649454320687474703A2F2F7777772E6965632E636800000000000000000000001649454320687474703A2F2F7777772E6965632E63680000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000064657363000000000000002E4945432036313936362D322E312044656661756C742052474220636F6C6F7572207370616365202D207352474200000000000000000000002E4945432036313936362D322E312044656661756C742052474220636F6C6F7572207370616365202D20735247420000000000000000000000000000000000000000000064657363000000000000002C5265666572656E63652056696577696E6720436F6E646974696F6E20696E2049454336313936362D322E3100000000000000000000002C5265666572656E63652056696577696E6720436F6E646974696F6E20696E2049454336313936362D322E31000000000000000000000000000000000000000000000000000076696577000000000013A4FE00145F2E0010CF140003EDCC0004130B00035C9E0000000158595A2000000000004C09560050000000571FE76D6561730000000000000001000000000000000000000000000000000000028F0000000273696720000000004352542063757276000000000000040000000005000A000F00140019001E00230028002D00320037003B00400045004A004F00540059005E00630068006D00720077007C00810086008B00900095009A009F00A400A900AE00B200B700BC00C100C600CB00D000D500DB00E000E500EB00F000F600FB01010107010D01130119011F0125012B01320138013E0145014C0152015901600167016E0175017C0183018B0192019A01A101A901B101B901C101C901D101D901E101E901F201FA0203020C0214021D0226022F02380241024B0254025D02670271027A0284028E029802A202AC02B602C102CB02D502E002EB02F50300030B03160321032D03380343034F035A03660372037E038A039603A203AE03BA03C703D303E003EC03F9040604130420042D043B0448045504630471047E048C049A04A804B604C404D304E104F004FE050D051C052B053A05490558056705770586059605A605B505C505D505E505F6060606160627063706480659066A067B068C069D06AF06C006D106E306F507070719072B073D074F076107740786079907AC07BF07D207E507F8080B081F08320846085A086E0882089608AA08BE08D208E708FB09100925093A094F09640979098F09A409BA09CF09E509FB0A110A270A3D0A540A6A0A810A980AAE0AC50ADC0AF30B0B0B220B390B510B690B800B980BB00BC80BE10BF90C120C2A0C430C5C0C750C8E0CA70CC00CD90CF30D0D0D260D400D5A0D740D8E0DA90DC30DDE0DF80E130E2E0E490E640E7F0E9B0EB60ED20EEE0F090F250F410F5E0F7A0F960FB30FCF0FEC1009102610431061107E109B10B910D710F511131131114F116D118C11AA11C911E81207122612451264128412A312C312E31303132313431363138313A413C513E5140614271449146A148B14AD14CE14F01512153415561578159B15BD15E0160316261649166C168F16B216D616FA171D17411765178917AE17D217F7181B18401865188A18AF18D518FA19201945196B199119B719DD1A041A2A1A511A771A9E1AC51AEC1B141B3B1B631B8A1BB21BDA1C021C2A1C521C7B1CA31CCC1CF51D1E1D471D701D991DC31DEC1E161E401E6A1E941EBE1EE91F131F3E1F691F941FBF1FEA20152041206C209820C420F0211C2148217521A121CE21FB22272255228222AF22DD230A23382366239423C223F0241F244D247C24AB24DA250925382568259725C725F726272657268726B726E827182749277A27AB27DC280D283F287128A228D429062938296B299D29D02A022A352A682A9B2ACF2B022B362B692B9D2BD12C052C392C6E2CA22CD72D0C2D412D762DAB2DE12E162E4C2E822EB72EEE2F242F5A2F912FC72FFE3035306C30A430DB3112314A318231BA31F2322A3263329B32D4330D3346337F33B833F1342B3465349E34D83513354D358735C235FD3637367236AE36E937243760379C37D738143850388C38C839053942397F39BC39F93A363A743AB23AEF3B2D3B6B3BAA3BE83C273C653CA43CE33D223D613DA13DE03E203E603EA03EE03F213F613FA23FE24023406440A640E74129416A41AC41EE4230427242B542F7433A437D43C044034447448A44CE45124555459A45DE4622466746AB46F04735477B47C04805484B489148D7491D496349A949F04A374A7D4AC44B0C4B534B9A4BE24C2A4C724CBA4D024D4A4D934DDC4E254E6E4EB74F004F494F934FDD5027507150BB51065150519B51E65231527C52C75313535F53AA53F65442548F54DB5528557555C2560F565C56A956F75744579257E0582F587D58CB591A596959B85A075A565AA65AF55B455B955BE55C355C865CD65D275D785DC95E1A5E6C5EBD5F0F5F615FB36005605760AA60FC614F61A261F56249629C62F06343639763EB6440649464E9653D659265E7663D669266E8673D679367E9683F689668EC6943699A69F16A486A9F6AF76B4F6BA76BFF6C576CAF6D086D606DB96E126E6B6EC46F1E6F786FD1702B708670E0713A719571F0724B72A67301735D73B87414747074CC7528758575E1763E769B76F8775677B37811786E78CC792A798979E77A467AA57B047B637BC27C217C817CE17D417DA17E017E627EC27F237F847FE5804780A8810A816B81CD8230829282F4835783BA841D848084E3854785AB860E867286D7873B879F8804886988CE8933899989FE8A648ACA8B308B968BFC8C638CCA8D318D988DFF8E668ECE8F368F9E9006906E90D6913F91A89211927A92E3934D93B69420948A94F4955F95C99634969F970A977597E0984C98B89924999099FC9A689AD59B429BAF9C1C9C899CF79D649DD29E409EAE9F1D9F8B9FFAA069A0D8A147A1B6A226A296A306A376A3E6A456A4C7A538A5A9A61AA68BA6FDA76EA7E0A852A8C4A937A9A9AA1CAA8FAB02AB75ABE9AC5CACD0AD44ADB8AE2DAEA1AF16AF8BB000B075B0EAB160B1D6B24BB2C2B338B3AEB425B49CB513B58AB601B679B6F0B768B7E0B859B8D1B94AB9C2BA3BBAB5BB2EBBA7BC21BC9BBD15BD8FBE0ABE84BEFFBF7ABFF5C070C0ECC167C1E3C25FC2DBC358C3D4C451C4CEC54BC5C8C646C6C3C741C7BFC83DC8BCC93AC9B9CA38CAB7CB36CBB6CC35CCB5CD35CDB5CE36CEB6CF37CFB8D039D0BAD13CD1BED23FD2C1D344D3C6D449D4CBD54ED5D1D655D6D8D75CD7E0D864D8E8D96CD9F1DA76DAFBDB80DC05DC8ADD10DD96DE1CDEA2DF29DFAFE036E0BDE144E1CCE253E2DBE363E3EBE473E4FCE584E60DE696E71FE7A9E832E8BCE946E9D0EA5BEAE5EB70EBFBEC86ED11ED9CEE28EEB4EF40EFCCF058F0E5F172F1FFF28CF319F3A7F434F4C2F550F5DEF66DF6FBF78AF819F8A8F938F9C7FA57FAE7FB77FC07FC98FD29FDBAFE4BFEDCFF6DFFFFFFEE000E41646F626500648000000001FFDB00840020212133243351303051422F2F2F42271C1C1C1C2722171717171722110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0122333334263422181822140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4011B0000030101010101010101010000000000010002030405060708090A0B0101010101010101010101010100000000000102030405060708090A0B1000020201030203040706030306020135010002110321123104415122136171328191B142A105D1C114F05223723362E182F1433492A2B215D2532473C263068393E2F2A3445464253545162674365565B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F6110002020005010606010301030503062F0001110221033141125161718191221332F0A1B104C1D1E1F14252236272149233824324A2B23453446373C2D28393A354E2F205152506162635644555367465B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F556667686FFDA000C03010002110311003F00A9EBE5E377DAFF0007F7737FF03400606B98CBE0FF0004FF00EE7FF1793FDDA328B89FF91FF9B3F94D4BE1D398EDF8BFF29A05211324691E4FC3FC3FF8DCBFF94D83296D1C6F3FF9AB743FBDFF0098500CCD449F6340D70E7925E51FE2D8A4990AF80DEDEDE5FF00C57FBBFE67FBA40DFD6C9FC52579379FF9BFBE4540FFD065300807C7CDFE1D914CA608A8F98FF83CDFF991188004D1BD65FE8F3FFE13FF00CD3FFA0BD174F7206521296A3CBFE1DDFCDF37FE57FECF4E83032AE2223F63E3FF00CDD93FF81FFE657664A063334459E65E6FE9C62797FF003562418EF175DFE1FF0006DF431E6FFC7FFBFC0DC8EA3437E6DBF0FF000FFE310647F84FD30FFB6818ED3EDFEDECFF00CC9FFD70AEBBA5FC27FE541503FFD9, 1, 0, 0, CAST(0x0000A92700CA5731 AS DateTime), 1, CAST(0x0000A92700CA57F3 AS DateTime), 1)
INSERT [dbo].[Gbl_Attachment] ([AttachmentId], [FileName], [OriginalFileName], [FileExtension], [ModuleName], [Attachment], [ShopId], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (3, N'Img_636680314564791740', N'deepa sign.jpg', N'jpg', N'Upload Shop Logo', 0xFFD8FFE107C445786966000049492A00080000000F000001040001000000C00C00000101040001000000900900000201030003000000C20000000601030001000000020000000F01020008000000C80000001001020009000000D00000001201030001000000010000001501030001000000030000001A01050001000000D90000001B01050001000000E1000000280103000100000002000000310102001E000000E900000032010200140000000701000013020300010000000100000069870400010000001C0100002403000008000800080053414D53554E470047542D49393038320080FC0A001027000080FC0A001027000041646F62652050686F746F73686F7020435336202857696E646F77732900323031343A30343A30322031363A33343A313300001E009A820500010000008A0200009D82050001000000920200002288030001000000030000002788030001000000F401000000900700040000003032323003900200140000009A0200000490020014000000AE02000001910700040000000102030001920A0001000000C20200000292050001000000CA02000003920A0001000000D202000004920A0001000000DA0200000592050001000000E20200000792030001000000010000000992030001000000000000000A92050001000000EA0200008692070008000000F202000000A00700040000003031303001A00300010000000100000002A00400010000003200000003A00400010000001E00000005A00400010000001003000001A30700010000000100000002A40300010000000000000003A40300010000000000000004A4050001000000FA02000006A40300010000000000000009A4030001000000000000000AA40300010000000000000020A402000C00000002030000000000005EEA000040420F007467000010270000323031343A30313A30312032333A30313A333400323031343A30313A30312032333A30313A3334003DEF3D0040420F00C66D000010270000F5000000640000000000000006000000C66D0000102700007201000064000000415343494900000001000000010000004F30384230534147443031000000010001000200040000005239380000000000000006000301030001000000060000001A01050001000000720300001B010500010000007A03000028010300010000000200000001020400010000008203000002020400010000003A0400000000000048000000010000004800000001000000FFD8FFED000C41646F62655F434D0001FFEE000E41646F626500648000000001FFDB0084000C08080809080C09090C110B0A0B11150F0C0C0F1518131315131318110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C010D0B0B0D0E0D100E0E10140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4013F0000010501010101010100000000000000030001020405060708090A0B0100010501010101010100000000000000010002030405060708090A0B1000010401030204020507060805030C33010002110304211231054151611322718132061491A1B14223241552C16233347282D14307259253F0E1F163733516A2B283264493546445C2A3743617D255E265F2B384C3D375E3F3462794A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F637475767778797A7B7C7D7E7F711000202010204040304050607070605350100021103213112044151617122130532819114A1B14223C152D1F0332462E1728292435315637334F1250616A2B283072635C2D2449354A317644555367465E2F2B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F62737475767778797A7B7C7FFDA000C03010002110311003F002E6EFB766335E6A392E76FB5A61CDADA3D5BB63BF31EFF00E6F7A1D2D7E1DECA0BDD663DF229DE4B9D5BDA377A3EA1F73ABB19FCDEE4DD5AA75B87686C87BDA6A11F4A2D2DA9FE9FF2FF007517240FB200CDC5F8FB1EC2F10E9A88F73BF94E6A484E78E540CA1E5DB6B4B6BC60D7DF7122ADF218206E75B6EDF77A75FF0027E9AAEFC8CCFB1545ADAFEDD635C2093E887D61CEBAC739BEEF45ADAD24A4CC7BABC5B9ED30F0C3B3E27DAD3F8A235EFACB5CC7B9AF681EF692D331FBCD553A86431B895120CDEEA8068124C96BDD1FF5295B6599153AB6BCE1DC2C6D761DCD258090EFD13FF9B7BECAFF009A494EA7EDAEAFFF00736EE23E976FBBFE9A4B03ED991FBDFE0278FCEFF49FD749253FFFD06CACEC6664D343DF1B2CDF6BB697359B1BBDADB1CD076BBDF5A9E466D1654EAF19E322C7830CA8EF207E7D8E8FA3B1A9BA6328664649A6CF51EF7DA5AD820B07A9FACB1C5DF4DFF69FFC0BD1FF0006AD8D907D3DB13EED91CFF2B6A48685F5F50B9CCB58C1435A1CDF4B7C5CE6BE377E9DA1F4E3BBDBFCB7A85D87937B6A11562D7468DC71B9E1CD236BABBAC6FA7FA3FE457FCE7F855A067550713E04FC3FDA524B9F98F6D36639BACDEFB2E0FB2C7088654D7DA763193B6AABDBEC6215D40CCADD73AB2F63DE5CDADDED7166CF42BB9BBBE85EDFE7E8DEAC5EF68BF1CECB0D81CFF480D9A9D877F367EE283F27201F6E25AEFEDD63FEFE929A1E85DFB96FF41F4BE89FE77F7BFF000C24AEFDAB33FEE1BFFEDCAFFF0024924A7FFFD9FFED0D6450686F746F73686F7020332E30003842494D040400000000002C1C015A00031B25471C0200000200001C0237000832303134303130311C023C000B3233303133342B303030303842494D04250000000000105BC256EA5002190D3DDB9F2CB5C6F15B3842494D043A0000000000E5000000100000000100000000000B7072696E744F7574707574000000050000000050737453626F6F6C0100000000496E7465656E756D00000000496E746500000000436C726D0000000F7072696E745369787465656E426974626F6F6C000000000B7072696E7465724E616D65544558540000000100000000000F7072696E7450726F6F6653657475704F626A630000000C00500072006F006F006600200053006500740075007000000000000A70726F6F6653657475700000000100000000426C746E656E756D0000000C6275696C74696E50726F6F660000000970726F6F66434D594B003842494D043B00000000022D00000010000000010000000000127072696E744F75747075744F7074696F6E7300000017000000004370746E626F6F6C0000000000436C6272626F6F6C00000000005267734D626F6F6C000000000043726E43626F6F6C0000000000436E7443626F6F6C00000000004C626C73626F6F6C00000000004E677476626F6F6C0000000000456D6C44626F6F6C0000000000496E7472626F6F6C000000000042636B674F626A630000000100000000000052474243000000030000000052642020646F7562406FE000000000000000000047726E20646F7562406FE0000000000000000000426C2020646F7562406FE000000000000000000042726454556E744623526C74000000000000000000000000426C6420556E744623526C7400000000000000000000000052736C74556E74462350786C40520000000000000000000A766563746F7244617461626F6F6C010000000050675073656E756D00000000506750730000000050675043000000004C656674556E744623526C74000000000000000000000000546F7020556E744623526C7400000000000000000000000053636C20556E74462350726340590000000000000000001063726F705768656E5072696E74696E67626F6F6C000000000E63726F7052656374426F74746F6D6C6F6E67000000000000000C63726F70526563744C6566746C6F6E67000000000000000D63726F705265637452696768746C6F6E67000000000000000B63726F7052656374546F706C6F6E6700000000003842494D03ED000000000010004800000001000100480000000100013842494D042600000000000E000000000000000000003F8000003842494D040D000000000004FFFFFFC43842494D04190000000000040000001E3842494D03F3000000000009000000000000000001003842494D271000000000000A000100000000000000013842494D03F5000000000048002F66660001006C66660006000000000001002F6666000100A1999A0006000000000001003200000001005A00000006000000000001003500000001002D000000060000000000013842494D03F80000000000700000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800003842494D0408000000000010000000010000024000000240000000003842494D041E000000000004000000003842494D041A0000000003490000000600000000000000000000001E000000320000000A006400650065007000610020007300690067006E0000000100000000000000000000000000000000000000010000000000000000000000320000001E00000000000000000000000000000000010000000000000000000000000000000000000010000000010000000000006E756C6C0000000200000006626F756E64734F626A6300000001000000000000526374310000000400000000546F70206C6F6E6700000000000000004C6566746C6F6E67000000000000000042746F6D6C6F6E670000001E00000000526768746C6F6E670000003200000006736C69636573566C4C73000000014F626A6300000001000000000005736C6963650000001200000007736C69636549446C6F6E67000000000000000767726F757049446C6F6E6700000000000000066F726967696E656E756D0000000C45536C6963654F726967696E0000000D6175746F47656E6572617465640000000054797065656E756D0000000A45536C6963655479706500000000496D672000000006626F756E64734F626A6300000001000000000000526374310000000400000000546F70206C6F6E6700000000000000004C6566746C6F6E67000000000000000042746F6D6C6F6E670000001E00000000526768746C6F6E67000000320000000375726C54455854000000010000000000006E756C6C54455854000000010000000000004D7367655445585400000001000000000006616C74546167544558540000000100000000000E63656C6C54657874497348544D4C626F6F6C010000000863656C6C546578745445585400000001000000000009686F727A416C69676E656E756D0000000F45536C696365486F727A416C69676E0000000764656661756C740000000976657274416C69676E656E756D0000000F45536C69636556657274416C69676E0000000764656661756C740000000B6267436F6C6F7254797065656E756D0000001145536C6963654247436F6C6F7254797065000000004E6F6E6500000009746F704F75747365746C6F6E67000000000000000A6C6566744F75747365746C6F6E67000000000000000C626F74746F6D4F75747365746C6F6E67000000000000000B72696768744F75747365746C6F6E6700000000003842494D042800000000000C000000023FF00000000000003842494D0414000000000004000000013842494D040C00000000045600000001000000320000001E00000098000011D00000043A00180001FFD8FFED000C41646F62655F434D0001FFEE000E41646F626500648000000001FFDB0084000C08080809080C09090C110B0A0B11150F0C0C0F1518131315131318110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C010D0B0B0D0E0D100E0E10140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4013F0000010501010101010100000000000000030001020405060708090A0B0100010501010101010100000000000000010002030405060708090A0B1000010401030204020507060805030C33010002110304211231054151611322718132061491A1B14223241552C16233347282D14307259253F0E1F163733516A2B283264493546445C2A3743617D255E265F2B384C3D375E3F3462794A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F637475767778797A7B7C7D7E7F711000202010204040304050607070605350100021103213112044151617122130532819114A1B14223C152D1F0332462E1728292435315637334F1250616A2B283072635C2D2449354A317644555367465E2F2B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F62737475767778797A7B7C7FFDA000C03010002110311003F002E6EFB766335E6A392E76FB5A61CDADA3D5BB63BF31EFF00E6F7A1D2D7E1DECA0BDD663DF229DE4B9D5BDA377A3EA1F73ABB19FCDEE4DD5AA75B87686C87BDA6A11F4A2D2DA9FE9FF2FF007517240FB200CDC5F8FB1EC2F10E9A88F73BF94E6A484E78E540CA1E5DB6B4B6BC60D7DF7122ADF218206E75B6EDF77A75FF0027E9AAEFC8CCFB1545ADAFEDD635C2093E887D61CEBAC739BEEF45ADAD24A4CC7BABC5B9ED30F0C3B3E27DAD3F8A235EFACB5CC7B9AF681EF692D331FBCD553A86431B895120CDEEA8068124C96BDD1FF5295B6599153AB6BCE1DC2C6D761DCD258090EFD13FF9B7BECAFF009A494EA7EDAEAFFF00736EE23E976FBBFE9A4B03ED991FBDFE0278FCEFF49FD749253FFFD06CACEC6664D343DF1B2CDF6BB697359B1BBDADB1CD076BBDF5A9E466D1654EAF19E322C7830CA8EF207E7D8E8FA3B1A9BA6328664649A6CF51EF7DA5AD820B07A9FACB1C5DF4DFF69FFC0BD1FF0006AD8D907D3DB13EED91CFF2B6A48685F5F50B9CCB58C1435A1CDF4B7C5CE6BE377E9DA1F4E3BBDBFCB7A85D87937B6A11562D7468DC71B9E1CD236BABBAC6FA7FA3FE457FCE7F855A067550713E04FC3FDA524B9F98F6D36639BACDEFB2E0FB2C7088654D7DA763193B6AABDBEC6215D40CCADD73AB2F63DE5CDADDED7166CF42BB9BBBE85EDFE7E8DEAC5EF68BF1CECB0D81CFF480D9A9D877F367EE283F27201F6E25AEFEDD63FEFE929A1E85DFB96FF41F4BE89FE77F7BFF000C24AEFDAB33FEE1BFFEDCAFFF0024924A7FFFD93842494D042100000000005500000001010000000F00410064006F00620065002000500068006F0074006F00730068006F00700000001300410064006F00620065002000500068006F0074006F00730068006F0070002000430053003600000001003842494D0406000000000007FFFC000000010100FFE10DEB687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F003C3F787061636B657420626567696E3D22EFBBBF222069643D2257354D304D7043656869487A7265537A4E54637A6B633964223F3E203C783A786D706D65746120786D6C6E733A783D2261646F62653A6E733A6D6574612F2220783A786D70746B3D2241646F626520584D5020436F726520352E332D633031312036362E3134353636312C20323031322F30322F30362D31343A35363A32372020202020202020223E203C7264663A52444620786D6C6E733A7264663D22687474703A2F2F7777772E77332E6F72672F313939392F30322F32322D7264662D73796E7461782D6E7323223E203C7264663A4465736372697074696F6E207264663A61626F75743D222220786D6C6E733A786D703D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F2220786D6C6E733A70686F746F73686F703D22687474703A2F2F6E732E61646F62652E636F6D2F70686F746F73686F702F312E302F2220786D6C6E733A786D704D4D3D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F6D6D2F2220786D6C6E733A73744576743D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F73547970652F5265736F757263654576656E74232220786D6C6E733A64633D22687474703A2F2F7075726C2E6F72672F64632F656C656D656E74732F312E312F2220786D703A43726561746F72546F6F6C3D224939303832585855424D4B332220786D703A4D6F64696679446174653D22323031342D30342D30325431363A33343A31332B30353A33302220786D703A437265617465446174653D22323031342D30312D30315432333A30313A33342220786D703A4D65746164617461446174653D22323031342D30342D30325431363A33343A31332B30353A3330222070686F746F73686F703A44617465437265617465643D22323031342D30312D30315432333A30313A3334222070686F746F73686F703A436F6C6F724D6F64653D2233222070686F746F73686F703A49434350726F66696C653D22735247422049454336313936362D322E312220786D704D4D3A446F63756D656E7449443D2231393435374633433137394231313931364136304630423539413430394243312220786D704D4D3A496E7374616E636549443D22786D702E6969643A35463032313835333536424145333131393031314535463444333739333537422220786D704D4D3A4F726967696E616C446F63756D656E7449443D223139343537463343313739423131393136413630463042353941343039424331222064633A666F726D61743D22696D6167652F6A706567223E203C786D704D4D3A486973746F72793E203C7264663A5365713E203C7264663A6C692073744576743A616374696F6E3D227361766564222073744576743A696E7374616E636549443D22786D702E6969643A3232323735324346353442414533313139303131453546344433373933353742222073744576743A7768656E3D22323031342D30342D30325431363A32343A30312B30353A3330222073744576743A736F6674776172654167656E743D2241646F62652050686F746F73686F7020435336202857696E646F777329222073744576743A6368616E6765643D222F222F3E203C7264663A6C692073744576743A616374696F6E3D227361766564222073744576743A696E7374616E636549443D22786D702E6969643A3546303231383533353642414533313139303131453546344433373933353742222073744576743A7768656E3D22323031342D30342D30325431363A33343A31332B30353A3330222073744576743A736F6674776172654167656E743D2241646F62652050686F746F73686F7020435336202857696E646F777329222073744576743A6368616E6765643D222F222F3E203C2F7264663A5365713E203C2F786D704D4D3A486973746F72793E203C2F7264663A4465736372697074696F6E3E203C2F7264663A5244463E203C2F783A786D706D6574613E2020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020203C3F787061636B657420656E643D2277223F3EFFE20C584943435F50524F46494C4500010100000C484C696E6F021000006D6E74725247422058595A2007CE00020009000600310000616373704D5346540000000049454320735247420000000000000000000000000000F6D6000100000000D32D4850202000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001163707274000001500000003364657363000001840000006C77747074000001F000000014626B707400000204000000147258595A00000218000000146758595A0000022C000000146258595A0000024000000014646D6E640000025400000070646D6464000002C400000088767565640000034C0000008676696577000003D4000000246C756D69000003F8000000146D6561730000040C0000002474656368000004300000000C725452430000043C0000080C675452430000043C0000080C625452430000043C0000080C7465787400000000436F70797269676874202863292031393938204865776C6574742D5061636B61726420436F6D70616E790000646573630000000000000012735247422049454336313936362D322E31000000000000000000000012735247422049454336313936362D322E31000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000058595A20000000000000F35100010000000116CC58595A200000000000000000000000000000000058595A200000000000006FA2000038F50000039058595A2000000000000062990000B785000018DA58595A2000000000000024A000000F840000B6CF64657363000000000000001649454320687474703A2F2F7777772E6965632E636800000000000000000000001649454320687474703A2F2F7777772E6965632E63680000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000064657363000000000000002E4945432036313936362D322E312044656661756C742052474220636F6C6F7572207370616365202D207352474200000000000000000000002E4945432036313936362D322E312044656661756C742052474220636F6C6F7572207370616365202D20735247420000000000000000000000000000000000000000000064657363000000000000002C5265666572656E63652056696577696E6720436F6E646974696F6E20696E2049454336313936362D322E3100000000000000000000002C5265666572656E63652056696577696E6720436F6E646974696F6E20696E2049454336313936362D322E31000000000000000000000000000000000000000000000000000076696577000000000013A4FE00145F2E0010CF140003EDCC0004130B00035C9E0000000158595A2000000000004C09560050000000571FE76D6561730000000000000001000000000000000000000000000000000000028F0000000273696720000000004352542063757276000000000000040000000005000A000F00140019001E00230028002D00320037003B00400045004A004F00540059005E00630068006D00720077007C00810086008B00900095009A009F00A400A900AE00B200B700BC00C100C600CB00D000D500DB00E000E500EB00F000F600FB01010107010D01130119011F0125012B01320138013E0145014C0152015901600167016E0175017C0183018B0192019A01A101A901B101B901C101C901D101D901E101E901F201FA0203020C0214021D0226022F02380241024B0254025D02670271027A0284028E029802A202AC02B602C102CB02D502E002EB02F50300030B03160321032D03380343034F035A03660372037E038A039603A203AE03BA03C703D303E003EC03F9040604130420042D043B0448045504630471047E048C049A04A804B604C404D304E104F004FE050D051C052B053A05490558056705770586059605A605B505C505D505E505F6060606160627063706480659066A067B068C069D06AF06C006D106E306F507070719072B073D074F076107740786079907AC07BF07D207E507F8080B081F08320846085A086E0882089608AA08BE08D208E708FB09100925093A094F09640979098F09A409BA09CF09E509FB0A110A270A3D0A540A6A0A810A980AAE0AC50ADC0AF30B0B0B220B390B510B690B800B980BB00BC80BE10BF90C120C2A0C430C5C0C750C8E0CA70CC00CD90CF30D0D0D260D400D5A0D740D8E0DA90DC30DDE0DF80E130E2E0E490E640E7F0E9B0EB60ED20EEE0F090F250F410F5E0F7A0F960FB30FCF0FEC1009102610431061107E109B10B910D710F511131131114F116D118C11AA11C911E81207122612451264128412A312C312E31303132313431363138313A413C513E5140614271449146A148B14AD14CE14F01512153415561578159B15BD15E0160316261649166C168F16B216D616FA171D17411765178917AE17D217F7181B18401865188A18AF18D518FA19201945196B199119B719DD1A041A2A1A511A771A9E1AC51AEC1B141B3B1B631B8A1BB21BDA1C021C2A1C521C7B1CA31CCC1CF51D1E1D471D701D991DC31DEC1E161E401E6A1E941EBE1EE91F131F3E1F691F941FBF1FEA20152041206C209820C420F0211C2148217521A121CE21FB22272255228222AF22DD230A23382366239423C223F0241F244D247C24AB24DA250925382568259725C725F726272657268726B726E827182749277A27AB27DC280D283F287128A228D429062938296B299D29D02A022A352A682A9B2ACF2B022B362B692B9D2BD12C052C392C6E2CA22CD72D0C2D412D762DAB2DE12E162E4C2E822EB72EEE2F242F5A2F912FC72FFE3035306C30A430DB3112314A318231BA31F2322A3263329B32D4330D3346337F33B833F1342B3465349E34D83513354D358735C235FD3637367236AE36E937243760379C37D738143850388C38C839053942397F39BC39F93A363A743AB23AEF3B2D3B6B3BAA3BE83C273C653CA43CE33D223D613DA13DE03E203E603EA03EE03F213F613FA23FE24023406440A640E74129416A41AC41EE4230427242B542F7433A437D43C044034447448A44CE45124555459A45DE4622466746AB46F04735477B47C04805484B489148D7491D496349A949F04A374A7D4AC44B0C4B534B9A4BE24C2A4C724CBA4D024D4A4D934DDC4E254E6E4EB74F004F494F934FDD5027507150BB51065150519B51E65231527C52C75313535F53AA53F65442548F54DB5528557555C2560F565C56A956F75744579257E0582F587D58CB591A596959B85A075A565AA65AF55B455B955BE55C355C865CD65D275D785DC95E1A5E6C5EBD5F0F5F615FB36005605760AA60FC614F61A261F56249629C62F06343639763EB6440649464E9653D659265E7663D669266E8673D679367E9683F689668EC6943699A69F16A486A9F6AF76B4F6BA76BFF6C576CAF6D086D606DB96E126E6B6EC46F1E6F786FD1702B708670E0713A719571F0724B72A67301735D73B87414747074CC7528758575E1763E769B76F8775677B37811786E78CC792A798979E77A467AA57B047B637BC27C217C817CE17D417DA17E017E627EC27F237F847FE5804780A8810A816B81CD8230829282F4835783BA841D848084E3854785AB860E867286D7873B879F8804886988CE8933899989FE8A648ACA8B308B968BFC8C638CCA8D318D988DFF8E668ECE8F368F9E9006906E90D6913F91A89211927A92E3934D93B69420948A94F4955F95C99634969F970A977597E0984C98B89924999099FC9A689AD59B429BAF9C1C9C899CF79D649DD29E409EAE9F1D9F8B9FFAA069A0D8A147A1B6A226A296A306A376A3E6A456A4C7A538A5A9A61AA68BA6FDA76EA7E0A852A8C4A937A9A9AA1CAA8FAB02AB75ABE9AC5CACD0AD44ADB8AE2DAEA1AF16AF8BB000B075B0EAB160B1D6B24BB2C2B338B3AEB425B49CB513B58AB601B679B6F0B768B7E0B859B8D1B94AB9C2BA3BBAB5BB2EBBA7BC21BC9BBD15BD8FBE0ABE84BEFFBF7ABFF5C070C0ECC167C1E3C25FC2DBC358C3D4C451C4CEC54BC5C8C646C6C3C741C7BFC83DC8BCC93AC9B9CA38CAB7CB36CBB6CC35CCB5CD35CDB5CE36CEB6CF37CFB8D039D0BAD13CD1BED23FD2C1D344D3C6D449D4CBD54ED5D1D655D6D8D75CD7E0D864D8E8D96CD9F1DA76DAFBDB80DC05DC8ADD10DD96DE1CDEA2DF29DFAFE036E0BDE144E1CCE253E2DBE363E3EBE473E4FCE584E60DE696E71FE7A9E832E8BCE946E9D0EA5BEAE5EB70EBFBEC86ED11ED9CEE28EEB4EF40EFCCF058F0E5F172F1FFF28CF319F3A7F434F4C2F550F5DEF66DF6FBF78AF819F8A8F938F9C7FA57FAE7FB77FC07FC98FD29FDBAFE4BFEDCFF6DFFFFFFEE000E41646F626500648000000001FFDB00840020212133243351303051422F2F2F42271C1C1C1C2722171717171722110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0122333334263422181822140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4011B0000030101010101010101010000000000010002030405060708090A0B0101010101010101010101010100000000000102030405060708090A0B1000020201030203040706030306020135010002110321123104415122136171328191B142A105D1C114F05223723362E182F1433492A2B215D2532473C263068393E2F2A3445464253545162674365565B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F6110002020005010606010301030503062F0001110221033141125161718191221332F0A1B104C1D1E1F14252236272149233824324A2B23453446373C2D28393A354E2F205152506162635644555367465B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F556667686FFDA000C03010002110311003F00A9EBE5E377DAFF0007F7737FF03400606B98CBE0FF0004FF00EE7FF1793FDDA328B89FF91FF9B3F94D4BE1D398EDF8BFF29A05211324691E4FC3FC3FF8DCBFF94D83296D1C6F3FF9AB743FBDFF0098500CCD449F6340D70E7925E51FE2D8A4990AF80DEDEDE5FF00C57FBBFE67FBA40DFD6C9FC52579379FF9BFBE4540FFD065300807C7CDFE1D914CA608A8F98FF83CDFF991188004D1BD65FE8F3FFE13FF00CD3FFA0BD174F7206521296A3CBFE1DDFCDF37FE57FECF4E83032AE2223F63E3FF00CDD93FF81FFE657664A063334459E65E6FE9C62797FF003562418EF175DFE1FF0006DF431E6FFC7FFBFC0DC8EA3437E6DBF0FF000FFE310647F84FD30FFB6818ED3EDFEDECFF00CC9FFD70AEBBA5FC27FE541503FFD9, 1, 0, 0, CAST(0x0000A92700CA9710 AS DateTime), 1, CAST(0x0000A92700CA9710 AS DateTime), 1)
INSERT [dbo].[Gbl_Attachment] ([AttachmentId], [FileName], [OriginalFileName], [FileExtension], [ModuleName], [Attachment], [ShopId], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (4, N'Img_636680315079176560', N'deepa sign.jpg', N'jpg', N'Upload Shop Logo', 0xFFD8FFE107C445786966000049492A00080000000F000001040001000000C00C00000101040001000000900900000201030003000000C20000000601030001000000020000000F01020008000000C80000001001020009000000D00000001201030001000000010000001501030001000000030000001A01050001000000D90000001B01050001000000E1000000280103000100000002000000310102001E000000E900000032010200140000000701000013020300010000000100000069870400010000001C0100002403000008000800080053414D53554E470047542D49393038320080FC0A001027000080FC0A001027000041646F62652050686F746F73686F7020435336202857696E646F77732900323031343A30343A30322031363A33343A313300001E009A820500010000008A0200009D82050001000000920200002288030001000000030000002788030001000000F401000000900700040000003032323003900200140000009A0200000490020014000000AE02000001910700040000000102030001920A0001000000C20200000292050001000000CA02000003920A0001000000D202000004920A0001000000DA0200000592050001000000E20200000792030001000000010000000992030001000000000000000A92050001000000EA0200008692070008000000F202000000A00700040000003031303001A00300010000000100000002A00400010000003200000003A00400010000001E00000005A00400010000001003000001A30700010000000100000002A40300010000000000000003A40300010000000000000004A4050001000000FA02000006A40300010000000000000009A4030001000000000000000AA40300010000000000000020A402000C00000002030000000000005EEA000040420F007467000010270000323031343A30313A30312032333A30313A333400323031343A30313A30312032333A30313A3334003DEF3D0040420F00C66D000010270000F5000000640000000000000006000000C66D0000102700007201000064000000415343494900000001000000010000004F30384230534147443031000000010001000200040000005239380000000000000006000301030001000000060000001A01050001000000720300001B010500010000007A03000028010300010000000200000001020400010000008203000002020400010000003A0400000000000048000000010000004800000001000000FFD8FFED000C41646F62655F434D0001FFEE000E41646F626500648000000001FFDB0084000C08080809080C09090C110B0A0B11150F0C0C0F1518131315131318110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C010D0B0B0D0E0D100E0E10140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4013F0000010501010101010100000000000000030001020405060708090A0B0100010501010101010100000000000000010002030405060708090A0B1000010401030204020507060805030C33010002110304211231054151611322718132061491A1B14223241552C16233347282D14307259253F0E1F163733516A2B283264493546445C2A3743617D255E265F2B384C3D375E3F3462794A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F637475767778797A7B7C7D7E7F711000202010204040304050607070605350100021103213112044151617122130532819114A1B14223C152D1F0332462E1728292435315637334F1250616A2B283072635C2D2449354A317644555367465E2F2B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F62737475767778797A7B7C7FFDA000C03010002110311003F002E6EFB766335E6A392E76FB5A61CDADA3D5BB63BF31EFF00E6F7A1D2D7E1DECA0BDD663DF229DE4B9D5BDA377A3EA1F73ABB19FCDEE4DD5AA75B87686C87BDA6A11F4A2D2DA9FE9FF2FF007517240FB200CDC5F8FB1EC2F10E9A88F73BF94E6A484E78E540CA1E5DB6B4B6BC60D7DF7122ADF218206E75B6EDF77A75FF0027E9AAEFC8CCFB1545ADAFEDD635C2093E887D61CEBAC739BEEF45ADAD24A4CC7BABC5B9ED30F0C3B3E27DAD3F8A235EFACB5CC7B9AF681EF692D331FBCD553A86431B895120CDEEA8068124C96BDD1FF5295B6599153AB6BCE1DC2C6D761DCD258090EFD13FF9B7BECAFF009A494EA7EDAEAFFF00736EE23E976FBBFE9A4B03ED991FBDFE0278FCEFF49FD749253FFFD06CACEC6664D343DF1B2CDF6BB697359B1BBDADB1CD076BBDF5A9E466D1654EAF19E322C7830CA8EF207E7D8E8FA3B1A9BA6328664649A6CF51EF7DA5AD820B07A9FACB1C5DF4DFF69FFC0BD1FF0006AD8D907D3DB13EED91CFF2B6A48685F5F50B9CCB58C1435A1CDF4B7C5CE6BE377E9DA1F4E3BBDBFCB7A85D87937B6A11562D7468DC71B9E1CD236BABBAC6FA7FA3FE457FCE7F855A067550713E04FC3FDA524B9F98F6D36639BACDEFB2E0FB2C7088654D7DA763193B6AABDBEC6215D40CCADD73AB2F63DE5CDADDED7166CF42BB9BBBE85EDFE7E8DEAC5EF68BF1CECB0D81CFF480D9A9D877F367EE283F27201F6E25AEFEDD63FEFE929A1E85DFB96FF41F4BE89FE77F7BFF000C24AEFDAB33FEE1BFFEDCAFFF0024924A7FFFD9FFED0D6450686F746F73686F7020332E30003842494D040400000000002C1C015A00031B25471C0200000200001C0237000832303134303130311C023C000B3233303133342B303030303842494D04250000000000105BC256EA5002190D3DDB9F2CB5C6F15B3842494D043A0000000000E5000000100000000100000000000B7072696E744F7574707574000000050000000050737453626F6F6C0100000000496E7465656E756D00000000496E746500000000436C726D0000000F7072696E745369787465656E426974626F6F6C000000000B7072696E7465724E616D65544558540000000100000000000F7072696E7450726F6F6653657475704F626A630000000C00500072006F006F006600200053006500740075007000000000000A70726F6F6653657475700000000100000000426C746E656E756D0000000C6275696C74696E50726F6F660000000970726F6F66434D594B003842494D043B00000000022D00000010000000010000000000127072696E744F75747075744F7074696F6E7300000017000000004370746E626F6F6C0000000000436C6272626F6F6C00000000005267734D626F6F6C000000000043726E43626F6F6C0000000000436E7443626F6F6C00000000004C626C73626F6F6C00000000004E677476626F6F6C0000000000456D6C44626F6F6C0000000000496E7472626F6F6C000000000042636B674F626A630000000100000000000052474243000000030000000052642020646F7562406FE000000000000000000047726E20646F7562406FE0000000000000000000426C2020646F7562406FE000000000000000000042726454556E744623526C74000000000000000000000000426C6420556E744623526C7400000000000000000000000052736C74556E74462350786C40520000000000000000000A766563746F7244617461626F6F6C010000000050675073656E756D00000000506750730000000050675043000000004C656674556E744623526C74000000000000000000000000546F7020556E744623526C7400000000000000000000000053636C20556E74462350726340590000000000000000001063726F705768656E5072696E74696E67626F6F6C000000000E63726F7052656374426F74746F6D6C6F6E67000000000000000C63726F70526563744C6566746C6F6E67000000000000000D63726F705265637452696768746C6F6E67000000000000000B63726F7052656374546F706C6F6E6700000000003842494D03ED000000000010004800000001000100480000000100013842494D042600000000000E000000000000000000003F8000003842494D040D000000000004FFFFFFC43842494D04190000000000040000001E3842494D03F3000000000009000000000000000001003842494D271000000000000A000100000000000000013842494D03F5000000000048002F66660001006C66660006000000000001002F6666000100A1999A0006000000000001003200000001005A00000006000000000001003500000001002D000000060000000000013842494D03F80000000000700000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF03E800003842494D0408000000000010000000010000024000000240000000003842494D041E000000000004000000003842494D041A0000000003490000000600000000000000000000001E000000320000000A006400650065007000610020007300690067006E0000000100000000000000000000000000000000000000010000000000000000000000320000001E00000000000000000000000000000000010000000000000000000000000000000000000010000000010000000000006E756C6C0000000200000006626F756E64734F626A6300000001000000000000526374310000000400000000546F70206C6F6E6700000000000000004C6566746C6F6E67000000000000000042746F6D6C6F6E670000001E00000000526768746C6F6E670000003200000006736C69636573566C4C73000000014F626A6300000001000000000005736C6963650000001200000007736C69636549446C6F6E67000000000000000767726F757049446C6F6E6700000000000000066F726967696E656E756D0000000C45536C6963654F726967696E0000000D6175746F47656E6572617465640000000054797065656E756D0000000A45536C6963655479706500000000496D672000000006626F756E64734F626A6300000001000000000000526374310000000400000000546F70206C6F6E6700000000000000004C6566746C6F6E67000000000000000042746F6D6C6F6E670000001E00000000526768746C6F6E67000000320000000375726C54455854000000010000000000006E756C6C54455854000000010000000000004D7367655445585400000001000000000006616C74546167544558540000000100000000000E63656C6C54657874497348544D4C626F6F6C010000000863656C6C546578745445585400000001000000000009686F727A416C69676E656E756D0000000F45536C696365486F727A416C69676E0000000764656661756C740000000976657274416C69676E656E756D0000000F45536C69636556657274416C69676E0000000764656661756C740000000B6267436F6C6F7254797065656E756D0000001145536C6963654247436F6C6F7254797065000000004E6F6E6500000009746F704F75747365746C6F6E67000000000000000A6C6566744F75747365746C6F6E67000000000000000C626F74746F6D4F75747365746C6F6E67000000000000000B72696768744F75747365746C6F6E6700000000003842494D042800000000000C000000023FF00000000000003842494D0414000000000004000000013842494D040C00000000045600000001000000320000001E00000098000011D00000043A00180001FFD8FFED000C41646F62655F434D0001FFEE000E41646F626500648000000001FFDB0084000C08080809080C09090C110B0A0B11150F0C0C0F1518131315131318110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C010D0B0B0D0E0D100E0E10140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4013F0000010501010101010100000000000000030001020405060708090A0B0100010501010101010100000000000000010002030405060708090A0B1000010401030204020507060805030C33010002110304211231054151611322718132061491A1B14223241552C16233347282D14307259253F0E1F163733516A2B283264493546445C2A3743617D255E265F2B384C3D375E3F3462794A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F637475767778797A7B7C7D7E7F711000202010204040304050607070605350100021103213112044151617122130532819114A1B14223C152D1F0332462E1728292435315637334F1250616A2B283072635C2D2449354A317644555367465E2F2B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F62737475767778797A7B7C7FFDA000C03010002110311003F002E6EFB766335E6A392E76FB5A61CDADA3D5BB63BF31EFF00E6F7A1D2D7E1DECA0BDD663DF229DE4B9D5BDA377A3EA1F73ABB19FCDEE4DD5AA75B87686C87BDA6A11F4A2D2DA9FE9FF2FF007517240FB200CDC5F8FB1EC2F10E9A88F73BF94E6A484E78E540CA1E5DB6B4B6BC60D7DF7122ADF218206E75B6EDF77A75FF0027E9AAEFC8CCFB1545ADAFEDD635C2093E887D61CEBAC739BEEF45ADAD24A4CC7BABC5B9ED30F0C3B3E27DAD3F8A235EFACB5CC7B9AF681EF692D331FBCD553A86431B895120CDEEA8068124C96BDD1FF5295B6599153AB6BCE1DC2C6D761DCD258090EFD13FF9B7BECAFF009A494EA7EDAEAFFF00736EE23E976FBBFE9A4B03ED991FBDFE0278FCEFF49FD749253FFFD06CACEC6664D343DF1B2CDF6BB697359B1BBDADB1CD076BBDF5A9E466D1654EAF19E322C7830CA8EF207E7D8E8FA3B1A9BA6328664649A6CF51EF7DA5AD820B07A9FACB1C5DF4DFF69FFC0BD1FF0006AD8D907D3DB13EED91CFF2B6A48685F5F50B9CCB58C1435A1CDF4B7C5CE6BE377E9DA1F4E3BBDBFCB7A85D87937B6A11562D7468DC71B9E1CD236BABBAC6FA7FA3FE457FCE7F855A067550713E04FC3FDA524B9F98F6D36639BACDEFB2E0FB2C7088654D7DA763193B6AABDBEC6215D40CCADD73AB2F63DE5CDADDED7166CF42BB9BBBE85EDFE7E8DEAC5EF68BF1CECB0D81CFF480D9A9D877F367EE283F27201F6E25AEFEDD63FEFE929A1E85DFB96FF41F4BE89FE77F7BFF000C24AEFDAB33FEE1BFFEDCAFFF0024924A7FFFD93842494D042100000000005500000001010000000F00410064006F00620065002000500068006F0074006F00730068006F00700000001300410064006F00620065002000500068006F0074006F00730068006F0070002000430053003600000001003842494D0406000000000007FFFC000000010100FFE10DEB687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F003C3F787061636B657420626567696E3D22EFBBBF222069643D2257354D304D7043656869487A7265537A4E54637A6B633964223F3E203C783A786D706D65746120786D6C6E733A783D2261646F62653A6E733A6D6574612F2220783A786D70746B3D2241646F626520584D5020436F726520352E332D633031312036362E3134353636312C20323031322F30322F30362D31343A35363A32372020202020202020223E203C7264663A52444620786D6C6E733A7264663D22687474703A2F2F7777772E77332E6F72672F313939392F30322F32322D7264662D73796E7461782D6E7323223E203C7264663A4465736372697074696F6E207264663A61626F75743D222220786D6C6E733A786D703D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F2220786D6C6E733A70686F746F73686F703D22687474703A2F2F6E732E61646F62652E636F6D2F70686F746F73686F702F312E302F2220786D6C6E733A786D704D4D3D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F6D6D2F2220786D6C6E733A73744576743D22687474703A2F2F6E732E61646F62652E636F6D2F7861702F312E302F73547970652F5265736F757263654576656E74232220786D6C6E733A64633D22687474703A2F2F7075726C2E6F72672F64632F656C656D656E74732F312E312F2220786D703A43726561746F72546F6F6C3D224939303832585855424D4B332220786D703A4D6F64696679446174653D22323031342D30342D30325431363A33343A31332B30353A33302220786D703A437265617465446174653D22323031342D30312D30315432333A30313A33342220786D703A4D65746164617461446174653D22323031342D30342D30325431363A33343A31332B30353A3330222070686F746F73686F703A44617465437265617465643D22323031342D30312D30315432333A30313A3334222070686F746F73686F703A436F6C6F724D6F64653D2233222070686F746F73686F703A49434350726F66696C653D22735247422049454336313936362D322E312220786D704D4D3A446F63756D656E7449443D2231393435374633433137394231313931364136304630423539413430394243312220786D704D4D3A496E7374616E636549443D22786D702E6969643A35463032313835333536424145333131393031314535463444333739333537422220786D704D4D3A4F726967696E616C446F63756D656E7449443D223139343537463343313739423131393136413630463042353941343039424331222064633A666F726D61743D22696D6167652F6A706567223E203C786D704D4D3A486973746F72793E203C7264663A5365713E203C7264663A6C692073744576743A616374696F6E3D227361766564222073744576743A696E7374616E636549443D22786D702E6969643A3232323735324346353442414533313139303131453546344433373933353742222073744576743A7768656E3D22323031342D30342D30325431363A32343A30312B30353A3330222073744576743A736F6674776172654167656E743D2241646F62652050686F746F73686F7020435336202857696E646F777329222073744576743A6368616E6765643D222F222F3E203C7264663A6C692073744576743A616374696F6E3D227361766564222073744576743A696E7374616E636549443D22786D702E6969643A3546303231383533353642414533313139303131453546344433373933353742222073744576743A7768656E3D22323031342D30342D30325431363A33343A31332B30353A3330222073744576743A736F6674776172654167656E743D2241646F62652050686F746F73686F7020435336202857696E646F777329222073744576743A6368616E6765643D222F222F3E203C2F7264663A5365713E203C2F786D704D4D3A486973746F72793E203C2F7264663A4465736372697074696F6E3E203C2F7264663A5244463E203C2F783A786D706D6574613E2020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020202020203C3F787061636B657420656E643D2277223F3EFFE20C584943435F50524F46494C4500010100000C484C696E6F021000006D6E74725247422058595A2007CE00020009000600310000616373704D5346540000000049454320735247420000000000000000000000000000F6D6000100000000D32D4850202000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001163707274000001500000003364657363000001840000006C77747074000001F000000014626B707400000204000000147258595A00000218000000146758595A0000022C000000146258595A0000024000000014646D6E640000025400000070646D6464000002C400000088767565640000034C0000008676696577000003D4000000246C756D69000003F8000000146D6561730000040C0000002474656368000004300000000C725452430000043C0000080C675452430000043C0000080C625452430000043C0000080C7465787400000000436F70797269676874202863292031393938204865776C6574742D5061636B61726420436F6D70616E790000646573630000000000000012735247422049454336313936362D322E31000000000000000000000012735247422049454336313936362D322E31000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000058595A20000000000000F35100010000000116CC58595A200000000000000000000000000000000058595A200000000000006FA2000038F50000039058595A2000000000000062990000B785000018DA58595A2000000000000024A000000F840000B6CF64657363000000000000001649454320687474703A2F2F7777772E6965632E636800000000000000000000001649454320687474703A2F2F7777772E6965632E63680000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000064657363000000000000002E4945432036313936362D322E312044656661756C742052474220636F6C6F7572207370616365202D207352474200000000000000000000002E4945432036313936362D322E312044656661756C742052474220636F6C6F7572207370616365202D20735247420000000000000000000000000000000000000000000064657363000000000000002C5265666572656E63652056696577696E6720436F6E646974696F6E20696E2049454336313936362D322E3100000000000000000000002C5265666572656E63652056696577696E6720436F6E646974696F6E20696E2049454336313936362D322E31000000000000000000000000000000000000000000000000000076696577000000000013A4FE00145F2E0010CF140003EDCC0004130B00035C9E0000000158595A2000000000004C09560050000000571FE76D6561730000000000000001000000000000000000000000000000000000028F0000000273696720000000004352542063757276000000000000040000000005000A000F00140019001E00230028002D00320037003B00400045004A004F00540059005E00630068006D00720077007C00810086008B00900095009A009F00A400A900AE00B200B700BC00C100C600CB00D000D500DB00E000E500EB00F000F600FB01010107010D01130119011F0125012B01320138013E0145014C0152015901600167016E0175017C0183018B0192019A01A101A901B101B901C101C901D101D901E101E901F201FA0203020C0214021D0226022F02380241024B0254025D02670271027A0284028E029802A202AC02B602C102CB02D502E002EB02F50300030B03160321032D03380343034F035A03660372037E038A039603A203AE03BA03C703D303E003EC03F9040604130420042D043B0448045504630471047E048C049A04A804B604C404D304E104F004FE050D051C052B053A05490558056705770586059605A605B505C505D505E505F6060606160627063706480659066A067B068C069D06AF06C006D106E306F507070719072B073D074F076107740786079907AC07BF07D207E507F8080B081F08320846085A086E0882089608AA08BE08D208E708FB09100925093A094F09640979098F09A409BA09CF09E509FB0A110A270A3D0A540A6A0A810A980AAE0AC50ADC0AF30B0B0B220B390B510B690B800B980BB00BC80BE10BF90C120C2A0C430C5C0C750C8E0CA70CC00CD90CF30D0D0D260D400D5A0D740D8E0DA90DC30DDE0DF80E130E2E0E490E640E7F0E9B0EB60ED20EEE0F090F250F410F5E0F7A0F960FB30FCF0FEC1009102610431061107E109B10B910D710F511131131114F116D118C11AA11C911E81207122612451264128412A312C312E31303132313431363138313A413C513E5140614271449146A148B14AD14CE14F01512153415561578159B15BD15E0160316261649166C168F16B216D616FA171D17411765178917AE17D217F7181B18401865188A18AF18D518FA19201945196B199119B719DD1A041A2A1A511A771A9E1AC51AEC1B141B3B1B631B8A1BB21BDA1C021C2A1C521C7B1CA31CCC1CF51D1E1D471D701D991DC31DEC1E161E401E6A1E941EBE1EE91F131F3E1F691F941FBF1FEA20152041206C209820C420F0211C2148217521A121CE21FB22272255228222AF22DD230A23382366239423C223F0241F244D247C24AB24DA250925382568259725C725F726272657268726B726E827182749277A27AB27DC280D283F287128A228D429062938296B299D29D02A022A352A682A9B2ACF2B022B362B692B9D2BD12C052C392C6E2CA22CD72D0C2D412D762DAB2DE12E162E4C2E822EB72EEE2F242F5A2F912FC72FFE3035306C30A430DB3112314A318231BA31F2322A3263329B32D4330D3346337F33B833F1342B3465349E34D83513354D358735C235FD3637367236AE36E937243760379C37D738143850388C38C839053942397F39BC39F93A363A743AB23AEF3B2D3B6B3BAA3BE83C273C653CA43CE33D223D613DA13DE03E203E603EA03EE03F213F613FA23FE24023406440A640E74129416A41AC41EE4230427242B542F7433A437D43C044034447448A44CE45124555459A45DE4622466746AB46F04735477B47C04805484B489148D7491D496349A949F04A374A7D4AC44B0C4B534B9A4BE24C2A4C724CBA4D024D4A4D934DDC4E254E6E4EB74F004F494F934FDD5027507150BB51065150519B51E65231527C52C75313535F53AA53F65442548F54DB5528557555C2560F565C56A956F75744579257E0582F587D58CB591A596959B85A075A565AA65AF55B455B955BE55C355C865CD65D275D785DC95E1A5E6C5EBD5F0F5F615FB36005605760AA60FC614F61A261F56249629C62F06343639763EB6440649464E9653D659265E7663D669266E8673D679367E9683F689668EC6943699A69F16A486A9F6AF76B4F6BA76BFF6C576CAF6D086D606DB96E126E6B6EC46F1E6F786FD1702B708670E0713A719571F0724B72A67301735D73B87414747074CC7528758575E1763E769B76F8775677B37811786E78CC792A798979E77A467AA57B047B637BC27C217C817CE17D417DA17E017E627EC27F237F847FE5804780A8810A816B81CD8230829282F4835783BA841D848084E3854785AB860E867286D7873B879F8804886988CE8933899989FE8A648ACA8B308B968BFC8C638CCA8D318D988DFF8E668ECE8F368F9E9006906E90D6913F91A89211927A92E3934D93B69420948A94F4955F95C99634969F970A977597E0984C98B89924999099FC9A689AD59B429BAF9C1C9C899CF79D649DD29E409EAE9F1D9F8B9FFAA069A0D8A147A1B6A226A296A306A376A3E6A456A4C7A538A5A9A61AA68BA6FDA76EA7E0A852A8C4A937A9A9AA1CAA8FAB02AB75ABE9AC5CACD0AD44ADB8AE2DAEA1AF16AF8BB000B075B0EAB160B1D6B24BB2C2B338B3AEB425B49CB513B58AB601B679B6F0B768B7E0B859B8D1B94AB9C2BA3BBAB5BB2EBBA7BC21BC9BBD15BD8FBE0ABE84BEFFBF7ABFF5C070C0ECC167C1E3C25FC2DBC358C3D4C451C4CEC54BC5C8C646C6C3C741C7BFC83DC8BCC93AC9B9CA38CAB7CB36CBB6CC35CCB5CD35CDB5CE36CEB6CF37CFB8D039D0BAD13CD1BED23FD2C1D344D3C6D449D4CBD54ED5D1D655D6D8D75CD7E0D864D8E8D96CD9F1DA76DAFBDB80DC05DC8ADD10DD96DE1CDEA2DF29DFAFE036E0BDE144E1CCE253E2DBE363E3EBE473E4FCE584E60DE696E71FE7A9E832E8BCE946E9D0EA5BEAE5EB70EBFBEC86ED11ED9CEE28EEB4EF40EFCCF058F0E5F172F1FFF28CF319F3A7F434F4C2F550F5DEF66DF6FBF78AF819F8A8F938F9C7FA57FAE7FB77FC07FC98FD29FDBAFE4BFEDCFF6DFFFFFFEE000E41646F626500648000000001FFDB00840020212133243351303051422F2F2F42271C1C1C1C2722171717171722110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0122333334263422181822140E0E0E14140E0E0E0E14110C0C0C0C0C11110C0C0C0C0C0C110C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0C0CFFC0001108001E003203012200021101031101FFDD00040004FFC4011B0000030101010101010101010000000000010002030405060708090A0B0101010101010101010101010100000000000102030405060708090A0B1000020201030203040706030306020135010002110321123104415122136171328191B142A105D1C114F05223723362E182F1433492A2B215D2532473C263068393E2F2A3445464253545162674365565B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F55666768696A6B6C6D6E6F6110002020005010606010301030503062F0001110221033141125161718191221332F0A1B104C1D1E1F14252236272149233824324A2B23453446373C2D28393A354E2F205152506162635644555367465B384C3D375E3F34694A485B495C4D4E4F4A5B5C5D5E5F556667686FFDA000C03010002110311003F00A9EBE5E377DAFF0007F7737FF03400606B98CBE0FF0004FF00EE7FF1793FDDA328B89FF91FF9B3F94D4BE1D398EDF8BFF29A05211324691E4FC3FC3FF8DCBFF94D83296D1C6F3FF9AB743FBDFF0098500CCD449F6340D70E7925E51FE2D8A4990AF80DEDEDE5FF00C57FBBFE67FBA40DFD6C9FC52579379FF9BFBE4540FFD065300807C7CDFE1D914CA608A8F98FF83CDFF991188004D1BD65FE8F3FFE13FF00CD3FFA0BD174F7206521296A3CBFE1DDFCDF37FE57FECF4E83032AE2223F63E3FF00CDD93FF81FFE657664A063334459E65E6FE9C62797FF003562418EF175DFE1FF0006DF431E6FFC7FFBFC0DC8EA3437E6DBF0FF000FFE310647F84FD30FFB6818ED3EDFEDECFF00CC9FFD70AEBBA5FC27FE541503FFD9, 1, NULL, NULL, CAST(0x0000A92700CAD357 AS DateTime), 1, CAST(0x0000A92700CAD357 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Gbl_Attachment] OFF
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
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankAccountType] ON 

INSERT [dbo].[Gbl_Master_BankAccountType] ([AccountTypeId], [AccountType], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (1, N'Saving', N'Saving use only', 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A823011DBADE AS DateTime))
INSERT [dbo].[Gbl_Master_BankAccountType] ([AccountTypeId], [AccountType], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModifiedBy], [ModificationDate]) VALUES (2, N'Current', N'Commercial use only', 0, 0, CAST(0x0000A82200000000 AS DateTime), 2, 2, CAST(0x0000A823011D5966 AS DateTime))
SET IDENTITY_INSERT [dbo].[Gbl_Master_BankAccountType] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Brand] ON 

INSERT [dbo].[Gbl_Master_Brand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 1, N'Jockey', N'Jockey', 0, 0, 1, 1, CAST(0x0000A90B00FADEDA AS DateTime), CAST(0x0000A90B00FB3FBF AS DateTime))
INSERT [dbo].[Gbl_Master_Brand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 1, N'Amul', N'Amul Brief', 0, 0, 1, 1, CAST(0x0000A90B00FB0B66 AS DateTime), CAST(0x0000A91200F62520 AS DateTime))
INSERT [dbo].[Gbl_Master_Brand] ([BrandId], [ShopId], [BrandName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (3, 1, N'Remand', NULL, 0, 0, 1, 1, CAST(0x0000A90B00FB1C59 AS DateTime), CAST(0x0000A90B00FB1C59 AS DateTime))
SET IDENTITY_INSERT [dbo].[Gbl_Master_Brand] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Category] ON 

INSERT [dbo].[Gbl_Master_Category] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 1, N'Top Waer', NULL, 0, 0, 1, 1, CAST(0x0000A90B00FB7B31 AS DateTime), CAST(0x0000A90B00FB7B31 AS DateTime))
INSERT [dbo].[Gbl_Master_Category] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 1, N'Inner Wear', NULL, 0, 0, 1, 1, CAST(0x0000A90B00FB853B AS DateTime), CAST(0x0000A90B00FB853B AS DateTime))
INSERT [dbo].[Gbl_Master_Category] ([CatId], [ShopId], [CatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (3, 1, N'Kid Wear', NULL, 0, 0, 1, 1, CAST(0x0000A90B00FB9B7F AS DateTime), CAST(0x0000A90B00FB9B7F AS DateTime))
SET IDENTITY_INSERT [dbo].[Gbl_Master_Category] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Customer] ON 

INSERT [dbo].[Gbl_Master_Customer] ([CustomerId], [CustomerTypeId], [FirstName], [MiddleName], [LastName], [Mobile], [Email], [Address], [District], [State], [PINCode], [ShopId], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (2, 1, N'Satish', N'kumar', N'Sonkar', N'9990614499', N'btech.csit@gmail.com', N'Jais Amethi', N'Amethi', N'U.P.', N'229305', 1, CAST(0x0000A90C012AB6A1 AS DateTime), 1, CAST(0x0000A90C01306DED AS DateTime), 1, 0, 0)
INSERT [dbo].[Gbl_Master_Customer] ([CustomerId], [CustomerTypeId], [FirstName], [MiddleName], [LastName], [Mobile], [Email], [Address], [District], [State], [PINCode], [ShopId], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (4, 2, N'Poonam', N' ', N'Sonkar', N'9972217679', N'sonkarpoonam41@gmail.com', N'Jais Amethi', N'Amethi', N'U.P.', N'229305', 1, CAST(0x0000A90D0102EA09 AS DateTime), 1, CAST(0x0000A90D0102EA09 AS DateTime), 1, 0, 0)
INSERT [dbo].[Gbl_Master_Customer] ([CustomerId], [CustomerTypeId], [FirstName], [MiddleName], [LastName], [Mobile], [Email], [Address], [District], [State], [PINCode], [ShopId], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (5, 1, N'Manish', N'kumar', N'Sonkar', N'9452540278', N'btech.csit@gmail.com', N'Jais Amethi', N'Amethi', N'U.P.', N'229305', 1, CAST(0x0000A90D0104EBA1 AS DateTime), 1, CAST(0x0000A90D0104EBA1 AS DateTime), 1, 0, 0)
INSERT [dbo].[Gbl_Master_Customer] ([CustomerId], [CustomerTypeId], [FirstName], [MiddleName], [LastName], [Mobile], [Email], [Address], [District], [State], [PINCode], [ShopId], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (6, 3, N'Rekha', NULL, N'sonkar', N'8115885648', N'btech.csit@gmail.com', N'Jais Amethi', N'Amethi', N'U.P.', N'229305', 1, CAST(0x0000A90D0105841F AS DateTime), 1, CAST(0x0000A90D0105841F AS DateTime), 1, 0, 0)
INSERT [dbo].[Gbl_Master_Customer] ([CustomerId], [CustomerTypeId], [FirstName], [MiddleName], [LastName], [Mobile], [Email], [Address], [District], [State], [PINCode], [ShopId], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (7, 3, N'Rishi', NULL, N'Sonkar', N'8985452458', N'btech.csit@gmail.com', N'Jais Amethi', N'Amethi', N'U.P.', N'229305', 1, CAST(0x0000A90D01061F3A AS DateTime), 1, CAST(0x0000A90D01061F3A AS DateTime), 1, 0, 0)
INSERT [dbo].[Gbl_Master_Customer] ([CustomerId], [CustomerTypeId], [FirstName], [MiddleName], [LastName], [Mobile], [Email], [Address], [District], [State], [PINCode], [ShopId], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (8, 1, N'Ram', N'Naresh', N'Sonkar', N'9415178952', N'btech.csit@gmail.com', N'Jais Amethi', N'Amethi', N'U.P.', N'229305', 1, CAST(0x0000A90D01064FDA AS DateTime), 1, CAST(0x0000A90D01064FDA AS DateTime), 1, 0, 0)
INSERT [dbo].[Gbl_Master_Customer] ([CustomerId], [CustomerTypeId], [FirstName], [MiddleName], [LastName], [Mobile], [Email], [Address], [District], [State], [PINCode], [ShopId], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (9, 1, N'Sunny', NULL, N'Sonkar', N'9854524582', N'btech.csit@gmail.com', N'Jais Amethi', N'Amethi', N'U.P.', N'229305', 1, CAST(0x0000A90D010699FC AS DateTime), 1, CAST(0x0000A90D010699FC AS DateTime), 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Gbl_Master_Customer] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_CustomerType] ON 

INSERT [dbo].[Gbl_Master_CustomerType] ([CustomerTypeId], [CustomerType], [Description], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsDeleted], [IsSync], [ShopId]) VALUES (1, N'Permanent', N'Permanent Customer', CAST(0x0000A90C010D210D AS DateTime), 1, CAST(0x0000A90C01107511 AS DateTime), 1, 0, 0, 1)
INSERT [dbo].[Gbl_Master_CustomerType] ([CustomerTypeId], [CustomerType], [Description], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsDeleted], [IsSync], [ShopId]) VALUES (2, N'Adhoc', NULL, CAST(0x0000A90C01108476 AS DateTime), 1, CAST(0x0000A90C01108476 AS DateTime), 1, 0, 0, 1)
INSERT [dbo].[Gbl_Master_CustomerType] ([CustomerTypeId], [CustomerType], [Description], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy], [IsDeleted], [IsSync], [ShopId]) VALUES (3, N'Favorite', N'Favorite Customer', CAST(0x0000A90D0105454C AS DateTime), 1, CAST(0x0000A90D0105454C AS DateTime), 1, 0, 0, 1)
SET IDENTITY_INSERT [dbo].[Gbl_Master_CustomerType] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Notification] ON 

INSERT [dbo].[Gbl_Master_Notification] ([NotificationId], [NotificationTypeId], [Message], [UserId], [ShopId], [IsPushed], [IsRead], [IsForAll], [PushedDate], [MessageExpireDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [IsDeleted], [IsSync]) VALUES (1, 3, N'okkkkk', 1, 1, 1, 0, 0, CAST(0x0000A92D010E7D87 AS DateTime), CAST(0x0000A92E017B0740 AS DateTime), 1, CAST(0x0000A92D010E7D87 AS DateTime), 1, CAST(0x0000A92D01351834 AS DateTime), 1, 0)
INSERT [dbo].[Gbl_Master_Notification] ([NotificationId], [NotificationTypeId], [Message], [UserId], [ShopId], [IsPushed], [IsRead], [IsForAll], [PushedDate], [MessageExpireDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [IsDeleted], [IsSync]) VALUES (2, 3, N'Sam', 1, 1, 1, 0, 0, CAST(0x0000A92D010E7D87 AS DateTime), CAST(0x0000A92E01890930 AS DateTime), 1, CAST(0x0000A92D011DCADB AS DateTime), 1, CAST(0x0000A92D013511DF AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Gbl_Master_Notification] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_NotificationType] ON 

INSERT [dbo].[Gbl_Master_NotificationType] ([NotificationTypeId], [NotificationType], [Description], [ShopId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [IsDeleted], [IsSync]) VALUES (3, N'Push Notification', N'Firebase push notification', 1, 1, CAST(0x0000A9290139315C AS DateTime), 1, CAST(0x0000A92A00CD3A4F AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_NotificationType] ([NotificationTypeId], [NotificationType], [Description], [ShopId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [IsDeleted], [IsSync]) VALUES (4, N'Email Notification', N'Email Notification', 1, 1, CAST(0x0000A92A00CD5EFE AS DateTime), 1, CAST(0x0000A92A00D0D92F AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_NotificationType] ([NotificationTypeId], [NotificationType], [Description], [ShopId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModificationDate], [IsDeleted], [IsSync]) VALUES (5, N'aaa', N'aaaaaa', 1, 1, CAST(0x0000A92A00D0BD92 AS DateTime), 1, CAST(0x0000A92A00D0C580 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Gbl_Master_NotificationType] OFF
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
SET IDENTITY_INSERT [dbo].[Gbl_Master_Product] ON 

INSERT [dbo].[Gbl_Master_Product] ([ProductId], [SubCatId], [ShopId], [ProductName], [Description], [MinQuantity], [ProductCode], [UnitId], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 1, 1, N'White T-Shirts', NULL, CAST(10.0000 AS Decimal(18, 4)), N'0125', 1, 0, 0, 1, 1, CAST(0x0000A90B01032C4C AS DateTime), CAST(0x0000A90B01032C4C AS DateTime))
INSERT [dbo].[Gbl_Master_Product] ([ProductId], [SubCatId], [ShopId], [ProductName], [Description], [MinQuantity], [ProductCode], [UnitId], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 2, 1, N'Formal Shirts', NULL, CAST(20.0000 AS Decimal(18, 4)), N'1255', 1, 0, 0, 1, 1, CAST(0x0000A90B011467E8 AS DateTime), CAST(0x0000A90B011467E8 AS DateTime))
SET IDENTITY_INSERT [dbo].[Gbl_Master_Product] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Shop] ON 

INSERT [dbo].[Gbl_Master_Shop] ([ShopId], [Name], [Address], [Mobile], [Email], [Distict], [State], [Owner], [LogoAttachmentId], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (1, N'Sonkar Garments', N'Jais, Raebareli', N'9990614499', N'sonkarpoonam41@gmail.com', N'Amethi', N'U.P.', 1, NULL, 0, 0, CAST(0x0000A90B00000000 AS DateTime), 1, CAST(0x0000A91100E8F890 AS DateTime), 1)
INSERT [dbo].[Gbl_Master_Shop] ([ShopId], [Name], [Address], [Mobile], [Email], [Distict], [State], [Owner], [LogoAttachmentId], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (3, N'ABC Enterprises', N'Jais Amethi', N'9990614499', N'btech.csit@gmail.com', N'Amethi', N'U.P.', 1, NULL, 0, 0, CAST(0x0000A91100E8EB09 AS DateTime), 1, CAST(0x0000A91100E8EB09 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Gbl_Master_Shop] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_SubCategory] ON 

INSERT [dbo].[Gbl_Master_SubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 1, 1, N'T-Shirt', NULL, 0, 0, 1, 1, CAST(0x0000A90B01005CEC AS DateTime), CAST(0x0000A90B01005CEC AS DateTime))
INSERT [dbo].[Gbl_Master_SubCategory] ([SubCatId], [CatId], [ShopId], [SubCatName], [Description], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 1, 1, N'Shirts', NULL, 0, 0, 1, 1, CAST(0x0000A90B01006C75 AS DateTime), CAST(0x0000A90B01006C75 AS DateTime))
SET IDENTITY_INSERT [dbo].[Gbl_Master_SubCategory] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Unit] ON 

INSERT [dbo].[Gbl_Master_Unit] ([UnitId], [ShopId], [UnitName], [Description], [IsSync], [IsActive], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 1, N'Pcs', N'1 Unit', 0, 0, 0, 1, 1, CAST(0x0000A90B01015DA8 AS DateTime), CAST(0x0000A90B01015DA8 AS DateTime))
SET IDENTITY_INSERT [dbo].[Gbl_Master_Unit] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_User] ON 

INSERT [dbo].[Gbl_Master_User] ([UserId], [Username], [Password], [Name], [Mobile], [Photo], [UserType], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted], [IsActive], [IsBlocked], [ShopId]) VALUES (1, N'btech.csit@gmail.com', N'870DA6596C3F6CA4CAB8108BDDE98953', N'Satish Sonkar', N'9990614499', NULL, 1, CAST(0x0000A90A00000000 AS DateTime), CAST(0x0000A90A0112BB68 AS DateTime), 1, 1, 0, 0, 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Gbl_Master_User] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_UserType] ON 

INSERT [dbo].[Gbl_Master_UserType] ([Id], [Type], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (1, N'SuperAdmin', NULL, 1, 1, CAST(0x0000A90A00000000 AS DateTime), CAST(0x0000A90A00000000 AS DateTime), 0, 0)
INSERT [dbo].[Gbl_Master_UserType] ([Id], [Type], [Description], [CreatedBy], [ModifiedBy], [CreationDate], [ModificationDate], [IsSync], [IsDeleted]) VALUES (2, N'Admin', NULL, 1, 1, CAST(0x0000A90A00000000 AS DateTime), CAST(0x0000A90A00000000 AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[Gbl_Master_UserType] OFF
SET IDENTITY_INSERT [dbo].[Gbl_Master_Vendor] ON 

INSERT [dbo].[Gbl_Master_Vendor] ([VendorId], [ShopId], [VendorName], [VendorMobile], [VendorAddress], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (1, 1, N'Singh Garments', N'9452545245', N'Tiloi', NULL, 0, 0, CAST(0x0000A90B0102E402 AS DateTime), 1, CAST(0x0000A90B0102E402 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Gbl_Master_Vendor] OFF
SET IDENTITY_INSERT [dbo].[Login] ON 

INSERT [dbo].[Login] ([Id], [UserId], [LoginDate], [LoginAttempt], [IsDeleted], [IsSync], [IsReset], [IsLoginBlocked], [GUID], [OTPid], [ReserExpireTime], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy]) VALUES (1, 1, CAST(0x0000A92D0133B058 AS DateTime), 0, 0, 0, 0, 0, NULL, N'SM2ca67040c46f4c82b2ff9a593482c9c7', CAST(0x0000A90A01023317 AS DateTime), CAST(0x0000A90A01124C25 AS DateTime), CAST(0x0000A92D0133B058 AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Login] OFF
SET IDENTITY_INSERT [dbo].[Stk_Dtl_Entry] ON 

INSERT [dbo].[Stk_Dtl_Entry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (1, 1, 1, 1, 2, CAST(125.0000 AS Decimal(18, 4)), CAST(125.0000 AS Decimal(18, 4)), CAST(20.0000 AS Decimal(18, 4)), N'#ffffff', NULL, 0, 0, CAST(0x0000A90B0103CF0A AS DateTime), 1, CAST(0x0000A90B0103CF0A AS DateTime), 1)
INSERT [dbo].[Stk_Dtl_Entry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (2, 2, 1, 1, 2, CAST(350.0000 AS Decimal(18, 4)), CAST(256.0000 AS Decimal(18, 4)), CAST(10.0000 AS Decimal(18, 4)), N'#ffffff', NULL, 0, 0, CAST(0x0000A90C00C06EF2 AS DateTime), 1, CAST(0x0000A90C00C06EF2 AS DateTime), 1)
INSERT [dbo].[Stk_Dtl_Entry] ([StockTrId], [StockMstId], [ProductId], [ShopId], [BrandId], [SellPrice], [PurchasePrice], [Qty], [Color], [Description], [IsSync], [IsDeleted], [CreatedDate], [CreatedBy], [ModificationDate], [ModifiedBy]) VALUES (3, 3, 2, 1, 2, CAST(350.0000 AS Decimal(18, 4)), CAST(262.0000 AS Decimal(18, 4)), CAST(11.0000 AS Decimal(18, 4)), N'#000000', NULL, 0, 0, CAST(0x0000A90C00C0F14F AS DateTime), 1, CAST(0x0000A90C00C0F14F AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Stk_Dtl_Entry] OFF
SET IDENTITY_INSERT [dbo].[Stk_Tr_Entry] ON 

INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (1, 1, 1, 1, 0, N'125', N'100', 0, CAST(5776.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(5776.0000 AS Decimal(18, 4)), CAST(0x0000A90B00000000 AS DateTime), 0, 0, 1, 1, CAST(0x0000A90B0103CF04 AS DateTime), CAST(0x0000A90B0103CF04 AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (2, 1, 1, 1, 0, N'125', N'100', 0, CAST(2560.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(2500.0000 AS Decimal(18, 4)), CAST(60.0000 AS Decimal(18, 4)), CAST(0x0000A90C00000000 AS DateTime), 0, 0, 1, 1, CAST(0x0000A90C00C06ECC AS DateTime), CAST(0x0000A90C00C06ECC AS DateTime))
INSERT [dbo].[Stk_Tr_Entry] ([Id], [VendorId], [ShopId], [PayModeId], [DebitAccount], [VendorReceiptNo], [ShopReceiptEntryNo], [ChequePageId], [TotalAmt], [AdditionalAmt], [PaidAmt], [RemainingAmt], [ReceiptDate], [IsSync], [IsDeleted], [CreatedBy], [ModifiedBy], [CreatedDate], [ModificationDate]) VALUES (3, 1, 1, 1, 0, N'125', N'100', 0, CAST(2882.0000 AS Decimal(18, 4)), CAST(118.0000 AS Decimal(18, 4)), CAST(3000.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0x0000A90C00000000 AS DateTime), 0, 0, 1, 1, CAST(0x0000A90C00C0F14E AS DateTime), CAST(0x0000A90C00C0F14E AS DateTime))
SET IDENTITY_INSERT [dbo].[Stk_Tr_Entry] OFF
SET IDENTITY_INSERT [dbo].[User_ShopMapper] ON 

INSERT [dbo].[User_ShopMapper] ([Id], [UserId], [ShopId], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (1, 1, 1, CAST(0x0000A90B00000000 AS DateTime), CAST(0x0000A90B00000000 AS DateTime), 1, 1, 0, 0)
INSERT [dbo].[User_ShopMapper] ([Id], [UserId], [ShopId], [CreationDate], [ModificationDate], [CreationBy], [ModifiedBy], [IsSync], [IsDeleted]) VALUES (2, 1, 3, CAST(0x0000A91100E92766 AS DateTime), CAST(0x0000A91100E92766 AS DateTime), 1, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[User_ShopMapper] OFF
ALTER TABLE [dbo].[Gbl_AppDowntime]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_AppDowntime_User_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Gbl_Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Gbl_AppDowntime] CHECK CONSTRAINT [FK_Gbl_AppDowntime_User_CreatedBy]
GO
ALTER TABLE [dbo].[Gbl_AppDowntime]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_AppDowntime_User_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Gbl_Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Gbl_AppDowntime] CHECK CONSTRAINT [FK_Gbl_AppDowntime_User_ModifiedBy]
GO
ALTER TABLE [dbo].[Gbl_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Attachment_Gbl_Master_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Attachment] CHECK CONSTRAINT [FK_Gbl_Attachment_Gbl_Master_Shop]
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
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
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
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_BankChequeDetails] CHECK CONSTRAINT [FK_Gbl_Master_BankChequeDetails_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_Brand]  WITH CHECK ADD  CONSTRAINT [FK_MasterBrand_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_Brand] CHECK CONSTRAINT [FK_MasterBrand_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_Category]  WITH CHECK ADD  CONSTRAINT [FK_MasterCategory_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_Category] CHECK CONSTRAINT [FK_MasterCategory_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_City]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_City_Gbl_Master_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[Gbl_Master_State] ([StateId])
GO
ALTER TABLE [dbo].[Gbl_Master_City] CHECK CONSTRAINT [FK_Gbl_Master_City_Gbl_Master_State]
GO
ALTER TABLE [dbo].[Gbl_Master_Customer]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Customer_Gbl_Master_CustomerType] FOREIGN KEY([CustomerTypeId])
REFERENCES [dbo].[Gbl_Master_CustomerType] ([CustomerTypeId])
GO
ALTER TABLE [dbo].[Gbl_Master_Customer] CHECK CONSTRAINT [FK_Gbl_Master_Customer_Gbl_Master_CustomerType]
GO
ALTER TABLE [dbo].[Gbl_Master_Customer]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Customer_Gbl_Master_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_Customer] CHECK CONSTRAINT [FK_Gbl_Master_Customer_Gbl_Master_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_CustomerType]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_CustomerType_Gbl_Master_Shop1] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_CustomerType] CHECK CONSTRAINT [FK_Gbl_Master_CustomerType_Gbl_Master_Shop1]
GO
ALTER TABLE [dbo].[Gbl_Master_DocProof]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_DocProof_Gbl_Master_DocProofType] FOREIGN KEY([DocProofTypeId])
REFERENCES [dbo].[Gbl_Master_DocProofType] ([DocProofTypeId])
GO
ALTER TABLE [dbo].[Gbl_Master_DocProof] CHECK CONSTRAINT [FK_Gbl_Master_DocProof_Gbl_Master_DocProofType]
GO
ALTER TABLE [dbo].[Gbl_Master_Employee]  WITH NOCHECK ADD  CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Attachment] FOREIGN KEY([ImageId])
REFERENCES [dbo].[Gbl_Attachment] ([AttachmentId])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Gbl_Master_Employee] CHECK CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Attachment]
GO
ALTER TABLE [dbo].[Gbl_Master_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Attachment_AddressProof] FOREIGN KEY([AddressDocProofImageId])
REFERENCES [dbo].[Gbl_Attachment] ([AttachmentId])
GO
ALTER TABLE [dbo].[Gbl_Master_Employee] CHECK CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Attachment_AddressProof]
GO
ALTER TABLE [dbo].[Gbl_Master_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Attachment_IdentityProof] FOREIGN KEY([IdentityDocProofImageId])
REFERENCES [dbo].[Gbl_Attachment] ([AttachmentId])
GO
ALTER TABLE [dbo].[Gbl_Master_Employee] CHECK CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Attachment_IdentityProof]
GO
ALTER TABLE [dbo].[Gbl_Master_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Master_AddressDocProof] FOREIGN KEY([AddressDocProofId])
REFERENCES [dbo].[Gbl_Master_DocProof] ([DocProofId])
GO
ALTER TABLE [dbo].[Gbl_Master_Employee] CHECK CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Master_AddressDocProof]
GO
ALTER TABLE [dbo].[Gbl_Master_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Master_IdentityDocProof] FOREIGN KEY([IdentityDocProofId])
REFERENCES [dbo].[Gbl_Master_DocProof] ([DocProofId])
GO
ALTER TABLE [dbo].[Gbl_Master_Employee] CHECK CONSTRAINT [FK_Gbl_Master_Employee_Gbl_Master_IdentityDocProof]
GO
ALTER TABLE [dbo].[Gbl_Master_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Employee_Role_Gbl_Master_Employee] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Gbl_Master_Employee_Role] ([RoleId])
GO
ALTER TABLE [dbo].[Gbl_Master_Employee] CHECK CONSTRAINT [FK_Gbl_Master_Employee_Role_Gbl_Master_Employee]
GO
ALTER TABLE [dbo].[Gbl_Master_Notification]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Notification_Gbl_Master_NotificationType] FOREIGN KEY([NotificationTypeId])
REFERENCES [dbo].[Gbl_Master_NotificationType] ([NotificationTypeId])
GO
ALTER TABLE [dbo].[Gbl_Master_Notification] CHECK CONSTRAINT [FK_Gbl_Master_Notification_Gbl_Master_NotificationType]
GO
ALTER TABLE [dbo].[Gbl_Master_Notification]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Notification_Gbl_Master_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_Notification] CHECK CONSTRAINT [FK_Gbl_Master_Notification_Gbl_Master_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_Notification]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Notification_Gbl_Master_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Gbl_Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Gbl_Master_Notification] CHECK CONSTRAINT [FK_Gbl_Master_Notification_Gbl_Master_User]
GO
ALTER TABLE [dbo].[Gbl_Master_NotificationType]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_NotificationType_Gbl_Master_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_NotificationType] CHECK CONSTRAINT [FK_Gbl_Master_NotificationType_Gbl_Master_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_Page]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Page_Gbl_Master_AppModule] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Gbl_Master_AppModule] ([ModuleId])
GO
ALTER TABLE [dbo].[Gbl_Master_Page] CHECK CONSTRAINT [FK_Gbl_Master_Page_Gbl_Master_AppModule]
GO
ALTER TABLE [dbo].[Gbl_Master_Product]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Product_Gbl_Master_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Gbl_Master_Unit] ([UnitId])
GO
ALTER TABLE [dbo].[Gbl_Master_Product] CHECK CONSTRAINT [FK_Gbl_Master_Product_Gbl_Master_Unit]
GO
ALTER TABLE [dbo].[Gbl_Master_Product]  WITH CHECK ADD  CONSTRAINT [FK_MasterProduct_MasterSubCategory] FOREIGN KEY([SubCatId])
REFERENCES [dbo].[Gbl_Master_SubCategory] ([SubCatId])
GO
ALTER TABLE [dbo].[Gbl_Master_Product] CHECK CONSTRAINT [FK_MasterProduct_MasterSubCategory]
GO
ALTER TABLE [dbo].[Gbl_Master_Product]  WITH CHECK ADD  CONSTRAINT [FK_MasterProduct_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_Product] CHECK CONSTRAINT [FK_MasterProduct_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_Shop]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_Shop_Gbl_Attachment] FOREIGN KEY([LogoAttachmentId])
REFERENCES [dbo].[Gbl_Attachment] ([AttachmentId])
GO
ALTER TABLE [dbo].[Gbl_Master_Shop] CHECK CONSTRAINT [FK_Gbl_Master_Shop_Gbl_Attachment]
GO
ALTER TABLE [dbo].[Gbl_Master_Shop]  WITH CHECK ADD  CONSTRAINT [FK_Shop_User] FOREIGN KEY([Owner])
REFERENCES [dbo].[Gbl_Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Gbl_Master_Shop] CHECK CONSTRAINT [FK_Shop_User]
GO
ALTER TABLE [dbo].[Gbl_Master_SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_MasterSubCategory_MasterCategory] FOREIGN KEY([CatId])
REFERENCES [dbo].[Gbl_Master_Category] ([CatId])
GO
ALTER TABLE [dbo].[Gbl_Master_SubCategory] CHECK CONSTRAINT [FK_MasterSubCategory_MasterCategory]
GO
ALTER TABLE [dbo].[Gbl_Master_SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_MasterSubCategory_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_SubCategory] CHECK CONSTRAINT [FK_MasterSubCategory_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_User]  WITH CHECK ADD  CONSTRAINT [FK_Gbl_Master_User_Gbl_Master_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_User] CHECK CONSTRAINT [FK_Gbl_Master_User_Gbl_Master_Shop]
GO
ALTER TABLE [dbo].[Gbl_Master_User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([UserType])
REFERENCES [dbo].[Gbl_Master_UserType] ([Id])
GO
ALTER TABLE [dbo].[Gbl_Master_User] CHECK CONSTRAINT [FK_User_UserType]
GO
ALTER TABLE [dbo].[Gbl_Master_User_Permission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Gbl_Master_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Gbl_Master_Page] ([PageId])
GO
ALTER TABLE [dbo].[Gbl_Master_User_Permission] CHECK CONSTRAINT [FK_UserPermission_Gbl_Master_Page]
GO
ALTER TABLE [dbo].[Gbl_Master_User_Permission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Gbl_Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Gbl_Master_User_Permission] CHECK CONSTRAINT [FK_UserPermission_User]
GO
ALTER TABLE [dbo].[Gbl_Master_Vendor]  WITH CHECK ADD  CONSTRAINT [FK_MasterVendor_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Gbl_Master_Vendor] CHECK CONSTRAINT [FK_MasterVendor_Shop]
GO
ALTER TABLE [dbo].[Login]  WITH CHECK ADD  CONSTRAINT [FK_Login_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Gbl_Master_User] ([UserId])
GO
ALTER TABLE [dbo].[Login] CHECK CONSTRAINT [FK_Login_User]
GO
ALTER TABLE [dbo].[Stk_Dtl_Entry]  WITH CHECK ADD  CONSTRAINT [FK_StockEntry_MasterBrand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Gbl_Master_Brand] ([BrandId])
GO
ALTER TABLE [dbo].[Stk_Dtl_Entry] CHECK CONSTRAINT [FK_StockEntry_MasterBrand]
GO
ALTER TABLE [dbo].[Stk_Dtl_Entry]  WITH CHECK ADD  CONSTRAINT [FK_StockEntry_MasterProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Gbl_Master_Product] ([ProductId])
GO
ALTER TABLE [dbo].[Stk_Dtl_Entry] CHECK CONSTRAINT [FK_StockEntry_MasterProduct]
GO
ALTER TABLE [dbo].[Stk_Dtl_Entry]  WITH CHECK ADD  CONSTRAINT [FK_StockEntry_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Stk_Dtl_Entry] CHECK CONSTRAINT [FK_StockEntry_Shop]
GO
ALTER TABLE [dbo].[Stk_Dtl_Entry]  WITH CHECK ADD  CONSTRAINT [FK_StockEntry_Stk_Tr_Entry] FOREIGN KEY([StockMstId])
REFERENCES [dbo].[Stk_Tr_Entry] ([Id])
GO
ALTER TABLE [dbo].[Stk_Dtl_Entry] CHECK CONSTRAINT [FK_StockEntry_Stk_Tr_Entry]
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
REFERENCES [dbo].[Gbl_Master_Vendor] ([VendorId])
GO
ALTER TABLE [dbo].[Stk_Tr_Entry] CHECK CONSTRAINT [FK_Stk_Tr_Entry_MasterVendor]
GO
ALTER TABLE [dbo].[Stk_Tr_Entry]  WITH CHECK ADD  CONSTRAINT [FK_Stk_Tr_Entry_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[Stk_Tr_Entry] CHECK CONSTRAINT [FK_Stk_Tr_Entry_Shop]
GO
ALTER TABLE [dbo].[User_ShopMapper]  WITH CHECK ADD  CONSTRAINT [FK_UserShopMapper_Shop] FOREIGN KEY([ShopId])
REFERENCES [dbo].[Gbl_Master_Shop] ([ShopId])
GO
ALTER TABLE [dbo].[User_ShopMapper] CHECK CONSTRAINT [FK_UserShopMapper_Shop]
GO
ALTER TABLE [dbo].[User_ShopMapper]  WITH CHECK ADD  CONSTRAINT [FK_UserShopMapper_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Gbl_Master_User] ([UserId])
GO
ALTER TABLE [dbo].[User_ShopMapper] CHECK CONSTRAINT [FK_UserShopMapper_User]
GO
USE [master]
GO
ALTER DATABASE [MyShop] SET  READ_WRITE 
GO
