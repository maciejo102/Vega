import { Vehicle, KeyValuePair } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {

  vehicles: Vehicle[] = []
  makes: any[] = []
  filter: any = {}
  models: any;

  // #filtering on client side
  // allVehicles: Vehicle[] = [];

  constructor(private vehicleService: VehicleService,
  private router: Router) { }

  ngOnInit() {

    this.vehicleService.getMakes()
      .subscribe(m => this.makes = m);

  // #filtering on client side
  //  this.vehicleService.getAll()
  //   .subscribe(v => {
  //     this.vehicles = this.allVehicles = v
  //  });
  
  this.populateVehicles();
  }

  onFilterChange() {
    
    this.onMakeChange();
    this.populateVehicles();

    // #filtering on client side
    // var vehicles = this.allVehicles;

    // if (this.filter.makeId)
    //   vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);

    // if (this.filter.modelID)
    //   vehicles = vehicles.filter(v => v.model.id == this.filter.modelId);

    //   this.vehicles = vehicles;
  }
  
  showVehicle(id: number) {
    this.router.navigate(["/vehicles/" + id]);
  }
  
  resetFilter(){
    this.filter = {};
    this.onFilterChange();
  }
  
  private populateVehicles() {
    this.vehicleService.getAll(this.filter)
    .subscribe(v =>{
      this.vehicles =  v
    });
  }

  private onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.filter.makeId);
  
    this.models = selectedMake ? selectedMake.models : [];
    delete this.filter.modelId;
    }
}
