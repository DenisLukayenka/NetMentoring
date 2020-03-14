import { Component } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';

@Component({
    selector: 'pac-system-visitor',
    templateUrl: './system-visitor.component.html',
    styleUrls: ['./system-visitor.component.scss']
})
export class SystemVisitorComponent {
    directoryPath: string;
    list$: Observable<string[]>;

    constructor(private http: HttpClient, private fetcher: DataFetcher){}

    FetchFolderData() {
        this.list$ = this.fetcher.FetchDefaultData();
    }
}