CREATE PROCEDURE [dbo].[2_1_3_SelectOrdersUniqueCustomers]
AS
	SELECT COUNT(DISTINCT o.CustomerID) as [Customers]
	FROM Orders as o
