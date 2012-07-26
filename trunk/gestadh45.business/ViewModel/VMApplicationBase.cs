using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using gestadh45.business.PersonalizedMsg;

namespace gestadh45.business.ViewModel
{
	public abstract class VMApplicationBase : ViewModelBase
	{
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

		public VMApplicationBase() {
			this.CreateOpenWindowCommand();
			this.CreateShowUCCommand();
		}

		#region OpenWindowCommand
		public ICommand OpenWindowCommand { get; internal set; }

		protected void CreateOpenWindowCommand() {
			this.OpenWindowCommand = new RelayCommand<string>(
				this.ExecuteOpenWindowCommand,
				this.CanExecuteOpenWindowCommand
			);
		}

		public virtual bool CanExecuteOpenWindowCommand(string codeUC) {
			return true;
		}

		public virtual void ExecuteOpenWindowCommand(string codeUC) {
			Messenger.Default.Send<NMOpenWindow>(
				new NMOpenWindow(codeUC)
			);
		}
		#endregion

		#region ShowUCCommand
		public ICommand ShowUCCommand { get; internal set; }

		private void CreateShowUCCommand() {
			this.ShowUCCommand = new RelayCommand<string>(
				this.ExecuteShowUCCommand,
				this.CanExecuteShowUCCommand
			);
		}

		public bool CanExecuteShowUCCommand(string pCodeUC) {
			return true;
		}

		public void ExecuteShowUCCommand(string pCodeUC) {
			this.ClearUserNotifications();
			this.ShowUC(pCodeUC);
		}
		#endregion

		#region Affichage des UC
		protected void ShowUC(string codeUC) {
			Messenger.Default.Send(new NMShowUC(codeUC));
		}
		#endregion

		#region Affichage des notifications utilisateur
		protected void ShowUserNotification(string notification) {
			Messenger.Default.Send(new NMUserNotification(notification));
		}

		protected void ShowUserNotifications(IEnumerable<string> notifications) {
			StringBuilder sb = new StringBuilder();
			foreach (string notif in notifications) {
				sb.AppendLine(notif);
			}
			this.ShowUserNotification(sb.ToString());
		}

		protected void ClearUserNotifications() {
			this.ShowUserNotification(string.Empty);
		}
		#endregion
	}
}
