
export interface UserLoginFormVM {
    userName: string;
    password: string;
  }
  
  
  //User registration input form
  export interface UserRegistrationFormVM {
    userName: string;
    email: string;
    firstName: string;
    lastName: string;
    password: string;
    confirmPassword: string;
    userRole: string;
  }
  
  //User registration object sent to REST API
  export interface UserRegToApiVM {
    username: string;
    email: string;
    firstname: string;
    lastname: string;
    password: string;
    role: string;
  }
  
  export interface UserRegResponseVM {
    username: string;
  }
  
  
  
  export interface CurrentUserVM {
    userName: string;
    userRole: string;
  }
  
  export interface LoginResponseMessageVM {
    userName: string;
    userRole: string;
    firstName: string;
    lastName: string;
    fullName: string;
    isAdminUserRole: boolean;
    message: string;
  }
  
  
  export interface LoginResponseVM {
    token: string;
    expiration: string;
    firstName: string;
    lastName: string;
    userName: string;
    roles: string[];
  }
  
  
  //object returned after login
  export interface CurrentLoggedInUserVM {
    token: string;
    expiration: string;
    firstName: string;
    lastName: string;
    userName: string;
    isadmin: boolean;
    userRole: string;
  }
  
  
  export interface UserProfileVM {
    firstName: string;
    lastName: string;
    email: string;
    userName: string;
    userRoles: [string];
  }
  
  