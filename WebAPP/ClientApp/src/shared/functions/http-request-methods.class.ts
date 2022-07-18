import { HttpHeaders, HttpClient } from "@angular/common/http";
import { Http, Headers } from '@angular/http';
import { environment } from "../../environments/environment";
import { Injectable } from "@angular/core";
import { Stay } from "../../dto/stay";

@Injectable()
export abstract class HttpRequestMethods {
    private httpHeader: HttpHeaders;

    constructor(private _http: Http, private httpClient: HttpClient, ) {

    }

    post(url: string, body: any, params?: any) {
        this.setHeaderToken();
        return this.httpClient.post(environment.apiUrl + url, body, { headers: this.httpHeader, params: params }).toPromise();
    }

    getWithAnyHeaders(url: string) {
        return this.httpClient.get(environment.apiUrl + url).toPromise();
    }

    get(url: string, params?: any) {
        this.setHeaderToken();
        return this.httpClient.get(environment.apiUrl + url, { headers: this.httpHeader, params: params }).toPromise();
    }

    getWithoutToken(url: string, params?: any) {
        return this.httpClient.get(environment.apiUrl + url, { headers: this.httpHeader, params: params }).toPromise();
    }

    patch(url: string, body: any, params?: any) {
        this.setHeaderToken();
        return this.httpClient.patch(environment.apiUrl + url, body, { headers: this.httpHeader, params: params }).toPromise();
    }

    patchWithoutToken(url: string, body: any, params?: any) {
        this.setJustHeader();
        return this.httpClient.patch(environment.apiUrl + url, body, { headers: this.httpHeader, params: params }).toPromise();
    }

    postFile(url: string, body: FormData, params?: any) {
        this.setJustToken();
        return this.httpClient.post(environment.apiUrl + url, body, { headers: this.httpHeader, params: params }).toPromise();
    }

    protected postFile2(url: string, body: any, params?: any) {
        this.setJustToken();
        return this.httpClient.post(environment.apiUrl + url, body, { headers: this.httpHeader, params: params, responseType: "blob" }).toPromise();
    }

    delete(url: string, params?: any) {
        this.setHeaderToken();
        return this.httpClient.delete(environment.apiUrl + url, { headers: this.httpHeader, params: params }).toPromise();
    }

    protected getFile(url: string, params?: any): Promise<any> {
        this.setHeaderToken();
        return this.httpClient.get(environment.apiUrl + url, { headers: this.httpHeader, params: params, responseType: "blob" }).toPromise();
    }

    getLatLong(street: string, zipCode: number) {
        return this.httpClient.get("https://api-adresse.data.gouv.fr/search/?q=" + street + "&postcode=" + zipCode).toPromise();
    }

    ///Headers functions
    setHeaderToken() {
        this.httpHeader = new HttpHeaders();
        let token = JSON.parse(sessionStorage.getItem("TOKEN"));
        this.httpHeader = this.httpHeader.append("Content-Type", "application/json");
        this.httpHeader = this.httpHeader.append("Authorization", `Bearer ${token}`);
    }

    setJustToken() {
        this.httpHeader = new HttpHeaders();
        let token = JSON.parse(sessionStorage.getItem("TOKEN"));
        this.httpHeader = this.httpHeader.append("Authorization", `Bearer ${token}`);
    }

    setJustHeader() {
        this.httpHeader = new HttpHeaders();
        this.httpHeader = this.httpHeader.append("Content-Type", "application/json");
    }
}
