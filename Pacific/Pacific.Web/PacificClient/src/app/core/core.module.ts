import { NgModule } from '@angular/core';
import { MainContainer } from './main-container/main-container.component';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatStepperModule } from '@angular/material/stepper';
import { SharedModule } from '../shared/shared.module';
import { SystemVisitorComponent } from './task-views/system-visitor/system-visitor.component';
import { OrmComponent } from './task-views/orm/orm.component';

@NgModule({
    declarations: [
      MainContainer,
      SystemVisitorComponent,
      OrmComponent,
    ],
    imports: [
      CommonModule,

      MatCheckboxModule,
      ScrollingModule,
      MatDividerModule,
      MatButtonModule,
      MatExpansionModule,
      MatFormFieldModule,
      MatInputModule, 
      FormsModule, ReactiveFormsModule,
      MatListModule,
      MatStepperModule,

      SharedModule,
    ],
    exports: [
        MainContainer,
    ]
  })
  export class CoreModule { }