﻿using Hangfire;

namespace Profolio.Server.Extensions
{
	public class HangfireActivator : JobActivator
	{
		private readonly IServiceProvider _serviceProvider;
		public HangfireActivator(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		public override object ActivateJob(Type jobType)
		{
			return _serviceProvider.GetService(jobType);
		}
	}
}