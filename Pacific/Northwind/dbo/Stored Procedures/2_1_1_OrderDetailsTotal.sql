CREATE PROCEDURE [dbo].[2_1_1_OrderDetailsTotal]
AS
	SELECT SUM(od.Quantity * od.UnitPrice) as Totals
		FROM [Order Details] as od
