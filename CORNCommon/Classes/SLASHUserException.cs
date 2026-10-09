using System;

namespace CORNCommon.Classes
{
	/// <summary>
	/// CORNUserException
	/// <author>Rizwan Ansari</author>
	/// <date></date>
	/// </summary>
	public class CORNUserException : Exception
	{	
		#region Constructors
		public CORNUserException(string Message, Exception InnerException) : base(Message, InnerException)
		{
		}
		#endregion
				
		#region Private Variables
		#endregion

		#region public Properties
		#endregion
		
		#region public static Methods
		#endregion

		#region Private Methods
		#endregion
		
		#region public Methods
		#endregion
	}
}
