import { NgModule } from '@angular/core';
import { MainContainer } from './main-container/main-container.component';
import { CommonModule, AsyncPipe } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { SystemVisitorComponent } from './task-views/system-visitor/system-visitor.component';

@NgModule({
    declarations: [
      MainContainer,
      SystemVisitorComponent,
    ],
    imports: [
      CommonModule,

      MatDividerModule,
      MatButtonModule,
      MatExpansionModule,
      MatFormFieldModule,
      MatInputModule, 
      FormsModule,

      SharedModule,
    ],
    exports: [
        MainContainer,
    ]
  })
  export class CoreModule { }