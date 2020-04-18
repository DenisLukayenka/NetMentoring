import { Component } from "@angular/core";
import { DomainRestriction }  from 'src/app/shared/Models/DomainRestrictiont';
import { NgForm } from '@angular/forms';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';

@Component({
    selector: 'pac-http',
    templateUrl: './http.component.html',
    styleUrls: ['./http.component.scss'],
})
export class HttpComponent {
    public DomainRestriction = DomainRestriction;
    public isLoadingCopySite = false;
    public isLoadingOrdersReport = false;
    public isError = false;

    constructor(private fetcher: DataFetcher) {}

    public copySite(form: NgForm) {
        this.isError = false;

        if(form.valid) {
            this.isLoadingCopySite = true;

            this.fetcher.copySiteRequest(form.value).subscribe(status => {
                this.isLoadingCopySite = !(!!status.isSuccess);
            },
            error => {
                this.isLoadingCopySite = false;
                this.isError = true;
            });
        }
    }

    public onOrderReportPost(form: NgForm) {
        this.isError = false;

        if(form.valid) {
            this.isLoadingOrdersReport = true;

            this.fetcher.orderReportPostRequest(form.value).subscribe(status => {
                this.isLoadingOrdersReport = !(!!status.isSuccess);
            },
            error => {
                this.isLoadingOrdersReport = false;
            });
        }
    }

    public onOrderReportGet(form: NgForm) {
        this.isError = false;

        if(form.valid) {
            this.isLoadingOrdersReport = true;

            this.fetcher.orderReportGetRequest(form.value).subscribe(status => {
                this.isLoadingOrdersReport = !(!!status.isSuccess);
            },
            error => {
                this.isLoadingOrdersReport = false;
            });
        }
    }
}