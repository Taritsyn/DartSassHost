namespace DartSassHost.Tests.Imports
{
	public abstract class WarningTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "imports/warnings";


		protected WarningTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }
	}
}