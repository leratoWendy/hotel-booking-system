import { Injectable } from '@angular/core';
import { GlobalConstants } from 'src/app/global-constants';
import { CreateBookingVM, Guest, GuestVM, GuestsBookingsVM, RoomVM } from '../Models/hotel-room-booking-vm';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth-service.service';
import { Observable, of } from 'rxjs';

//import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class CrudService {

  private _baseurl = GlobalConstants._RestBaseURL;

  localSetting = "en-GB";

  public currUser: string = localStorage.getItem('userName') || '';
  public currUserFullName: string = localStorage.getItem('fullName') || '';


  sampleViewRooms: RoomVM = {
    roomId: 0,
    roomNumber: 0,
    roomType: '',  
    nightPrice: 0,

  }

  sampleBookings: GuestsBookingsVM = {
    bookingId: 0,
    guestId: 0,
    roomId: 0,
    checkinDate: new Date(),
    checkoutDate: new Date(),
    totalPrice: 0,
  }

  sampleGuests: GuestVM = {

      guestId: 0,
      firstName: '',
      lastName: '',   
      identityUserName: '', 
      email: '',
      phone: '',
  }

  sampleCreateBooking: CreateBookingVM = {
    
      checkinDate: new Date(),
      checkoutDate: new Date(),
      guestUsername: '',
      roomNumber: 0,
    
  }

  constructor(private http: HttpClient, private authenticationService: AuthService) { }

  getAllRooms(): Observable<RoomVM[]> {
    return this.http.get<RoomVM[]>(this._baseurl + '/api/Rooms');
  }


  getAllGuests(): Observable<GuestVM[]> {
    return this.http.get<Guest[]>(this._baseurl + '/api/Guests');
  }

  createBooking(bookingData: CreateBookingVM): Observable<CreateBookingVM> {
    return this.http.post<CreateBookingVM>(this._baseurl + '/api/GuestsBookings', bookingData);
  }
  


}
