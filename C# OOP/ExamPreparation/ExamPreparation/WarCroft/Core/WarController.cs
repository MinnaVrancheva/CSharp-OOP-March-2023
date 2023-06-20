using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private readonly List<Character> characters;
		private readonly List<Item> items;
		public WarController()
		{
			characters = new List<Character>();
			items = new List<Item>();
		}

		public string JoinParty(string[] args)
		{
			string characterType = args[0];
			string name = args[1];

			Character character;
			if(characterType == nameof(Warrior))
            {
				character = new Warrior(name);
            }
			else if (characterType == nameof(Priest))
            {
				character = new Priest(name);
            }
			else
            {
				throw new ArgumentException(String.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }

			characters.Add(character);
			return String.Format(SuccessMessages.JoinParty, name);
		}

		public string AddItemToPool(string[] args)
		{
			string name = args[0];

			Item item;
			if(name == nameof(HealthPotion))
            {
				item = new HealthPotion();
            }
			else if (name == nameof(FirePotion))
            {
				item = new FirePotion();
            }
			else
            {
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, name));
			}

			items.Add(item);
			return String.Format(SuccessMessages.AddItemToPool, name);
		}

		public string PickUpItem(string[] args)
		{
			string characterName = args[0];
			Character character = characters.FirstOrDefault(x => x.Name == characterName);

			if (character == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
			}

			if(items.Count == 0)
            {
				throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
			}

			Item lastItem = items.Last();
			character.Bag.AddItem(lastItem);
			items.Remove(lastItem);

			return string.Format(SuccessMessages.PickUpItem, characterName, lastItem.GetType().Name);
		}

		public string UseItem(string[] args)
		{
			string characterName = args[0];
			string itemName = args[1];

			Character character = characters.FirstOrDefault(x => x.Name == characterName);

			if (character == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
			}

			//if(!character.Bag.Items.Any())
            //{
			//	throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            //}

			Item item = character.Bag.Items.FirstOrDefault(x => x.GetType().Name == itemName);

			//if(item == null)
            //{
			//	throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, itemName));
			//}

			character.Bag.GetItem(itemName);
			character.UseItem(item);
			return string.Format(SuccessMessages.UsedItem, characterName, item.GetType().Name);
		}

		public string GetStats()
		{
			StringBuilder sb = new StringBuilder();

			foreach (Character character in characters.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health))
            {
				string deadOrAlive = character.IsAlive ? "Alive" : "Dead";
				sb.AppendLine($"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, Status: {deadOrAlive}");
            }

			return sb.ToString().TrimEnd();
		}

		public string Attack(string[] args)
		{
			string attackerName = args[0];
			string receiverName = args[1];

			Character attacker = characters.FirstOrDefault(x => x.Name == attackerName);
			Character defender = characters.FirstOrDefault(x => x.Name == receiverName);

			 if (attacker == null || defender == null)
            {
				string firstCharacter = attacker == null ? attackerName : receiverName;
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, firstCharacter));
            }

			if (attacker is Priest)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attacker.Name));
			}

			Warrior warrior = attacker as Warrior;
			warrior.Attack(defender);

			string result = defender.IsAlive
				? string.Format(SuccessMessages.AttackCharacter, warrior.Name, defender.Name, warrior.AbilityPoints, defender.Name, defender.Health, defender.BaseHealth, defender.Armor, defender.BaseArmor)
				: string.Format(SuccessMessages.AttackCharacter, warrior.Name, defender.Name, warrior.AbilityPoints, defender.Name, defender.Health, defender.BaseHealth, defender.Armor, defender.BaseArmor)
				+ Environment.NewLine
				+ string.Format(SuccessMessages.AttackKillsCharacter, defender.Name);

			return result;
		}

		public string Heal(string[] args)
		{
			string healerName = args[0];
			string healingReceiverName = args[1];

			Character healer = characters.FirstOrDefault(x => x.Name == healerName);
			Character receiver = characters.FirstOrDefault(x => x.Name == healingReceiverName);

			if (healer == null || receiver == null)
			{
				string firstCharacter = healer == null ? healerName : healingReceiverName;
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, firstCharacter));
			}

			if (healer is Warrior)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healer.Name));
			}

			Priest priest = healer as Priest;
			priest.Heal(receiver);

			return string.Format(SuccessMessages.HealCharacter, priest.Name, receiver.Name, healer.AbilityPoints, receiver.Name, receiver.Health);
		}
	}
}
