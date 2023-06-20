using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users;
        private VehicleRepository vehicles;
        private RouteRepository routes;

        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }
        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int currentRouteId = routes.GetAll().Count() + 1;
            IRoute route = new Route(startPoint, endPoint, length, currentRouteId);

            IRoute lockedRoute = routes.GetAll().FirstOrDefault(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length > length);

            if (routes.GetAll().Any(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length == length))
            {
                return String.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
            }
            else if (routes.GetAll().Any(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length < length))
            {
                return String.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
            }
            
            routes.AddModel(route);

            if(lockedRoute != null)
            {
                lockedRoute.LockRoute();
            }

            return String.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);
            IRoute route = routes.FindById(routeId);
            IVehicle vehicle = vehicles.FindById(licensePlateNumber);

            if (user.IsBlocked)
            {
                return String.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }
            else if(vehicle.IsDamaged)
            {
                return String.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }
            else if(route.IsLocked)
            {
                return String.Format(OutputMessages.RouteLocked, routeId);
            }

            vehicle.Drive(route.Length);

            if(isAccidentHappened == true)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
            }

            return vehicle.ToString().TrimEnd();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.FindById(drivingLicenseNumber) != null)
            {
                return String.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }

            IUser user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);
            return String.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }

        public string RepairVehicles(int count)
        {
            var selectedVehciles = vehicles.GetAll().Where(x => x.IsDamaged == true).OrderBy(x => x.Brand).ThenBy(x => x.Model);

            int countNumber = Math.Max(selectedVehciles.Count(), count);
            
                        
            foreach (var vehicle in selectedVehciles)
            {
                int count2 = 0;

                for (int i = 0;  i < countNumber; i++)
                {
                    vehicle.ChangeStatus();
                    vehicle.Recharge();
                    count2++;

                    if (count2 == countNumber)
                    {
                        break;
                    }
                }
            }

            return String.Format(OutputMessages.RepairedVehicles, countNumber);
        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            
            IVehicle vehicle;
            if (vehicleType == nameof(PassengerCar))
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            else if (vehicleType == nameof(CargoVan))
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            else
            {
                return String.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
            }

            if (vehicles.FindById(licensePlateNumber) != null)
            {
                return String.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            }

            vehicles.AddModel(vehicle);
            return String.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        }

        public string UsersReport()
        {
            var arrangedUsers = users.GetAll().OrderByDescending(x => x.Rating).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** E-Drive-Rent ***");

            foreach (var user in arrangedUsers)
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
