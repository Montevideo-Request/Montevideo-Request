import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { faKey } from '@fortawesome/free-solid-svg-icons';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  success = false;
  submitted = false;
  token: string;
  returnUrl: string;
  error = '';
  faUser = faUser;
  faKey = faKey;
  faSpinnner = faSpinner;

  constructor(private formBuilder: FormBuilder, private auth: AuthenticationService, public router: Router) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.compose([Validators.required])]
    });

    this.auth.logout();
  }

  get a() { return this.loginForm.controls; }

  public submit() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    this.auth.login(this.a.email.value, this.a.password.value)
      .pipe(first())
      .subscribe(
        result => {
          this.loading = false;
          this.success = true;
          this.router.navigate(['welcome']);
        }
        , (err: any) => {
          this.error = err;
          if (err.status === 401) {
            this.error = 'Invalid username or password';
          }
          this.loading = false;
        }
      );
  }
}
