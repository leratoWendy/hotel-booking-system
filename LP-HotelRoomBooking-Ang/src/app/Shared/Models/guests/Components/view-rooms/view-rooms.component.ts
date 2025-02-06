import { Component, OnInit } from '@angular/core';
import { RoomVM } from '../../../hotel-room-booking-vm';
import { CrudService } from 'src/app/Shared/Services/crud.service';

@Component({
  selector: 'app-view-rooms',
  templateUrl: './view-rooms.component.html',
  styleUrls: ['./view-rooms.component.css']
})
export class ViewRoomsComponent implements OnInit {
  public allRooms: RoomVM[] = [];

  constructor(private service: CrudService) { }

  ngOnInit(): void {
    this.service.getAllRooms().subscribe((data: RoomVM[]) => {
      this.allRooms = data;
    });

    console.log('Rooms: ', this.allRooms);
  }
}
