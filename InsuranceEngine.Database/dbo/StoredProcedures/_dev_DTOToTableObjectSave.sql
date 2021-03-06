CREATE PROCEDURE [dbo].[_dev_DTOToTableObjectSave]
	@table_name SYSNAME
AS
SET NOCOUNT ON

DECLARE @temp TABLE (
	sort INT
	,code TEXT
	)


INSERT INTO @temp
SELECT 13
	,CHAR(9) + 'dao.' + COLUMN_NAME + ' = dto.' + replace(COLUMN_NAME, '_', '') + ';'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @table_name
ORDER BY ORDINAL_POSITION

SELECT code
FROM @temp
ORDER BY sort

GO
