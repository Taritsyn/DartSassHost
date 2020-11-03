﻿using System;
using System.Globalization;
using System.Text;

using AdvancedStringBuilder;

using SassHost.Resources;

namespace SassHost.Helpers
{
	/// <summary>
	/// Sass error helpers
	/// </summary>
	public static class SassErrorHelpers
	{
		#region Generation of error messages

		/// <summary>
		/// Generates a compiler load error message
		/// </summary>
		/// <param name="description">Description of error</param>
		/// <param name="quoteDescription">Makes a quote from the description</param>
		/// <returns>Compiler load error message</returns>
		internal static string GenerateCompilerLoadErrorMessage(string description, bool quoteDescription = false)
		{
			string compilerNotLoadedPart = Strings.Compiler_SassCompilerNotLoaded;
			string message;

			if (!string.IsNullOrWhiteSpace(description))
			{
				var stringBuilderPool = StringBuilderPool.Shared;
				StringBuilder messageBuilder = stringBuilderPool.Rent();
				messageBuilder.Append(compilerNotLoadedPart);
				messageBuilder.Append(" ");
				if (quoteDescription)
				{
					messageBuilder.AppendFormat(Strings.Common_SeeOriginalErrorMessage, description);
				}
				else
				{
					messageBuilder.Append(description);
				}

				message = messageBuilder.ToString();
				stringBuilderPool.Return(messageBuilder);
			}
			else
			{
				message = compilerNotLoadedPart;
			}

			return message;
		}

		/// <summary>
		/// Generates a compilation error message
		/// </summary>
		/// <param name="type">Type of the compilation error</param>
		/// <param name="description">Description of error</param>
		/// <param name="documentName">Document name</param>
		/// <param name="lineNumber">Line number</param>
		/// <param name="columnNumber">Column number</param>
		/// <param name="sourceFragment">Source fragment</param>
		/// <returns>Compilation error message</returns>
		internal static string GenerateCompilationErrorMessage(string type, string description, string documentName,
			int lineNumber, int columnNumber, string sourceFragment)
		{
			if (description == null)
			{
				throw new ArgumentNullException(nameof(description));
			}

			if (string.IsNullOrWhiteSpace(description))
			{
				throw new ArgumentException(
					string.Format(Strings.Common_ArgumentIsEmpty, nameof(description)),
					nameof(description)
				);
			}

			var stringBuilderPool = StringBuilderPool.Shared;
			StringBuilder messageBuilder = stringBuilderPool.Rent();
			if (!string.IsNullOrWhiteSpace(type))
			{
				messageBuilder.Append(type);
				messageBuilder.Append(": ");
			}
			messageBuilder.Append(description);

			bool documentNameNotEmpty = !string.IsNullOrWhiteSpace(documentName);
			if (documentNameNotEmpty || lineNumber > 0)
			{
				messageBuilder.AppendLine();
				messageBuilder.Append("   at ");
				if (documentNameNotEmpty)
				{
					messageBuilder.Append(documentName);
				}
				if (lineNumber > 0)
				{
					if (documentNameNotEmpty)
					{
						messageBuilder.Append(":");
					}
					messageBuilder.Append(lineNumber);
					if (columnNumber > 0)
					{
						messageBuilder.Append(":");
						messageBuilder.Append(columnNumber);
					}
				}
				if (!string.IsNullOrWhiteSpace(sourceFragment))
				{
					messageBuilder.Append(" -> ");
					messageBuilder.Append(sourceFragment);
				}
			}

			string errorMessage = messageBuilder.ToString();
			stringBuilderPool.Return(messageBuilder);

			return errorMessage;
		}

		#endregion

		#region Generation of error details

		/// <summary>
		/// Generates a detailed error message
		/// </summary>
		/// <param name="sassException">Sass exception</param>
		/// <param name="omitMessage">Flag for whether to omit message</param>
		/// <returns>Detailed error message</returns>
		public static string GenerateErrorDetails(SassException sassException, bool omitMessage = false)
		{
			if (sassException == null)
			{
				throw new ArgumentNullException(nameof(sassException));
			}

			var stringBuilderPool = StringBuilderPool.Shared;
			StringBuilder detailsBuilder = stringBuilderPool.Rent();
			WriteCommonErrorDetails(detailsBuilder, sassException, omitMessage);

			var sassСompilationException = sassException as SassCompilationException;
			if (sassСompilationException != null)
			{
				WriteCompilationErrorDetails(detailsBuilder, sassСompilationException);
			}

			detailsBuilder.TrimEnd();

			string errorDetails = detailsBuilder.ToString();
			stringBuilderPool.Return(detailsBuilder);

			return errorDetails;
		}

		/// <summary>
		/// Generates a detailed error message
		/// </summary>
		/// <param name="sassСompilationException">Sass compilation exception</param>
		/// <param name="omitMessage">Flag for whether to omit message</param>
		/// <returns>Detailed error message</returns>
		public static string GenerateErrorDetails(SassCompilationException sassСompilationException,
			bool omitMessage = false)
		{
			if (sassСompilationException == null)
			{
				throw new ArgumentNullException(nameof(sassСompilationException));
			}

			var stringBuilderPool = StringBuilderPool.Shared;
			StringBuilder detailsBuilder = stringBuilderPool.Rent();
			WriteCommonErrorDetails(detailsBuilder, sassСompilationException, omitMessage);
			WriteCompilationErrorDetails(detailsBuilder, sassСompilationException);

			detailsBuilder.TrimEnd();

			string errorDetails = detailsBuilder.ToString();
			stringBuilderPool.Return(detailsBuilder);

			return errorDetails;
		}

		/// <summary>
		/// Writes a detailed error message to the buffer
		/// </summary>
		/// <param name="buffer">Instance of <see cref="StringBuilder"/></param>
		/// <param name="sassException">Sass exception</param>
		/// <param name="omitMessage">Flag for whether to omit message</param>
		private static void WriteCommonErrorDetails(StringBuilder buffer, SassException sassException,
			bool omitMessage = false)
		{
			if (!omitMessage)
			{
				buffer.AppendFormatLine("{0}: {1}", Strings.ErrorDetails_Message,
					sassException.Message);
			}
		}

		/// <summary>
		/// Writes a detailed error message to the buffer
		/// </summary>
		/// <param name="buffer">Instance of <see cref="StringBuilder"/></param>
		/// <param name="sassСompilationException">Sass compilation exception</param>
		private static void WriteCompilationErrorDetails(StringBuilder buffer,
			SassCompilationException sassСompilationException)
		{
			//if (sassСompilationException.ErrorCode > 0)
			//{
			//	buffer.AppendFormatLine("{0}: {1}", Strings.ErrorDetails_ErrorCode, sassСompilationException.ErrorCode);
			//}
			buffer.AppendFormatLine("{0}: {1}", Strings.ErrorDetails_Description, sassСompilationException.Description);
			if (!string.IsNullOrWhiteSpace(sassСompilationException.File))
			{
				buffer.AppendFormatLine("{0}: {1}", Strings.ErrorDetails_File, sassСompilationException.File);
			}
			if (sassСompilationException.LineNumber > 0)
			{
				buffer.AppendFormatLine("{0}: {1}", Strings.ErrorDetails_LineNumber,
					sassСompilationException.LineNumber.ToString(CultureInfo.InvariantCulture));
			}
			if (sassСompilationException.ColumnNumber > 0)
			{
				buffer.AppendFormatLine("{0}: {1}", Strings.ErrorDetails_ColumnNumber,
					sassСompilationException.ColumnNumber.ToString(CultureInfo.InvariantCulture));
			}
			if (!string.IsNullOrWhiteSpace(sassСompilationException.SourceFragment))
			{
				buffer.AppendFormatLine("{1}:{0}{0}{2}", Environment.NewLine,
					Strings.ErrorDetails_SourceFragment,
					sassСompilationException.SourceFragment);
			}
		}

		#endregion

		#region Exception wrapping

		internal static SassCompilerLoadException WrapCompilerLoadException(Exception exception,
			bool quoteDescription = false)
		{
			string description = exception.Message;
			string message = GenerateCompilerLoadErrorMessage(description, quoteDescription);

			var sassCompilerLoadException = new SassCompilerLoadException(message, exception)
			{
				Description = description
			};

			return sassCompilerLoadException;
		}

		#endregion
	}
}