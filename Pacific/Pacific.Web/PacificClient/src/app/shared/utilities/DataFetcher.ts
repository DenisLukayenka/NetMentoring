import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ROOT_URL } from '../Models/Config';
import { SystemVisitorResponse } from '../Models/Responses/SystemVisitorResponse';
import { SystemVisitorRequest } from '../Models/Requests/SystemVisitorRequest';
import { DataTableType } from '../Models/DataTableType';
import { StatusResponse } from '../Models/Responses/AddEmployeeStatusResponse';
import { AddEmployeeRequest } from '../Models/Requests/AddEmployeeRequest';
import { ProductsMoveRequest } from '../Models/Requests/ProductsMoveRequest';
import { Product } from '../Models/Product';
import { AddProductsRequest } from '../Models/Requests/AddProductsRequest';
import { ProductViewModel } from '../Models/ProductViewModel';
import { SimilarProductRequest } from '../Models/Requests/SimilarProductRequest';
import { ReplaceProductRequest } from '../Models/Requests/ReplaceProductRequest';

@Injectable({
    providedIn: 'root',
})
export class DataFetcher {
    constructor(private http: HttpClient) {}

    FetchFileSystemData(request: SystemVisitorRequest): Observable<SystemVisitorResponse> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');
        const httpParams = new HttpParams()
                                .set('FolderPath', request.FolderPath)
                                .set('ShowFilteredFiles', request.ShowFilteredFiles.toString())
                                .set('ShowFilteredDirectories', request.ShowFilteredDirectories.toString());

        return this.http.get<SystemVisitorResponse>(ROOT_URL + '/request/GetSystemFiles', { headers: headers, params: httpParams });
    }

    fetchDataFromDb(dataType: DataTableType): Observable<any> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');

        const httpParams = new HttpParams()
                                .set('requestType', dataType.toString());

        return this.http.get<any>(ROOT_URL + '/request/FetchDataFromDb', { headers: headers, params: httpParams });
    }

    postEmployee(employee: any): Observable<StatusResponse> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');
        var request = new AddEmployeeRequest();
        request.Employee = employee;

        return this.http.post<any>(ROOT_URL + '/request/AddEmployee', request, { headers: headers });
    }

    moveProductsToCategory(productsIds: number[], categoryId: number): Observable<StatusResponse> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');
        var request = {
            ProductsIds: productsIds,
            CategoryId: categoryId,
        } as ProductsMoveRequest;

        return this.http.post<StatusResponse>(ROOT_URL + '/request/MoveProductsToCategory', request, { headers });
    }

    addProducts(products: Product[]): Observable<StatusResponse> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');
        var request = {
            Products: products,
        } as AddProductsRequest;

        return this.http.post<StatusResponse>(ROOT_URL + '/request/AddProductsCollection', request, { headers });
    }

    getSimilarProduct(productId: number): Observable<ProductViewModel[]> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');
        var request = {
            ProductId: productId,
        } as SimilarProductRequest;

        return this.http.post<ProductViewModel[]>(ROOT_URL + '/request/GetSimilarProducts', request, { headers });
    }

    replaceProduct(originProductId: number, originOrderId: number, targetProductId: number) {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');
        var request = {
            OriginOrderId: originOrderId,
            OriginProductId: originProductId,
            TargetProductId: targetProductId,
        } as ReplaceProductRequest;

        return this.http.post<StatusResponse>(ROOT_URL + '/request/ReplaceProduct', request, { headers });
    }
}
