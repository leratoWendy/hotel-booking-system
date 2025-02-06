import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { GuestsRoutingModule } from './guests-routing.module';
import { GuestsListComponent } from './Components/guests-list/guests-list.component';
import { ViewRoomsComponent } from './Components/view-rooms/view-rooms.component';
import { GuestsHomeComponent } from './Components/guests-home/guests-home.component';
//import { MyBookingsComponent } from './Components/my-bookings/my-bookings.component';
import { BookRoomComponent } from './Components/book-room/book-room.component';


@NgModule({
  declarations: [
    GuestsListComponent,
    ViewRoomsComponent,
    GuestsHomeComponent,
    //MyBookingsComponent,
    BookRoomComponent,
    
  ],
  imports: [
    CommonModule,
    GuestsRoutingModule,
    FormsModule
  ]
})
export class GuestsModule { }
