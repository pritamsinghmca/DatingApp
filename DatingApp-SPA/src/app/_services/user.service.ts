import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

// const httpOptions = {
//   headers: new HttpHeaders({
//     'Authorization': 'Bearer ' + localStorage.getItem('token')
//   })
// };


@Injectable({
  providedIn: 'root'
})
export class UserService {
  basUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.basUrl + 'user');
  }
  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.basUrl + 'user/' + id);
  }
}
