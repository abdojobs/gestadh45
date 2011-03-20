using System.Collections.ObjectModel;
using Visifire.Charts;

namespace gestadh45.service.Graphs
{
	public class Graphique
	{
		public ObservableCollection<DonneeGraph> Donnees { get; set; }
		public string Titre { get; set; }
		public RenderAs Type { get; set; }
	}
}
