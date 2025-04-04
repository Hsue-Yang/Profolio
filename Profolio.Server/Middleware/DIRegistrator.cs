using System.Reflection;

namespace Profolio.Server.Middleware
{
	public static class DIRegistrator
	{
		/// <summary> Dynamic DI Register </summary>
		/// <param name="services"></param>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static IServiceCollection AddDIRegister(this IServiceCollection services, Assembly assembly)
		{
			// 取得編譯時期的所有型別dll
			var types = assembly.GetTypes();

			foreach (var type in types)
			{
				if (type.IsClass == false || type.IsAbstract || type.IsPublic == false)
				{
					continue;
				}
				// 以Service或Repository結尾的型別，自動註冊
				if (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository"))
				{
					var interfaceType = type.GetInterface($"I{type.Name}");
					if (interfaceType != null)
					{
						services.AddScoped(interfaceType, type);
					}
				}
			}

			return services;
		}
	}
}