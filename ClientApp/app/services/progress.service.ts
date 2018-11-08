import { Injectable, ContentChild } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { BrowserXhr } from '@angular/http';

@Injectable()
export class ProgressService {
    private uploadProgress: Subject<any> | undefined; 

    startTracking() {
        this.uploadProgress = new Subject();
        return this.uploadProgress;
    }

    notify(progress: any){
        if (this.uploadProgress)
        return this.uploadProgress.next(progress);
    }

    stopTracking(){
        if (this.uploadProgress)
        this.uploadProgress.complete();
    }
}

@Injectable()
export class BrowserXhrWithProgress extends BrowserXhr {
    constructor(private service: ProgressService) { super() }

    build(): XMLHttpRequest {
        var xhr: XMLHttpRequest = super.build();
    
        xhr.upload.onprogress = (event) => {
            this.service.notify(this.createProgress(event));
        }

        xhr.upload.onloadend = () =>{
            this.service.stopTracking();
        };
    return xhr;
    }

    private createProgress(event: any) {
        return {
            total: event.total,
            percentage: Math.round(event.loaded / event.total * 100)
        }
    }
}