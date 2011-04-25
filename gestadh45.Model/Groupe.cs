
using System;
namespace gestadh45.Model
{
	public partial class Groupe
	{
		///// <summary>
		///// Obtient/Définit l'heure de début dans un TimeSpan
		///// </summary>
		//public DateTime HeureDebutDT {
		//    get { return new DateTime(new TimeSpan((int)this.HeureDebut, (int)this.MinuteDebut, 0).Ticks); }
		//    set {
		//        this.HeureDebut = value.Hour;
		//        this.MinuteDebut = value.Minute;
		//    }
		//}

		///// <summary>
		///// Obtient/Définit l'heure de fin dans un TimeSpan
		///// </summary>
		//public DateTime HeureFinDT {
		//    get { return new DateTime(new TimeSpan((int)this.HeureFin, (int)this.MinuteFin, 0).Ticks); }
		//    set {
		//        this.HeureFin = value.Hour;
		//        this.MinuteFin = value.Minute;
		//    }
		//}
		
		public override string ToString() {
			return string.Format(
				"{0} ({1} - {2})", 
				this.JourSemaine.ToString(), 
				this.HeureDebutDT.ToString("t"), 
				this.HeureFinDT.ToString("t")
			);
		}
	}
}
