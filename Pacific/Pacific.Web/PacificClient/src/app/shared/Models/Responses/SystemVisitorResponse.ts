import { IResponse } from './IResponse';
import { FileData } from './FileData';

export class SystemVisitorResponse implements IResponse {
    files: FileData[];
}