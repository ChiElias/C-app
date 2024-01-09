import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((resp: HttpErrorResponse) => {
        if (resp) {
          switch (resp.status) {
            case 400:
              const errors = resp.error.errors
              if (errors) {
                const modelStateErrs = []
                for (const key in errors) {
                  if (errors[key])
                    modelStateErrs.push(errors[key])
                }
                throw modelStateErrs.flat()
              } else
                this.toastr.error(resp.error, resp.status.toString())
              break
            case 401:
              this.toastr.error("Unauthorised", resp.status.toString())
              break
            case 404:
              this.router.navigateByUrl('/not-found')
              break
            case 500:
              const navigationExtras: NavigationExtras = { state: { errors: resp.error } }
              this.router.navigateByUrl('/server-error', navigationExtras)
              break
            default:
              this.toastr.error("Something unexpected went wrong")
              console.log(resp)
              break
          }
        }
        throw resp
      })
    )
  }
}
