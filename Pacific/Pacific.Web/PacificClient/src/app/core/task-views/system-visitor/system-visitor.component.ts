import { map } from 'rxjs/operators';

import { Component, ViewChild, AfterViewInit, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable, fromEvent } from 'rxjs';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';
import { SystemVisitorRequest } from 'src/app/shared/Models/Requests/SystemVisitorRequest';
import { FileData } from 'src/app/shared/Models/Responses/FileData';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';

@Component({
    selector: 'pac-system-visitor',
    templateUrl: './system-visitor.component.html',
    styleUrls: ['./system-visitor.component.scss']
})
export class SystemVisitorComponent implements AfterViewInit, OnInit {
    directoryPath: string;
    showFilteredFiles: boolean;
    showFilteredDirectories: boolean;
    list$: Observable<FileData[]>;

    @ViewChild(CdkVirtualScrollViewport, {static: false}) viewport: CdkVirtualScrollViewport;

    constructor(private fetcher: DataFetcher){}

    ngOnInit(): void {
      this.showFilteredDirectories = false;
      this.showFilteredFiles = true;
    }

    fetchFolderData() {
        const request = new SystemVisitorRequest(this.directoryPath, this.showFilteredFiles, this.showFilteredDirectories);

        this.list$ = this.fetcher.FetchFileSystemData(request).pipe(map(data => data.files));
    }

    ngAfterViewInit() {
        fromEvent(window, 'resize').subscribe(() => {
          this.viewport.checkViewportSize();
        });
      }
}