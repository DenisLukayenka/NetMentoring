import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ProductViewModel } from '../../Models/ProductViewModel';
import { SelectionModel } from '@angular/cdk/collections';
import { DataFetcher } from '../../utilities/DataFetcher';

@Component({
    selector: 'pac-unshipped-product-dialog',
    styleUrls: ['./unshipped-product-dialog.component.scss'],
    templateUrl: './unshipped-product-dialog.component.html'
})
export class UnshippedProductDialogComponent implements OnInit {
    public dataSource$: Observable<ProductViewModel[]>;
    public selection: SelectionModel<ProductViewModel>;
    
    constructor(@Inject(MAT_DIALOG_DATA) public data: any, private fetcher: DataFetcher) {
        this.dataSource$ = this.fetcher.getSimilarProduct(data.productId);
    }

    public ngOnInit() {
        this.selection = new SelectionModel<any>(false, []);
    }
}