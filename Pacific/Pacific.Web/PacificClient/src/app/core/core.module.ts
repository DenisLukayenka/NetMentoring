import { NgModule } from '@angular/core';
import { MainContainer } from './main-container/main-container.component';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { SystemVisitorComponent } from './task-views/system-visitor/system-visitor.component';
import { OrmComponent } from './task-views/orm/orm.component';
import { MaterialModule } from '../material-ui/material.module';

@NgModule({
    declarations: [
      MainContainer,
      SystemVisitorComponent,
      OrmComponent,
    ],
    imports: [
      CommonModule,
      SharedModule,
      MaterialModule,
    ],
    exports: [
        MainContainer,
    ]
  })
  export class CoreModule { }