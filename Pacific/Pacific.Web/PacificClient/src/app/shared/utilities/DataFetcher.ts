import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ROOT_URL } from '../Models/Config';

@Injectable({
    providedIn: 'root',
})
export class DataFetcher {
    private headers: {

    }
    constructor(private http: HttpClient) {}

    FetchDefaultData(): Observable<string[]> {
        const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8').set('Accept', '*/*');
      
        
        return this.http.get<string[]>(ROOT_URL + '/Home/GetDefault', { headers });
    }
}
