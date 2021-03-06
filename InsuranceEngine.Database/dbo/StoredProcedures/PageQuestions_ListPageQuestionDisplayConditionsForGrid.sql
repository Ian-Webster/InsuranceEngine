CREATE PROCEDURE [dbo].[PageQuestions_ListPageQuestionDisplayConditionsForGrid] 
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
			CAST(null as int)	as PageQuestionConditionalDisplayID			
			,cast(null as nvarchar(50))   as SourcePageQuestionName
			,cast(null as nvarchar(50))   as TargetPageQuestionName
			,cast(null as nvarchar(50))   as TriggerAnswer
			,cast(null as bit)	as Hide
			,CAST(null as int)	as TotalRows
		WHERE
			1 = 2  
	END	

	IF OBJECT_ID('tempdb..#ResultSet') IS NOT NULL
			DROP TABLE #ResultSet

	IF OBJECT_ID('tempdb..#ResultSetIndexed') IS NOT NULL
		DROP TABLE #ResultSetIndexed

	CREATE TABLE #ResultSet (
		PageQuestionConditionalDisplayID INT			
		,SourcePageQuestionName nvarchar(50)
		,TargetPageQuestionName nvarchar(50)
		,TriggerAnswer nvarchar(50)
		,Hide bit
		,TotalRows INT
		)

	CREATE TABLE #ResultSetIndexed (
		ID INT IDENTITY(1, 1)
		,PageQuestionConditionalDisplayID INT			
		,SourcePageQuestionName nvarchar(50)
		,TargetPageQuestionName nvarchar(50)
		,TriggerAnswer nvarchar(50)
		,Hide bit
		,TotalRows INT
		)	
		
		
	/* get raw data */
	INSERT INTO #ResultSet (
					PageQuestionConditionalDisplayID	
					,SourcePageQuestionName
					,TargetPageQuestionName
					,TriggerAnswer
					,Hide
					,TotalRows
		)	
	SELECT distinct pqc.Page_Question_Conditional_Display_ID
		,sourceQuestion.QuestionName
		,targetQuestion.QuestionName
		,answer.AnswerText
		,pqc.Hide
		,0
	FROM Page_Question_Conditional_Display pqc
	JOIN Page_Question sourceQuestion on pqc.Source_Page_Question_ID = sourceQuestion.Page_Question_ID
	JOIN Page_Question targetQuestion on pqc.Target_Page_Question_ID = targetQuestion.Page_Question_ID
	JOIN Question_Possible_Answer answer on answer.Question_Possible_Answer_ID = pqc.Trigger_Question_Possible_Answer_ID
	WHERE pqc.Target_Page_Question_ID = @PageQuestionID
	ORDER BY sourceQuestion.QuestionName

	
	DECLARE @sql NVARCHAR(max)

	SET @sql = 'INSERT INTO #ResultSetIndexed
	(
		PageQuestionConditionalDisplayID	
		,SourcePageQuestionName
		,TargetPageQuestionName
		,TriggerAnswer
		,Hide
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
			SELECT COUNT(PageQuestionConditionalDisplayID)
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

	SELECT 	PageQuestionConditionalDisplayID	
			,SourcePageQuestionName
			,TargetPageQuestionName
			,TriggerAnswer
			,Hide
			,TotalRows
	FROM #ResultSetIndexed
	WHERE ID > @startIndex 
	AND ID < @endIndex	



END
GO
