import { DataTableType } from '../DataModels/DataTableType';
import { IRequest } from './IRequest';

export class DataTypeRequest implements IRequest {
    public OrmRequestType: DataTableType;
}