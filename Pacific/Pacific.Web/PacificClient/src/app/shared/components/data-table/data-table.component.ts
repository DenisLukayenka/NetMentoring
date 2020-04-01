import { 
    Component, 
    Input, 
    ChangeDetectionStrategy, 
    OnChanges, 
    SimpleChanges, 
    ViewChild,
    OnInit,
} from "@angular/core";
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';


@Component({
    selector: 'pac-data-table',
    templateUrl: './data-table.component.html',
    styleUrls: ['./data-table.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DataTableComponent implements OnChanges {
    @Input() responseData: any[] = [];
    @Input() selectedRows: SelectionModel<any>;
    @Input() allowMultiSelect: boolean = true;
    @Input() initialSelection: any[] = [];

    public dataSource: MatTableDataSource<any>;
    public displayedColumns: string[];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort, {static: true}) sort: MatSort;

    ngOnChanges(changes: SimpleChanges): void {
        let currentValue = this.retrieveCurrentValue(changes);

        if (currentValue?.length > 0) {
            this.dataSource = new MatTableDataSource(currentValue);
            this.displayedColumns = ['selectDef'].concat(Object.keys(currentValue[0]));

            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        }
    }

    private retrieveCurrentValue(changes: SimpleChanges) {
        if (!changes.responseData?.currentValue) {
            return null;
        }
        let keys = Object.keys(changes.responseData.currentValue);

        return changes.responseData.currentValue[keys[0]];
    }

    /** Whether the number of selected elements matches the total number of rows. */
    isAllSelected() {
        const numSelected = this.selectedRows.selected.length;
        const numRows = this.dataSource.data.length;
        return numSelected == numRows;
    }
    
    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {
        this.isAllSelected() ?
            this.selectedRows.clear() :
            this.dataSource.data.forEach(row => this.selectedRows.select(row));
    }
}