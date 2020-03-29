import { Component, OnInit } from "@angular/core";
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Product } from '../../Models/Product';
import { DataFetcher } from '../../utilities/DataFetcher';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'pac-product-dialog',
    styleUrls: ['./product-dialog.component.scss'],
    templateUrl: './product-dialog.component.html'
})
export class ProductDialogComponent implements OnInit {
    public products: Product[] = [];
    public productsForm: FormGroup;
    public isLoading = false;

    constructor(private builder: FormBuilder, private fetcher: DataFetcher, public dialogRef: MatDialogRef<ProductDialogComponent>) {}

    public ngOnInit() {
        this.productsForm = this.builder.group({
            Name: ['', Validators.required],
            SupplierName: ['', Validators.required],
            CategoryName: ['', Validators.required],
            QuantityPerUnit: ['', Validators.pattern("^[0-9]*$")],
            UnitPrice: ['', Validators.pattern("^[0-9]*$")],
            UnitsInStock: ['', Validators.pattern("^[0-9]*$")],
            UnitsOnOrder: ['', Validators.pattern("^[0-9]*$")],
            ReorderLevel: ['', Validators.pattern("^[0-9]*$")],
            Discontinued: [''],
        })
    }

    public addProduct() {
        if(!this.productsForm.invalid){
            let currentProduct = this.productsForm.value;

            this.products.push({
                    Name: currentProduct.Name,
                    CategoryName: currentProduct.CategoryName,
                    QuantityPerUnit: +currentProduct.QuantityPerUnit,
                    ReorderLevel: +currentProduct.ReorderLevel,
                    SupplierName: currentProduct.SupplierName,
                    UnitPrice: +currentProduct.UnitPrice,
                    UnitsInStock: +currentProduct.UnitsInStock,
                    UnitsOnOrder: +currentProduct.UnitsOnOrder,
                    Discontinued: !!currentProduct.Discontinued,
                }
            );
            this.productsForm.reset();
        }
    }

    public onFormSubmit() {
        this.isLoading = true;

        this.fetcher.addProducts(this.products).subscribe(result => {
            if (result) {
                this.isLoading = false;
                this.dialogRef.close();
            } else {
                console.log("Unsuccess result to add products: ");
                console.log(this.products);
            }
        }, error => {
            this.isLoading = false;
            console.log("Error was occured, pls see results:");
            console.log(error);
            console.log("---------------------------");
            console.log(this.products);
        })
    }
}