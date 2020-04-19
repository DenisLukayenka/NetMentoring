CREATE PROCEDURE [dbo].[2_4_2_SelectEmployeesByOrdersCount]
AS
	SELECT e.EmployeeID, e.FirstName, e.LastName
	FROM Employees as e

	WHERE e.EmployeeID in (
		SELECT o.EmployeeID
		FROM Orders as o
		
		GROUP BY o.EmployeeID
		HAVING COUNT(o.OrderID) > 150)