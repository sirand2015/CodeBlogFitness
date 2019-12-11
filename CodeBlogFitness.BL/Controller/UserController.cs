using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CodeBlogFitness.BL.Model;

namespace CodeBlogFitness.BL.Controller
{
	/// <summary>
	/// Контроллер пользователей
	/// </summary>
	public class UserController
	{
		/// <summary>
		/// Пользователь
		/// </summary>
		public User User { get; }

		/// <summary>
		/// Создание нового пользователя
		/// </summary>
		/// <param name="user">Пользователь приложения</param>
		public UserController(string userName, string genderName, DateTime birthDay, double weight, double height)
		{
			//TODO: Проверка

			var gender = new Gender(genderName);
			User = new User(userName, gender, birthDay, weight, height);
		}

		/// <summary>
		/// Сохранить данные пользователя
		/// </summary>
		public void Save()
		{
			var formatter = new BinaryFormatter();
			using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, User);
			}
		}

		/// <summary>
		/// Получить данные пользователя
		/// </summary>
		/// <returns>Пользователь приложения</returns>
		public UserController()
		{
			var formatter = new BinaryFormatter();
			using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
			{
				if (formatter.Deserialize(fs) is User)
				{
					User = formatter.Deserialize(fs) as User;
				}
				else
				{
					throw new FileLoadException("Не удалось получить данные пользователя из файла ", "user.dat");
				}
			}
		}
	}
}