using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using gestadh45.dal;

namespace gestadh45.consoletest
{
	class Program
	{
		static void Main(string[] args) {
			var context = new GestAdh45Entities();
			var repo = new Repository<Sexe>(context);

			foreach(Sexe sexe in repo.GetAll()) {
				Console.WriteLine(string.Format("{0} - {1}", sexe.ID, sexe.ToLongString()));
			}

			repo.Dispose();
			context.Dispose();

			Console.ReadKey();
		}
	}
}
