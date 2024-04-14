USE [master]
GO
/****** Object:  Database [PizzaPlace]    Script Date: 14/04/2024 5:08:40 pm ******/
CREATE DATABASE [PizzaPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PizzaPlace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PizzaPlace.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PizzaPlace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PizzaPlace_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PizzaPlace] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PizzaPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PizzaPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PizzaPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PizzaPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PizzaPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PizzaPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [PizzaPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PizzaPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PizzaPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PizzaPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PizzaPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PizzaPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PizzaPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PizzaPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PizzaPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PizzaPlace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PizzaPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PizzaPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PizzaPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PizzaPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PizzaPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PizzaPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PizzaPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PizzaPlace] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PizzaPlace] SET  MULTI_USER 
GO
ALTER DATABASE [PizzaPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PizzaPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PizzaPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PizzaPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PizzaPlace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PizzaPlace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PizzaPlace] SET QUERY_STORE = ON
GO
ALTER DATABASE [PizzaPlace] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PizzaPlace]
GO
/****** Object:  Table [dbo].[category]    Script Date: 14/04/2024 5:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ingredient]    Script Date: 14/04/2024 5:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredient](
	[ingredient_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ingredient] PRIMARY KEY CLUSTERED 
(
	[ingredient_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_detail]    Script Date: 14/04/2024 5:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_detail](
	[order_detail_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[pizza_id] [nvarchar](50) NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_order_detail_id] PRIMARY KEY CLUSTERED 
(
	[order_detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_stamp]    Script Date: 14/04/2024 5:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_stamp](
	[order_id] [int] NOT NULL,
	[date] [date] NOT NULL,
	[time] [time](7) NOT NULL,
 CONSTRAINT [PK_order_stamp] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pizza]    Script Date: 14/04/2024 5:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pizza](
	[pizza_id] [nvarchar](50) NOT NULL,
	[recipe_id] [nvarchar](50) NOT NULL,
	[size] [nvarchar](50) NOT NULL,
	[price] [float] NOT NULL,
 CONSTRAINT [PK_pizzas] PRIMARY KEY CLUSTERED 
(
	[pizza_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recipe]    Script Date: 14/04/2024 5:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe](
	[recipe_id] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[category_id] [int] NOT NULL,
 CONSTRAINT [PK_recipe] PRIMARY KEY CLUSTERED 
(
	[recipe_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recipe_ingredient]    Script Date: 14/04/2024 5:08:40 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe_ingredient](
	[recipe_ingredient_id] [int] IDENTITY(1,1) NOT NULL,
	[recipe_id] [nvarchar](50) NOT NULL,
	[ingredient_id] [int] NOT NULL,
 CONSTRAINT [PK_recipe_ingredient] PRIMARY KEY CLUSTERED 
(
	[recipe_ingredient_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [PizzaPlace] SET  READ_WRITE 
GO
