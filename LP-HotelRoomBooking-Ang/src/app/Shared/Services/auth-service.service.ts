import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { GlobalConstants } from 'src/app/global-constants';
import { UserProfileVM, UserRegToApiVM, UserLoginFormVM, LoginResponseVM, LoginResponseMessageVM } from '../Models/user-auth-vm';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _baseurl: string = GlobalConstants._RestBaseURL;

  public currentUserIsAdmin: boolean = false;

  public userProfileInfo: UserProfileVM = {
    firstName: '',
    lastName: '',
    userName: '',
    email: '',
    userRoles: ['']
  }

  public logoutEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor(private http: HttpClient) { }

  getUserToken() {
    let currUserToken: string = localStorage.getItem('usertoken') || 'N/A';
    console.log('getUserToken: currUserToken' + currUserToken);

    if (this.isLoggedIn && currUserToken && currUserToken !== 'N/A') {
      return currUserToken;
    } else {
      return '';
    }
  }

  getUserProfile(): Observable<UserProfileVM> {
    let UserToken: string = this.getUserToken();
    if (this.isLoggedIn && UserToken) {
      let reqHeader = new HttpHeaders().set("Authorization", "bearer " + UserToken);
      return this.http.get<UserProfileVM>(this._baseurl + '/api/UserProfile', { headers: reqHeader });
    } else {
      return of(this.userProfileInfo);
    }
  }

  getAdminAllUserProfile(): Observable<UserProfileVM[]> {
    let UserToken: string = this.getUserToken();
    if (this.isLoggedIn && UserToken) {
      let reqHeader = new HttpHeaders().set("Authorization", "bearer " + UserToken);
      return this.http.get<UserProfileVM[]>(this._baseurl + '/api/UserProfile/AllUsers', { headers: reqHeader });
    } else {
      return of([this.userProfileInfo]);
    }
  }

  userRegistration(newUserRegData: UserRegToApiVM): Observable<any> {
    let regUrl: string = this._baseurl + '/api/ApplicationUser/Register';

    return this.http.post(regUrl, newUserRegData).pipe(
      map((data) => {
        console.log('Registration response data: ', data);
        return data;
      }),
      catchError((err) => {
        console.error(err);
        let errorResponse = {
          'userRegistrationErrorMessage': 'Error: Unable to Register user: ' + newUserRegData.username + ' Server Error: ' + err
        };
        localStorage.setItem('userRegistrationErrorMessage', errorResponse.userRegistrationErrorMessage);
        return of(errorResponse);
      })
    );
  }

  userLogin(userName: string, userPw: string): Observable<any> {
    let loginUrl: string = this._baseurl + '/api/ApplicationUser/Login';

    let logObj: UserLoginFormVM = {
      userName: userName,
      password: userPw
    };

    return this.http.post(loginUrl, logObj).pipe(
      map((data: any) => {
        localStorage.setItem('user', JSON.stringify(data));
        const loggedInUser: LoginResponseVM = data;
        const currentUserLoginResponse: LoginResponseMessageVM = {
          userName: loggedInUser.userName,
          userRole: loggedInUser.roles[0] || '',
          firstName: loggedInUser.firstName,
          lastName: loggedInUser.lastName,
          fullName: `${loggedInUser.firstName} ${loggedInUser.lastName}`,
          isAdminUserRole: loggedInUser.roles.includes('Administrator'),
          message: 'Success'
        };

        localStorage.setItem('isAdminUser', currentUserLoginResponse.isAdminUserRole ? 'TRUE' : 'FALSE');
        localStorage.setItem('usertoken', loggedInUser.token);
        localStorage.setItem('userRole', currentUserLoginResponse.userRole);
        localStorage.setItem('userName', currentUserLoginResponse.userName);
        localStorage.setItem('firstName', currentUserLoginResponse.firstName);
        localStorage.setItem('lastName', currentUserLoginResponse.lastName);
        localStorage.setItem('fullName', currentUserLoginResponse.fullName);

        return currentUserLoginResponse;
      }),
      catchError((err) => {
        console.error(err);
        const errorResponse: LoginResponseMessageVM = {
          userName: '',
          userRole: '',
          firstName: '',
          lastName: '',
          fullName: '',
          isAdminUserRole: false,
          message: 'Error: Unable to login user: ' + logObj.userName + ' Server Error: ' + err
        };
        localStorage.setItem('loginErrorMessage', errorResponse.message);
        return of(errorResponse);
      })
    );
  }

  // isLoggedIn(): boolean {
  //   let loggedInUser = localStorage.getItem('user');
  //   let currUser: string = localStorage.getItem('userName') || '';
  //   return !!loggedInUser && !!currUser;
  // }
  get isLoggedIn(): boolean {
    let loggedInUser = localStorage.getItem('user');
    let currUser: string = localStorage.getItem('userName') || '';
    console.log("isLoggedIn - loggedInUser: " + loggedInUser);
    console.log("isLoggedIn - currUser: " + currUser);

    if (loggedInUser && currUser) {
      return true;
    } else {
      return false;
    }
  }

  get loggedInUsrFullName(): string {
    return localStorage.getItem('fullName') || '';
  }

  isGuest(): boolean {
    let userRole: string = localStorage.getItem('userRole') || '';
    return this.isLoggedIn && userRole === 'Customer';
  }

  isAdmin(): boolean {
    let isAdminUser: string = localStorage.getItem('isAdminUser') || 'NA';
    return this.isLoggedIn && isAdminUser === 'TRUE';
  }

  logout() {
    localStorage.clear();
    this.logoutEvent.emit();
  }
}
