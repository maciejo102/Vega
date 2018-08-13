import * as _ from 'underscore';
import { Vehicle, SaveVehicle } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service'; 
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { forkJoin } from 'rxjs/observable/forkJoin';



@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {

  featureId: any;
  features: any[] = [];
  makes: any[] = [];
  models: any[] = [];
  vehicle: SaveVehicle = {
    id: 0,
    modelId: 0,
    makeId: 0,
    isRegistered: false,
    features: [],
    contact : {
      name: '',
      email: '',
      phone: ''
    }
  };

  constructor(private vehicleService: VehicleService,
private route: ActivatedRoute,
private router: Router) {

  route.params.subscribe(params => {
  this.vehicle.id = params['id'] as number
});
  
 }

  ngOnInit() {
    
    var sources =  [ this.vehicleService.getMakes(), this.vehicleService.getFeatures() ];

    if (this.vehicle.id)
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));
    
    forkJoin(sources)
      .subscribe((dataFromServer) => {
      this.makes = dataFromServer[0];
      this.features = dataFromServer[1];
      
      if (this.vehicle.id)
      this.setVehicle(dataFromServer[2]);
      this.populateModels();
    }, err => { if (err.status = 404) this.router.navigate(['/home'])});


    // this.vehicleService.getMakes()
    //   .subscribe(makes => this.makes = makes);
      
      // if logging is realized after invoking asynchrous api method (outside the lambda expression
      // there is a time lag between initialize variable makes and logging this variable.
      // It should be move to lambda expression to avoid lag
      
      // console.log("MAKES: ", this.makes);
      
      // this.vehicleService.getFeatures()
      //   .subscribe(features => this.features = features)
  }

  
  onMakeChange(){
   this.populateModels();
    // delete modelId when changing make
    delete this.vehicle.modelId;
  }
  
  onFeatureToggle(featureId: any, $event: any) {
    if ($event.target.checked){
      this.vehicle.features.push(featureId);
    }
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }
  
  submit() {
    if (this.vehicle.id) 
      this.updateVehicle(this.vehicle);
    else
      this.saveVehicle(this.vehicle);
}

deleteVehicle() {
  if (confirm("Are you sure?")) {
    this.vehicleService.delete(this.vehicle)
      .subscribe(v => { 
        this.router.navigate(["/home"]);
        alert('Vehicle deleted.')
      });
  }
}


private updateVehicle(vehicle: SaveVehicle){
  this.vehicleService.update(vehicle)
  .subscribe(
    v => {
      console.log(v);
      alert('Succesfully modified vehicle.')
    }
  );
}

private saveVehicle(vehicle: SaveVehicle){
  this.vehicleService.create(this.vehicle)
  .subscribe(
    // success scenario
    v => {
      console.log(v);
      alert('Succesfully created vehicle.')
    }
  );
}

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    // for large amount of data, better way is to call for models separately to the server 
    // instead of load  all make objects in one request
    this.models = selectedMake ? selectedMake.models : [];
  }

  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, 'id');
  }
}
