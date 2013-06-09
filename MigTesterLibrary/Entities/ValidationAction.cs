namespace DataMigrationValidator
{
	public class ValidationAction
	{
		private string m_SourceQueryFileName = string.Empty;
		private string m_DestinationQueryFileName = string.Empty;
		private bool m_ShallRunTest = false;

		/// <summary>
		/// Gets or sets the name of the source query file.
		/// </summary>
		public string SourceQueryFileName
		{
			get
			{
				return m_SourceQueryFileName;
			}
			set
			{
				m_SourceQueryFileName = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the destination query file.
		/// </summary>
		public string DestinationQueryFileName
		{
			get
			{
				return m_DestinationQueryFileName;
			}
			set
			{
				m_DestinationQueryFileName = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether validate data on source query and destination query.
		/// </summary>
		/// <value>
		///  if <c>true</c> validate data; otherwise, if <c>false</c>, don't validate.
		/// </value>
		public bool ShallRunTest
		{
			get
			{
				return m_ShallRunTest;
			}
			set
			{
				m_ShallRunTest = value;
			}
		}
	}
}
