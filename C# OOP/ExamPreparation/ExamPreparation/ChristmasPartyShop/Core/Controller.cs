using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }
        public string AddBooth(int capacity)
        {
            int boothId = this.booths.Models.Count + 1;

            IBooth booth = new Booth(boothId, capacity);

            booths.AddModel(booth);

            return String.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (booths.Models.Any(x => x.CocktailMenu.Models.Any(x => x.Name == cocktailName && x.Size == size)))
            {
                return String.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            if (size != "Small" && size != "Large" && size != "Middle")
            {
                return String.Format(OutputMessages.InvalidCocktailSize, size);
            }

            ICocktail cocktail;
            if (cocktailTypeName == nameof(Hibernation))
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            else if (cocktailTypeName == nameof(MulledWine))
            {
                cocktail = new MulledWine(cocktailName, size);
            }
            else
            {
                return String.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            IBooth boot = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            boot.CocktailMenu.AddModel(cocktail);

            return String.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (booths.Models.Any(x => x.DelicacyMenu.Models.Any(x => x.Name == delicacyName)))
            {
                return String.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy delicacy;
            if (delicacyTypeName == nameof(Gingerbread))
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == nameof(Stolen))
            {
                delicacy = new Stolen(delicacyName);
            }
            else
            {
                return String.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            IBooth boot = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            boot.DelicacyMenu.AddModel(delicacy);

            return String.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            return booth.ToString().TrimEnd();
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Bill {booth.CurrentBill:F2} lv");

            booth.Charge();
            booth.ChangeStatus();

            sb.AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            var selectedBooths = booths.Models.Where(x => x.IsReserved == false && x.Capacity >= countOfPeople).OrderBy(x => x.Capacity).ThenByDescending(x => x.BoothId);

            IBooth booth = selectedBooths.First();
            if (booth == null)
            {
                return String.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            booth.ChangeStatus();
            return String.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            string[] orderParameters = order.Split("/");

            if (orderParameters[0] != nameof(Gingerbread)
                && orderParameters[0] != nameof(Stolen)
                && orderParameters[0] != nameof(Hibernation)
                && orderParameters[0] != nameof(MulledWine))
            {
                return string.Format(OutputMessages.NotRecognizedType, orderParameters[0]);
            }

            if (!booth.DelicacyMenu.Models.Any(x => x.Name == orderParameters[1]) && 
            !booth.CocktailMenu.Models.Any(x => x.Name == orderParameters[1]))
            {
                return String.Format(OutputMessages.NotRecognizedItemName, orderParameters[0], orderParameters[1]);
            }

            bool IsCocktail = false;

            if (orderParameters[0] == "Hibernation" || orderParameters[0] == "MulledWine")
            {
                IsCocktail = true;
            }
            if (IsCocktail)
            {
                ICocktail desiredCocktail = booth
                   .CocktailMenu.Models
                   .FirstOrDefault(m => m.GetType().Name == orderParameters[0] && m.Name == orderParameters[1] && m.Size == orderParameters[3]);

                if (!booth.CocktailMenu.Models.Any(x => x.GetType().Name == orderParameters[0] && x.Name == orderParameters[1] && x.Size == orderParameters[3]))
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, orderParameters[3], orderParameters[1]);
                }

                booth.UpdateCurrentBill(double.Parse(orderParameters[2]) * desiredCocktail.Price);
                return String.Format(OutputMessages.SuccessfullyOrdered, boothId, orderParameters[2], orderParameters[1]);
            }
            else
            {
                IDelicacy desiredDelicacy = booth
                   .DelicacyMenu.Models
                   .FirstOrDefault(m => m.GetType().Name == orderParameters[0] && m.Name == orderParameters[1]);

                if (!booth.DelicacyMenu.Models.Any(x => x.GetType().Name == orderParameters[0] && x.Name == orderParameters[1]))
                {
                    return String.Format(OutputMessages.DelicacyAlreadyAdded, orderParameters[0], orderParameters[1]);
                }

                booth.UpdateCurrentBill(double.Parse(orderParameters[2]) * desiredDelicacy.Price);
                return String.Format(OutputMessages.SuccessfullyOrdered, boothId, orderParameters[2], orderParameters[1]);
            }
        }
    }
}
