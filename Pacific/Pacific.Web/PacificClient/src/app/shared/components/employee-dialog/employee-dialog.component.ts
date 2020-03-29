import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataFetcher } from '../../utilities/DataFetcher';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'pac-add-employee-dialog',
    templateUrl: './employee-dialog.component.html',
    styleUrls: ['./employee-dialog.component.scss'],
})
export class EmployeeDialogComponent implements OnInit {
    employeeForm: FormGroup;
    isLoading: boolean;

    constructor(
        private builder: FormBuilder, 
        private fetcher: DataFetcher,
        public dialogRef: MatDialogRef<EmployeeDialogComponent>){
            
        this.isLoading = false;
    }

    ngOnInit(): void {
        this.employeeForm = this.builder.group({
            FirstName: ['', Validators.required],
            LastName: ['', Validators.required],
            Title: [''],
            TitleOfCourtesy: [''],
            BirthDate: [new Date()],
            HireDate: [new Date()],
            Address: [''],
            City: [''],
            Region: [''],
            PostalCode: [''],
            Country: [''],
            HomePhone: [''],
            Extension: [''],
            Notes: [''],
            PhotoPath: [''],
        })
    }

    public onFormSubmit() {
        if (!this.employeeForm.invalid) {
            this.isLoading = true;
            this.fetcher.postEmployee(this.employeeForm.value)
                .subscribe(result =>
                    {
                        this.isLoading = !result.isSuccess;
                        this.dialogRef.close();
                    },
                    error => {
                        console.log(error);
                        this.isLoading = false;
                    });
        }
    }
}