IF NOT EXISTS (SELECT * FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[CreditCards]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[CreditCards](
		[CreditCardID][int] IDENTITY(1,1) NOT NULL,
		[CardNumber] varchar(20) NOT NULL,
		[LimitDate][datetime] NOT NULL,
		[Holder] nvarchar(35) NOT NULL,
		[EmployeeID][int] NULL,
		CONSTRAINT "PK_CreditCards" PRIMARY KEY CLUSTERED ([CreditCardID] ASC),
		CONSTRAINT "FK_CreditCards_Employees" FOREIGN KEY
		(
			"EmployeeID"
		) REFERENCES [dbo].[Employees] (
			"EmployeeID"
		)
	)
	CREATE INDEX [EmployeeID] ON "dbo"."CreditCards"("EmployeeID")
END
	