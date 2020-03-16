import { IRequest } from './IRequest';

export class SystemVisitorRequest implements IRequest {
    FolderPath: string;
    
    constructor(path: string)
    {
        this.FolderPath = path;
    }
}