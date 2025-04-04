namespace Profolio.Server.Services.Interfaces
{
	public interface IBlobStorageService
	{
		Task<string> UploadBlob(string container, string blobName, string content);
		Task<string> GetBlobContent(string container, string blobName);
	}
}