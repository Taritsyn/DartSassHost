namespace DartSassHost
{
	/// <summary>
	/// Information about the problem
	/// </summary>
	public sealed class ProblemInfo
	{
		/// <summary>
		/// Gets or sets a message that describes the current problem
		/// </summary>
		public string Message
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a description of problem
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a ID of deprecation
		/// </summary>
		public string DeprecationId
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value that indicates if the problem is a deprecation
		/// </summary>
		public bool IsDeprecation
		{
			get { return DeprecationId != null; }
		}

		/// <summary>
		/// Gets or sets a name of file, where the problem occurred
		/// </summary>
		public string File
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a line number
		/// </summary>
		public int LineNumber
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a column number
		/// </summary>
		public int ColumnNumber
		{
			get;
			set;
		}


		/// <summary>
		/// Gets or sets a source fragment
		/// </summary>
		public string SourceFragment
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a string representation of the call stack
		/// </summary>
		public string CallStack
		{
			get;
			set;
		}


		/// <summary>
		/// Constructs an instance of the problem information
		/// </summary>
		public ProblemInfo()
		{
			Message = string.Empty;
			Description = string.Empty;
			DeprecationId = null;
			File = string.Empty;
			LineNumber = 0;
			ColumnNumber = 0;
			SourceFragment = string.Empty;
			CallStack = string.Empty;
		}
	}
}