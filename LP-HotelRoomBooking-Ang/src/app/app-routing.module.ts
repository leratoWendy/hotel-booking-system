import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DefaultHomeComponent } from './Guest/default-home/default-home.component';
import { ContactUsComponent } from './Shared/Layout/contact-us/contact-us.component';
import { PageNotFoundComponent } from './Shared/Layout/page-not-found/page-not-found.component';
import { UserLoginComponent } from './Auth/user-login/user-login.component';
import { UserRegistrationComponent } from './Auth/user-registration/user-registration.component';
import { UserProfileComponent } from './Auth/user-profile/user-profile.component';
import { GuestGuard } from './Guards/Auth/guest.guard';
import { AdminGuard } from './Guards/Auth/admin.guard';

const routes: Routes = [
  { path: 'home', component: DefaultHomeComponent },
  { path: 'login', component: UserLoginComponent },
  { path: 'register', component: UserRegistrationComponent },
  { path: 'userprofile', component: UserProfileComponent },
  { path: 'contactus', component: ContactUsComponent },
  {
    path: 'guest',
    canActivate: [GuestGuard],
    loadChildren: () =>
      import('./Shared/Models/guests/guests.module').then((m) => m.GuestsModule),
  },
  {
    path: 'admin',
    canActivate: [AdminGuard],
    loadChildren: () =>
      import('./Shared/Models/admin/admin.module').then((m) => m.AdminModule),
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
