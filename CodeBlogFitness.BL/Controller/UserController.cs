using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using CodeBlogFitness.BL.Model;

namespace CodeBlogFitness.BL.Controller
{
	/// <summary>
	/// Контроллер пользователей
	/// </summary>
	public class UserController : ControllerBase
	{
		private const string USERS_FILE_NAME = "users.dat";
		/// <summary>
		/// Список пользователей
		/// </summary>
		public List<User> Users { get; }

		/// <summary>
		/// Текущий пользователь
		/// </summary>
		public User CurrentUser { get;  }

		public bool IsNewUser { get; } = true;

		/// <summary>
		/// Создание нового контроллера пользователя
		/// </summary>
		/// <param name="user">Пользователь приложения</param>
		public UserController(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new ArgumentNullException("Имя польователя не может быть пустым", nameof(userName));
			}
			Users = GetUsersData();
			CurrentUser = Users.SingleOrDefault(u => u.Name == userName);
			if (CurrentUser == null)
			{
				CurrentUser = new User(userName);
				Users.Add(CurrentUser);
				IsNewUser = true;
				Save();
			}
		}

		/// <summary>
		/// Получить сохраненный список пользователей
		/// </summary>
		/// <returns></returns>
		private List<User> GetUsersData()
		{
			return Load<List<User>>(USERS_FILE_NAME) ?? new List<User>();
		}

		public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
		{
			// Проверка

			CurrentUser.Gender = new Gender(genderName);
			CurrentUser.BirthDate = birthDate;
			CurrentUser.Weight = weight;
			CurrentUser.Height = height;
			Save();
		}

		/// <summary>
		/// Сохранить данные пользователя
		/// </summary>
		public void Save()
		{
			Save(USERS_FILE_NAME, Users);
		}
	}
}