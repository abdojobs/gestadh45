
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
			return string.Format("{0}", this.Numero);
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
				DateCreation = DateTime.Now,
				DateModification = DateTime.Now,

				DateAchat = this.DateAchat,
				DateMiseEnService =  this.DateMiseEnService,
				Commentaire = this.Commentaire
			};
		}
	}
}
