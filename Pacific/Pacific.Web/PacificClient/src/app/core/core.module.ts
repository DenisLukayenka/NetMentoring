import { NgModule } from '@angular/core';
import { MainContainer } from './main-container/main-container.component';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    declarations: [
      MainContainer,
    ],
    imports: [
      CommonModule,
      MatExpansionModule,
      MatFormFieldModule,
      MatInputModule,
      FormsModule,
      BrowserAnimationsModule,
      MatExpansionModule,
    ],
    exports: [
        MainContainer,
    ]
  })
  export class CoreModule { }