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
  query: any = {}
  models: any;
  columns = [
    { title: 'Id' }, 
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { }
  ]
  
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

  
  showVehicle(id: number) {
    this.router.navigate(["/vehicles/" + id]);
  }
  
  resetFilter(){
    this.query = {};
    this.onFilterChange();
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

  
  private populateVehicles() {
    this.vehicleService.getAll(this.query)
    .subscribe(v =>{
      this.vehicles =  v
    });
  }

  private onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.query.makeId);
  
    this.models = selectedMake ? selectedMake.models : [];
    delete this.query.modelId;
    }

    sortBy(columnName: any) {
      if (this.query.sortBy === columnName) {
        this.query.isSortingAscending = !this.query.isSortingAscending;
      }
      else {
        this.query.sortBy = columnName;
        this.query.isSortingAscending = true;
      }

      this.populateVehicles();
    }
}
