import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskViewComponent } from './components/task-view/task-view.component';
import { TaskViewModule } from './components/task-view/task-view.module';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { DataTableComponent } from './components/data-table/data-table.component';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
    declarations: [
      DataTableComponent,
    ],
    imports: [
      CommonModule,
      MatTableModule,
      TaskViewModule,
      MatPaginatorModule,
      MatSortModule,
    ],
    exports: [
        TaskViewComponent,
        DataTableComponent,
    ]
  })
  export class SharedModule { }