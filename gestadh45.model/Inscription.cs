using System;

namespace gestadh45.model
{
	public class Inscription : BaseClass
	{
		#region properties
		/// <summary>
		/// Obtient/Définit l'adhérent
		/// </summary>
		public Adherent Adherent { get; set; }

		/// <summary>
		/// Obtient/Définit le groupe
		/// </summary>
		public Groupe Groupe { get; set; }

		/// <summary>
		/// Obtient/Définit un booléen indiquant si le certificat médical a été remis ou non
		/// </summary>
		public bool CertificatMedicalRemis { get; set; }

		/// <summary>
		/// Obtient/Définit le montant de la cotisation versé
		/// </summary>
		public decimal Cotisation { get; set; }

		/// <summary>
		/// Obtient/Définit la date de création
		/// </summary>
		public DateTime DateCreation { get; set; }

		/// <summary>
		/// Obtient/Définit la date de modification
		/// </summary>
		public DateTime DateModification { get; set; }

		/// <summary>
		/// Obtient/Définit le commentaire
		/// </summary>
		public string Commentaire { get; set; }

		/// <summary>
		/// Obtient/Définit le statut de l'inscription
		/// </summary>
		public StatutInscription StatutInscription { get; set; }

		/// <summary>
		/// Obtient la propriété servant au regroupement des inscriptions dans l'affichage
		/// </summary>
		public string Regroupement {
			get { return this.Groupe.ToString(); }
		}
		#endregion

		/// <summary>
		/// Renvoit adhérent groupe
		/// </summary>
		/// <returns>Adhérent - Groupe</returns>
		public override string ToString() {
			return string.Format("{0} - {1}", this.Adherent, this.Groupe.Libelle);
		}
	}
}
