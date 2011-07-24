using gestadh45.tests.dao;
using System;

namespace gestadh45.tests
{
	public class Program
	{
		static void Main(string[] args) {
			RunTestsDao testDao = new RunTestsDao(@"D:\Documents\Dropbox\Eybens Escalade\gestadh45\dev45.eyb");
			testDao.Launch();

			Console.ReadLine();
		}
	}
}
