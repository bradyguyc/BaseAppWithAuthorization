using BaseAppWithAuthorization.MSALClient;
using BaseAppWithAuthorization.ViewModels;

using Microsoft.Identity.Client;

namespace BaseAppWithAuthorization.Views
{
    public partial class MainView : ContentPage
    {
        int count = 0;

        public MainView(MainViewViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
         
            
        }

        
    }
}
