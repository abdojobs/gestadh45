
using System;
using System.Linq;

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
			return string.Format("{0} - {1}", this.Numero, this.Modele.ToString());
		}

		/// <summary>
		/// Gets the libelle.
		/// </summary>
		public string Libelle {
			get { return this.ToString(); }
		}

		/// <summary>
		/// Obtient la date de fin de vie de l'équipement en se basant (dans l'ordre) soit sur sa date d'achat, soit sur sa date demise en service, soit sur sa date de saisie dans la BDD.
		/// </summary>
		public DateTime DateFinDeVie {
			get {
				DateTime dateFinDeVie;

				if (this.DateAchat.HasValue) {
					dateFinDeVie = this.DateAchat.Value.AddYears(this.Modele.Categorie.DureeDeVie.NbAnnees).AddMonths(this.Modele.Categorie.DureeDeVie.NbMois);
				}
				else {
					dateFinDeVie = this.DateCreation.AddYears(this.Modele.Categorie.DureeDeVie.NbAnnees).AddMonths(this.Modele.Categorie.DureeDeVie.NbMois);
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
		/// Obtient un booléen indiquant si l'équipement est au rebut
		/// </summary>
		/// <value>
		///   <c>true</c> si l'équipement est au rebut, <c>false</c> sinon.
		/// </value>
		/// <remarks>Un équipement est au rebut si il a au moins une vérification et que sa dernière vérification à le statut rebut</remarks>
		public bool EstAuRebut {
			get {
				return this.Verifications.Count > 0 
					&& this.Verifications
					.OrderByDescending(v => v.CampagneVerification.Date)
					.First()
					.StatutVerification
					.EstRebut;
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
				ID_Modele = this.ID_Modele,
				DateCreation = DateTime.Now,
				DateModification = DateTime.Now,
				DateAchat = this.DateAchat,
				Commentaire = this.Commentaire,
			};
		}
	}
}
