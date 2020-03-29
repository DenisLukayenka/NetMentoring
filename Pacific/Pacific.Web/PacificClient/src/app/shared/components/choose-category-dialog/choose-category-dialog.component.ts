import { Component } from "@angular/core";
import { DataFetcher } from '../../utilities/DataFetcher';
import { DataTableType } from '../../Models/DataTableType';
import { CategoryViewModel } from '../../Models/CategoryViewModel';
import { ListCategoryResponse } from '../../Models/Responses/ListCategoriesResponse';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'pac-choose-category-dialog',
    styleUrls: ['./choose-category-dialog.component.scss'],
    templateUrl: './choose-category-dialog.component.html'
})
export class ChooseCategoryDialog {
    public categories: CategoryViewModel[];
    public targetCategoryId: number;

    constructor(private fetcher: DataFetcher, public dialogRef: MatDialogRef<ChooseCategoryDialog>) {
        this.fetcher.fetchDataFromDb(DataTableType.Categories).subscribe((data: ListCategoryResponse) => {
            this.categories = data.categories;

            if(this.categories?.length > 0) {
                this.targetCategoryId = this.categories[0].id;
            }
        });
    }
}