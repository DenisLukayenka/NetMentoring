CREATE PROCEDURE [dbo].[1_1_3_SelectOrdersByShipDate]
	@dateShipped Date
AS
	SELECT o.OrderID as [Order Number], (CASE
		WHEN o.ShippedDate IS NULL THEN 'Not Shipped'
		ELSE CONVERT(varchar, o.ShippedDate, 103)
	END) as [Shipped Date]
	FROM Orders as o
		WHERE o.ShippedDate > @dateShipped OR o.ShippedDate IS NULL
