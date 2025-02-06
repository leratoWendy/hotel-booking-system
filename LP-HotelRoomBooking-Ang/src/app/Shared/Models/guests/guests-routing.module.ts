import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GuestsHomeComponent } from './Components/guests-home/guests-home.component';
//import { MyBookingsComponent } from './Components/my-bookings/my-bookings.component';
import { ViewRoomsComponent } from './Components/view-rooms/view-rooms.component';
import { BookRoomComponent } from './Components/book-room/book-room.component';

const routes: Routes = [
  {
    path: '', component: GuestsHomeComponent,
    children: [
      { path: 'viewrooms', component: ViewRoomsComponent },
     { path: 'bookroom', component: BookRoomComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GuestsRoutingModule { }
