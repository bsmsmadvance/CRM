SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[dbo].[AP2SP_PF_AG_001_2] '10133AA00184'



CREATE PROC [dbo].[PF_MD_001_2]
	@ProjectNo nvarchar(50)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
/* Declare @t Table (Period nvarchar(50),AMT money,AMTTtext nvarchar(100),DueDate nvarchar(50))
Declare @StartPeriod int
Declare @StartDate DateTime
Declare @EndDate DateTime
Declare @StartMoney money

Set @StartPeriod=0

DECLARE @ContractNumber2 NVARCHAR(50),@Period Int,@DueDate DateTime,@PayableAmount money,@IsExtra int
DECLARE curs CURSOR FOR 

	SELECT ContractNumber,Period,DueDate,PayableAmount,IsExtra FROM ICON_EntForms_AgreementPeriod WITH (NOLOCK)
	Where Period>0 and PaymentType = '6' and ContractNumber = @ContractNumber
	order by Period

OPEN curs;

FETCH NEXT FROM curs INTO @ContractNumber2,@Period,@DueDate,@PayableAmount,@IsExtra
WHILE (@@FETCH_STATUS = 0)
	BEGIN
print(@StartPeriod)
	IF(@StartPeriod=0)
	BEGIN 
		Set @StartPeriod=@Period
		Set @StartDate=@DueDate
		Set @StartMoney=@PayableAmount
	END
	IF(@IsExtra=1)
	BEGIN
		IF(@Period<>@StartPeriod)
		BEGIN
			IF(@Period-1<>@StartPeriod)
			Begin
				Insert Into @t(Period,AMT,AMTTtext,DueDate)Values(RIGHT('00'+Convert(nvarchar(10),@StartPeriod),3)+' - '+RIGHT('00'+Convert(nvarchar(10),(@Period-1)),3),@StartMoney,[dbo].[fnBHT_BahtText](@StartMoney),[dbo].[FormatDatetime]('TH','dd mmmm yyyy',@StartDate)+' - '+[dbo].[FormatDatetime]('TH','dd mmmm yyyy',@EndDate))
			End
				Else Insert Into @t(Period,AMT,AMTTtext,DueDate)Values(RIGHT('00'+Convert(nvarchar(10),@StartPeriod),3),@StartMoney, [dbo].[fnBHT_BahtText](@StartMoney),[dbo].[FormatDatetime]('TH','dd mmmm yyyy',@StartDate))
			END
			Insert Into @t(Period,AMT,AMTTtext,DueDate)Values(RIGHT('00'+Convert(nvarchar(10),@Period),3),@PayableAmount,[dbo].[fnBHT_BahtText](@PayableAmount),[dbo].[FormatDatetime]('TH','dd mmmm yyyy',@DueDate))	
			Set @StartPeriod=0
		END
		Set @EndDate=@DueDate
		FETCH NEXT FROM curs INTO @ContractNumber2,@Period,@DueDate,@PayableAmount,@IsExtra
	END
CLOSE curs;
DEALLOCATE curs
IF(@IsExtra<>1)
BEGIN
	IF(@Period<>@StartPeriod)
		
		BEGIN
			Insert Into @t(Period,AMT,AMTTtext,DueDate)Values(RIGHT('00'+convert(nvarchar(10),@StartPeriod),3)+' - '+RIGHT('00'+convert(nvarchar(10),@Period),3),@StartMoney,[dbo].[fnBHT_BahtText](@StartMoney),[dbo].[FormatDatetime]('TH','dd mmmm yyyy',@StartDate)+' - '+[dbo].[FormatDatetime]('TH','dd mmmm yyyy',@DueDate))
		END
	ELSE
		BEGIN
			Insert Into @t(Period,AMT,AMTTtext,DueDate)Values(RIGHT('00'++convert(nvarchar(10),@Period),3),@PayableAmount,[dbo].[fnBHT_BahtText](@PayableAmount),[dbo].[FormatDatetime]('TH','dd mmmm yyyy',@DueDate))
	END	
END


--งวดดาวน์พิเศษ AP จ่ายให้
INSERT INTO @t
SELECT RIGHT('00'+convert(nvarchar(10),(SELECT MAX(Period)+1 FROM ICON_EntForms_AgreementPeriod Where Period>0 and PaymentType = '6' and ContractNumber = @ContractNumber)),3),
	LastDownAmount, [dbo].[fnBHT_BahtText](LastDownAmount), [dbo].[FormatDatetime]('TH','dd mmmm yyyy',LastDownDate)
FROM dbo.ICON_EntForms_Agreement WITH (NOLOCK)
WHERE  ContractNumber = @ContractNumber
	AND IsAPPay = 1;


if(exists(SELECT * from @t))
SELECT * from @t
else select '9999' Period,0 AMT,'Test' AMTTtext,NULL as DueDate */

SELECT 'Period' = ''
        , 'AMT' = ''
        , 'AMTTtext' = ''
        , 'DueDate' = ''
        FROM [PRJ].[Project] P WITH (NOLOCK)
        WHERE P.ProjectNo = @ProjectNo


GO
