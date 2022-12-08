using System.Collections.Generic;

using BenchmarkDotNet.Attributes;

namespace DartSassHost.Benchmarks
{
	public abstract class ScssCompilationBenchmarkBase
	{
		private static readonly Dictionary<string, Document> s_documents = new Dictionary<string, Document>
		{
			{ "angular-material", new Document("angular-material/@angular/material/_theming.scss") },
			{ "bootstrap", new Document("bootstrap/bootstrap.scss") },
			{ "foundation", new Document("foundation/scss/foundation.scss") }
		};

		public static Dictionary<string, Document> Documents
		{
			get { return s_documents; }
		}

		[ParamsSource(nameof(GetDocumentNames))]
		public string DocumentName { get; set; }


		static ScssCompilationBenchmarkBase()
		{
			JsEngineSwitcherInitializer.Initialize();
			Utils.PopulateTestData("Files", s_documents);
		}


		public static IEnumerable<string> GetDocumentNames()
		{
			foreach (string key in s_documents.Keys)
			{
				yield return key;
			}
		}
	}
}