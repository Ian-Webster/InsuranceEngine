create PROCEDURE [dbo].[RenderedPages_ListRenderedPagesForGrid] 
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
			CAST(null as int)	as RenderedPageID			
			,cast(null as nvarchar(50))   as [Name]
			,cast(null as nvarchar(50))   as [Title]
			,cast(null as datetime)   as LastRenderDate
			,CAST(null as int)	as TotalRows
		WHERE
			1 = 2  
	END	

	IF OBJECT_ID('tempdb..#ResultSet') IS NOT NULL
			DROP TABLE #ResultSet

	IF OBJECT_ID('tempdb..#ResultSetIndexed') IS NOT NULL
		DROP TABLE #ResultSetIndexed

	CREATE TABLE #ResultSet (
		RenderedPageID INT			
		,[Name] nvarchar(50)
		,[Title] nvarchar(50)
		,LastRenderDate datetime
		,TotalRows INT
		)

	CREATE TABLE #ResultSetIndexed (
		ID INT IDENTITY(1, 1)
		,RenderedPageID INT		
		,[Name] nvarchar(50)
		,[Title] nvarchar(50)
		,LastRenderDate datetime
		,TotalRows INT
		)	
		
		
	/* get raw data */
	INSERT INTO #ResultSet (
					RenderedPageID	
					,[Name]
					,[Title]
					,LastRenderDate
					,TotalRows
		)	
	SELECT distinct rp.Rendered_Page_ID AS RenderedPageID
		,p.Name
		,p.Title
		,rp.LastRenderDate
		,0
	FROM Rendered_Page rp
	JOIN [Page] p ON p.Page_ID = rp.Page_ID
	WHERE p.Scheme_ID = @SchemeID
	ORDER BY p.Name

	
	DECLARE @sql NVARCHAR(max)

	SET @sql = 'INSERT INTO #ResultSetIndexed
	(
		RenderedPageID	
		,[Name]
		,[Title]
		,LastRenderDate
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
			SELECT COUNT(RenderedPageID)
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

	SELECT 	RenderedPageID	
			,[Name]
			,[Title]
			,LastRenderDate
			,TotalRows 
	FROM #ResultSetIndexed
	WHERE ID > @startIndex 
	AND ID < @endIndex	



END
GO
