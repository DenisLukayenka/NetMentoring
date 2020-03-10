import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CoreModule } from './core/core.module';
import { MainContainer } from './core/main-container/main-container.component';

const routes: Routes = [
  { path: '', component: MainContainer }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    CoreModule,
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
