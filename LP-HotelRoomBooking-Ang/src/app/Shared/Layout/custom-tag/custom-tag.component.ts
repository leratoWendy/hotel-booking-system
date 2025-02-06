import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Services/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-custom-tag',
  templateUrl: './custom-tag.component.html',
  styleUrls: ['./custom-tag.component.css']
})
export class CustomTagComponent  {

  public isGuest: boolean = false;
  public isAdminUser: boolean = false;
  //public isLoggedIn: boolean = false;

  get isLoggedIn(): boolean {
    return this.auth.isLoggedIn;
  }
  get loggedInUsrFullName(): string | undefined {
    return this.auth.loggedInUsrFullName;
  }

  constructor(private auth: AuthService, private router: Router) {
    //this.updateUserStatus();
    // this.auth.logoutEvent.subscribe(() => {
    //   this.router.navigate(['/home']);
    // });
  

  // ngOnInit(): void {
  //   this.updateUserStatus();
  // }

  
    this.isGuest = this.auth.isGuest();
    this.isAdminUser = this.auth.isAdmin();
    //this.isLoggedIn = this.auth.isLoggedIn;
    //this.loggedInUsrFullName = this.auth.loggedInUsrFullName;

    console.log("isGuest: " + this.isGuest);
    console.log("isAdminUser: " + this.isAdminUser);
    //console.log("isLoggedIn: " + this.isLoggedIn);
    console.log("loggedInUsername: " + this.loggedInUsrFullName);
  }

  Logout() {
    this.auth.logout();
    this.router.navigate(['/home']);
  }
  }





