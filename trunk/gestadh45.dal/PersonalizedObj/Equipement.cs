
using System;
namespace gestadh45.dal
{
	public partial class Equipement : ICloneable
	{
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString() {
			return string.Format("{0} {1} - {2}", this.Categorie.ToString(), this.Marque.ToString(), this.Numero);
		}

		/// <summary>
		/// Gets the libelle.
		/// </summary>
		public string Libelle {
			get { return this.ToString(); }
		}

		/// <summary>
		/// Gets a value indicating whether [est au rebut].
		/// </summary>
		/// <value>
		///   <c>true</c> if [est au rebut]; otherwise, <c>false</c>.
		/// </value>
		public bool EstAuRebut {
			get { return this.DateMiseAuRebut.HasValue; }
		}

		/// <summary>
		/// Obtient la date de fin de vie de l'équipement en se basant (dans l'ordre) soit sur sa date d'achat, soit sur sa date demise en service, soit sur sa date de saisie dans la BDD.
		/// </summary>
		public DateTime DateFinDeVie {
			get {
				DateTime dateFinDeVie;

				if (this.DateAchat.HasValue) {
					dateFinDeVie = this.DateAchat.Value.AddYears(this.DureeDeVie.NbAnnees).AddMonths(this.DureeDeVie.NbMois);
				}
				else if (this.DateMiseEnService.HasValue) {
					dateFinDeVie = this.DateMiseEnService.Value.AddYears(this.DureeDeVie.NbAnnees).AddMonths(this.DureeDeVie.NbMois);
				}
				else {
					dateFinDeVie = this.DateCreation.AddYears(this.DureeDeVie.NbAnnees).AddMonths(this.DureeDeVie.NbMois);
				}

				return dateFinDeVie;
			}
		}

		/// <summary>
		/// Obtient un booléen indiquant si l'équipement a atteint sa fin de vie
		/// </summary>
		public bool FinDeVieAtteinte {
			get {
				return DateTime.Now > this.DateFinDeVie;
			}
		}

		/// <summary>
		/// Crée un nouvel objet qui est une copie de l'instance en cours.
		/// </summary>
		/// <returns>
		/// Nouvel objet qui est une copie de cette instance.
		/// </returns>
		public object Clone() {
			return new Equipement()
			{
				ID = Guid.NewGuid(),
				Numero = string.Copy(this.Numero),
				ID_Marque = this.ID_Marque,
				ID_Categorie = this.ID_Categorie,
				DateCreation = DateTime.Now,
				DateModification = DateTime.Now,

				DateAchat = this.DateAchat,
				DateMiseEnService =  this.DateMiseEnService,
				Commentaire = this.Commentaire,
				ID_DureeDeVie = this.ID_DureeDeVie
			};
		}
	}
}
