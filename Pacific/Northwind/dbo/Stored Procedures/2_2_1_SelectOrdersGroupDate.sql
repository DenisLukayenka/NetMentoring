CREATE PROCEDURE [dbo].[2_2_1_SelectOrdersGroupDate]
AS
	SELECT YEAR(o.OrderDate) as year, COUNT(o.OrderDate) as count
	FROM Orders as o

	GROUP BY YEAR(o.OrderDate)