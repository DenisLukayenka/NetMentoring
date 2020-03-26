import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ROOT_URL } from '../Models/Config';
import { SystemVisitorResponse } from '../Models/Responses/SystemVisitorResponse';
import { SystemVisitorRequest } from '../Models/Requests/SystemVisitorRequest';
import { DataTableType } from '../Models/DataTableType';

@Injectable({
    providedIn: 'root',
})
export class DataFetcher<T> {
    constructor(private http: HttpClient) {}

    FetchFileSystemData(request: SystemVisitorRequest): Observable<SystemVisitorResponse> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');
        const httpParams = new HttpParams()
                                .set('FolderPath', request.FolderPath)
                                .set('ShowFilteredFiles', request.ShowFilteredFiles.toString())
                                .set('ShowFilteredDirectories', request.ShowFilteredDirectories.toString());

        return this.http.get<SystemVisitorResponse>(ROOT_URL + '/request/GetSystemFiles', { headers: headers, params: httpParams });
    }

    fetchDataFromDb(dataType: DataTableType): Observable<T> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');

        const httpParams = new HttpParams()
                                .set('requestType', dataType.toString());

        return this.http.get<T>(ROOT_URL + '/request/FetchDataFromDb', { headers: headers, params: httpParams });
    }
}
