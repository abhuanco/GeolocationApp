/*
 Navicat Premium Data Transfer

 Source Server         : SQL Server 2022
 Source Server Type    : SQL Server
 Source Server Version : 16004135 (16.00.4135)
 Source Host           : localhost:1433
 Source Catalog        : GeolocationApi
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16004135 (16.00.4135)
 File Encoding         : 65001

 Date: 05/12/2024 20:07:05
*/


-- ----------------------------
-- Table structure for Visits
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Visits]') AND type IN ('U'))
	DROP TABLE [dbo].[Visits]
GO

CREATE TABLE [dbo].[Visits] (
  [Id] uniqueidentifier  NOT NULL,
  [Country] char(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [Emoji] nvarchar(4) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Currency] char(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [CurrencyName] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Symbol] char(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Latitude] bigint  NULL,
  [Longitude] bigint  NULL,
  [Ip] nvarchar(max) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [VisitDate] datetime2(7)  NOT NULL,
  [CreatedDate] datetime2(7)  NOT NULL,
  [UpdatedDate] datetime2(7)  NOT NULL
)
GO

ALTER TABLE [dbo].[Visits] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Primary Key structure for table Visits
-- ----------------------------
ALTER TABLE [dbo].[Visits] ADD CONSTRAINT [PK_Visits] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

