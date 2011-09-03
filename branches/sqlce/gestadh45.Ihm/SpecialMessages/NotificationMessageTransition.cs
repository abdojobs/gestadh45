using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.Ihm.SpecialMessages
{
	public class NotificationMessageTransition : NotificationMessage
	{
		/// <summary>
		/// Point de départ de la transition
		/// </summary>
		public Point SensTransition { get; set; }

		/// <summary>
		/// Constructeur
		/// </summary>
		/// <param name="sensTransition">Sens de la translation</param>
		public NotificationMessageTransition(Point sensTransition)
			: base(TypesNotification.ModificationTransition) {
				this.SensTransition = sensTransition;
		}
	}
}
