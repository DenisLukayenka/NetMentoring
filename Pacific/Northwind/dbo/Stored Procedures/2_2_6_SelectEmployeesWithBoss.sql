CREATE PROCEDURE [dbo].[2_2_6_SelectEmployeesWithBoss]
AS
	SELECT e.EmployeeID, (e.LastName + e.FirstName) as 'Employee name',  m.FullName as 'Manager name'
	FROM Employees as e

	CROSS APPLY (
		SELECT em.EmployeeID, (em.LastName + em.FirstName) as FullName
		FROM Employees as em
			WHERE e.ReportsTo = em.EmployeeID
	) as m
