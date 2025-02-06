import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserRegToApiVM, UserRegistrationFormVM } from 'src/app/Shared/Models/user-auth-vm';
import { AuthService } from 'src/app/Shared/Services/auth-service.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent {
  userCreated: boolean = false;

  formModel: UserRegistrationFormVM = {
    userName: '',
    email: '',
    firstName: '',
    lastName: '',
    password: '',
    confirmPassword: '',
    userRole: 'Customer'
  };

  regform: UserRegToApiVM = {
    username: '',
    email: '',
    firstname: '',
    lastname: '',
    password: '',
    role: 'Customer',
  };

  constructor(private auth: AuthService, private router: Router) { }

  onSubmit(form: NgForm) {
    //console.log(form.value);
    this.regform.username = form.value.userName;
    this.regform.email = form.value.email;
    this.regform.firstname = form.value.firstName;
    this.regform.lastname = form.value.lastName;
    this.regform.password = form.value.password;

    localStorage.setItem('onSubmitUserRegComponent', form.value);

    this.auth.userRegistration(this.regform).subscribe((data) => {
      this.userCreated = true;
    });

    alert('New User Created. Username:' + this.regform.username + ' \nRegistration successful.\nPlease login to continue!');
    this.router.navigate(['/login']);
  }

}
