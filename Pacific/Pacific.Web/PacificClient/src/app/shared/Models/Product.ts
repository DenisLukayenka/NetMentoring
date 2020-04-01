export class Product {
    public Name: string;
    public SupplierName: string;
    public CategoryName: string;
    public QuantityPerUnit: number;
    public UnitPrice: number;
    public UnitsInStock: number;
    public UnitsOnOrder: number;
    public ReorderLevel: number;
    public Discontinued: boolean = false;
}