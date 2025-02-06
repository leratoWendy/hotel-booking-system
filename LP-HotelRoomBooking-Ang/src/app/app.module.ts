import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DefaultHomeComponent } from './Guest/default-home/default-home.component';
import { CustomTagComponent } from './Shared/Layout/custom-tag/custom-tag.component';
import { ContactUsComponent } from './Shared/Layout/contact-us/contact-us.component';
import { PageNotFoundComponent } from './Shared/Layout/page-not-found/page-not-found.component';
import { DefaultFooterComponent } from './Shared/Layout/default-footer/default-footer.component';
import { UserLoginComponent } from './Auth/user-login/user-login.component';
import { UserRegistrationComponent } from './Auth/user-registration/user-registration.component';
import { UserProfileComponent } from './Auth/user-profile/user-profile.component';
import { GuestsModule } from './Shared/Models/guests/guests.module';
import { AdminModule } from './Shared/Models/admin/admin.module';

@NgModule({
  declarations: [
    AppComponent,
    DefaultHomeComponent,
    CustomTagComponent,
    ContactUsComponent,
    PageNotFoundComponent,
    DefaultFooterComponent,
    UserLoginComponent,
    UserRegistrationComponent,
    UserProfileComponent,
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    GuestsModule,
    AdminModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
