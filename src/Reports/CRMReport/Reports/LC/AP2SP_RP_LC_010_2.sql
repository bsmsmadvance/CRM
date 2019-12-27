SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[AP2SP_RP_LC_010_2] '10025AA07020009'
ALTER PROCEDURE [dbo].[AP2SP_RP_LC_010_2]
	@ContractNumber  nvarchar(50),
	@UserName   nvarchar(150)
AS

SELECT	'SaveDate' = '' --SaveDate
        ,'Reward' = '' --Reward
        ,'ContractID' = '' --ContractID
        ,'ProductID' = '' --ProductID
        ,'ContactID' = '' --ContactID
  
FROM	[SAL].[Agreement] --This is temp table actual table start from below
        --[ICON_Payment_HousePayment] HP  

--WHERE (HP.ContractID = @ContractNumber) AND Reward <> 0

GO
