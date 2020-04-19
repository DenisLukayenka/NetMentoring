CREATE PROCEDURE [dbo].[1_1_1_SelectOrders]
	@dateShipped Date,
	@viaShip int
AS
	SELECT o.OrderID, o.ShippedDate, o.ShipVia
	FROM Orders as o
		WHERE o.ShippedDate >= @dateShipped AND o.ShipVia >= @viaShip
