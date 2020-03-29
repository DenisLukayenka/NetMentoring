import { IRequest } from './IRequest';

export class ProductsMoveRequest implements IRequest {
    public ProductsIds: number[];
    public CategoryId: number;
}