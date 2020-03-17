import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskViewComponent } from './task-view/task-view.component';
import { TaskViewModule } from './task-view/task-view.module';
import { PushSubscriberComponent } from './components/push-subscriber/push-subscriber.component';

@NgModule({
    declarations: [
      PushSubscriberComponent
    ],
    imports: [
      CommonModule,
      TaskViewModule,
    ],
    exports: [
        TaskViewComponent,
        PushSubscriberComponent,
    ]
  })
  export class SharedModule { }