CREATE PROCEDURE [dbo].[Pages_ListPagesForGrid] 
	@SchemeID INT,
	@WhereClause NVARCHAR(max),
	@OrderClause NVARCHAR(128),
	@PageSize INT,
	@CurrentPageIndex INT
AS
BEGIN

	--supplying a data contract
	IF 1 = 2 BEGIN
		SELECT
			CAST(null as int)	as PageID
			,cast(null as nvarchar(100))   as PageTemplate
			,cast(null as nvarchar(50))   as [Name]
			,cast(null as nvarchar(50))   as [Title]
			,cast(null as int)   as DisplayOrder
			,cast(null as nvarchar(50))   as NextPageName
			,CAST(null as int)	as TotalRows
		WHERE
			1 = 2  
	END	

	IF OBJECT_ID('tempdb..#ResultSet') IS NOT NULL
			DROP TABLE #ResultSet

	IF OBJECT_ID('tempdb..#ResultSetIndexed') IS NOT NULL
		DROP TABLE #ResultSetIndexed

	CREATE TABLE #ResultSet (
		PageID INT		
		,PageTemplate nvarchar(100)		
		,[Name] nvarchar(50)
		,[Title] nvarchar(50)
		,DisplayOrder int
		,NextPageName nvarchar(50)
		,TotalRows INT
		)

	CREATE TABLE #ResultSetIndexed (
		ID INT IDENTITY(1, 1)
		,PageID INT		
		,PageTemplate nvarchar(100)		
		,[Name] nvarchar(50)
		,[Title] nvarchar(50)
		,DisplayOrder int
		,NextPageName nvarchar(50)
		,TotalRows INT
		)	
		
		
	/* get raw data */
	INSERT INTO #ResultSet (
					PageID	
					,PageTemplate		
					,[Name]
					,[Title]
					,DisplayOrder
					,NextPageName
					,TotalRows
		)	
	SELECT distinct p.Page_ID AS PageID
		,pt.NAME AS PageTemplate
		,p.NAME
		,p.Title
		,p.DisplayOrder
		,isnull(nextP.NAME, 'n/a') AS NextPageName
		,0
	FROM Page p
	JOIN Page_Template pt ON pt.Page_Template_ID = p.Page_Template_ID
	LEFT JOIN Page nextP ON p.Next_Page_ID = nextp.Page_ID
	WHERE p.Scheme_ID = @SchemeID
	ORDER BY p.DisplayOrder

	
	DECLARE @sql NVARCHAR(max)

	SET @sql = 'INSERT INTO #ResultSetIndexed
	(
		PageID	
		,PageTemplate		
		,[Name]
		,[Title]
		,DisplayOrder
		,NextPageName
		,TotalRows  
	) select * from #ResultSet'

	IF (@WhereClause <> '')
	BEGIN
		SET @sql = @sql + ' where ' + @WhereClause
	END
	
	IF (@OrderClause <> '')
	BEGIN
		SET @sql = @sql + ' order by ' + @OrderClause
	END

	EXEC (@sql)
	--print @sql

	/* sort out total row count */
	DECLARE @count INT

	SET @count = (
			SELECT COUNT(PageID)
			FROM #ResultSetIndexed
			)

	UPDATE #ResultSetIndexed
	SET TotalRows = @count

	declare @startIndex int
	declare @endIndex int

	IF (@CurrentPageIndex = 1)
	BEGIN
		
		set @startIndex = 0
		set @endIndex = @startIndex + @PageSize + 1
	END
	ELSE
	BEGIN
		set @startIndex = (@CurrentPageIndex-1) * @PageSize
		set @endIndex = @startIndex + @PageSize + 1 
	END

	SELECT 	PageID	
			,PageTemplate		
			,[Name]
			,[Title]
			,DisplayOrder
			,NextPageName
			,TotalRows 
	FROM #ResultSetIndexed
	WHERE ID > @startIndex 
	AND ID < @endIndex	



END
GO
