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
		private void EnvoyerMsgNotificationIhm(NotificationIhm pNotification, MsgNotificationIhm.ModeAffichage pModeAffichage) {
			Messenger.Default.Send(new MsgNotificationIhm(pNotification), pModeAffichage);
		}

		/// <summary>
		/// Envoie une demande d'effacement de la zonne de notification
		/// </summary>
		protected void RazNotificationIhm() {
			this.EnvoyerMsgNotificationIhm(new NotificationIhm(), MsgNotificationIhm.ModeAffichage.Remplacement);
		}

		protected void AfficherErreurIhm(string pErreur, MsgNotificationIhm.ModeAffichage pModeAffichage) {
			NotificationIhm notification = new NotificationIhm(pErreur, TypesNotification.Erreur);
			this.EnvoyerMsgNotificationIhm(notification, pModeAffichage);
		}

		protected void AfficherErreursIhm(List<string> pErreurs, MsgNotificationIhm.ModeAffichage pModeAffichage) {
			foreach (string err in pErreurs) {
				this.AfficherErreurIhm(err, pModeAffichage);
			}
		}

		protected void AfficherInformationIhm(string pInformation, MsgNotificationIhm.ModeAffichage pModeAffichage) {
			NotificationIhm notification = new NotificationIhm(pInformation, TypesNotification.Information);
			this.EnvoyerMsgNotificationIhm(notification, pModeAffichage);
		}

		protected void AfficherInformationsIhm(List<string> pInformations, MsgNotificationIhm.ModeAffichage pModeAffichage) {
			foreach (string info in pInformations) {
				this.AfficherInformationIhm(info, pModeAffichage);
			}
		}
		#endregion
	}
}
