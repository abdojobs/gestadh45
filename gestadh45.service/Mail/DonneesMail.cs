using System.Collections.Generic;

namespace gestadh45.service.Mail
{
	public class DonneesMail
	{
		/// <summary>
		/// Gets or sets the serveur SMTP.
		/// </summary>
		/// <value>
		/// The serveur.
		/// </value>
		public string ServeurSmtp { get; set; }

		/// <summary>
		/// Gets or sets the adresse expediteur.
		/// </summary>
		/// <value>
		/// The adresse expediteur.
		/// </value>
		public string AdresseExpediteur { get; set; }

		/// <summary>
		/// Gets or sets the nom expediteur.
		/// </summary>
		/// <value>
		/// The nom expediteur.
		/// </value>
		public string NomExpediteur { get; set; }
		
		/// <summary>
		/// Gets or sets the destinataires publiques.
		/// </summary>
		/// <value>
		/// The destinataires publiques.
		/// </value>
		public IEnumerable<string> DestinatairesPubliques { get; set; }

		/// <summary>
		/// Gets or sets the destinataires prives.
		/// </summary>
		/// <value>
		/// The destinataires prives.
		/// </value>
		public IEnumerable<string> DestinatairesPrives { get; set; }
		
		/// <summary>
		/// Gets or sets the sujet.
		/// </summary>
		/// <value>
		/// The sujet.
		/// </value>
		public string Sujet { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public string Message { get; set; }


		/// <summary>
		/// Initializes a new instance of the <see cref="DonneesMail"/> class.
		/// </summary>
		public DonneesMail() {
			this.DestinatairesPubliques = new List<string>();
			this.DestinatairesPrives = new List<string>();
		}
	}
}
