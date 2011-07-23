using System.Windows;

namespace gestadh45.Ihm.Tools
{
	public static class TransitionHelper
	{
		/// <summary>
		/// Obtient le point de départ pour une translation Gauche-Droite
		/// </summary>
		public static Point TranslationGaucheDroite {
			get { return new Point(-1, 0); }
		}

		/// <summary>
		/// Obtient le point de départ pour une translation Droite-Gauche
		/// </summary>
		public static Point TranslationDroiteGauche {
			get { return new Point(1, 0); }
		}
	}
}
