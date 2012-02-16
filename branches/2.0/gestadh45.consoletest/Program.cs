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
			//var context = new GestAdh45Entities();
			var repo = new Repository<Inscription, GestAdh45Entities>();

			foreach(object obj in repo.GetAll()) {
				Console.WriteLine(obj.ToString());
			}

			repo.Dispose();
			//context.Dispose();

			Console.ReadKey();
		}
	}
}
