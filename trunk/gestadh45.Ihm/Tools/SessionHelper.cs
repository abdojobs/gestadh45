using System.Linq;
using gestadh45.dal;
using gestadh45.Ihm.ObjetsIhm;
using gestadh45.Ihm.ViewModel;

namespace gestadh45.Ihm.Tools
{
	public static class SessionHelper
	{
		public static bool ConnectAppUser(string pLogin, string pPassword) {
			AppUser user = ViewModelLocator
				.DaoAppUser
				.List()
				.Where(u => u.Login.Equals(pLogin) && u.Password.Equals(pPassword))
				.First();

			ViewModelLocator.CurrentSession = new SessionIhm()
			{
				SessionUser = user
			};

			return user != null;
		}
	}
}
