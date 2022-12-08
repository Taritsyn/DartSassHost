using System.Collections.Generic;

namespace DartSassHost.Benchmarks
{
	public sealed class Document
	{
		public string RelativePath
		{
			get;
			set;
		}

		public string AbsolutePath
		{
			get;
			set;
		}

		public string Content
		{
			get;
			set;
		}

		public IList<string> IncludePaths
		{
			get;
			set;
		}


		public Document()
			: this(string.Empty)
		{ }

		public Document(string relativePath)
		{
			RelativePath = relativePath;
		}
	}
}