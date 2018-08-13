import  * as Raven  from 'raven-js';
import { ErrorHandler, isDevMode } from "../../../../node_modules/@angular/core";

export class AppErrorHandler implements ErrorHandler {
    handleError(error: any): void {
        if (!isDevMode()) {
            Raven.captureException(error);
        }
            
        else {
            console.log(error);
        }
    }
}