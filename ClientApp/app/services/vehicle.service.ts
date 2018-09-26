import { SaveVehicle } from './../models/vehicle';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {
  constructor(private http: Http) {}
  
  private readonly vehiclesEndpoint = "/api/vehicles/"

  getVehicles(filter: string) {
    
      return this.http.get(this.vehiclesEndpoint + "?" + this.toQueryString(filter))
        .map(response => response.json());
    }

    
    delete(vehicle: SaveVehicle) {
      return this.http.delete(this.vehiclesEndpoint + vehicle.id)
      .map(response => response.json());
    }
    
    update(vehicle: SaveVehicle) {
      return this.http.put(this.vehiclesEndpoint + vehicle.id, vehicle)
      .map(response => response.json());
    }
    
  getVehicle(id: any) {
    return this.http.get(this.vehiclesEndpoint +  id)
    .map(response => response.json())
  }
  
  getMakes() {
    return this.http.get("/api/makes")
    .map(response => response.json())
  }
  
  getFeatures() {
    return this.http.get("/api/features")
    .map(response => response.json());
  }
  
  create(vehicle: any) {
    return this.http.post("/api/vehicles", vehicle)
    .map(response => response.json()); 
  }


  private toQueryString(obj: any){
    // prop=value
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
  
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
  
    return parts.join('&');
  }
}
