﻿
namespace gestadh45.poco
{
	public partial class Groupe
	{
		public override string ToString() {
			return string.Format(
				"{0} ({1} - {2})",
				this.JourSemaine.ToString(),
				this.HeureDebut.ToString("t"),
				this.HeureFin.ToString("t")
			);
		}
	}
}
