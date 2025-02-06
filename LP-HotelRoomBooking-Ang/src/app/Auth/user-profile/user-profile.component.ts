import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserProfileVM } from 'src/app/Shared/Models/user-auth-vm';
import { AuthService } from 'src/app/Shared/Services/auth-service.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  userProfileData: UserProfileVM = {
    firstName: '',
    lastName: '',
    userName: '',
    email: '',
    userRoles: ['']
  };
  
  isAdmin: boolean = false;
  isGuest: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.getUserProfile().subscribe((data) => {
      console.log('data: ', data);
      this.userProfileData = data;

      
      this.isAdmin = this.userProfileData.userRoles.includes('Administrator');
      this.isGuest = this.userProfileData.userRoles.includes('Customer');
    });

    console.log('UserProfileComponent: ', this.userProfileData);
  }
}
