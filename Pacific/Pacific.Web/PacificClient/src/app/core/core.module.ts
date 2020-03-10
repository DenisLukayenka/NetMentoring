import { NgModule } from '@angular/core';
import { MainContainer } from './main-container/main-container.component';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { TaskViewModule } from '../shared/task-view/task-view.module';
import { SystemVisitorComponent } from './task-views/system-visitor/system-visitor.component';

@NgModule({
    declarations: [
      MainContainer,
      SystemVisitorComponent,
    ],
    imports: [
      CommonModule,
      MatExpansionModule,
      MatFormFieldModule,
      MatInputModule,
      FormsModule,
      MatExpansionModule,
      SharedModule,
    ],
    exports: [
        MainContainer,
    ]
  })
  export class CoreModule { }