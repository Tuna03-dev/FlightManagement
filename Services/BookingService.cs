﻿using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IAirlineRepository airlineRepository;
        private readonly IAirportRepository airportRepository;
        private readonly IPassengerRepository passengerRepository;
        private readonly IFlightRepository flightRepository;
        private readonly IBookingPlatformRepository platformRepository;
       
        public BookingService()
        {
            platformRepository = new BookingPlatformRepository();
            flightRepository = new FlightRepository();
            airlineRepository = new AirlineRepository();
            airportRepository = new AirportRepository();
            passengerRepository = new PassengerRepository();
            bookingRepository = new BookingRepository();
        }
        public void AddBooking(Booking booking)
        {
            bookingRepository.AddBooking(booking);
        }

        public void DeleteBooking(int id)
        {
            bookingRepository.DeleteBooking(id);
        }

        public List<BookingInfoDTO> FindByAirlineAirportAnddate(string departureAirportCode, string ArrivalAirportCode, string airlineCode, DateTime? departureDate, DateTime? arrivalDate, DateTime? bookingTime,string name, bool status)
        {
            var foundBookings = GetBookingInfos().Where(b => (departureAirportCode == null || b.DepartingAirportCode.Equals(departureAirportCode))
                                                            && (ArrivalAirportCode == null || b.ArrivingAirportCode.Equals(ArrivalAirportCode))
                                                            && (airlineCode == null || b.AirlineCode.Equals(airlineCode))
                                                            && (departureDate == null || b.DepartureTime >= departureDate)
                                                            && (arrivalDate == null || b.ArrivalTime <= departureDate)
                                                            && (bookingTime == null || b.BookingTime >= bookingTime)
                                                            && (b.Status == status)
                                                            && b.PassengerName.ToLower().Contains(name.ToLower())).ToList();
            return foundBookings;
        }

        public List<BookingInfoDTO> FindPersonalBookingByAirlineAirportAnddate(string departureAirportCode, string ArrivalAirportCode, string airlineCode, DateTime? departureDate, DateTime? arrivalDate, DateTime? bookingTime, bool status, int passengerId)
        {
            var foundBookings = GetPersonalBookingInfor(passengerId).Where(b => (departureAirportCode == null || b.DepartingAirportCode.Equals(departureAirportCode))
                                                           && (ArrivalAirportCode == null || b.ArrivingAirportCode.Equals(ArrivalAirportCode))
                                                           && (airlineCode == null || b.AirlineCode.Equals(airlineCode))
                                                           && (departureDate == null || b.DepartureTime >= departureDate)
                                                           && (arrivalDate == null || b.ArrivalTime <= departureDate)
                                                           && (bookingTime == null || b.BookingTime >= bookingTime)
                                                           && (b.Status == status)).ToList();
            return foundBookings;
        }

        public List<Booking> GetAllBookings()
        {
            return bookingRepository.GetAll();
        }

        public Booking GetBookingById(int id)
        {
            return bookingRepository.GetBookingById(id);
        }

        public List<BookingInfoDTO> GetBookingInfos()
        {
            var bookingInfoDTOs = from booking in GetAllBookings()
                                  join flight in flightRepository.GetAllFlights() on booking.FlightId equals flight.Id
                                  join platform in platformRepository.GetAll() on booking.BookingPlatformId equals platform.Id
                                  join passenger in passengerRepository.GetAll() on booking.PassengerId equals passenger.Id
                                  join departingAirPort in airportRepository.GetAllAirports() on flight.DepartingAirport equals departingAirPort.Id
                                  join arrivingAirPort in airportRepository.GetAllAirports() on flight.ArrivingAirport equals arrivingAirPort.Id
                                  join airline in airlineRepository.GetAll() on flight.AirlineId equals airline.Id
                                  
                                  select new BookingInfoDTO()
                                  {
                                      id = booking.Id,
                                      PassengerName = passenger.FirstName + " " + passenger.LastName,
                                      AirlineName = airline.Name,
                                      ArrivingAirportName = arrivingAirPort.Name,
                                      DepartingAirportName = departingAirPort.Name,
                                      BookingTime = booking.BookingTime,
                                      DepartureTime = flight.DepartureTime,
                                      ArrivalTime = flight.ArrivalTime,
                                      PlatformName = platform.Name,
                                      DepartingAirportCode = departingAirPort.Code,
                                      ArrivingAirportCode = arrivingAirPort.Code,
                                      AirlineCode = airline.Code,
                                      Status = booking.Status
                                  };

            return bookingInfoDTOs.ToList();                  
                                 
                                 
        }

        public List<BookingInfoDTO> GetPersonalBookingInfor(int passengerId)
        {
            var bookingInfoDTOs = from booking in GetAllBookings()
                                  join flight in flightRepository.GetAllFlights() on booking.FlightId equals flight.Id
                                  join platform in platformRepository.GetAll() on booking.BookingPlatformId equals platform.Id
                                  join passenger in passengerRepository.GetAll() on booking.PassengerId equals passenger.Id
                                  join departingAirPort in airportRepository.GetAllAirports() on flight.DepartingAirport equals departingAirPort.Id
                                  join arrivingAirPort in airportRepository.GetAllAirports() on flight.ArrivingAirport equals arrivingAirPort.Id
                                  join airline in airlineRepository.GetAll() on flight.AirlineId equals airline.Id
                                  where passengerId == booking.PassengerId
                                  select new BookingInfoDTO()
                                  {
                                      id = booking.Id,
                                      PassengerName = passenger.FirstName + " " + passenger.LastName,
                                      AirlineName = airline.Name,
                                      ArrivingAirportName = arrivingAirPort.Name,
                                      DepartingAirportName = departingAirPort.Name,
                                      BookingTime = booking.BookingTime,
                                      DepartureTime = flight.DepartureTime,
                                      ArrivalTime = flight.ArrivalTime,
                                      PlatformName = platform.Name,
                                      DepartingAirportCode = departingAirPort.Code,
                                      ArrivingAirportCode = arrivingAirPort.Code,
                                      AirlineCode = airline.Code,
                                      Status = booking.Status
                                  };

            return bookingInfoDTOs.ToList();
        }

        public void UpdateBooking(Booking booking)
        {
            bookingRepository.UpdateBooking(booking);
        }
    }
}