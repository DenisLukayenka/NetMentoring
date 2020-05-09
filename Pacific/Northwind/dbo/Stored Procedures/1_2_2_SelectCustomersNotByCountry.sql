CREATE PROCEDURE [dbo].[1_2_2_SelectCustomersNotByCountry]
AS
	SELECT c.ContactName, c.Country
	FROM Customers as c
		WHERE c.Country NOT IN ('USA', 'Canada')
	ORDER BY c.ContactName