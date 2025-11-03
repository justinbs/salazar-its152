import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private base = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  login(payload: { username: string; password: string }) {
    // Adjust endpoint names to match your LE4 backend if different:
    return this.http.post<{ token: string }>(`${this.base}/auth/login`, payload);
  }

  register(payload: { username: string; password: string }) {
    return this.http.post(`${this.base}/auth/register`, payload);
  }
}
