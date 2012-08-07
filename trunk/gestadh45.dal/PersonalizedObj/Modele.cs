
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
			get {
				var descriptionCouleur = string.Concat(this.Couleur1, "/", this.Couleur2, "/", this.Couleur3);
				descriptionCouleur = descriptionCouleur.Replace("//", "/");

				if (descriptionCouleur.EndsWith("/")) {
					descriptionCouleur = descriptionCouleur.Substring(0, descriptionCouleur.Length - 1);
				}

				return descriptionCouleur;
			}
		}
	}
}
