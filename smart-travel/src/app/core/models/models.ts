// ─── Auth ───────────────────────────────────────────────
export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  expiration: string;
  user: SystemUser;
}

// ─── SystemUser ─────────────────────────────────────────
export interface SystemUser {
  idSystemUser: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  registrationDate: string;
  roles: string[];
}

// ─── Service ────────────────────────────────────────────
export interface Service {
  idService: number;
  serviceDescription: string;
  price: number;
  idTypeService: number;
  typeDescription?: string;
  idSupplier: number;
  companyName?: string;
}

// ─── Package ────────────────────────────────────────────
export interface Package {
  idPackage: number;
  packageName: string;
  price: number;
  offerStart: string;
  offerEnd: string;
  details?: DetailPackage[];
}

export interface DetailPackage {
  idDetailPackage: number;
  numberPersons: number;
  costPrice: number;
  idService: number;
  serviceDescription?: string;
}

// ─── Reservation ────────────────────────────────────────
export interface Reservation {
  idReservation: number;
  dateRequest: string;
  idReservationStatus: number;
  statusDescription?: string;
  idSystemUser: number;
  idPackage?: number;
  packageName?: string;
  details?: DetailReservation[];
  payments?: Payment[];
}

export interface DetailReservation {
  idDetailReservation: number;
  dateCheckIn: string;
  dateCheckOut: string;
  total: number;
  idReservation: number;
  idService: number;
  serviceDescription?: string;
  idRoom?: number;
  idFlight?: number;
  idVehicle?: number;
}

export interface CreateReservationRequest {
  idPackage?: number;
  details: CreateDetailReservationRequest[];
  passengerIds: number[];
}

export interface CreateDetailReservationRequest {
  dateCheckIn: string;
  dateCheckOut: string;
  idService: number;
  idRoom?: number;
  idFlight?: number;
  idVehicle?: number;
}

// ─── Flight ─────────────────────────────────────────────
export interface Flight {
  idFlight: number;
  idService: number;
  price?: number;
  dateDeparture: string;
  dateArrival: string;
  capacity: number;
  airportId_Origen: number;
  airportId_Arrive: number;
  originAirportName?: string;
  originCode?: string;
  arrivalAirportName?: string;
  arrivalCode?: string;
  seats?: FlightSeat[];
}

export interface FlightSeat {
  idFlightSeat: number;
  idFlight: number;
  idSeatClass: number;
  className?: string;
  seatNumber: string;
  isAvailable: boolean;
}

export interface FlightSearchRequest {
  originAirportId: number;
  arrivalAirportId: number;
  dateDeparture: string;
  passengers: number;
}

// ─── Hotel ──────────────────────────────────────────────
export interface Hotel {
  idHotel: number;
  idDestination: number;
  city?: string;
  country?: string;
  hotelName: string;
  stars: number;
  email?: string;
  rooms?: Room[];
}

export interface Room {
  idRoom: number;
  idHotel: number;
  idTypeRoom: number;
  typeDescription?: string;
  idService: number;
  price?: number;
}

// ─── Vehicle ────────────────────────────────────────────
export interface Vehicle {
  idVehicle: number;
  idService: number;
  idDestination: number;
  make: string;
  model: string;
  transmission?: string;
  price?: number;
}

// ─── Passenger ──────────────────────────────────────────
export interface Passenger {
  idPassenger: number;
  firstName: string;
  lastName: string;
  birthDate: string;
  documentNumber: string;
  idDocumentType: number;
  documentName?: string;
  idCountry: number;
  countryName?: string;
}

export interface CreatePassengerRequest {
  firstName: string;
  lastName: string;
  birthDate: string;
  documentNumber: string;
  idDocumentType: number;
  idCountry: number;
}

// ─── Payment ────────────────────────────────────────────
export interface Payment {
  idPayment: number;
  amount: number;
  paymentDate: string;
  idPaymentStatus: number;
  statusDescription?: string;
  idReservation: number;
  idPaymentMethod: number;
  methodName?: string;
}

export interface CreatePaymentRequest {
  idReservation: number;
  amount: number;
  idPaymentMethod: number;
}

// ─── Supplier ───────────────────────────────────────────
export interface Supplier {
  idSupplier: number;
  companyName: string;
  rnc: string;
  email: string;
}

// ─── Destination ────────────────────────────────────────
export interface Destination {
  idDestination: number;
  idCountry: number;
  countryName?: string;
  city: string;
  street: string;
}

// ─── Airport ────────────────────────────────────────────
export interface Airport {
  idAirport: number;
  idDestination: number;
  city?: string;
  airportName: string;
  codeIATA: string;
}

// ─── Promotion ──────────────────────────────────────────
export interface Promotion {
  idPromotion: number;
  promotionName: string;
  idDiscountType: number;
  typeName?: string;
  discountValue: number;
  dateFrom: string;
  dateTo: string;
  minPersons: number;
  isActive: boolean;
}

// ─── Catálogos ──────────────────────────────────────────
export interface ReservationStatus {
  idReservationStatus: number;
  statusDescription: string;
}

export interface PaymentStatus {
  idPaymentStatus: number;
  statusDescription: string;
}

export interface PaymentMethod {
  idPaymentMethod: number;
  methodName: string;
  isActive: boolean;
}

export interface TypeService {
  idTypeService: number;
  typeDescription: string;
}

export interface TypeRoom {
  idTypeRoom: number;
  typeDescription: string;
}

export interface SeatClass {
  idSeatClass: number;
  className: string;
}

export interface Country {
  idCountry: number;
  countryName: string;
  isoCode: string;
}

export interface DocumentType {
  idDocumentType: number;
  documentName: string;
}


