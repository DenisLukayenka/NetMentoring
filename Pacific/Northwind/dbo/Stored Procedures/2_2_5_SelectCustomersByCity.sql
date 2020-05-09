CREATE PROCEDURE [dbo].[2_2_5_SelectCustomersByCity]
AS
	SELECT lCust.ContactName as [Left Customer], rCust.ContactName as [Right Customer], lCust.City
	FROM Customers as lCust

	CROSS APPLY (
		SELECT rCust.ContactName
		FROM Customers as rCust
			WHERE 
				rCust.CustomerID <> lCust.CustomerID AND 
				rCust.City = lCust.City
	) as rCust
