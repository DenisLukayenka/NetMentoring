CREATE PROCEDURE [dbo].[1_3_1_SelectOrderDetailsByQuantity]
	@leftQuantityBound int = 3,
	@rightQuantityBound int = 10
AS
	SELECT DISTINCT od.OrderID
	FROM [Order Details] as od
		WHERE od.Quantity BETWEEN @leftQuantityBound AND @rightQuantityBound
RETURN 0
