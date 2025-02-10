using System;
#if !NETSTANDARD1_3
using System.Runtime.Serialization;
#endif
#if !NETSTANDARD1_3 && !NET8_0_OR_GREATER
using System.Security.Permissions;
#endif

namespace DartSassHost
{
	/// <summary>
	/// The exception that is thrown during a Sass compilation
	/// </summary>
#if !NETSTANDARD1_3
	[Serializable]
#endif
	public sealed class SassCompilationException : SassException
	{
		/// <summary>
		/// Status code
		/// </summary>
		private int _status;

		/// <summary>
		/// Name of file, where the error occurred
		/// </summary>
		private string _file = string.Empty;

		/// <summary>
		/// Line number
		/// </summary>
		private int _lineNumber;

		/// <summary>
		/// Column number
		/// </summary>
		private int _columnNumber;

		/// <summary>
		/// Source fragment
		/// </summary>
		private string _sourceFragment = string.Empty;

		/// <summary>
		/// String representation of the call stack
		/// </summary>
		private string _callStack = string.Empty;

		/// <summary>
		/// Gets or sets a status code
		///		1 - normal errors like parsing or <с>eval</с> errors;
		///		3 - "untranslated" exceptions like file not found.
		/// </summary>
		public int Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		/// Gets or sets a name of file, where the error occurred
		/// </summary>
		public string File
		{
			get { return _file; }
			set { _file = value; }
		}

		/// <summary>
		/// Gets or sets a line number
		/// </summary>
		public int LineNumber
		{
			get { return _lineNumber; }
			set { _lineNumber = value; }
		}

		/// <summary>
		/// Gets or sets a column number
		/// </summary>
		public int ColumnNumber
		{
			get { return _columnNumber; }
			set { _columnNumber = value; }
		}

		/// <summary>
		/// Gets or sets a source fragment
		/// </summary>
		public string SourceFragment
		{
			get { return _sourceFragment; }
			set { _sourceFragment = value; }
		}

		/// <summary>
		/// Gets or sets a string representation of the call stack
		/// </summary>
		public string CallStack
		{
			get { return _callStack; }
			set { _callStack = value; }
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="SassCompilationException"/> class
		/// with a specified error message
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public SassCompilationException(string message)
			: base(message)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="SassCompilationException"/> class
		/// with a specified error message and a reference to the inner exception
		/// that is the cause of this exception
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <param name="innerException">The exception that is the cause of the current exception</param>
		public SassCompilationException(string message, Exception innerException)
			: base(message, innerException)
		{ }
#if !NETSTANDARD1_3

		/// <summary>
		/// Initializes a new instance of the <see cref="SassCompilationException"/> class with serialized data
		/// </summary>
		/// <param name="info">The object that holds the serialized data</param>
		/// <param name="context">The contextual information about the source or destination</param>
#if NET8_0_OR_GREATER
		[Obsolete(DiagnosticId = "SYSLIB0051")]
#endif
		private SassCompilationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info != null)
			{
				_status = info.GetInt32("Status");
				_file = info.GetString("File");
				_lineNumber = info.GetInt32("LineNumber");
				_columnNumber = info.GetInt32("ColumnNumber");
				_sourceFragment = info.GetString("SourceFragment");
				_callStack = info.GetString("CallStack");
			}
		}


		#region Exception overrides

		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> to populate with data</param>
		/// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization</param>
#if NET8_0_OR_GREATER
		[Obsolete(DiagnosticId = "SYSLIB0051")]
#else
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
#endif
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException(nameof(info));
			}

			base.GetObjectData(info, context);
			info.AddValue("Status", _status);
			info.AddValue("File", _file);
			info.AddValue("LineNumber", _lineNumber);
			info.AddValue("ColumnNumber", _columnNumber);
			info.AddValue("SourceFragment", _sourceFragment);
			info.AddValue("CallStack", _callStack);
		}

		#endregion
#endif
	}
}