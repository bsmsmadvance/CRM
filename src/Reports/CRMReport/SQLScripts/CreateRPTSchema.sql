
IF NOT EXISTS ( SELECT  *
                FROM    sys.schemas
                WHERE   name = N'RPT' ) 
    EXEC('CREATE SCHEMA RPT');
GO