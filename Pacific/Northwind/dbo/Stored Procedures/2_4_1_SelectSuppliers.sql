CREATE PROCEDURE [dbo].[2_4_1_SelectSuppliers]
AS
	SELECT s.CompanyName
	FROM Suppliers as s

	WHERE s.SupplierID in (
		SELECT p.SupplierID
		FROM Products as p
			WHERE p.UnitsInStock = 0
	)