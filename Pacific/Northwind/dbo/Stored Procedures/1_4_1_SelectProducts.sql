CREATE PROCEDURE [dbo].[1_4_1_SelectProducts]
AS
	SELECT p.ProductName
	FROM Products as p
	WHERE p.ProductName LIKE '%cho_olade%'
