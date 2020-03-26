import { Product } from './Product';

export class Category {
    public Id: number;
    public Name: string;
    public Description: string;
    public Picture: BinaryType;
    public Products: Product[];
}