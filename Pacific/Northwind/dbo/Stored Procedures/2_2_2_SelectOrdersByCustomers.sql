CREATE PROCEDURE [dbo].[2_2_2_SelectOrdersByCustomers]
AS
	SELECT (
		SELECT e.LastName + e.FirstName
		FROM Employees as e
			WHERE e.EmployeeID = o.EmployeeID) as Seller, COUNT(o.EmployeeID) as Amount
	FROM Orders as o

	GROUP BY o.EmployeeID
	HAVING o.EmployeeID IS NOT NULL

	ORDER BY COUNT(o.EmployeeID) DESC
