﻿namespace Profolio.Server.Extensions
{
	/// <summary> 請求 擴充方法 </summary>
	public static class RequestExtension
    {
        /// <summary> 是否是 AJAX請求 </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request) => request.Headers["X-Requested-With"] == "XMLHttpRequest" || request.Headers.ContainsKey("requestverificationtoken");


        private static bool IsInternetExplorer(string userAgent)
           => userAgent.Contains("MSIE") || userAgent.Contains("Trident");

        /// <summary> 判斷是否是IE </summary>
        public static bool IsInternetExplorer(this HttpRequest request)
            => IsInternetExplorer(request.Headers["User-Agent"]);
    }
}