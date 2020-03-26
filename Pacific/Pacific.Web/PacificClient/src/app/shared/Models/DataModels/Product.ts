import { Category } from './Category';

export class Product {
    public Id: number;
    public Name: string;
    public QuantityPerUnit: number;
    public UnitPrice: number;
    public UnitsInStock: number;
    public UnitsOnOrder: number;
    public ReorderLevel: number;
    public Discontinued: boolean;

    public CategoryId: number;
    public Category: Category;
}