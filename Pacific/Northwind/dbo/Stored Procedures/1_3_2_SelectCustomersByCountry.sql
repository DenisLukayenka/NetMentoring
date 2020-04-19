CREATE PROCEDURE [dbo].[1_3_2_SelectCustomersByCountry]
AS
	SELECT c.CustomerID, c.Country
	FROM Customers as c
		WHERE LOWER(LEFT(c.Country, 1)) BETWEEN 'b' AND 'g'

	ORDER BY c.Country
