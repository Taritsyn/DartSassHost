using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	public abstract class ErrorTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "modules/errors";


		protected ErrorTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }
	}
}