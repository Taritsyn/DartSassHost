namespace DartSassHost.Tests.Imports
{
	public abstract class ErrorTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "imports/errors";


		protected ErrorTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }
	}
}