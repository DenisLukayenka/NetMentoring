import { NgModule } from '@angular/core';
import { CommonModule, JsonPipe } from '@angular/common';
import { TaskViewComponent } from './components/task-view/task-view.component';
import { TaskViewModule } from './components/task-view/task-view.module';
import { DataTableComponent } from './components/data-table/data-table.component';
import { EmployeeDialogComponent } from './components/employee-dialog/employee-dialog.component';
import { MaterialModule } from '../material-ui/material.module';

@NgModule({
    declarations: [
      DataTableComponent,
      EmployeeDialogComponent,
    ],
    imports: [
      CommonModule,
      TaskViewModule,
      MaterialModule,
    ],
    entryComponents: [
      EmployeeDialogComponent,
    ],
    exports: [
        TaskViewComponent,
        DataTableComponent,
        EmployeeDialogComponent,
    ]
  })
  export class SharedModule { }