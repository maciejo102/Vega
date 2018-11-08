import { ProgressService } from './../../services/progress.service';
import { PhotoService } from './../../services/photo.service';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit, ElementRef, ViewChild, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component ({
  selector: 'app-vehicle-view',
  templateUrl: './vehicle-view.component.html',
  styleUrls: ['./vehicle-view.component.css']
})
export class VehicleViewComponent implements OnInit {
  private readonly SupportedFileTypes: string[] = ["image/jpeg", "image/png"];

  @ViewChild('fileInput') fileInput: ElementRef = new ElementRef(this);
  vehicleId: any;
  vehicle: any;
  photos: any[] = [];
  progress: any;
  progressSub: any;
  uploadSub: any;

  constructor(private vehicleService: VehicleService,
    private photoService: PhotoService, 
    private route: ActivatedRoute,
    private router: Router,
    private progressService: ProgressService,
    private zone: NgZone) {

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

    this.photoService.getPhotos(this.vehicleId)
      .subscribe(p => this.photos = p);
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

  // photo section
  uploadPhoto(){
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    
    if (nativeElement.files) {
      var file = nativeElement.files[0];

       if (this.SupportedFileTypes.indexOf(file.type)  == -1){
        alert("Unsupported file type. Supported types: .jpeg, .jpg, .png")
        return;
       }

      this.progressSub = this.progressService.startTracking()
      .subscribe(progress => { 
        console.log(progress);

        // to raise property chainge must run in zone
        this.zone.run(() => {
          this.progress = progress; 
        });
      },
      () => {},
      () => { this.progress = null; }
      );

      this.uploadSub = this.photoService.upload(this.vehicleId, file)
        .subscribe(photo => { 
          this.photos.push(photo);
        }, () => alert("Error occured while uploading a file."));
    }
    
    else throw Error("File is empty or damaged.")

    nativeElement.value = "";
  } 

  deleteUpload(){
    if (this.uploadSub)
      this.uploadSub.unsubscribe();
    if (this.progressSub){
      this.progressSub.unsubscribe();
        this.progress = null;
    }
  }


}