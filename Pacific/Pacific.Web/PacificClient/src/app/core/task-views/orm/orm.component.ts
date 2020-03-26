import { Component } from "@angular/core";
import { DataTableType } from 'src/app/shared/Models/DataTableType';
import { DataFetcher } from 'src/app/shared/utilities/DataFetcher';
import { Observable } from 'rxjs';

@Component({
    selector: 'pac-orm',
    templateUrl: './orm.component.html',
    styles: ['./orm.component.scss'],
})
export class OrmComponent {
    public DataTableType = DataTableType;
    public dataSource$: Observable<any[]>;
    public selected: DataTableType;

    constructor(private fetcher: DataFetcher<any>) {
        this.selected = DataTableType.Products;
    }

    public fetchData() {
        this.dataSource$ = null;
        this.dataSource$ = this.fetcher.fetchDataFromDb(this.selected);
        
    }
}