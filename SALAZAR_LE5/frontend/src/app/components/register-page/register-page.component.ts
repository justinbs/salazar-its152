import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html'
})
export class RegisterPageComponent {
  model = { username: '', password: '' };
  message = '';
  busy = false;

  constructor(private auth: AuthService) {}

  onSubmit(): void {
    if (!this.model.username || !this.model.password) {
      this.message = 'Please enter username and password.';
      return;
    }
    this.busy = true;
    this.auth.register(this.model).subscribe({
      next: () => {
        this.message = 'Registration successful. You can now login.';
        this.busy = false;
      },
      error: (err) => {
        console.error(err);
        this.message = 'Registration failed.';
        this.busy = false;
      }
    });
  }
}
