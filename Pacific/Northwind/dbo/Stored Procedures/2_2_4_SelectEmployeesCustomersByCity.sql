CREATE PROCEDURE [dbo].[2_2_4_SelectEmployeesCustomersByCity]
AS
	SELECT c.CustomerID, emp.EmployeeID, c.City
	FROM Customers as c

	CROSS APPLY (
		SELECT e.EmployeeID
		FROM Employees as e
			WHERE e.City = c.City) as emp
