using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel
{
	public abstract class ViewModelBaseApplication : ViewModelBase
	{
		public ICommand AnnulerCommand { get; set; }
		public ICommand FenetreCommand { get; set; }

		protected IDaoFactory mDaoFactory;

		/// <summary>
		/// Obtient/Définit le code de l'UC "parent" de cet élément
		/// </summary>
		public string CodeUCOrigine { get; set; }
		
		/// <summary>
		/// Obtient/Définit un booléen indiquant si l'élément est dans sa propre fenêtre (True) ou dans l'écran principal (False)
		/// </summary>
		public bool ModeFenetre { get; set; }

		public ViewModelBaseApplication() {
			this.mDaoFactory = new DaoFactory();

			this.CodeUCOrigine = CodesUC.ConsultationInfosClub;

			this.CreateAnnulerCommand();
			this.CreateFenetreCommand();
		}

		/// <summary>
		/// Methode permettant d'appeller RaisePropertyChanged en passant la propriété au lieu de son nom
		/// </summary>
		/// <remarks>Implémentation tirée de <a href="http://mfelicio.wordpress.com/2010/01/10/safe-usage-of-inotifypropertychanged-in-mvvm-scenarios/">http://mfelicio.wordpress.com/2010/01/10/safe-usage-of-inotifypropertychanged-in-mvvm-scenarios/</a></remarks>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyExpression"></param>
		protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression) {
			if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess) {
				var memberExpr = propertyExpression.Body as MemberExpression;
				string propertyName = memberExpr.Member.Name;
				this.RaisePropertyChanged(propertyName);
			}
		}

		#region AnnulerCommand
		protected void CreateAnnulerCommand() {
			this.AnnulerCommand = new RelayCommand<string>(
				this.ExecuteAnnulerCommand
			);
		}

		public virtual void ExecuteAnnulerCommand(string pCodeUc) {
			if (this.ModeFenetre) {
				Messenger.Default.Send<NotificationMessageFermetureFenetre>(
					new NotificationMessageFermetureFenetre()
				);
			}
			else {
				Messenger.Default.Send<NotificationMessageChangementUC>(
					new NotificationMessageChangementUC(pCodeUc)
				);
			}
		}
		#endregion

		#region FenetreCommand
		protected void CreateFenetreCommand() {
			this.FenetreCommand = new RelayCommand<string>(
				this.ExecuteFenetreCommand,
				this.CanExecuteFenetreCommand
			);
		}

		public virtual bool CanExecuteFenetreCommand(string pCodeUC) {
			return true;
		}

		public virtual void ExecuteFenetreCommand(string pCodeUC) {
			Messenger.Default.Send<NotificationMessageOuvertureFenetre>(
				new NotificationMessageOuvertureFenetre(pCodeUC)
			);
		}
		#endregion

		#region affichage des notifications ihm
		private void AfficherNotificationIhm(string pType, string pNotification) {
			var msg = new NotificationMessageIhm(pType, pNotification);
			Messenger.Default.Send(msg);
		}

		protected void AfficherErreurIhm(string pErreur) {
			this.AfficherNotificationIhm(TypesNotification.Erreur, pErreur);
		}

		protected void AfficherInformationIhm(string pInformation) {
			this.AfficherNotificationIhm(TypesNotification.Information, pInformation);
		}

		protected void AfficherErreursIhm(List<string> pErreurs) {
			StringBuilder sb = new StringBuilder();

			foreach (string err in pErreurs) {
				sb.AppendLine(err);
			}

			this.AfficherErreurIhm(sb.ToString());
		}

		protected void AfficherInformationsIhm(List<string> pInformations) {
			StringBuilder sb = new StringBuilder();

			foreach (string info in pInformations) {
				sb.AppendLine(info);
			}

			this.AfficherInformationIhm(sb.ToString());
		}

		#endregion
	}
}
