import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { ApiResponse } from 'src/app/models/ApiResponse';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private apiService: ApiService
  ) { }

  register(username: string, name: string, surname: string, email: string, password: string):  Observable<ApiResponse>{
    return this.apiService.post('/Users', {username, name, surname, email, password})
  }
}
