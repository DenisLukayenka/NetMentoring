CREATE PROCEDURE [dbo].[2_1_2_SelectOrdersNotShipped]
AS
	SELECT COUNT(o.ShippedDate) as [Not shipped count]
	FROM Orders as o
