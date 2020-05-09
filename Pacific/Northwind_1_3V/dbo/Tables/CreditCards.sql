CREATE TABLE [dbo].[CreditCards] (
    [CreditCardID] INT           IDENTITY (1, 1) NOT NULL,
    [CardNumber]   VARCHAR (20)  NOT NULL,
    [LimitDate]    DATETIME      NOT NULL,
    [Holder]       NVARCHAR (35) NOT NULL,
    [EmployeeID]   INT           NULL,
    CONSTRAINT [PK_CreditCards] PRIMARY KEY CLUSTERED ([CreditCardID] ASC),
    CONSTRAINT [FK_CreditCards_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);


GO
CREATE NONCLUSTERED INDEX [EmployeeID]
    ON [dbo].[CreditCards]([EmployeeID] ASC);

