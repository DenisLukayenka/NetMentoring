using System;
using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;
using TaskSerialization.DB;

namespace TaskSerialization.Helpers
{
	public class OrderDetailSurrogate : ISerializationSurrogate
	{
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if(obj is Order_Detail od)
			{
				if(context.Context is IObjectContextAdapter contextAdapter)
				{
					contextAdapter.ObjectContext.LoadProperty(od, o => o.Order);
					contextAdapter.ObjectContext.LoadProperty(od, o => o.Product);
				}

				info.AddValue("OrderID", od.OrderID);
				info.AddValue("ProductID", od.ProductID);
				info.AddValue("UnitPrice", od.UnitPrice);
				info.AddValue("Quantity", od.Quantity);
				info.AddValue("Discount", od.Discount);

				info.AddValue("Product", od.Product);
				info.AddValue("Order", od.Order);
			}
		}

		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			if(obj is Order_Detail od)
			{
				od.OrderID = info.GetValue("OrderID", typeof(int)).ConvertFromDbValue<int>();
				od.ProductID = info.GetValue("ProductID", typeof(int)).ConvertFromDbValue<int>();
				od.UnitPrice = info.GetValue("UnitPrice", typeof(decimal)).ConvertFromDbValue<decimal>();
				od.Quantity = info.GetValue("Quantity", typeof(Int16)).ConvertFromDbValue<Int16>();
				od.Discount = info.GetValue("Discount", typeof(Single)).ConvertFromDbValue<Single>();

				od.Order = info.GetValue("Order", typeof(Order)).ConvertFromDbValue<Order>();
				od.Product = info.GetValue("Product", typeof(Product)).ConvertFromDbValue<Product>();

				return od;
			}

			return null;
		}
	}
}
