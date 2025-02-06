import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLoginFormVM } from 'src/app/Shared/Models/user-auth-vm';
import { AuthService } from 'src/app/Shared/Services/auth-service.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent {
  formModel: UserLoginFormVM = {
    userName: '',
    password: ''
  };


  constructor(private auth: AuthService, private router: Router) {

  }

  ngOnInit(){

  }


  pageRedirect(): void {
    if (this.auth.isLoggedIn) {
      this.router.navigate(['/userprofile']);
    } else if (this.auth.isGuest()) {
      this.router.navigate(['/guest/viewrooms']);
    } else if (this.auth.isAdmin()) {
      this.router.navigate(['/admin/gstlist']);
    } else {
      this.router.navigate(['/home']);
    }
  }

  

  onSubmit(form: NgForm): void {
    this.auth.userLogin(form.value.userName, form.value.password).subscribe((data) => {
      setTimeout(() => {
        if (this.auth.isAdmin()) {
          this.router.navigate(['/admin/gstlist']);
        } else if (this.auth.isGuest()) {
          this.router.navigate(['/guest/viewrooms']);
        } else {
          this.router.navigate(['/home']);
        }
      }, 1000);
    });
  }

  }


