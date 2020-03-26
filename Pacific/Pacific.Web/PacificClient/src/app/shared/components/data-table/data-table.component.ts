import { Component, Input, ViewChild } from "@angular/core";
import { DataTableType } from '../../Models/DataModels/DataTableType';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
    selector: 'pac-data-table',
    templateUrl: './data-table.component.html',
    styleUrls: ['./data-table.component.scss']
})
export class DataTableComponent<T> {
    @Input() dataType: DataTableType;
    @Input() data: any;

    displayedColumns: string[] = Object.keys({} as T);
    dataSource: MatTableDataSource<T>;

    @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

    ngOnInit() {
        this.dataSource.paginator = this.paginator;
    }
}