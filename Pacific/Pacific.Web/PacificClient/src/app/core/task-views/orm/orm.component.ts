import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'pac-orm',
    templateUrl: './orm.component.html',
    styles: ['./orm.component.scss'],
})
export class OrmComponent {
    firstFormGroup: FormGroup;

    constructor(private _formBuilder: FormBuilder) {}

    ngOnInit() {
        this.firstFormGroup = this._formBuilder.group({
          firstCtrl: ['', Validators.required]
        });
    }
}