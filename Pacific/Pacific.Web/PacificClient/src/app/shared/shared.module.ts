import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskViewComponent } from './task-view/task-view.component';
import { TaskViewModule } from './task-view/task-view.module';
import { MatTableModule } from '@angular/material/table';

@NgModule({
    declarations: [
    ],
    imports: [
      CommonModule,
      MatTableModule,
      TaskViewModule,
    ],
    exports: [
        TaskViewComponent,
    ]
  })
  export class SharedModule { }