import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { type } from 'os';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(
        req: import('@angular/common/http/http').HttpRequest<any>,
        next: import('@angular/common/http/http').HttpHandler
    ): import('rxjs').Observable<import('@angular/common/http/http').HttpEvent<any>> {

        return next.handle(req).pipe(
            catchError(error => {
                if (error.status === 401) {
                    return throwError(error.statusText);
                }
                if (error instanceof HttpErrorResponse) {
                    const applicationError = error.headers.get('Application-Error');
                    if (applicationError) {
                        return throwError(applicationError);
                    }
                    const serverError = error.error;
                    let modelSatateErrors = '';
                    if (serverError.errors && typeof serverError.errors === 'object') {
                        for (const key in serverError.errors) {
                            if (serverError.errors[key]) {
                                modelSatateErrors += serverError.errors[key] + '\n';
                            }
                        }
                    }
                    return throwError(modelSatateErrors || serverError || 'Server Error');
                }
            })
        );

    }

}
export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
