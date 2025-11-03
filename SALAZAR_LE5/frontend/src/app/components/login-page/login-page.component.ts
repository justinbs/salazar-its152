import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { TokenStorageService } from '../../services/token-storage.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html'
})
export class LoginPageComponent {
  model = { username: '', password: '' };
  message = '';
  busy = false;

  constructor(
    private auth: AuthService,
    private tokenStorage: TokenStorageService
  ) {}

  onSubmit(): void {
    if (!this.model.username || !this.model.password) {
      this.message = 'Please enter username and password.';
      return;
    }
    this.busy = true;
    this.auth.login(this.model).subscribe({
      next: (res) => {
        // expecting { token: '...' }
        this.tokenStorage.saveToken(res.token);
        this.message = 'Logged in successfully. Token saved.';
        this.busy = false;
      },
      error: (err) => {
        console.error(err);
        this.message = 'Login failed.';
        this.busy = false;
      }
    });
  }
}
