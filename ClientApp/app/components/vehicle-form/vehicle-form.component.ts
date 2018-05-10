import { VehicleService } from './../../services/vehicle.service'; 
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {

  features: any[] = [];
  makes: any[] = [];
  models: any[] = [];
  vehicle: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);
      
      // if logging is realized after invoking asynchrous api method (outside the lambda expression
      // there is a time lag between initialize variable makes and logging this variable.
      // It should be move to lambda expression to avoid lag
      
      // console.log("MAKES: ", this.makes);
      
      this.vehicleService.getFeatures()
        .subscribe(features => this.features = features)
  }

  onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
    // for large amount of data, better way is to call for models separately to the server 
    // instead of load  all make objects in one request
    this.models = selectedMake ? selectedMake.models : [];
  }

}
