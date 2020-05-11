namespace TaskSerialization.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure;
    using System.Runtime.Serialization;
	using TaskSerialization.Helpers;

	[Serializable]
    public partial class Product: ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        private Product(SerializationInfo info, StreamingContext context)
        {
            this.ProductID = info.GetValue("ProductID", typeof(int)).ConvertFromDbValue<int>();
            this.ProductName = info.GetValue("ProductName", typeof(string)).ConvertFromDbValue<string>();
            this.SupplierID = info.GetValue("SupplierID", typeof(int)).ConvertFromDbValue<int>();
            this.CategoryID = info.GetValue("CategoryID", typeof(int)).ConvertFromDbValue<int>();
            this.QuantityPerUnit = info.GetValue("QuantityPerUnit", typeof(string)).ConvertFromDbValue<string>();
            this.UnitPrice = info.GetValue("UnitPrice", typeof(decimal)).ConvertFromDbValue<decimal>();
            this.UnitsInStock = info.GetValue("UnitsInStock", typeof(Int16)).ConvertFromDbValue<Int16>();
            this.UnitsOnOrder = info.GetValue("UnitsOnOrder", typeof(Int16)).ConvertFromDbValue<Int16>();
            this.ReorderLevel = info.GetValue("ReorderLevel", typeof(Int16)).ConvertFromDbValue<Int16>();
            this.Discontinued = info.GetValue("Discontinued", typeof(bool)).ConvertFromDbValue<bool>();

            this.Supplier = info.GetValue("Supplier", typeof(Supplier)).ConvertFromDbValue<Supplier>();
            this.Category = info.GetValue("Category", typeof(Category)).ConvertFromDbValue<Category>();
            this.Order_Details = info.GetValue("Order_Details", typeof(ICollection<Order_Detail>)).ConvertFromDbValue<ICollection<Order_Detail>>();
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(context.Context is IObjectContextAdapter contextAdapter)
            {
                contextAdapter.ObjectContext.LoadProperty(this, p => p.Supplier);
                contextAdapter.ObjectContext.LoadProperty(this, p => p.Category);
                contextAdapter.ObjectContext.LoadProperty(this, p => p.Order_Details);
            }

            info.AddValue("ProductID", this.ProductID);
            info.AddValue("ProductName", this.ProductName);
            info.AddValue("SupplierID", this.SupplierID);
            info.AddValue("CategoryID", this.CategoryID);
            info.AddValue("QuantityPerUnit", this.QuantityPerUnit);
            info.AddValue("UnitPrice", this.UnitPrice); 
            info.AddValue("UnitsInStock", this.UnitsInStock);
            info.AddValue("UnitsOnOrder", this.UnitsOnOrder);
            info.AddValue("ReorderLevel", this.ReorderLevel);
            info.AddValue("Discontinued", this.Discontinued);

            info.AddValue("Supplier", this.Supplier, typeof(Supplier));
            info.AddValue("Category", this.Category, typeof(Category));
            info.AddValue("Order_Details", this.Order_Details, typeof(ICollection<Order_Detail>));

        }
    }
}
