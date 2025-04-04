using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using Profolio.Server.Dto;
using Profolio.Server.Extensions;
using Profolio.Server.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Profolio.Server.Filters
{
	public class ExceptionFilter : ExceptionFilterAttribute
	{
		private readonly ILogger<ExceptionFilter> _logger;
		private readonly IMongoService _mongoService;

		public ExceptionFilter(ILogger<ExceptionFilter> logger, IMongoService mongoService)
		{
			_logger = logger;
			_mongoService = mongoService;
		}

		public override void OnException(ExceptionContext context)
		{
			var ex = context.Exception;
			var response = new BaseResponseDto
			{
				Status = Enums.SuccessEnum.Success,
				Message = ex.Message,
			};
			_logger.Log(LogLevel.Error, ex.Message);
			_mongoService.InsertOneAsync("Profolio", new BsonDocument
			{
				{ "RequestUrl", context.HttpContext.Request.Path.Value ?? "" },
				{ "Message", ex.Message ?? "" },
				{ "StatusCode", context.HttpContext.Response.StatusCode },
				{ "Headers", new BsonDocument(context.HttpContext.Response.Headers.ToDictionary(h => h.Key, h => (BsonValue)h.Value.ToString())) ?? null},
				{ "StackTrace", ex.StackTrace ?? "" },
				{ "InnerException", ex.InnerException?.Message ?? "" },
				{ "InnerStackTrace", ex.InnerException ?.StackTrace ?? "" },
				{ "Source", ex.Source ?? "" },
				{ "TargetSite", ex.TargetSite ?.Name ?? "" },
				{ "Type", ex.GetType().Name ?? "" },
				{ "Time", DateTime.Now },
			});
			// Model binding 資料驗證例外統一處理.
			if (ex is ValidationException)
			{
				var errorMsgList = context.ModelState.SelectMany(x => x.Value.Errors).Select(y => y.ErrorMessage).ToList();
				response.Message = string.Join(",", errorMsgList);
			}

			// 查無資料的例外處理.
			if (ex is NullReferenceException)
			{
				response.Message = "ValidMessage_ExceptionFilter_查無資料";
			}
			context.ExceptionHandled = true;
			if (RequestExtension.IsAjaxRequest(context.HttpContext.Request))
			{
				// 處理 AJAX 請求，返回 JSON 結果
				context.Result = new JsonResult(
					new { Status = response.Status, Message = "RedirectMessage_Base_發生未預期的錯誤，請重新確認", })
				{ StatusCode = 500 };
			}
			else
			{
				// 將例外處理訊息放進filterContext回傳至前端.
				//context.Result = new JsonResult(response);
				//context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;  // 回200 OK配合前端的錯誤訊息處理.
				//context.ExceptionHandled = true;
				//base.OnException(context);
			}
		}
	}
}