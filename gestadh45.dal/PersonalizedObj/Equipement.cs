
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
			return string.Format("{0} ({1} {2})", this.Numero, this.Categorie.ToString(), this.Marque.ToString());
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
			get { return !this.DateMiseAuRebut.HasValue; }
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
				Commentaire = this.Commentaire
			};
		}
	}
}
