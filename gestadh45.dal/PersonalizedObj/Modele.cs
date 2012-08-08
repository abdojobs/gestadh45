
using System.Collections.Generic;
namespace gestadh45.dal
{
	public partial class Modele
	{
		/// <summary>
		/// [Categorie] [Marque] [Nom] [Couleurs]
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0} {1} {2} {3}", this.Categorie.ToString(), this.Marque.ToString(), this.Nom, this.DescriptionCouleur);
		}

		/// <summary>
		/// [Nom] [Couleurs]
		/// </summary>
		public string LibelleCourt {
			get { return string.Format("{0} {1}", this.Nom, this.DescriptionCouleur); }
		}

		/// <summary>
		/// Obtient la liste des couleurs concaténées
		/// </summary>
		public string DescriptionCouleur {
			get { return this.ConcatColors(); }
		}

		private string ConcatColors() {
			var couleurs = new List<string>();

			if (!string.IsNullOrWhiteSpace(this.Couleur1)) {
				couleurs.Add(this.Couleur1);
			}

			if (!string.IsNullOrWhiteSpace(this.Couleur2)) {
				couleurs.Add(this.Couleur2);
			}

			if (!string.IsNullOrWhiteSpace(this.Couleur3)) {
				couleurs.Add(this.Couleur3);
			}

			return string.Join("/", couleurs);
		}
	}
}
