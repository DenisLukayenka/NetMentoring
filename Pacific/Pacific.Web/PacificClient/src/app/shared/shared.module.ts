import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskViewComponent } from './task-view/task-view.component';
import { TaskViewModule } from './task-view/task-view.module';

@NgModule({
    declarations: [
    ],
    imports: [
      CommonModule,
      TaskViewModule,
    ],
    exports: [
        TaskViewComponent,
    ]
  })
  export class SharedModule { }