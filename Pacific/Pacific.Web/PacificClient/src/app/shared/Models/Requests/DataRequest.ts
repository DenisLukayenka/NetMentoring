import { IRequest } from './IRequest';
import { DataTableType } from '../DataTableType';

export class DataTypeRequest implements IRequest {
    public requestType: DataTableType;
}