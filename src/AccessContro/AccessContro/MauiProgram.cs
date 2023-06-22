using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AccessContro;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>().UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        //builder.c((_, services) =>
        //{
        //    services.AddTransient<MyMiddleware>();
        //});
        System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
