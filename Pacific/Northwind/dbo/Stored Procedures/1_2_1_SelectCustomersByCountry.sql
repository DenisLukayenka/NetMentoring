CREATE PROCEDURE [dbo].[1_2_1_SelectCustomersByCountry]
AS
	SELECT c.ContactName, c.Country
	FROM Customers as c
		WHERE c.Country IN ('USA', 'Canada')
	ORDER BY c.ContactName, c.Country
