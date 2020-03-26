import { Component } from "@angular/core";
import { DataTableType } from 'src/app/shared/Models/DataTableType';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeDialogComponent } from 'src/app/shared/components/employee-dialog/employee-dialog.component';

@Component({
    selector: 'pac-orm',
    templateUrl: './orm.component.html',
    styleUrls: ['./orm.component.scss'],
})
export class OrmComponent {
    public DataTableType = DataTableType;
    public dataSource$: Observable<any[]>;
    public selected: DataTableType;

    constructor(private fetcher: DataFetcher, public dialog: MatDialog) {
        this.selected = DataTableType.Products;
    }

    public fetchData() {
        this.dataSource$ = null;
        this.dataSource$ = this.fetcher.fetchDataFromDb(this.selected);
    }

    public openEmployeeDialog() {
        const dialogRef = this.dialog.open(EmployeeDialogComponent);

        dialogRef.afterClosed().subscribe(result => {
            console.log(`Dialog result: ${result}`);
        });
    }
}