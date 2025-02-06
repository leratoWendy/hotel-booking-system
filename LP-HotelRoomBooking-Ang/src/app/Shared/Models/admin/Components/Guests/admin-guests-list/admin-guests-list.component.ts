import { Component } from '@angular/core';
import { Guest, GuestVM } from 'src/app/Shared/Models/hotel-room-booking-vm';
import { CrudService } from 'src/app/Shared/Services/crud.service';

@Component({
  selector: 'app-admin-guests-list',
  templateUrl: './admin-guests-list.component.html',
  styleUrls: ['./admin-guests-list.component.css']
})
export class AdminGuestsListComponent {
  allGuests: GuestVM[] | undefined;

  constructor(
    private service: CrudService) { }

    ngOnInit() {
      this.service.getAllGuests().subscribe(
        (data) => {
          console.log('Fetched data:', data);  
          this.allGuests = data;
        },
        (error) => {
          console.error('Error fetching data', error);  
        }
      );
    
      console.log('Admin AdminGuestListComponent: ', this.allGuests);
    }
    

}
