namespace Model
{
	public class User
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public string Token { get; set; }

		public User()
		{
			Name = string.Empty;
			Email = string.Empty;
			Password = string.Empty;
			Role = string.Empty;
			Token = string.Empty;
		}
	}
}
