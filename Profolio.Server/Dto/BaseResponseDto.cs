using Profolio.Server.Enums;

namespace Profolio.Server.Dto
{
	public class BaseResponseDto
	{
		/// <summary> Status(1:Success 2:Fail) </summary>
		public SuccessEnum Status { get; set; }

		/// <summary> Data </summary>
		public object Data { get; set; }

		/// <summary> Message </summary>
		public string Message { get; set; }

		/// <summary> Error List </summary>
		public List<VM_ResponseErrorData> ErrorList { get; set; }
	}
	/// <summary> Error Model </summary>
	public class VM_ResponseErrorData
	{
		/// <summary> Name </summary>
		public string Key { get; set; }
		/// <summary> Error Text List </summary>
		public List<string> ErrorTextList { get; set; }
	}
	/// <summary> Common Response Model </summary>
	public class VM_Response<T>
	{
		/// <summary> Status(1:Success 2:Fail) </summary>
		public SuccessEnum Status { get; set; }

		/// <summary> Data </summary>
		public T Data { get; set; }

		/// <summary> Message </summary>
		public string Message { get; set; }

		/// <summary> Error List </summary>
		public List<VM_ResponseErrorData> ErrorList { get; set; }
	}
}
