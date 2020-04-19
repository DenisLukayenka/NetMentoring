CREATE PROCEDURE [dbo].[2_3_1_SelectEmployeesByRegion]
	@regionName nchar(50) = 'Western'
AS
	SELECT DISTINCT e.EmployeeID, (e.LastName + e.FirstName) as 'Name'
	FROM Employees as e

	INNER JOIN EmployeeTerritories as et ON et.EmployeeID = e.EmployeeID
	INNER JOIN Territories as t ON t.TerritoryID = et.TerritoryID
	INNER JOIN Region as r ON r.RegionID = t.RegionID

	WHERE r.RegionDescription = @regionName
