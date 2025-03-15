using Android.App;
using Android.Content.PM;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Identity.Client;

namespace BaseAppWithAuthorization
{
    
    [IntentFilter(new[] { Intent.ActionView },
      Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
      DataHost = "auth",
      DataScheme = "msal5903a498-6b45-4c70-a6b0-57b439fd14b1")]
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
  

}
