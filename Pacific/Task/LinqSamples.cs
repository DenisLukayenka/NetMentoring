// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("Выборка всех клиентов, чей суммарный оборот превосходит некоторую величину.")]
		public void Linq1()
		{
			int amount = 100000;

			var customersSum =
				from c in dataSource.Customers
				where c.Orders.Sum(o => o.Total) > amount
				select new
				{
					Id = c.CustomerID,
					OrdersTotal = c.Orders.Sum(o => o.Total)
				};

			foreach (var c in customersSum)
			{
				ObjectDumper.Write(c);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе")]

		public void Linq2()
		{
			var customerSuppliers =
				from c in dataSource.Customers
				select new
				{
					CustomerID = c.CustomerID,
					Suppliers = from s in dataSource.Suppliers
								where s.City == c.City && s.Country == c.Country
								select new
								{
									Name = s.SupplierName,
									Address = s.Address
								}
				};

			foreach (var c in customerSuppliers)
			{
				ObjectDumper.Write($"CustomerID {c.CustomerID}, Suppliers:");

				foreach (var supplier in c.Suppliers)
				{
					ObjectDumper.Write($"\tName: {supplier.Name}, Address: {supplier.Address}.");
				}
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 3")]
		[Description("Найдите всех клиентов, у которых были заказы, превосходящие по сумме величин")]
		public void Linq3()
		{
			decimal x = 10000;

			var customers =
				from c in dataSource.Customers
				where c.Orders.Any(o => o.Total > x)
				select c;

			foreach (var customer in customers)
			{
				ObjectDumper.Write(customer);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 4")]
		[Description("Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами")]
		public void Linq4()
		{
			var customers =
				from c in dataSource.Customers
				where c.Orders.Any()
				select new
				{
					CustomerID = c.CustomerID,
					StartDate = (from order in c.Orders
								 orderby order.OrderDate
								 select order).First().OrderDate
				};

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"CustomerID: {customer.CustomerID}, Date: {customer.StartDate}");
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 5")]
		[Description("Сделайте предыдущее задание, но выдайте список отсортированным по году, месяцу, оборотам клиента")]
		public void Linq5()
		{
			var customers =
				from c in dataSource.Customers
				where c.Orders.Any()
				orderby c.Orders.Min(o => o.OrderDate.Year),
				c.Orders.Min(o => o.OrderDate.Month),
				c.Orders.Sum(o => o.Total) descending,
				c.CustomerID

				select new
				{
					CustomerID = c.CustomerID,
					Month = c.Orders.Min(o => o.OrderDate.Month),
					Year = c.Orders.Min(o => o.OrderDate.Year),
					OrdersTotal = c.Orders.Sum(o => o.Total)
				};


			foreach (var customer in customers)
			{
				ObjectDumper.Write($"CustomerID: {customer.CustomerID}, Month: {customer.Month}, Year: {customer.Year}, OrdersTotal: {customer.OrdersTotal}");
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 6")]
		[Description("Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора")]
		public void Linq6()
		{
			var customers =
				from c in dataSource.Customers
				where c.PostalCode != null && c.PostalCode.Any(code => code < '0' || code > '9')
					  || string.IsNullOrWhiteSpace(c.Region)
					  || c.Phone.Substring(0, 1) != "("
				select c;

			foreach (var customer in customers)
			{
				ObjectDumper.Write(customer);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 7")]
		[Description("Сгруппируйте все продукты по категориям, внутри – по наличию на складе, внутри последней группы отсортируйте по стоимости")]
		public void Linq7()
		{
			var products =
				from p in dataSource.Products
				group p by p.Category
				into productsCategory
				select new
				{
					Category = productsCategory.Key,
					Products = from p in productsCategory

							   group p by p.UnitsInStock into productsUnits
							   select new
							   {
								   ProductUnits = productsUnits.Key,

								   Price = from p in productsUnits
										   orderby p.UnitPrice
										   group p by p.UnitPrice
										   into productsId

										   select new
										   {
											   Price = productsId.Key,
											   Product = from p in productsId select p.ProductID
										   }
							   }
				};

			foreach (var p in products)
			{
				ObjectDumper.Write($"Category: {p.Category}");

				foreach (var product in p.Products)
				{
					ObjectDumper.Write($"Units: {product.ProductUnits}");

					foreach (var productPrice in product.Price)
					{
						foreach (var pproductPrice in productPrice.Product)
						{
							ObjectDumper.Write(pproductPrice);
							ObjectDumper.Write(productPrice.Price);
							ObjectDumper.Write("\n");
						}
					}
				}
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 8")]
		[Description("Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами")]
		public void Linq8()
		{
			var products = from p in dataSource.Products
						   group p by p.UnitPrice < 20 ? "Cheap"
							   : p.UnitPrice < 40 ? "Middle"
							   : "Nutella";

			foreach (var product in products)
			{
				ObjectDumper.Write($"Product group: {product.Key}");

				foreach (var p in product)
				{
					ObjectDumper.Write($"\tProduct name: {p.ProductName}, price: {p.UnitPrice}");
				}
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 9")]
		[Description("Рассчитайте среднюю прибыльность каждого города и среднюю интенсивность")]
		public void Linq9()
		{
			var cityInfo = from c in dataSource.Customers
						   group c by c.City into customerCity
						   select new
						   {
							   City = customerCity.Key,
							   AvgSum = customerCity.Average(p => p.Orders.Sum(o => o.Total)),
							   Intensity = customerCity.Average(p => p.Orders.Length)
						   };

			foreach (var info in cityInfo)
			{
				ObjectDumper.Write($"City: {info.City}, Average sum of orders: {info.AvgSum}, average intensity: {info.Intensity}");
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 10")]
		[Description("Сделайте среднегодовую статистику активности клиентов по месяцам, статистику по годам, по годам и месяцам")]
		public void Linq10()
		{
			var stat = from c in dataSource.Customers
					   select new
					   {
						   CustomerId = c.CustomerID,

						   MonthStat = from o in c.Orders
									   group o by o.OrderDate.Month
							   into customerOrderMonth
									   select new
									   {
										   Month = customerOrderMonth.Key,
										   OrderCount = customerOrderMonth.Count()
									   },

						   YearStat = from o in c.Orders
									  group o by o.OrderDate.Year
							   into customerOrderYear
									  select new
									  {
										  Year = customerOrderYear.Key,
										  OrderCount = customerOrderYear.Count()
									  },

						   YearMonthStat = from o in c.Orders
										   group o by new { o.OrderDate.Year, o.OrderDate.Month }
							   into customerOrderYearMonth
										   select new
										   {
											   Year = customerOrderYearMonth.Key.Year,
											   Month = customerOrderYearMonth.Key.Month,
											   OrderCount = customerOrderYearMonth.Count()
										   }
					   };

			foreach (var info in stat)
			{
				ObjectDumper.Write($"CustomerId: {info.CustomerId}.");

				ObjectDumper.Write("\tMonth statistic:");
				foreach (var mStat in info.MonthStat)
				{
					ObjectDumper.Write($"\t\tMonth: {mStat.Month}, Order count: {mStat.OrderCount};");
				}


				ObjectDumper.Write($"\tYear statistic: ");
				foreach (var mStat in info.YearStat)
				{
					ObjectDumper.Write($"\t\tYear: {mStat.Year}, Order count: {mStat.OrderCount};");
				}

				ObjectDumper.Write($"\tYear and month statistic: ");

				foreach (var mStat in info.YearMonthStat)
				{
					ObjectDumper.Write($"\t\tYear: {mStat.Year}, Month: {mStat.Month}, Order count: {mStat.OrderCount};");
				}
			}
		}
	}
}
