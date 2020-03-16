import { IRequest } from './IRequest';

export class SystemVisitorRequest implements IRequest {
    FolderPath: string;
    ShowFilteredFiles: boolean;
    ShowFilteredDirectories: boolean;

    constructor(path: string, showFilteredFiles: boolean = true, showFilteredDirectories: boolean = true)
    {
        this.FolderPath = path;
        this.ShowFilteredFiles = showFilteredFiles;
        this.ShowFilteredDirectories = showFilteredDirectories;
    }
}