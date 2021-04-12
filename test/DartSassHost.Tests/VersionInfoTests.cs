using System;
using System.Text.RegularExpressions;

using NUnit.Framework;

namespace DartSassHost.Tests
{
	[TestFixture]
	public class VersionInfoTests
	{
		/// <summary>
		/// Regular expression for working with the version of the LibSass library
		/// </summary>
		private static readonly Regex _versionRegex = new Regex(@"^(?<major>\d+)\.(?<minor>\d+)" +
			@"(?:\.(?<patch>\d+)(?:\.(?<build>\d+))?)?$");


		[SetUp]
		public void Init()
		{
			JsEngineSwitcherInitializer.Initialize();
		}

		[Test]
		public void VersionFormatIsCorrect()
		{
			// Arrange
			bool formatIsCorrect = false;
			int major = -1;
			int minor = -1;
			int patch = -1;
			int build = -1;

			// Act
			using (var sassCompiler = new SassCompiler())
			{
				string version = sassCompiler.Version;
				Match match = _versionRegex.Match(version);

				if (match.Success)
				{
					formatIsCorrect = true;

					GroupCollection groups = match.Groups;
					major = int.Parse(groups["major"].Value);
					minor = int.Parse(groups["minor"].Value);
					Group patchGroup = groups["patch"];
					if (patchGroup.Success)
					{
						patch = int.Parse(patchGroup.Value);
					}
					Group buildGroup = groups["build"];
					if (buildGroup.Success)
					{
						build = int.Parse(buildGroup.Value);
					}
				}
			}

			// Assert
			Assert.True(formatIsCorrect);
			Assert.True(major > 0);
			Assert.True(minor >= 0);
		}
	}
}