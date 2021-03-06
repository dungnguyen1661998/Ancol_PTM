USE [master]
GO
/****** Object:  Database [lib]    Script Date: 09/27/2018 09:47:39 ******/
CREATE DATABASE [lib] 
ALTER DATABASE [lib] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [lib].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [lib] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [lib] SET ANSI_NULLS OFF
GO
ALTER DATABASE [lib] SET ANSI_PADDING OFF
GO
ALTER DATABASE [lib] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [lib] SET ARITHABORT OFF
GO
ALTER DATABASE [lib] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [lib] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [lib] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [lib] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [lib] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [lib] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [lib] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [lib] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [lib] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [lib] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [lib] SET  DISABLE_BROKER
GO
ALTER DATABASE [lib] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [lib] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [lib] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [lib] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [lib] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [lib] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [lib] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [lib] SET  READ_WRITE
GO
ALTER DATABASE [lib] SET RECOVERY SIMPLE
GO
ALTER DATABASE [lib] SET  MULTI_USER
GO
ALTER DATABASE [lib] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [lib] SET DB_CHAINING OFF
GO
USE [lib]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 09/27/2018 09:47:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Skills] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[InsAt] [datetime] NULL,
	[InsBy] [nvarchar](50) NULL,
	[UpdAt] [datetime] NULL,
	[UpdBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 09/27/2018 09:47:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ClientName] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[InsAt] [datetime] NULL,
	[InsBy] [nvarchar](50) NULL,
	[UpdAt] [datetime] NULL,
	[UpdBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskStatus]    Script Date: 09/27/2018 09:47:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskStatus](
	[Id] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[InsAt] [datetime] NULL,
	[InsBy] [nvarchar](50) NULL,
	[UpdAt] [datetime] NULL,
	[UpdBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_TaskStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStory]    Script Date: 09/27/2018 09:47:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStory](
	[Id] [uniqueidentifier] NOT NULL,
	[Story] [nvarchar](max) NULL,
	[Projectid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
	[InsAt] [datetime] NULL,
	[InsBy] [nvarchar](50) NULL,
	[UpdAt] [datetime] NULL,
	[UpdBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserStory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectTask]    Script Date: 09/27/2018 09:47:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTask](
	[Id] [uniqueidentifier] NOT NULL,
	[Employeeid] [uniqueidentifier] NULL,
	[TaskStartDate] [datetime] NULL,
	[TaskEndDate] [datetime] NULL,
	[TaskStatusid] [uniqueidentifier] NULL,
	[UserStoryid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
	[InsAt] [datetime] NULL,
	[InsBy] [nvarchar](50) NULL,
	[UpdAt] [datetime] NULL,
	[UpdBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_ProjectTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManagerComment]    Script Date: 09/27/2018 09:47:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManagerComment](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[ProjectTaskid] [uniqueidentifier] NULL,
	[Comments] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[InsAt] [datetime] NULL,
	[InsBy] [nvarchar](50) NULL,
	[UpdAt] [datetime] NULL,
	[UpdBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_ManagerComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_UserStory_Project]    Script Date: 09/27/2018 09:47:39 ******/
ALTER TABLE [dbo].[UserStory]  WITH CHECK ADD  CONSTRAINT [FK_UserStory_Project] FOREIGN KEY([Projectid])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[UserStory] CHECK CONSTRAINT [FK_UserStory_Project]
GO
/****** Object:  ForeignKey [FK_ProjectTask_Employee]    Script Date: 09/27/2018 09:47:39 ******/
ALTER TABLE [dbo].[ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_Employee] FOREIGN KEY([Employeeid])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[ProjectTask] CHECK CONSTRAINT [FK_ProjectTask_Employee]
GO
/****** Object:  ForeignKey [FK_ProjectTask_TaskStatus]    Script Date: 09/27/2018 09:47:39 ******/
ALTER TABLE [dbo].[ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_TaskStatus] FOREIGN KEY([TaskStatusid])
REFERENCES [dbo].[TaskStatus] ([Id])
GO
ALTER TABLE [dbo].[ProjectTask] CHECK CONSTRAINT [FK_ProjectTask_TaskStatus]
GO
/****** Object:  ForeignKey [FK_ProjectTask_UserStory]    Script Date: 09/27/2018 09:47:39 ******/
ALTER TABLE [dbo].[ProjectTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTask_UserStory] FOREIGN KEY([UserStoryid])
REFERENCES [dbo].[UserStory] ([Id])
GO
ALTER TABLE [dbo].[ProjectTask] CHECK CONSTRAINT [FK_ProjectTask_UserStory]
GO
/****** Object:  ForeignKey [FK_ManagerComment_ProjectTask]    Script Date: 09/27/2018 09:47:39 ******/
ALTER TABLE [dbo].[ManagerComment]  WITH CHECK ADD  CONSTRAINT [FK_ManagerComment_ProjectTask] FOREIGN KEY([ProjectTaskid])
REFERENCES [dbo].[ProjectTask] ([Id])
GO
ALTER TABLE [dbo].[ManagerComment] CHECK CONSTRAINT [FK_ManagerComment_ProjectTask]
GO
