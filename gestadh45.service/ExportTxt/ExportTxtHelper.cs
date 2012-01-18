using System.Collections.Generic;
using System.IO;

namespace gestadh45.service.ExportTxt
{
	public static class ExportTxtHelper
	{
		/// <summary>
		/// Exporter une liste de chaînes dans un fichier txt
		/// </summary>
		/// <param name="pSavePath">Chemin du fichier txt</param>
		/// <param name="pElements">Liste de chaînes à écrire</param>
		/// <param name="pDelimiteur">Délimiteur</param>
		public static void IEnumerableToTxt(string pSavePath, IEnumerable<string> pElements, string pDelimiteur) {
			using (StreamWriter writer = new StreamWriter(pSavePath)) {
				foreach (string elem in pElements) {
					writer.Write(elem + pDelimiteur);
				}
			}
		}
	}
}
