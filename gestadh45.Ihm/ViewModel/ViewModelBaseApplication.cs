using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.dao;
using gestadh45.Ihm.ObjetsIhm;
using gestadh45.Ihm.SpecialMessages;

namespace gestadh45.Ihm.ViewModel
{
	public abstract class ViewModelBaseApplication : ViewModelBase
	{
		public ICommand FenetreCommand { get; set; }

		protected IDaoFactory mDaoFactory;		

		public ViewModelBaseApplication() {
			this.mDaoFactory = new DaoFactory();

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

		#region affichage des UC
		private void EnvoyerMsgAffichageUC(string pCodeUC, MsgAfficherUC.TypeAffichage pTypeAffichage) {
			var msg = new MsgAfficherUC(pCodeUC, pTypeAffichage);
			Messenger.Default.Send(msg);
		}

		protected void AfficherEcran(string pCodeUC) {
			this.EnvoyerMsgAffichageUC(pCodeUC, MsgAfficherUC.TypeAffichage.Interne);
		}

		protected void AfficherEcranFenetre(string pCodeUC) {
			this.EnvoyerMsgAffichageUC(pCodeUC, MsgAfficherUC.TypeAffichage.Externe);
		}
		#endregion

		#region affichage des notifications ihm
		private void EnvoyerMsgNotificationIhm(List<NotificationIhm> pNotifications) {
			Messenger.Default.Send(new MsgNotificationIhm(pNotifications));
		}

		/// <summary>
		/// Envoie une demande d'effacement de la zonne de notification
		/// </summary>
		protected void RazNotificationIhm() {
			this.EnvoyerMsgNotificationIhm(new List<NotificationIhm>());
		}

		protected void AfficherErreurIhm(string pErreur) {
			List<NotificationIhm> liste = new List<NotificationIhm>();
			liste.Add(new NotificationIhm(pErreur, TypesNotification.Erreur));

			this.EnvoyerMsgNotificationIhm(liste);
		}

		protected void AfficherErreursIhm(List<string> pErreurs) {
			List<NotificationIhm> liste = new List<NotificationIhm>();
			
			foreach (string err in pErreurs) {
				liste.Add(new NotificationIhm(err, TypesNotification.Erreur));
			}

			this.EnvoyerMsgNotificationIhm(liste);
		}

		protected void AfficherInformationIhm(string pInformation) {
			List<NotificationIhm> liste = new List<NotificationIhm>();
			liste.Add(new NotificationIhm(pInformation, TypesNotification.Information));

			this.EnvoyerMsgNotificationIhm(liste);
		}

		protected void AfficherInformationsIhm(List<string> pInformations) {
			List<NotificationIhm> liste = new List<NotificationIhm>();

			foreach (string info in pInformations) {
				liste.Add(new NotificationIhm(info, TypesNotification.Information));
			}

			this.EnvoyerMsgNotificationIhm(liste);
		}
		#endregion
	}
}
