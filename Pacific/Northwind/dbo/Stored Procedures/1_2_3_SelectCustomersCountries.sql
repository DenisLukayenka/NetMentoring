CREATE PROCEDURE [dbo].[1_2_3_SelectCustomersCountries]
AS
	SELECT DISTINCT c.Country
	FROM Customers as c
	ORDER BY c.Country DESC
