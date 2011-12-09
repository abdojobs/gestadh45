using gestadh45.dal;

namespace gestadh45.Ihm.ObjetsIhm
{
	public class SessionIhm
	{
		/// <summary>
		/// Obtient/Définit l'user en session
		/// </summary>
		public AppUser SessionUser { get; set; }

		/// <summary>
		/// Obtient un booléen indiquant si un user est connecté
		/// </summary>
		public bool IsUserConnected {
			get { return this.SessionUser != null; }
		}

		/// <summary>
		/// Obtient un booléen indiquant si le user est un "reader" (renvoit False si aucun user n'est conencté)
		/// </summary>
		public bool IsUserReader {
			get {
				return this.SessionUser != null && this.SessionUser.Reader;
			}
		}

		/// <summary>
		/// Obtient un booléen indiquant si le user est un "writer" (renvoit False si aucun user n'est conencté)
		/// </summary>
		public bool IsUserWriter {
			get {
				return this.SessionUser != null && this.SessionUser.Writer;
			}
		}
	}
}
