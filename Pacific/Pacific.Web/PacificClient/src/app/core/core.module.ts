import { NgModule } from '@angular/core';
import { MainContainer } from './main-container/main-container.component';
import { CommonModule } from '@angular/common';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { CollapseModule } from 'ngx-bootstrap/collapse';

@NgModule({
    declarations: [
      MainContainer,
    ],
    imports: [
      CommonModule,
      AccordionModule.forRoot(),
      CollapseModule,
    ],
    exports: [
        MainContainer,
    ]
  })
  export class CoreModule { }