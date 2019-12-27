SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fnFormatDateLongTH
(
	@incomingDate DateTime
)

	RETURNS nvarchar(50)

AS BEGIN
	DECLARE @convertDateLong nvarchar(50)
	DECLARE @convertDay nvarchar(50)
	DECLARE @convertMonth nvarchar(50)
	DECLARE @convertYear nvarchar(50)
	SET @convertDay =  DAY(@incomingDate);
	SET @convertMonth = (select CASE 
								WHEN MONTH(@incomingDate) = 1 then N'มกราคม'
								WHEN MONTH(@incomingDate) = 2 then N'กุมภาพันธ์'  
								WHEN MONTH(@incomingDate) = 3 then N'มีนาคม' 
								WHEN MONTH(@incomingDate) = 4 then N'เมษายน' 
								WHEN MONTH(@incomingDate) = 5 then N'พฤกษภาคม' 
								WHEN MONTH(@incomingDate) = 6 then N'มิถุนายน' 
								WHEN MONTH(@incomingDate) = 7 then N'กรกฏาคม' 
								WHEN MONTH(@incomingDate) = 8 then N'สิงหาคม' 
								WHEN MONTH(@incomingDate) = 9 then N'กันยายน' 
								WHEN MONTH(@incomingDate) = 10 then N'ตุลาคม' 
								WHEN MONTH(@incomingDate) = 11 then N'พฤศจิกายน' 
								else N'ธันวาคม'
						END);
	SET @convertYear = (YEAR(@incomingDate) + 543)
	SET @convertDateLong = @convertDay + ' ' + @convertMonth + ' ' + @convertYear
	RETURN @convertDateLong
END


/*
Function (DateTimeVar d) 
Switch (day(d)=1, "01",day(d)=2,"02" ,day(d)=3,"03" ,day(d)=4,"04" ,day(d)=5,"05" ,
day(d)=6,"06" ,day(d)=7,"07" ,day(d)=8,"08" ,day(d)=9,"09" ,day(d)=10,"10",day(d)=11,"11",
day(d)=12,"12" ,day(d)=13,"13" ,day(d)=14,"14" ,day(d)=15,"15" ,day(d)=16,"16",day(d)=17,"17",
day(d)=18,"18" ,day(d)=19,"19" ,day(d)=20,"20" ,day(d)=21,"21" ,day(d)=22,"22",day(d)=23,"23",
day(d)=24,"24" ,day(d)=25,"25" ,day(d)=26,"26" ,day(d)=27,"27" ,day(d)=28,"28",day(d)=29,"29",
day(d)=30,"30" ,day(d)=31,"31")
& "/" & 
Switch (month(d)=1, "01",month(d)=2,"02" ,month(d)=3,"03" ,month(d)=4,"04" ,month(d)=5,"05" ,
month(d)=6,"06" ,month(d)=7,"07" ,month(d)=8,"08" ,month(d)=9,"09" ,month(d)=10,"10" 
,month(d)=11,"11" ,month(d)=12,"12")
& "/" &
CStr((year(d)+543),"####")
*/