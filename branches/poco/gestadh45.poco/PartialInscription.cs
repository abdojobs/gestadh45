using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gestadh45.poco
{
	public partial class Inscription
	{
		public override string ToString() {
			return string.Format("{0} - {1}", this.Adherent, this.Groupe.Libelle);
		}
	}
}
