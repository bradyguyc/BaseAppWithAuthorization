using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using BaseAppWithAuthorization.MSALClient;

using Microsoft.Identity.Client;

namespace BaseAppWithAuthorization
{
    
    [IntentFilter(new[] { Intent.ActionView },
      Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
      DataHost = "auth",
      DataScheme = "msal5998ef82-5d29-43eb-8c29-3c3dfd3dfbfb")]
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // configure platform specific params
            PlatformConfig.Instance.RedirectUri = $"msal{PublicClientSingleton.Instance.MSALClientHelper.AzureAdConfig.ClientId}://auth";
            PlatformConfig.Instance.ParentWindow = this;

            // Initialize MSAL and platformConfig is set
            _ = Task.Run(async () => await PublicClientSingleton.Instance.MSALClientHelper.InitializePublicClientAppAsync()).Result;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
  

}
