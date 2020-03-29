import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskViewComponent } from './components/task-view/task-view.component';
import { TaskViewModule } from './components/task-view/task-view.module';
import { DataTableComponent } from './components/data-table/data-table.component';
import { EmployeeDialogComponent } from './components/employee-dialog/employee-dialog.component';
import { MaterialModule } from '../material-ui/material.module';
import { ChooseCategoryDialog } from './components/choose-category-dialog/choose-category-dialog.component';

@NgModule({
    declarations: [
      DataTableComponent,
      EmployeeDialogComponent,
      ChooseCategoryDialog,
    ],
    imports: [
      CommonModule,
      TaskViewModule,
      MaterialModule,
    ],
    entryComponents: [
      EmployeeDialogComponent,
      ChooseCategoryDialog,
    ],
    exports: [
        TaskViewComponent,
        DataTableComponent,
        EmployeeDialogComponent,
        ChooseCategoryDialog,
    ]
  })
  export class SharedModule { }