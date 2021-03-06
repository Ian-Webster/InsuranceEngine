CREATE PROCEDURE [dbo].[Questions_ListQuestionsForGrid] 
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
			CAST(null as int)	as QuestionID
			,cast(null as nvarchar(100))   as QuestionTemplate
			,cast(null as nvarchar(50))   as [Name]
			,cast(null as nvarchar(50))   as [Code]
			,CAST(null as int)	as TotalRows
		WHERE
			1 = 2  
	END	

	IF OBJECT_ID('tempdb..#ResultSet') IS NOT NULL
			DROP TABLE #ResultSet

	IF OBJECT_ID('tempdb..#ResultSetIndexed') IS NOT NULL
		DROP TABLE #ResultSetIndexed

	CREATE TABLE #ResultSet (
		QuestionID INT		
		,QuestionTemplate nvarchar(100)		
		,[Name] nvarchar(50)
		,[Code] nvarchar(50)
		,TotalRows INT
		)

	CREATE TABLE #ResultSetIndexed (
		ID INT IDENTITY(1, 1)
		,QuestionID INT		
		,QuestionTemplate nvarchar(100)		
		,[Name] nvarchar(50)
		,[Code] nvarchar(50)
		,TotalRows INT
		)	
		
		
	/* get raw data */
	INSERT INTO #ResultSet (
					QuestionID	
					,QuestionTemplate		
					,[Name]
					,[Code]
					,TotalRows
		)	
	SELECT distinct q.Question_ID AS QuestionID
		,qt.Name AS QuestionTemplate
		,q.Name
		,q.Code
		,0
	FROM Question q
	JOIN Question_Template qt ON qt.Question_Template_ID = q.Question_Template_ID
	WHERE q.Scheme_ID = @SchemeID
	ORDER BY q.Name

	
	DECLARE @sql NVARCHAR(max)

	SET @sql = 'INSERT INTO #ResultSetIndexed
	(
		QuestionID	
		,QuestionTemplate		
		,[Name]
		,[Code]
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
			SELECT COUNT(QuestionID)
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

	SELECT 	QuestionID	
			,QuestionTemplate		
			,[Name]
			,[Code]
			,TotalRows 
	FROM #ResultSetIndexed
	WHERE ID > @startIndex 
	AND ID < @endIndex	



END
GO
