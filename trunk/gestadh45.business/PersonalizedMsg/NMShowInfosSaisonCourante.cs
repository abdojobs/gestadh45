using GalaSoft.MvvmLight.Messaging;
using gestadh45.dal;

namespace gestadh45.business.PersonalizedMsg
{
	public class NMShowInfosSaisonCourante : NotificationMessage<Saison>
	{
		public NMShowInfosSaisonCourante(Saison saison) : base(saison, NMType.NMShowInfosSaisonCourante) { }
	}
}
