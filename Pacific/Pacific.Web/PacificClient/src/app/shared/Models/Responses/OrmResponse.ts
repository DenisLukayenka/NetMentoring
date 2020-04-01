import { Product } from '../DataModels/Product';
import { IResponse } from './IResponse';

export class OrmResponse implements IResponse {
    public Products: Product[];
}