import { map } from 'rxjs/operators';

import { Component } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';
import { SystemVisitorRequest } from 'src/app/shared/Models/Requests/SystemVisitorRequest';
import { FileData } from 'src/app/shared/Models/Responses/FileData';

@Component({
    selector: 'pac-system-visitor',
    templateUrl: './system-visitor.component.html',
    styleUrls: ['./system-visitor.component.scss']
})
export class SystemVisitorComponent {
    directoryPath: string;
    list$: Observable<FileData[]>;

    constructor(private http: HttpClient, private fetcher: DataFetcher){}

    FetchFolderData() {
        const request = new SystemVisitorRequest(this.directoryPath);

        this.list$ = this.fetcher.FetchFileSystemData(request).pipe(map(data => data.files));
    }
}