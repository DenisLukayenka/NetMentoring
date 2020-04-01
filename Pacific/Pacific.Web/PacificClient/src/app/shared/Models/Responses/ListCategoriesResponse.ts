import { IResponse } from './IResponse';
import { CategoryViewModel } from '../CategoryViewModel';

export class ListCategoryResponse implements IResponse {
    public categories: CategoryViewModel[];
}