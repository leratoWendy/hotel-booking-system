import { Component, OnInit } from '@angular/core';
import { UserProfileVM } from 'src/app/Shared/Models/user-auth-vm';
import { AuthService } from 'src/app/Shared/Services/auth-service.service';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.css']
})
export class ContactUsComponent implements OnInit {
  isAdmin: boolean = false;
  isGuest: boolean = false;
  userProfileData: UserProfileVM = {
    firstName: '',
    lastName: '',
    userName: '',
    email: '',
    userRoles: ['']
  };

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.getUserProfile().subscribe((data) => {
      console.log('data: ', data);
      this.userProfileData = data;

      this.isAdmin = this.userProfileData.userRoles.includes('Administrator');
      this.isGuest = this.userProfileData.userRoles.includes('Customer');

      
    });
  }
}
