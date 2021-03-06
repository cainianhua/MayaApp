USE [MAYA]
GO
/****** Object:  StoredProcedure [dbo].[USP_Create_District]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_Create_District]
(
    @ParentId int,	-- Parent DistrictID.
	@Name nvarchar(16),
	@Description nvarchar(100),
	@Lng VARCHAR(50),
	@Lat VARCHAR(50),
	@ActionDate datetime,
	@ActionBy nvarchar(50),
	@ReturnValue	int OUTPUT
)
AS
DECLARE @Rgt INT
DECLARE @Layer INT
IF EXISTS (SELECT 1 FROM Districts WHERE DistrictID = @ParentId)
BEGIN
    SET XACT_ABORT ON
    BEGIN TRANSACTION
    SELECT @Rgt = Rgt,@Layer = Layer FROM vwDistricts WHERE DistrictID = @ParentId
    UPDATE Districts SET Rgt = Rgt + 2 WHERE Rgt >= @Rgt
    UPDATE Districts SET Lft = Lft + 2 WHERE Lft >= @Rgt

	INSERT INTO [dbo].[Districts]
			   ([Name]
			   ,[Description]
			   ,[Lft]
			   ,[Rgt]
			   ,[Lng]
			   ,[Lat]
			   ,[CreatedDate]
			   ,[CreatedBy]
			   ,[UpdatedDate]
			   ,[UpdatedBy])
		 VALUES
			   (@Name
			   ,@Description
			   ,@Rgt
			   ,@Rgt + 1
			   ,@Lng
			   ,@Lat
			   ,@ActionDate
			   ,@ActionBy
			   ,@ActionDate
			   ,@ActionBy)

	SET @ReturnValue = SCOPE_IDENTITY()
	  
    COMMIT TRANSACTION
    SET XACT_ABORT OFF    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Delete_District]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_Delete_District](
     @DistrictId int
)
AS
DECLARE @Lft INT
DECLARE @Rgt INT
IF EXISTS (SELECT 1 FROM [dbo].[Districts] WHERE DistrictID = @DistrictID)
BEGIN
	SET XACT_ABORT ON
	BEGIN TRANSACTION
	SELECT @Lft = Lft, @Rgt = Rgt FROM [dbo].[Districts] WHERE DistrictID = @DistrictID
	DELETE FROM [dbo].[Districts] WHERE Lft >= @Lft AND Rgt <= @Rgt
	UPDATE [dbo].[Districts] SET Lft = Lft - (@Rgt - @Lft + 1) WHERE Lft > @Lft
	UPDATE [dbo].[Districts] SET Rgt = Rgt - (@Rgt - @Lft + 1) WHERE Rgt > @Rgt
	COMMIT TRANSACTION
	SET XACT_ABORT OFF
END

GO
/****** Object:  StoredProcedure [dbo].[USP_Get_Districts_Of_Child]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_Get_Districts_Of_Child](
	@DistrictID INT
)
AS
BEGIN
	DECLARE @lft INT
	DECLARE @rgt INT
	DECLARE @layer INT
	IF EXISTS (SELECT 1 FROM Districts WHERE DistrictID = @DistrictID)
	BEGIN
		SELECT @lft = lft,@rgt = rgt,@layer = layer FROM vwDistricts WHERE DistrictID = @DistrictID
		SELECT * FROM vwDistricts WHERE lft BETWEEN @lft AND @rgt AND Layer = @layer + 1 ORDER BY lft ASC
	END
END


GO
/****** Object:  StoredProcedure [dbo].[USP_Get_Districts_Of_Parent]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_Get_Districts_Of_Parent](
	@DistrictID INT
)
AS
BEGIN
	DECLARE @lft INT
	DECLARE @rgt INT
	IF EXISTS (SELECT 1 FROM Districts WHERE DistrictID = @DistrictID)
	BEGIN
		SELECT @lft = lft,@rgt = rgt FROM Districts WHERE DistrictID = @DistrictID
		SELECT * FROM Districts WHERE lft < @lft AND rgt > @rgt ORDER BY lft ASC
	END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Save_Article]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************************************
    Author      : Code Generator v1.0
    CreatedDate : 2014-11-07 23:46:10
    UpdatedDate : 2014-11-07 23:46:10
    Description : Save or update a Article
*********************************************************************/
CREATE PROCEDURE [dbo].[USP_Save_Article] (
     @ArticleId	int
    ,@Title	nvarchar(50)
    ,@ArticleContent	nvarchar(max)
    ,@Tags	nvarchar(50)
    ,@SortOrder	int
    ,@DistrictId	int
    ,@CategoryId	int
    ,@ActionDate   DateTime
    ,@ActionBy     nvarchar(50)
    ,@ReturnValue	int OUTPUT
)
AS
BEGIN
    SET @ReturnValue = @ArticleId
    IF NOT EXISTS(SELECT 1 FROM [Articles] WHERE [ArticleId] = @ArticleId)
    BEGIN
        INSERT INTO [dbo].[Articles]
                   ([Title]
                   ,[ArticleContent]
                   ,[Tags]
                   ,[SortOrder]
                   ,[DistrictId]
                   ,[CategoryId]
                   ,[CreatedDate]
                   ,[CreatedBy]
                   ,[UpdatedDate]
                   ,[UpdatedBy])
             VALUES 
                   (@Title
                   ,@ArticleContent
                   ,@Tags
                   ,@SortOrder
                   ,@DistrictId
                   ,@CategoryId
                   ,@ActionDate
                   ,@ActionBy
                   ,@ActionDate
                   ,@ActionBy)
        SET @ReturnValue = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[Articles]
           SET [Title] = @Title
              ,[ArticleContent] = @ArticleContent
              ,[Tags] = @Tags
              ,[SortOrder] = @SortOrder
              ,[DistrictId] = @DistrictId
              ,[CategoryId] = @CategoryId
              ,[UpdatedDate] = @ActionDate
              ,[UpdatedBy] = @ActionBy
         WHERE ArticleId = @ArticleId
    END
END



GO
/****** Object:  StoredProcedure [dbo].[USP_Save_Category]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
    Author      : Code Generator v1.0
    CreatedDate : 2014-11-07 23:46:09
    UpdatedDate : 2014-11-07 23:46:09
    Description : Save or update a Category
*********************************************************************/
CREATE PROCEDURE [dbo].[USP_Save_Category] (
     @CategoryId	int
    ,@Name	nvarchar(50)
    ,@Description	nvarchar(255)
    ,@SortOrder	int
    ,@ActionDate   DateTime
    ,@ActionBy     nvarchar(50)
    ,@ReturnValue	int OUTPUT
)
AS
BEGIN
    SET @ReturnValue = @CategoryId
    IF NOT EXISTS(SELECT 1 FROM [Categories] WHERE [CategoryId] = @CategoryId)
    BEGIN
        INSERT INTO [dbo].[Categories]
                   ([Name]
                   ,[Description]
                   ,[SortOrder]
                   ,[CreatedDate]
                   ,[CreatedBy]
                   ,[UpdatedDate]
                   ,[UpdatedBy])
             VALUES 
                   (@Name
                   ,@Description
                   ,@SortOrder
                   ,@ActionDate
                   ,@ActionBy
                   ,@ActionDate
                   ,@ActionBy)
        SET @ReturnValue = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[Categories]
           SET [Name] = @Name
              ,[Description] = @Description
              ,[SortOrder] = @SortOrder
              ,[UpdatedDate] = @ActionDate
              ,[UpdatedBy] = @ActionBy
         WHERE CategoryId = @CategoryId
    END
END



GO
/****** Object:  StoredProcedure [dbo].[USP_Save_Music]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
    Author      : Code Generator v1.0
    CreatedDate : 2014-11-07 23:46:08
    UpdatedDate : 2014-11-07 23:46:08
    Description : Save or update a Music
*********************************************************************/
CREATE PROCEDURE [dbo].[USP_Save_Music] (
     @MusicId	int
    ,@Name	nvarchar(50)
    ,@Description	nvarchar(255)
    ,@LinkTo	nvarchar(512)
    ,@SortOrder	int
    ,@DistrictId	int
    ,@ActionDate   DateTime
    ,@ActionBy     nvarchar(50)
    ,@ReturnValue	int OUTPUT
)
AS
BEGIN
    SET @ReturnValue = @MusicId
    IF NOT EXISTS(SELECT 1 FROM [Musics] WHERE [MusicId] = @MusicId)
    BEGIN
        INSERT INTO [dbo].[Musics]
                   ([Name]
                   ,[Description]
                   ,[LinkTo]
                   ,[SortOrder]
                   ,[DistrictId]
                   ,[CreatedDate]
                   ,[CreatedBy]
                   ,[UpdatedDate]
                   ,[UpdatedBy])
             VALUES 
                   (@Name
                   ,@Description
                   ,@LinkTo
                   ,@SortOrder
                   ,@DistrictId
                   ,@ActionDate
                   ,@ActionBy
                   ,@ActionDate
                   ,@ActionBy)
        SET @ReturnValue = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[Musics]
           SET [Name] = @Name
              ,[Description] = @Description
              ,[LinkTo] = @LinkTo
              ,[SortOrder] = @SortOrder
              ,[DistrictId] = @DistrictId
              ,[UpdatedDate] = @ActionDate
              ,[UpdatedBy] = @ActionBy
         WHERE MusicId = @MusicId
    END
END



GO
/****** Object:  StoredProcedure [dbo].[USP_Save_Product]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
    Author      : Code Generator v1.0
    CreatedDate : 2014-11-07 23:46:08
    UpdatedDate : 2014-11-07 23:46:08
    Description : Save or update a Product
*********************************************************************/
CREATE PROCEDURE [dbo].[USP_Save_Product] (
     @ProductId	int
    ,@Name	nvarchar(50)
    ,@Description	nvarchar(1024)
    ,@LinkTo	nvarchar(512)
    ,@Pic	nvarchar(128)
    ,@SortOrder	int
    ,@ActionDate   DateTime
    ,@ActionBy     nvarchar(50)
    ,@ReturnValue	int OUTPUT
)
AS
BEGIN
    SET @ReturnValue = @ProductId
    IF NOT EXISTS(SELECT 1 FROM [Products] WHERE [ProductId] = @ProductId)
    BEGIN
        INSERT INTO [dbo].[Products]
                   ([Name]
                   ,[Description]
                   ,[LinkTo]
                   ,[Pic]
                   ,[SortOrder]
                   ,[CreatedDate]
                   ,[CreatedBy]
                   ,[UpdatedDate]
                   ,[UpdatedBy])
             VALUES 
                   (@Name
                   ,@Description
                   ,@LinkTo
                   ,@Pic
                   ,@SortOrder
                   ,@ActionDate
                   ,@ActionBy
                   ,@ActionDate
                   ,@ActionBy)
        SET @ReturnValue = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[Products]
           SET [Name] = @Name
              ,[Description] = @Description
              ,[LinkTo] = @LinkTo
              ,[Pic] = @Pic
              ,[SortOrder] = @SortOrder
              ,[UpdatedDate] = @ActionDate
              ,[UpdatedBy] = @ActionBy
         WHERE ProductId = @ProductId
    END
END



GO
/****** Object:  StoredProcedure [dbo].[USP_Save_User]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
    Author      : Code Generator v1.0
    CreatedDate : 2014-11-07 23:46:10
    UpdatedDate : 2014-11-07 23:46:10
    Description : Save or update a User
*********************************************************************/
CREATE PROCEDURE [dbo].[USP_Save_User] (
     @UserId	bigint
    ,@UserName	nvarchar(50)
    ,@Password	nvarchar(64)
    ,@PasswordSalt	nvarchar(64)
    ,@Email	nvarchar(128)
    ,@EmailStatus	int
    ,@Role	int
    ,@ActionDate   DateTime
    ,@ActionBy     nvarchar(50)
    ,@ReturnValue	bigint OUTPUT
)
AS
BEGIN
    SET @ReturnValue = @UserId
    IF NOT EXISTS(SELECT 1 FROM [Users] WHERE [UserId] = @UserId)
    BEGIN
        INSERT INTO [dbo].[Users]
                   ([UserName]
                   ,[Password]
                   ,[PasswordSalt]
                   ,[Email]
                   ,[EmailStatus]
                   ,[Role]
                   ,[CreatedDate]
                   ,[CreatedBy]
                   ,[UpdatedDate]
                   ,[UpdatedBy])
             VALUES 
                   (@UserName
                   ,@Password
                   ,@PasswordSalt
                   ,@Email
                   ,@EmailStatus
                   ,@Role
                   ,@ActionDate
                   ,@ActionBy
                   ,@ActionDate
                   ,@ActionBy)
        SET @ReturnValue = SCOPE_IDENTITY()
    END
    ELSE
    BEGIN
        UPDATE [dbo].[Users]
           SET [UserName] = @UserName
              ,[Password] = @Password
              ,[PasswordSalt] = @PasswordSalt
              ,[Email] = @Email
              ,[EmailStatus] = @EmailStatus
              ,[Role] = @Role
              ,[UpdatedDate] = @ActionDate
              ,[UpdatedBy] = @ActionBy
         WHERE UserId = @UserId
    END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Update_District]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
    Author      : Code Generator v1.0
    CreatedDate : 2014-11-16 02:15:48
    UpdatedDate : 2014-11-16 02:15:48
    Description : Save or update a District
*********************************************************************/
CREATE PROCEDURE [dbo].[USP_Update_District] (
     @DistrictId	int
    ,@Name	nvarchar(50)
    ,@Description	nvarchar(255)
    ,@Lng	varchar(50)
    ,@Lat	varchar(50)
    ,@ActionDate   DateTime
    ,@ActionBy     nvarchar(50)
    ,@ReturnValue	int OUTPUT
)
AS
BEGIN
    SET @ReturnValue = @DistrictId
    IF EXISTS(SELECT 1 FROM [Districts] WHERE [DistrictId] = @DistrictId)
    BEGIN
        UPDATE [dbo].[Districts]
           SET [Name] = @Name
              ,[Description] = @Description
              ,[Lng] = @Lng
              ,[Lat] = @Lat
              ,[UpdatedDate] = @ActionDate
              ,[UpdatedBy] = @ActionBy
         WHERE DistrictId = @DistrictId
    END
END


GO
/****** Object:  Table [dbo].[Articles]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[ArticleId] [int] IDENTITY(1000001,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ArticleContent] [nvarchar](max) NOT NULL,
	[Tags] [nvarchar](50) NULL,
	[SortOrder] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(10001,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[SortOrder] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Districts]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Districts](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Lft] [int] NOT NULL,
	[Rgt] [int] NOT NULL,
	[Lng] [varchar](50) NULL,
	[Lat] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Musics]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Musics](
	[MusicId] [int] IDENTITY(1000001,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[LinkTo] [nvarchar](512) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Musics] PRIMARY KEY CLUSTERED 
(
	[MusicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1000001,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[LinkTo] [nvarchar](512) NOT NULL,
	[Pic] [nvarchar](128) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1200001,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](64) NOT NULL,
	[PasswordSalt] [nvarchar](64) NULL,
	[Email] [nvarchar](128) NOT NULL,
	[EmailStatus] [int] NULL,
	[Role] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vwDistricts]    Script Date: 2014/11/17 1:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwDistricts]
AS
SELECT   DistrictId, Name, Description, Lft, Rgt, Lng, Lat, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, 
                dbo.DistrictLayerCount(DistrictId) AS Layer
FROM      dbo.Districts

GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name], [Description], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10001, N'电流电压', N'电流电压', 1, CAST(0x0000A3E600000000 AS DateTime), N'rcai', CAST(0x0000A3E600000000 AS DateTime), N'rcai')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10002, N'插座标准', N'插座标准', 2, CAST(0x0000A3E600000000 AS DateTime), N'rcai', CAST(0x0000A3E600000000 AS DateTime), N'rcai')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10003, N'全球通使用方法', N'全球通使用方法', 3, CAST(0x0000A3E600000000 AS DateTime), N'rcai', CAST(0x0000A3E600000000 AS DateTime), N'rcai')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10004, N'大使馆资料', N'大使馆资料', 4, CAST(0x0000A3E600000000 AS DateTime), N'rcai', CAST(0x0000A3E600000000 AS DateTime), N'rcai')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10005, N'当地紧急电话', N'当地紧急电话', 5, CAST(0x0000A3E600000000 AS DateTime), N'rcai', CAST(0x0000A3E600000000 AS DateTime), N'rcai')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10006, N'机场信息', N'机场信息', 6, CAST(0x0000A3E600000000 AS DateTime), N'rcai', CAST(0x0000A3E600000000 AS DateTime), N'rcai')
INSERT [dbo].[Categories] ([CategoryId], [Name], [Description], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10007, N'出入境卡填写', N'出入境卡填写', 7, CAST(0x0000A3E600000000 AS DateTime), N'rcai', CAST(0x0000A3E600000000 AS DateTime), N'rcai')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Districts] ON 

INSERT [dbo].[Districts] ([DistrictId], [Name], [Description], [Lft], [Rgt], [Lng], [Lat], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, N'全球', N'树状结构根节点信息，不会在系统里面显示，只用来体现树状结构的完整性。', 1, 14, N'0', N'0', CAST(0x0000A3DE00000000 AS DateTime), N'rcai', CAST(0x0000A3DE00000000 AS DateTime), N'rcai')
INSERT [dbo].[Districts] ([DistrictId], [Name], [Description], [Lft], [Rgt], [Lng], [Lat], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, N'中国', N'中华人民共和国', 2, 11, N'10', N'23', CAST(0x0000A3E400F3F46E AS DateTime), N'rcai', CAST(0x0000A3E400F3F46E AS DateTime), N'rcai')
INSERT [dbo].[Districts] ([DistrictId], [Name], [Description], [Lft], [Rgt], [Lng], [Lat], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, N'北京市', NULL, 3, 4, N'10', N'10', CAST(0x0000A3E400F41954 AS DateTime), N'rcai', CAST(0x0000A3E400F41954 AS DateTime), N'rcai')
INSERT [dbo].[Districts] ([DistrictId], [Name], [Description], [Lft], [Rgt], [Lng], [Lat], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, N'广州市', NULL, 5, 6, N'10', N'10', CAST(0x0000A3E4016647E2 AS DateTime), N'rcai', CAST(0x0000A3E4016647E2 AS DateTime), N'rcai')
INSERT [dbo].[Districts] ([DistrictId], [Name], [Description], [Lft], [Rgt], [Lng], [Lat], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (6, N'上海市', N'1111', 7, 8, N'10', N'30', CAST(0x0000A3E500234F7C AS DateTime), N'rcai', CAST(0x0000A3E50026E65A AS DateTime), N'rcai')
INSERT [dbo].[Districts] ([DistrictId], [Name], [Description], [Lft], [Rgt], [Lng], [Lat], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (7, N'湖南省', N'我的家乡', 9, 10, NULL, NULL, CAST(0x0000A3E50027477B AS DateTime), N'rcai', CAST(0x0000A3E5002785BB AS DateTime), N'rcai')
SET IDENTITY_INSERT [dbo].[Districts] OFF
SET IDENTITY_INSERT [dbo].[Musics] ON 

INSERT [dbo].[Musics] ([MusicId], [Name], [Description], [LinkTo], [SortOrder], [DistrictId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1000001, N'asdf', N'asdf', N'http://www.abc.com', 9999, 2, CAST(0x0000A3E6000FA26A AS DateTime), N'rcai', CAST(0x0000A3E6000FFC37 AS DateTime), N'rcai')
INSERT [dbo].[Musics] ([MusicId], [Name], [Description], [LinkTo], [SortOrder], [DistrictId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1000002, N'asdf', N'asdf', N'asdf', 9999, 7, CAST(0x0000A3E6001008C8 AS DateTime), N'rcai', CAST(0x0000A3E6001008C8 AS DateTime), N'rcai')
SET IDENTITY_INSERT [dbo].[Musics] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [Name], [Description], [LinkTo], [Pic], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1000002, N'测试产品一', N'测试产品一', N'http://www.baidu.com', N'/Uploads/20141117/Images/86632aedc1fa410da51117bf55cc7dfd.jpg', 9999, CAST(0x0000A3E6000481C3 AS DateTime), N'rcai', CAST(0x0000A3E6000481C3 AS DateTime), N'rcai')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [LinkTo], [Pic], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2000003, N'测试产品3', N'测试产品3', N'http://www.baidu.com', N'/Uploads/20141117/Images/4cef10782ab94e088ef8e94df0ee1b6b.jpg', 9999, CAST(0x0000A3E60004C864 AS DateTime), N'rcai', CAST(0x0000A3E6000591F5 AS DateTime), N'rcai')
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [LinkTo], [Pic], [SortOrder], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2000004, N'啊速度发送', N'阿三地方', N'http://www.qq.com', N'/Uploads/20141117/Images/865a2d3156f44dcb827cf07fa669f6a8.jpg', 9999, CAST(0x0000A3E60005F285 AS DateTime), N'rcai', CAST(0x0000A3E60005F285 AS DateTime), N'rcai')
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserName], [Password], [PasswordSalt], [Email], [EmailStatus], [Role], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1200002, N'rcai', N'52af3ce8a82f62707789fe00899ed3f0', N'123456', N'cainianhua@gmail.com', 1, 1, CAST(0x0000A3DC00000000 AS DateTime), N'Admin', CAST(0x0000A3DC00000000 AS DateTime), N'Admin')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [PasswordSalt], [Email], [EmailStatus], [Role], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2400003, N'mayaabc', N'932ead2253e87951093b2b17245433ac', N'80607', N'mayaabc@a.com', 0, 0, CAST(0x0000A3E50126BFCE AS DateTime), N'rcai', CAST(0x0000A3E50126BFCE AS DateTime), N'rcai')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [PasswordSalt], [Email], [EmailStatus], [Role], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3600004, N'abc', N'78a30ec8426ae05b6d24c9d98dee145a', N'77324', N'abc@a.co', 0, 0, CAST(0x0000A3E50126EE10 AS DateTime), N'rcai', CAST(0x0000A3E50126EE10 AS DateTime), N'rcai')
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [PasswordSalt], [Email], [EmailStatus], [Role], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3600005, N'test', N'ede4b61fd563686334e07d584b5b19de', N'80849', N'test@a.com', 0, 0, CAST(0x0000A3E5012A09AD AS DateTime), N'rcai', CAST(0x0000A3E5012A09AD AS DateTime), N'rcai')
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_Users_Email]    Script Date: 2014/11/17 1:05:04 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Users_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_Users_Name]    Script Date: 2014/11/17 1:05:04 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Users_Name] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Articles] ADD  CONSTRAINT [DF_Articles_SortOrder]  DEFAULT ((9999)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_SortOrder]  DEFAULT ((9999)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Musics] ADD  CONSTRAINT [DF_Musics_SortOrder]  DEFAULT ((9999)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_SortOrder]  DEFAULT ((9999)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_EmailStatus]  DEFAULT ((0)) FOR [EmailStatus]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Categories]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Districts] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Districts] ([DistrictId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Districts]
GO
ALTER TABLE [dbo].[Musics]  WITH CHECK ADD  CONSTRAINT [FK_Musics_Districts] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Districts] ([DistrictId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Musics] CHECK CONSTRAINT [FK_Musics_Districts]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经度, 以本初子午线为0度，东经为正，西经为负，值的范围为[-180, 180]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Districts', @level2type=N'COLUMN',@level2name=N'Lng'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纬度，值的范围[-90, 90]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Districts', @level2type=N'COLUMN',@level2name=N'Lat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0未验证，1通过验证，2验证失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'EmailStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Districts"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 228
               Right = 241
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwDistricts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwDistricts'
GO
