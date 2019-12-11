using System;
using CodeBlogFitness.BL.Controller;

namespace CodeBlogFitness.CMD
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Вас приветствует приложение CodeBlogFitness");
			Console.WriteLine("Введите имя пользователя: ");
			var name = Console.ReadLine();
			var userController = new UserController(name);
			if (userController.IsNewUser)
			{
				Console.Write("Введите пол: ");
				var gender = Console.ReadLine();
				var birthDate = ParseDateTime();
				double weight = ParseDouble("вес");
				double height = ParseDouble("рост");

				userController.SetNewUserData(gender, birthDate, weight, height);
			}
			Console.WriteLine(userController.CurrentUser);
			Console.ReadLine();
		}

		private static DateTime ParseDateTime()
		{
			DateTime birthDate;
			while (true)
			{
				Console.Write("Введите дату рождения (dd.MM.yyyy): ");
				if (DateTime.TryParse(Console.ReadLine(), out birthDate))
				{
					break;
				}
				else
				{
					Console.WriteLine("Неверный формат даты рождения.");
				}
			}

			return birthDate;
		}

		private static double ParseDouble(string name)
		{
			while (true)
			{
				Console.Write($"Введите {name}: ");
				if (double.TryParse(Console.ReadLine(), out double value))
				{
					return value;
				}
				else
				{
					Console.WriteLine($"Неверный формат {name}");
				}
			}
		}
	}
}