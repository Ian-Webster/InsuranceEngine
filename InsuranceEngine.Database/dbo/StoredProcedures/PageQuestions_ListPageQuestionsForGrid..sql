create PROCEDURE [dbo].[PageQuestions_ListPageQuestionsForGrid] 
	@PageID INT,
	@WhereClause NVARCHAR(max),
	@OrderClause NVARCHAR(128),
	@PageSize INT,
	@CurrentPageIndex INT
AS
BEGIN

	--supplying a data contract
	IF 1 = 2 BEGIN
		SELECT
			CAST(null as int)	as PageQuestionID			
			,cast(null as nvarchar(50))   as Question
			,cast(null as nvarchar(250))   as QuestionText
			,cast(null as nvarchar(50))   as QuestionName
			,cast(null as int)   as DisplayOrder			
			,cast(null as bit)   as DefaultShow	
			,CAST(null as int)	as TotalRows
		WHERE
			1 = 2  
	END	

	IF OBJECT_ID('tempdb..#ResultSet') IS NOT NULL
			DROP TABLE #ResultSet

	IF OBJECT_ID('tempdb..#ResultSetIndexed') IS NOT NULL
		DROP TABLE #ResultSetIndexed

	CREATE TABLE #ResultSet (
		PageQuestionID INT			
		,Question nvarchar(50)
		,QuestionText nvarchar(250)
		,QuestionName nvarchar(50)
		,DisplayOrder int
		,DefaultShow bit
		,TotalRows INT
		)

	CREATE TABLE #ResultSetIndexed (
		ID INT IDENTITY(1, 1)
		,PageQuestionID INT			
		,Question nvarchar(50)
		,QuestionText nvarchar(250)
		,QuestionName nvarchar(50)
		,DisplayOrder int
		,DefaultShow bit
		,TotalRows INT
		)	
		
		
	/* get raw data */
	INSERT INTO #ResultSet (
					PageQuestionID	
					,Question
					,QuestionText
					,QuestionName
					,DisplayOrder
					,DefaultShow
					,TotalRows
		)	
	SELECT distinct pq.Page_Question_ID AS PageQuestionID
		,q.Name
		,pq.QuestionText
		,pq.QuestionName
		,pq.DisplayOrder
		,pq.DefaultShow
		,0
	FROM Page_Question pq
	JOIN Question q ON q.Question_ID = pq.Question_ID
	WHERE pq.Page_ID = @PageID
	ORDER BY pq.DisplayOrder

	
	DECLARE @sql NVARCHAR(max)

	SET @sql = 'INSERT INTO #ResultSetIndexed
	(
		PageQuestionID	
		,Question
		,QuestionText
		,QuestionName
		,DisplayOrder
		,DefaultShow
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
			SELECT COUNT(PageQuestionID)
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

	SELECT 	PageQuestionID	
			,Question
			,QuestionText
			,QuestionName
			,DisplayOrder
			,DefaultShow
			,TotalRows
	FROM #ResultSetIndexed
	WHERE ID > @startIndex 
	AND ID < @endIndex	



END
GO
