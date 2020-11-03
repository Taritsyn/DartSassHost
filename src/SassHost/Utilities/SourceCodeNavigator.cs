using System;
using System.Globalization;
using System.Text;

using AdvancedStringBuilder;

using SassHost.Extensions;

namespace SassHost.Utilities
{
	internal static class SourceCodeNavigator
	{
		private const byte DEFAULT_TAB_SIZE = 4;
		private const int DEFAULT_MAX_FRAGMENT_LENGTH = 95;

		/// <summary>
		/// Array of characters used to find the next line break
		/// </summary>
		private static readonly char[] _nextLineBreakChars = new char[] { '\r', '\n' };


		/// <summary>
		/// Finds a next line break
		/// </summary>
		/// <param name="sourceCode">Source code</param>
		/// <param name="startPosition">Position in the input string that defines the leftmost
		/// position to be searched</param>
		/// <param name="lineBreakPosition">Position of line break</param>
		/// <param name="lineBreakLength">Length of line break</param>
		private static void FindNextLineBreak(string sourceCode, int startPosition,
			out int lineBreakPosition, out int lineBreakLength)
		{
			int length = sourceCode.Length - startPosition;

			FindNextLineBreak(sourceCode, startPosition, length,
				out lineBreakPosition, out lineBreakLength);
		}

		/// <summary>
		/// Finds a next line break
		/// </summary>
		/// <param name="sourceCode">Source code</param>
		/// <param name="startPosition">Position in the input string that defines the leftmost
		/// position to be searched</param>
		/// <param name="length">Number of characters in the substring to include in the search</param>
		/// <param name="lineBreakPosition">Position of line break</param>
		/// <param name="lineBreakLength">Length of line break</param>
		private static void FindNextLineBreak(string sourceCode, int startPosition, int length,
			out int lineBreakPosition, out int lineBreakLength)
		{
			lineBreakPosition = sourceCode.IndexOfAny(_nextLineBreakChars, startPosition, length);
			if (lineBreakPosition != -1)
			{
				lineBreakLength = 1;
				char currentCharacter = sourceCode[lineBreakPosition];

				if (currentCharacter == '\r')
				{
					int nextCharacterPosition = lineBreakPosition + 1;
					char nextCharacter;

					if (sourceCode.TryGetChar(nextCharacterPosition, out nextCharacter)
						&& nextCharacter == '\n')
					{
						lineBreakLength = 2;
					}
				}
			}
			else
			{
				lineBreakLength = 0;
			}
		}

		/// <summary>
		/// Gets a source fragment
		/// </summary>
		/// <param name="sourceCode">Source code</param>
		/// <param name="nodeCoordinates">Node coordinates</param>
		/// <param name="tabSize">Number of spaces in the tab</param>
		/// <param name="maxFragmentLength">Maximum length of the source fragment</param>
		/// <returns>Source fragment</returns>
		internal static string GetSourceFragment(string sourceCode,
			SourceCodeNodeCoordinates nodeCoordinates, byte tabSize = DEFAULT_TAB_SIZE,
			int maxFragmentLength = DEFAULT_MAX_FRAGMENT_LENGTH)
		{
			string sourceFragment = string.Empty;
			int lineNumber = nodeCoordinates.LineNumber;
			int columnNumber = nodeCoordinates.ColumnNumber;

			if (!string.IsNullOrEmpty(sourceCode))
			{
				int previousLineNumber = lineNumber - 1;
				int currentLineNumber = lineNumber;
				int nextLineNumber = lineNumber + 1;

				string previousLine = string.Empty;
				string currentLine = string.Empty;
				string nextLine = string.Empty;

				int lineCount = 0;
				int lineBreakPosition = int.MinValue;
				int lineBreakLength = 0;

				do
				{
					string line;
					int startLinePosition = lineBreakPosition == int.MinValue ? 0 : lineBreakPosition + lineBreakLength;

					FindNextLineBreak(sourceCode, startLinePosition, out lineBreakPosition, out lineBreakLength);

					if (lineBreakPosition != -1)
					{
						line = sourceCode.Substring(startLinePosition, lineBreakPosition - startLinePosition);
					}
					else
					{
						line = sourceCode.Substring(startLinePosition);
					}

					lineCount++;

					if (lineCount == previousLineNumber)
					{
						previousLine = line;
					}
					else if (lineCount == currentLineNumber)
					{
						currentLine = line;
					}
					else if (lineCount == nextLineNumber)
					{
						nextLine = line;
					}
				}
				while (lineBreakPosition != -1 && lineCount <= nextLineNumber);

				int lineNumberSize = nextLineNumber.ToString(CultureInfo.InvariantCulture).Length;
				if (currentLineNumber == lineCount)
				{
					lineNumberSize = currentLineNumber.ToString(CultureInfo.InvariantCulture).Length;
				}

				int fragmentStartPosition;
				int fragmentLength;

				CalculateCutPositions(currentLine, columnNumber, maxFragmentLength,
					out fragmentStartPosition, out fragmentLength);

				var stringBuilderPool = StringBuilderPool.Shared;
				StringBuilder sourceFragmentBuilder = stringBuilderPool.Rent();

				if (currentLine.Length > 0)
				{
					if (previousLine.Length > 0)
					{
						sourceFragmentBuilder.AppendLine(FormatSourceCodeLine(previousLine,
							new SourceCodeNodeCoordinates(previousLineNumber, 0),
							lineNumberSize, fragmentStartPosition, fragmentLength, tabSize));
					}

					sourceFragmentBuilder.AppendLine(FormatSourceCodeLine(currentLine,
						new SourceCodeNodeCoordinates(currentLineNumber, columnNumber),
						lineNumberSize, fragmentStartPosition, fragmentLength, tabSize));

					if (nextLine.Length > 0)
					{
						sourceFragmentBuilder.AppendLine(FormatSourceCodeLine(nextLine,
							new SourceCodeNodeCoordinates(nextLineNumber, 0),
							lineNumberSize, fragmentStartPosition, fragmentLength, tabSize));
					}

					sourceFragmentBuilder.TrimEnd();
				}

				sourceFragment = sourceFragmentBuilder.ToString();
				stringBuilderPool.Return(sourceFragmentBuilder);
			}

			return sourceFragment;
		}

		/// <summary>
		/// Calculates a cut positions
		/// </summary>
		/// <param name="line">Line content</param>
		/// <param name="columnNumber">Column number</param>
		/// <param name="maxFragmentLength">Maximum length of the source fragment</param>
		/// <param name="fragmentStartPosition">Start position of source fragment</param>
		/// <param name="fragmentLength">Length of source fragment</param>
		private static void CalculateCutPositions(string line, int columnNumber, int maxFragmentLength,
			out int fragmentStartPosition, out int fragmentLength)
		{
			int lineLength = line.Length;
			var leftOffset = (int)Math.Floor((double)maxFragmentLength / 2);

			fragmentStartPosition = 0;
			fragmentLength = maxFragmentLength;

			if (lineLength > maxFragmentLength)
			{
				fragmentStartPosition = columnNumber - leftOffset - 1;
				if (fragmentStartPosition < 0)
				{
					fragmentStartPosition = 0;
				}
				fragmentLength = maxFragmentLength - 2;
			}
		}

		/// <summary>
		/// Formats a line of source code
		/// </summary>
		/// <param name="line">Line content</param>
		/// <param name="nodeCoordinates">Node coordinates</param>
		/// <param name="lineNumberSize">Number of symbols in the line number caption</param>
		/// <param name="fragmentStartPosition">Start position of source fragment</param>
		/// <param name="fragmentLength">Length of source fragment</param>
		/// <param name="tabSize">Number of spaces in the tab</param>
		/// <returns>Formatted line</returns>
		private static string FormatSourceCodeLine(string line, SourceCodeNodeCoordinates nodeCoordinates,
			int lineNumberSize, int fragmentStartPosition = 0, int fragmentLength = 0, byte tabSize = 4)
		{
			const string ellipsisSymbol = "…";
			const byte leftPaddingSize = 7;

			int lineNumber = nodeCoordinates.LineNumber;
			int columnNumber = nodeCoordinates.ColumnNumber;
			int lineLength = line.Length;

			string processedLine;
			if (fragmentStartPosition == 0 && fragmentLength == lineLength)
			{
				processedLine = line;
			}
			else if (fragmentStartPosition >= lineLength)
			{
				processedLine = string.Empty;
			}
			else
			{
				int fragmentEndPosition = fragmentStartPosition + fragmentLength - 1;

				bool beginningCutOff = fragmentStartPosition > 0;
				bool endingCutOff = fragmentEndPosition <= lineLength;
				if (fragmentEndPosition + 1 == lineLength)
				{
					endingCutOff = false;
				}

				if (fragmentEndPosition >= lineLength)
				{
					endingCutOff = false;
					fragmentEndPosition = lineLength - 1;
					fragmentLength = fragmentEndPosition - fragmentStartPosition + 1;
				}

				processedLine = line.Substring(fragmentStartPosition, fragmentLength);
				if (beginningCutOff)
				{
					processedLine = ellipsisSymbol + processedLine;
				}
				if (endingCutOff)
				{
					processedLine = processedLine + ellipsisSymbol;
				}
			}

			string result = string.Format("Line {0}: {1}",
				lineNumber.ToString(CultureInfo.InvariantCulture).PadLeft(lineNumberSize),
				processedLine.TabsToSpaces(tabSize));
			if (columnNumber > 0)
			{
				int cursorOffset = columnNumber - fragmentStartPosition;
				if (fragmentStartPosition > 0)
				{
					cursorOffset++;
				}

				result += Environment.NewLine + string.Empty
					.PadRight(
						(cursorOffset < processedLine.Length ?
							processedLine.Substring(0, cursorOffset - 1) : processedLine
						).TabsToSpaces(tabSize).Length + lineNumberSize + leftPaddingSize
					)
					.Replace(" ", "-") + "^"
					;
			}

			return result;
		}
	}
}