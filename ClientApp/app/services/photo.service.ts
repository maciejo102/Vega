import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class PhotoService {
    constructor(private http: Http) { }

    upload(vehicleId: number, photo: File){
        var formData = new FormData();
        formData.append("file", photo)
        return this.http.post(`/api/vehicles/${vehicleId}/photos`, formData)
            .map(response => response.json());
    }

    getPhotos(vehicleId: number) {
        return this.http.get(`/api/vehicles/${vehicleId}/photos`)
            .map(response => response.json());
    }
}