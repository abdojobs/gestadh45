using System.Net.Mail;

namespace gestadh45.service.Mail
{
	public class MailHelper
	{
		private DonneesMail _donnees;

		public MailHelper(DonneesMail pDonnees) {
			this._donnees = pDonnees;
		}

		public void Envoyer() {
			SmtpClient serveur = new SmtpClient(this._donnees.ServeurSmtp);

			MailMessage msg = new MailMessage()
			{
				Sender = new MailAddress(this._donnees.AdresseExpediteur, this._donnees.NomExpediteur),
				From = new MailAddress(this._donnees.AdresseExpediteur, this._donnees.NomExpediteur),
				Subject = this._donnees.Sujet,
				Body = this._donnees.Message
			};

			foreach (string adr in this._donnees.DestinatairesPubliques) {
				msg.CC.Add(adr);
			}

			foreach (string adr in this._donnees.DestinatairesPrives) {
				msg.Bcc.Add(adr);
			}

			serveur.Send(msg);
		}
	}
}
