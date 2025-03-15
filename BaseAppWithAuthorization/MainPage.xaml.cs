using BaseAppWithAuthorization.MSALClient;
using Microsoft.Identity.Client;

namespace BaseAppWithAuthorization
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            IAccount cachedUserAccount = PublicClientSingleton.Instance.MSALClientHelper.FetchSignedInUserFromCache().Result;
            
            _ = Dispatcher.DispatchAsync(async () =>
            {
            
            if (cachedUserAccount == null)
            {
                await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
            }

            });
            
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
