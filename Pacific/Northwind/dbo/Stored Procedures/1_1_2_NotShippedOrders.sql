CREATE PROCEDURE [dbo].[1_1_2_NotShippedOrders]
AS
	SELECT o.OrderID, CASE
		WHEN o.ShippedDate IS NULL THEN 'Not Shipped'
		ELSE o.ShippedDate
	END
	FROM Orders as o
		WHERE o.ShippedDate IS NULL
