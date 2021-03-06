CREATE PROCEDURE [dbo].[_dev_TableToDTO] 
	@table_name SYSNAME
AS
SET NOCOUNT ON

DECLARE @temp TABLE (
	sort INT
	,code TEXT
	)

INSERT INTO @temp
SELECT 1
	,'[Serializable]'

INSERT INTO @temp
SELECT 1
	,'public class ' + replace(@table_name, '_', '') + 'DTO'

INSERT INTO @temp
SELECT 2
	,'{'


INSERT INTO @temp
SELECT 13
	,CHAR(9) + '[DataMember]' + CHAR(13) +
	CHAR(9) + '[Display (Name = "' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]' + CHAR(13) +
	CASE 
		WHEN DATA_TYPE LIKE '%CHAR%'
			THEN 	
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must enter the ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 
				CASE WHEN CHARACTER_MAXIMUM_LENGTH is not null
					THEN CHAR(9) + '[MaxLength('+ cast(CHARACTER_MAXIMUM_LENGTH as varchar(5))  +', ErrorMessage= "' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + ' cannot be longer than ' + cast(CHARACTER_MAXIMUM_LENGTH as varchar(5))  + ' characters")]'
						 + CASE WHEN CHARACTER_MAXIMUM_LENGTH > 50
							THEN + CHAR(13) + CHAR(9) + '[UIHint("MultilineText")]'
							ELSE ''
						END   
				END + CHAR(13) + 
					CHAR(9) + 'public ' + 'string '
		WHEN DATA_TYPE LIKE '%TEXT%'
			THEN 
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must enter the ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 
				CASE WHEN CHARACTER_MAXIMUM_LENGTH is not null
					THEN CHAR(9) + '[MaxLength('+ cast(CHARACTER_MAXIMUM_LENGTH as varchar(5))  +', ErrorMessage= "' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + ' cannot be longer than ' + cast(CHARACTER_MAXIMUM_LENGTH as varchar(5))  + ' characters")]'
				END + CHAR(13) + 
					CHAR(9) + 'public ' + 'string '
		WHEN DATA_TYPE LIKE '%TINYINT%'
			THEN 
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must enter the ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 			
				CHAR(9) + 'public ' + 'byte' + (
				CASE 
					WHEN IS_NULLABLE = 'YES'
						THEN '? '
					ELSE ' '
					END
				)			
		WHEN DATA_TYPE LIKE '%INT%'
			THEN 
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must enter the ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 
				CHAR(9) + 'public ' + 'int' + (
				CASE 
					WHEN IS_NULLABLE = 'YES'
						THEN '? '
					ELSE ' '
					END
				)
		WHEN DATA_TYPE LIKE '%DATETIME%'
			THEN 
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must select a date for ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 
				CHAR(9) + 'public ' + 'DateTime' + (
				CASE 
					WHEN IS_NULLABLE = 'YES'
						THEN '? '
					ELSE ' '
					END
				)
		WHEN DATA_TYPE LIKE '%DATE%'
			THEN
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must select a date for ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 
				 CHAR(9) + 'public ' + 'DateTime' + (
				CASE 
					WHEN IS_NULLABLE = 'YES'
						THEN '? '
					ELSE ' '
					END
				)
		WHEN DATA_TYPE LIKE '%BINARY%'
			THEN CHAR(9) + 'public ' + 'byte[]'
		WHEN DATA_TYPE = 'BIT'
			THEN 
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must select an answer for ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 			
				CHAR(9) + 'public ' + 'bool' + (
				CASE 
					WHEN IS_NULLABLE = 'YES'
						THEN '? '
					ELSE ' '
					END
				)
		WHEN DATA_TYPE LIKE '%DECIMAL%'
			THEN 
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must select enter the ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 				
				CHAR(9) + 'public ' + 'decimal' + (
				CASE 
					WHEN IS_NULLABLE = 'YES'
						THEN '? '
					ELSE ' '
					END
				)
		WHEN DATA_TYPE LIKE '%MONEY%'
			THEN 
				CASE WHEN IS_NULLABLE = 'NO'
					THEN  CHAR(9) + '[Required( ErrorMessage= "You must select enter the ' + dbo.SpaceUpper(replace(COLUMN_NAME, '_', ' ')) + '")]'
					ELSE ''
				END + CHAR(13) + 				
				CHAR(9) + 'public ' + 'decimal' + (
				CASE 
					WHEN IS_NULLABLE = 'YES'
						THEN '? '
					ELSE ' '
					END
				)					
		ELSE CHAR(9) + 'public ' + 'object '
		END + replace(COLUMN_NAME, '_', '') + ' { get; set; }'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @table_name
ORDER BY ORDINAL_POSITION

INSERT INTO @temp
SELECT 16
	,CHAR(13) + CHAR(10) + '}'

SELECT code
FROM @temp
ORDER BY sort

GO
