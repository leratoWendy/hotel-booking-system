import { Component } from '@angular/core';
import { CreateBookingVM } from '../../../hotel-room-booking-vm';
import { CrudService } from 'src/app/Shared/Services/crud.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-book-room',
  templateUrl: './book-room.component.html',
  styleUrls: ['./book-room.component.css']
})
export class BookRoomComponent {
  
  booking: CreateBookingVM = {
    checkinDate: new Date(),
    checkoutDate: new Date(),
    guestUsername: '',
    roomNumber: 0
  };

  constructor(private service: CrudService) {}

  onSubmit(form: NgForm): void {
    if (form.invalid) {
       
      return;
    }
    
    this.service.createBooking(this.booking).subscribe(response => {
      console.log('Booking created:', response);
      form.resetForm(); 
    }, error => {
      console.error('Error creating booking:', error);
    });
  }

}
