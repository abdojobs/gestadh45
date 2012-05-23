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
			var context = new gestadh45Entities();
			var repo = new Repository<Inscription>(context);

			foreach(object obj in repo.GetAll()) {
				Console.WriteLine(obj.ToString());
			}

			context.Dispose();

			Console.ReadKey();
		}
	}
}
