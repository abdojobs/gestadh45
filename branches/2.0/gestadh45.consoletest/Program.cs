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
			var repoAdh = new Repository<Adherent>();

			Console.WriteLine(repoAdh.GetById(64));

			Console.ReadKey();
		}
	}
}
