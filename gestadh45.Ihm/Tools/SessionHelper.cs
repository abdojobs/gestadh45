using System.Linq;
using gestadh45.Ihm.ViewModel;

namespace gestadh45.Ihm.Tools
{
	public static class SessionHelper
	{
		public static bool CheckAppUser(string pLogin, string pPassword) {
			return ViewModelLocator
				.DaoAppUser
				.List()
				.Where(u => u.Login.Equals(pLogin) && u.Password.Equals(pPassword))
				.Count() > 0;

			// TODO mettre en session l'appuser
		}
	}
}
