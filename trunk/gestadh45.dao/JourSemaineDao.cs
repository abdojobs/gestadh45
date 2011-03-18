using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using gestadh45.Model;

namespace gestadh45.dao
{
	public class JourSemaineDao
	{
		private static JourSemaineDao Instance;

		private JourSemaineDao() {
		}

		public static JourSemaineDao GetInstance(Entities pContexte) {
			if (Instance == null) {
				Instance = new JourSemaineDao();
			}
			Instance.Context = pContexte;
			return Instance;
		}

		public List<JourSemaine> List() {
			return (from j in Instance.Context.JourSemaines
					orderby j.Numero
					select j).ToList<JourSemaine>();
		}

		public JourSemaine Read(int pJourId) {
			return (from j in Instance.Context.JourSemaines
					where j.ID == pJourId
					select j).First<JourSemaine>();
		}

		public void Refresh(JourSemaine pJourSemaine) {
			this.Context.Refresh(RefreshMode.StoreWins, pJourSemaine);
		}

		private Entities Context { get; set; }
	}
}
