import { PhotoService } from './../../services/photo.service';
import { YesNoPipe } from './../shared/custom-pipes/yes-no.pipe';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component ({
  selector: 'app-vehicle-view',
  templateUrl: './vehicle-view.component.html',
  styleUrls: ['./vehicle-view.component.css']
})
export class VehicleViewComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef = new ElementRef(this);
  vehicleId: any;
  vehicle: any;

  constructor(private vehicleService: VehicleService,
    private photoService: PhotoService, 
    private route: ActivatedRoute,
    private router: Router) {

    this.route.params.subscribe(p =>{
      this.vehicleId = p['id'] as number;
      if (isNaN(this.vehicleId))
        this.goToVehicles();
    });
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId)
    .subscribe(v => this.vehicle = v,
      err => {
        if (err.status == 404) this.goToVehicles();
        return;
      });
  }

  editVehicle(){
    this.router.navigate(['/vehicles/edit/' + this.vehicleId])
  }

  deleteVehicle() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle)
        .subscribe(v => { 
          this.goToVehicles();
          alert('Vehicle deleted.');
        });
    }
  }

  goToVehicles(){
    this.router.navigate(["/vehicles"]);
  }

  uploadPhoto(){
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    if (nativeElement.files)
    this.photoService.upload(this.vehicleId, nativeElement.files[0])
      .subscribe(x => console.log(x));
    else throw Error("File is empty or damaged.")
  }
}
