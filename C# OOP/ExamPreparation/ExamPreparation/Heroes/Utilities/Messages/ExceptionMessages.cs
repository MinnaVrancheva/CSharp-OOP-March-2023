using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Utilities.Messages
{
    public static class ExceptionMessages
    {
        public const string WeaponTypeCannotBeNull = "Weapon type cannot be null or empty.";
        public const string DurabilityBelowZero = "Durability cannot be below 0.";
        public const string InvalidHeroName = "Hero name cannot be null or empty.";
        public const string TooLowHeroHealth = "Hero health cannot be below 0.";
        public const string TooLowHeroArmour = "Hero armour cannot be below 0.";
        public const string WeaponIsNull = "Weapon cannot be null.";
        public const string HeroAlreadyExists = "The hero {0} already exists.";
        public const string InvalidHeroType = "Invalid hero type.";
        public const string WeaponAlreadyExists = "The weapon {0} already exists.";
        public const string InvalidWeaponType = "Invalid weapon type.";
        public const string HeroDoesNotExists = "Hero {0} does not exist.";
        public const string WeaponDoesNotExists = "Weapon {0} does not exist.";
        public const string HeroHasWeapon = "Hero {0} is well-armed.";
    }
}
