CREATE PROCEDURE [dbo].[_dev_TableToAutoMapperMappings] 
	@table_name SYSNAME
AS
SET NOCOUNT ON

DECLARE @temp TABLE (
	sort INT
	,code TEXT
	)

INSERT INTO @temp
SELECT  1
		,CHAR(9) + 'AutoMapper.Mapper.CreateMap<' + @table_name + ', ' + replace(@table_name, '_', '') + 'DTO>()'

INSERT INTO @temp
SELECT 2
	,CHAR(9) + CHAR(9) + '.ForMember(dest => dest.' + replace(COLUMN_NAME, '_', '')  + ', opt => opt.MapFrom(source => source.' + COLUMN_NAME + '))'
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @table_name
AND replace(COLUMN_NAME, '_', '') <> COLUMN_NAME
ORDER BY ORDINAL_POSITION

SELECT code
FROM @temp
ORDER BY sort

GO
