using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ObjetsIhm
{
	public class NotificationIhm
	{
		/// <summary>
		/// Obtient/Définit le texte de la notification
		/// </summary>
		public string Texte { get; set; }

		/// <summary>
		/// Obtient/Définit la couleur de la notification
		/// </summary>
		public SolidColorBrush Couleur { get; set;}

		#region constructeurs
		/// <summary>
		/// Constructeur vide pour effacer les notifications affichées
		/// </summary>
		public NotificationIhm() {
			this.Texte = string.Empty;
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pTexte">Texte de la notification</param>
		/// <param name="pType">Type de notification</param>
		public NotificationIhm(string pTexte, string pType) {
			this.Texte = pTexte;
			this.DefinirCouleur(pType);
		}

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="pTextes">Liste des textes</param>
		/// <param name="pType">Type de notification</param>
		public NotificationIhm(List<string> pTextes, string pType) {
			StringBuilder sb = new StringBuilder();
			foreach (string texte in pTextes) {
				sb.AppendLine(texte);
			}

			this.Texte = sb.ToString();
			this.DefinirCouleur(pType);
		}
		#endregion

		private void DefinirCouleur(string pTypeNotification) {
		switch (pTypeNotification) {
				case TypesNotification.Erreur:
					this.Couleur = Brushes.Red;
					break;

				case TypesNotification.Information:
					this.Couleur = Brushes.Blue;
					break;

				default:
					this.Couleur = Brushes.Black;
					break;
			}
		}
	}
}
