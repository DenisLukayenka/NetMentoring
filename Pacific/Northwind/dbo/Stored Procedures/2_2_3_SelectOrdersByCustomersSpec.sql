CREATE PROCEDURE [dbo].[2_2_3_SelectOrdersByCustomersSpec]
AS
	SELECT o.EmployeeID, o.CustomerID, COUNT(o.OrderID) as 'Amount'
	FROM Orders as o
		WHERE YEAR(o.ShippedDate) = 1998

	GROUP BY o.EmployeeID, o.CustomerID
