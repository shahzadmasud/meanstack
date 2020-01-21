import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/' ;
  userToken: any;

constructor(private http: HttpClient) { }

login(model: any) {
  return this.http.post(this.baseUrl + 'login', model).pipe(
    map((response: any) => {
      const user = response;
      console.log(user) ;
      if (user) {
        localStorage.setItem('token', user.passwordHash);
        this.userToken = user.passwordHash;
      }
    })
  );
}

register(model: any) {
  return this.http.post(this.baseUrl + 'register', model);
}

// private requestOptions() {
//   const headers = new Headers({'Content-type': 'application/json'});
//   return new RequestOptions({headers: headers});
// }

}
