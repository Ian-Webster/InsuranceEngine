CREATE PROCEDURE [dbo].[PageQuestions_ListPageQuestionValidationForGrid] 
	@PageQuestionID INT,
	@WhereClause NVARCHAR(max),
	@OrderClause NVARCHAR(128),
	@PageSize INT,
	@CurrentPageIndex INT
AS
BEGIN

	--supplying a data contract
	IF 1 = 2 BEGIN
		SELECT
			CAST(null as int)	as PageQuestionValidationID			
			,cast(null as nvarchar(1024))   as ValidationMessage
			,cast(null as nvarchar(100))   as ValidationType
			,CAST(null as int)	as TotalRows
		WHERE
			1 = 2  
	END	

	IF OBJECT_ID('tempdb..#ResultSet') IS NOT NULL
			DROP TABLE #ResultSet

	IF OBJECT_ID('tempdb..#ResultSetIndexed') IS NOT NULL
		DROP TABLE #ResultSetIndexed

	CREATE TABLE #ResultSet (
		PageQuestionValidationID INT			
		,ValidationMessage nvarchar(1024)
		,ValidationType nvarchar(100)
		,TotalRows INT
		)

	CREATE TABLE #ResultSetIndexed (
		ID INT IDENTITY(1, 1)
		,PageQuestionValidationID INT			
		,ValidationMessage nvarchar(1024)
		,ValidationType nvarchar(100)
		,TotalRows INT
		)	
		
		
	/* get raw data */
	INSERT INTO #ResultSet (
					PageQuestionValidationID	
					,ValidationMessage
					,ValidationType
					,TotalRows
		)	
	SELECT distinct pqv.Page_Question_Validation_ID AS PageQuestionValidationID
		,pqv.ErrorMessage
		,vt.Name
		,0
	FROM Page_Question_Validation pqv
	JOIN Validation_Type vt ON vt.Validation_Type_ID = pqv.Validation_Type_ID
	WHERE pqv.Page_Question_ID = @PageQuestionID
	ORDER BY pqv.ErrorMessage

	
	DECLARE @sql NVARCHAR(max)

	SET @sql = 'INSERT INTO #ResultSetIndexed
	(
		PageQuestionValidationID	
		,ValidationMessage
		,ValidationType
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
			SELECT COUNT(PageQuestionValidationID)
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

	SELECT 	PageQuestionValidationID	
			,ValidationMessage
			,ValidationType
			,TotalRows
	FROM #ResultSetIndexed
	WHERE ID > @startIndex 
	AND ID < @endIndex	



END
GO
