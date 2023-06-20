using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private DelicacyRepository delicacyMenu;
        private CocktailRepository cocktailMenu;
        private double currentBill;
        private double turnover;

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            delicacyMenu = new DelicacyRepository();
            cocktailMenu = new CocktailRepository();
            currentBill = 0;
            turnover = 0;
        }

        public int BoothId { get { return boothId; } private set { boothId = value; } }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                }
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => this.delicacyMenu;

        public IRepository<ICocktail> CocktailMenu => this.cocktailMenu;

        public double CurrentBill => this.currentBill;

        public double Turnover => this.turnover;

        public bool IsReserved { get; private set; }

        public void ChangeStatus()
        {
            if (!IsReserved)
            {
                this.IsReserved = true;
            }
            this.IsReserved = false;
        }

        public void Charge()
        {
            this.turnover += currentBill;
            this.currentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            this.currentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {this.BoothId}");
            sb.AppendLine($"Capacity: {this.Capacity}");
            sb.AppendLine($"Turnover: {this.Turnover:F2} lv");
            sb.AppendLine($"-Cocktail menu:");

            foreach (var cocktail in cocktailMenu.Models)
            {
                sb.AppendLine($"--{cocktail}");
            }

            sb.AppendLine($"-Delicacy menu:");

            foreach (var delicacy in delicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
