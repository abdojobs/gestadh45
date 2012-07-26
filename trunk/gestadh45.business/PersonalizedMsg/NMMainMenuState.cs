using GalaSoft.MvvmLight.Messaging;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMMainMenuState : NotificationMessage
	{
		/// <summary>
		/// Obtient/Définit un booléen indiquant l'état souhaité du menu principal
		/// </summary>
		public bool Enabled { get; set; }

		public NMMainMenuState(bool enabled)
			: base(NMType.NMMainMenuState) {
				this.Enabled = enabled;
		}
	}
}
