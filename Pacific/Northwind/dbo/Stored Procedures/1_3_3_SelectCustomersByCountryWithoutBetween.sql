CREATE PROCEDURE [dbo].[1_3_3_SelectCustomersByCountryWithoutBetween]
AS
	SELECT c.CustomerID, c.Country
	FROM Customers as c
		WHERE LOWER(LEFT(c.Country, 1)) IN ('b', 'c', 'd', 'e', 'f', 'g')

	ORDER BY c.Country
