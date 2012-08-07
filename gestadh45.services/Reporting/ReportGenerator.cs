using System.Collections.Generic;
using System.IO;
using DoddleReport;
using DoddleReport.Writers;
using gestadh45.services.Reporting.Templates;

namespace gestadh45.services.Reporting
{
	public class ReportGenerator<T> where T : ITemplateReport
	{
		private Report _report;
		private string _saveFilepath;
		
		public ReportGenerator(ICollection<T> items, string saveFilePath) {
			this._report = new Report(items.ToReportSource());
			this._saveFilepath = saveFilePath;
		}

		public void GenerateHTMLReport() {
			using (var fs = new StreamWriter(this._saveFilepath)) {
				var writer = new HtmlReportWriter();
				writer.WriteReport(this._report, fs.BaseStream);
			}
		}

		public void GenerateExcelReport() {
			using (var fs = new StreamWriter(this._saveFilepath)) {
				var writer = new DoddleReport.OpenXml.ExcelReportWriter();
				writer.WriteReport(this._report, fs.BaseStream);
			}
		}
	}
}
