import { map } from 'rxjs/operators';

import { Component, ViewChild, AfterViewInit, OnInit } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, fromEvent, Subscription } from 'rxjs';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';
import { SystemVisitorRequest } from 'src/app/shared/Models/Requests/SystemVisitorRequest';
import { FileData } from 'src/app/shared/Models/Responses/FileData';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import { SwPush } from '@angular/service-worker';
import { ROOT_URL } from 'src/app/shared/Models/Config';

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

        private _subscription: PushSubscription; 
        public operationName: string;

        readonly httpOptions = {
            headers: new HttpHeaders({
              'Content-Type': 'application/json'
            })
          };

        @ViewChild(CdkVirtualScrollViewport) viewport: CdkVirtualScrollViewport;

        constructor(private fetcher: DataFetcher, private swPush: SwPush, private httpClient: HttpClient) {
            swPush.subscription.subscribe((subscribtion) => {
                this._subscription = subscribtion;
                this.operationName = (this._subscription === null) ? 'Subscribe' : 'Unsubscribe';
            })
        }

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

        operation() {
            (this._subscription === null) ? this.subscribe() : this.unsubscribe(this._subscription.endpoint);
        }

        private subscribe() {
            this.httpClient.get(ROOT_URL + 'api/PublicKey', { responseType: 'text' }).subscribe(publicKey => {
                this.swPush.requestSubscription({
                    serverPublicKey: publicKey
                })
                .then(subscription => this.httpClient.post(ROOT_URL + 'api/PushSubscriptions', subscription, this.httpOptions)
                    .subscribe(() => {}, error => console.log(error)
                ))
                .catch(error => console.log(error));
            },
            error => console.log(error));
        };
        private unsubscribe(endpoint) {
            this.swPush.unsubscribe()
                .then(() => this.httpClient.delete(ROOT_URL + 'api/PushSubscriptions' + encodeURIComponent(endpoint))
                    .subscribe(() => {},
                        error => console.log(error)
                    ))
                    .catch(error => console.log(error));
        }
}