import { Component } from "@angular/core";
import { DataTableType } from 'src/app/shared/Models/DataTableType';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeDialogComponent } from 'src/app/shared/components/employee-dialog/employee-dialog.component';
import { SelectionModel } from '@angular/cdk/collections';
import { ChooseCategoryDialog } from 'src/app/shared/components/choose-category-dialog/choose-category-dialog.component';
import { ProductDialogComponent } from 'src/app/shared/components/product-dialog/product-dialog.component';

@Component({
    selector: 'pac-orm',
    templateUrl: './orm.component.html',
    styleUrls: ['./orm.component.scss'],
})
export class OrmComponent {
    public DataTableType = DataTableType;
    public dataSource$: Observable<any[]>;
    public selectedSelector: DataTableType;

    public selection: SelectionModel<any>;

    constructor(private fetcher: DataFetcher, public dialog: MatDialog) {
        this.selectedSelector = DataTableType.Products;
    }

    public fetchData() {
        this.dataSource$ = null;
        this.dataSource$ = this.fetcher.fetchDataFromDb(this.selectedSelector);
        
        this.resetSelection(this.allowMultiSelect, []);
    }

    public openEmployeeDialog() {
        this.dialog.open(EmployeeDialogComponent);
    }

    public changeCategory() {
        const dialogRef = this.dialog.open(ChooseCategoryDialog);

        dialogRef.afterClosed().subscribe(result => {
            if(result !== undefined && result !== null) {
                this.fetcher.moveProductsToCategory(this.selection.selected.map(s => s.id), result).subscribe(result => {
                    if(result) {
                        this.fetchData();
                    }
                });
            }
        });
    }

    public isChangeCategoryEnabled(): boolean {
        return this.selectedSelector === DataTableType.Products && this.selection?.selected.length > 0;
    }

    private resetSelection(allowMultiSelect: boolean, initialSelection: []) {
        this.selection = new SelectionModel<any>(allowMultiSelect, initialSelection);
    }

    public openProductsDialog() {
        this.dialog.open(ProductDialogComponent);
    }

    public get allowMultiSelect() {
        return this.selectedSelector !== DataTableType.NotShippedProducts;
    }
}