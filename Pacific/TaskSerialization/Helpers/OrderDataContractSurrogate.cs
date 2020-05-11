using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using TaskSerialization.DB;

namespace TaskSerialization.Helpers
{
	public class OrderDataContractSurrogate : IDataContractSurrogate
	{
		public Type GetDataContractType(Type type)
		{
			return type;
		}

		public object GetObjectToSerialize(object obj, Type targetType)
		{
			switch (obj)
			{
				case Order order:
					return this.CopyOrderProxy(order);
				case Customer customer:
					return this.CopyCustomerProxy(customer);
				case Employee employee:
					return this.CopyEmployeeProxy(employee);
				case Order_Detail od:
					return this.CopyOrderDetailProxy(od);
				case Shipper shipper:
					return this.CopyShipperProxy(shipper);
				default:
					return obj;
			}
			
		}

		public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
		{
			return typeDeclaration;
		}

		public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
		{
		}

		public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
		{
			return null;
		}

		public object GetCustomDataToExport(Type clrType, Type dataContractType)
		{
			return null;
		}

		public object GetDeserializedObject(object obj, Type targetType)
		{
			return obj;
		}

		public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
		{
			throw new NotImplementedException();
		}

		protected object CopyOrderProxy(Order order)
		{
			return new Order {
				OrderID = order.OrderID,
				Customer = order.Customer,
				CustomerID = order.CustomerID,

				Employee = order.Employee,
				EmployeeID = order.EmployeeID,

				OrderDate = order.OrderDate,
				RequiredDate = order.RequiredDate,
				ShippedDate = order.ShippedDate,

				ShipVia = order.ShipVia,
				Freight = order.Freight,
				ShipName = order.ShipName,
				ShipAddress = order.ShipAddress,
				ShipCity = order.ShipCity,
				ShipRegion = order.ShipRegion,
				ShipPostalCode = order.ShipPostalCode,
				ShipCountry = order.ShipCountry,

				Order_Details = order.Order_Details,
				Shipper = order.Shipper,
			};
		}

		protected object CopyCustomerProxy(Customer customer)
		{
			return new Customer
			{
				CustomerID = customer.CustomerID,

				CompanyName = customer.CompanyName,
				ContactName = customer.ContactName,
				ContactTitle = customer.ContactTitle,

				Address = customer.Address,
				City = customer.City,
				Region = customer.Region,
				PostalCode = customer.PostalCode,

				Country = customer.Country,
				Phone = customer.Phone,
				Fax = customer.Fax,

				CustomerDemographics = customer.CustomerDemographics,
				Orders = new List<Order>(),
			};
		}

		protected object CopyEmployeeProxy(Employee employee)
		{
			return new Employee
			{
				EmployeeID = employee.EmployeeID,

				LastName = employee.LastName,
				FirstName = employee.FirstName,

				Title = employee.Title,
				TitleOfCourtesy = employee.TitleOfCourtesy,

				BirthDate = employee.BirthDate,
				HireDate = employee.HireDate,

				Address = employee.Address,
				City = employee.City,
				Region = employee.Region,
				PostalCode = employee.PostalCode,
				Country = employee.Country,

				HomePhone = employee.HomePhone,
				Extension = employee.Extension,

				Photo = employee.Photo,
				Notes = employee.Notes,
				ReportsTo = employee.ReportsTo,
				PhotoPath = employee.PhotoPath,

				Employee1 = null,
				Employees1 = new List<Employee>(),
				Orders = null,
				Territories = null,
			};
		}

		protected object CopyShipperProxy(Shipper shipper)
		{
			return new Shipper
			{
				ShipperID = shipper.ShipperID,

				CompanyName = shipper.CompanyName,
				Phone = shipper.Phone,
				Orders = new List<Order>(),
			};
		}

		protected object CopyOrderDetailProxy(Order_Detail orderDetail)
		{
			return new Order_Detail
			{
				OrderID = orderDetail.OrderID,
				ProductID = orderDetail.ProductID,

				UnitPrice = orderDetail.UnitPrice,
				Quantity = orderDetail.Quantity,
				Discount = orderDetail.Discount,
				Order = null,
				Product = null,
			};
		}

	}
}
