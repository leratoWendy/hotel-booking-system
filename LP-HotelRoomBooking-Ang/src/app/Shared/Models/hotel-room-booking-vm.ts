export interface BookingStatus {
    statusId: number;
    statusName: string;  
  }
  
  export interface Guest {
    guestId: number;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    phone: string;
    bookings: Booking[];
    payments: Payment[];
  }
  
  export interface Payment {
    paymentId: number;
    guestId: number;
    bookingId: number;
    paymentDate: Date;
    amount: number;
    booking: Booking;
    guest: Guest;
  }
  
  export interface Room {
    roomId: number;
    roomNumber: number;
    roomType: string;
    nightPrice: number;
    bookings: Booking[];
  }
  
  export interface RoomVM {
    roomId: number;
    roomNumber: number;
    roomType: string;  
    nightPrice: number;
  }
  
  export interface GuestsBookingsVM {
    bookingId: number;
    guestId: number;
    roomId: number;
    checkinDate: Date;
    checkoutDate: Date;
    totalPrice: number;
  }

  export interface GuestVM {
    guestId: number;
    firstName?: string;
    lastName?: string;
    identityUserName?: string;
    email: string;
    phone: string;
  }
  
  
//   export interface GuestVM {
//     guestId: number;
//     firstName: string;  
//     lastName: string;   
//     userName: string; 
//     email: string;
//     phone: string;
//   }
  
  export interface Booking {
    bookingId: number;
    guestId: number;
    roomId: number;
    checkinDate: Date;
    checkoutDate: Date;
    totalPrice: number;
    guest: Guest;
    room: Room;
    payments: Payment[];
  }

  export interface CreateBookingVM {
    checkinDate: Date;
    checkoutDate: Date;
    guestUsername: string;
    roomNumber: number;
  }
  
  