import { 
    Component, 
    Input, 
    ChangeDetectionStrategy, 
    OnChanges, 
    SimpleChanges, 
    ViewChild
} from "@angular/core";
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';


@Component({
    selector: 'pac-data-table',
    templateUrl: './data-table.component.html',
    styleUrls: ['./data-table.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DataTableComponent implements OnChanges {
    @Input() responseData: any[];
    dataSource: MatTableDataSource<any>;
    displayedColumns: string[];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort, {static: true}) sort: MatSort;

    ngOnChanges(changes: SimpleChanges): void {
        let currentValue = this.retrieveCurrentValue(changes);

        if (currentValue?.length > 0) {
            this.dataSource = new MatTableDataSource(currentValue);
            this.displayedColumns = Object.keys(currentValue[0]);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        }
    }

    private retrieveCurrentValue(changes: SimpleChanges) {
        if (!changes.responseData.currentValue) {
            return null;
        }
        let keys = Object.keys(changes.responseData.currentValue);

        return changes.responseData.currentValue[keys[0]];
    }
}