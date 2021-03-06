create PROCEDURE [dbo].[Questions_ListPossibleAnswerForGrid] 
	@QuestionID INT,
	@WhereClause NVARCHAR(max),
	@OrderClause NVARCHAR(128),
	@PageSize INT,
	@CurrentPageIndex INT
AS
BEGIN

	--supplying a data contract
	IF 1 = 2 BEGIN
		SELECT
			CAST(null as int)	as PossibleAnswerID			
			,cast(null as nvarchar(50))   as AnswerText
			,cast(null as nvarchar(20))   as AnswerValue
			,CAST(null as int)	as DisplayOrder
			,CAST(null as int)	as TotalRows
		WHERE
			1 = 2  
	END	

	IF OBJECT_ID('tempdb..#ResultSet') IS NOT NULL
			DROP TABLE #ResultSet

	IF OBJECT_ID('tempdb..#ResultSetIndexed') IS NOT NULL
		DROP TABLE #ResultSetIndexed

	CREATE TABLE #ResultSet (
		PossibleAnswerID INT			
		,AnswerText nvarchar(50)
		,AnswerValue nvarchar(20)
		,DisplayOrder int
		,TotalRows INT
		)

	CREATE TABLE #ResultSetIndexed (
		ID INT IDENTITY(1, 1)
		,PossibleAnswerID INT			
		,AnswerText nvarchar(50)
		,AnswerValue nvarchar(20)
		,DisplayOrder int
		,TotalRows INT
		)	
		
		
	/* get raw data */
	INSERT INTO #ResultSet (
					PossibleAnswerID	
					,AnswerText
					,AnswerValue
					,DisplayOrder
					,TotalRows
		)	
	SELECT distinct Question_Possible_Answer_ID
		,AnswerText
		,AnswerValue
		,DisplayOrder
		,0
	FROM Question_Possible_Answer
	WHERE Question_ID = @QuestionID
	ORDER BY DisplayOrder

	
	DECLARE @sql NVARCHAR(max)

	SET @sql = 'INSERT INTO #ResultSetIndexed
	(
		PossibleAnswerID	
		,AnswerText
		,AnswerValue
		,DisplayOrder
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
			SELECT COUNT(PossibleAnswerID)
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

	SELECT 	PossibleAnswerID	
			,AnswerText
			,AnswerValue
			,DisplayOrder
			,TotalRows
	FROM #ResultSetIndexed
	WHERE ID > @startIndex 
	AND ID < @endIndex	



END
GO
