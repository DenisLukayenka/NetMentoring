import { map } from 'rxjs/operators';

import { Component, ViewChild, AfterViewInit, OnInit, ChangeDetectionStrategy } from "@angular/core";
import { HttpHeaders } from '@angular/common/http';
import { Observable, fromEvent, pipe } from 'rxjs';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';
import { SystemVisitorRequest } from 'src/app/shared/Models/Requests/SystemVisitorRequest';
import { FileData } from 'src/app/shared/Models/Responses/FileData';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';

@Component({
        selector: 'pac-system-visitor',
        templateUrl: './system-visitor.component.html',
        styleUrls: ['./system-visitor.component.scss'],
})
export class SystemVisitorComponent implements AfterViewInit, OnInit {
    directoryPath: string;
    showFilteredFiles: boolean;
    showFilteredDirectories: boolean;
    list$: Observable<FileData[]>;
    isLoading: boolean;
    itemsCount: number;

    public operationName: string;

    @ViewChild(CdkVirtualScrollViewport) viewport: CdkVirtualScrollViewport;

    constructor(private fetcher: DataFetcher<any>) {}

    ngOnInit(): void {
        this.showFilteredDirectories = false;
        this.showFilteredFiles = true;
        this.isLoading = false;
        this.itemsCount = 0;
    }

    ngAfterViewInit() {
        fromEvent(window, 'resize').subscribe(() => {
            this.viewport.checkViewportSize();
        });
    }

    public fetchFolderData() {
        this.resetData();

        this.isLoading = true;

        const request = new SystemVisitorRequest(this.directoryPath, this.showFilteredFiles, this.showFilteredDirectories);
        this.list$ = this.fetcher.FetchFileSystemData(request).pipe(map(data => data.files));

        this.list$.subscribe(data => {
            this.itemsCount = data.length;
            this.isLoading = false;
        });
    }

    public resetData() {
        this.list$ = null;
        this.isLoading = false;
        this.itemsCount = 0;
    }
}
