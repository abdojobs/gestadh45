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

			foreach (var field in this._report.DataFields) {
				field.HeaderStyle.Bold = true;
			}
		}

		#region Personalization
		public void SetTitle(string reportTitle) {
			this._report.TextFields.Title = reportTitle;
		}

		public void SetSubTitle(string reportSubTitle) {
			this._report.TextFields.SubTitle = reportSubTitle;
		}

		public void SetHeader(string reportHeader) {
			this._report.TextFields.Header = reportHeader;
		}

		public void SetFooter(string reportFooter) {
			this._report.TextFields.Footer = reportFooter;
		}
		#endregion

		#region Generation
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
		#endregion
	}
}
