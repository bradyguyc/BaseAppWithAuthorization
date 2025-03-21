using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using BaseAppWithAuthorization.MSALClient;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;


namespace BaseAppWithAuthorization.ViewModels
{
    public partial class MainViewViewModel : ObservableObject
    {
        [ObservableProperty] bool isSignedIn = false;
        [ObservableProperty] List<string> idTokenClaims = new ();
        public MainViewViewModel()
        {
            IsSignedIn = false;
            IAccount cachedUserAccount = PublicClientSingleton.Instance.MSALClientHelper.FetchSignedInUserFromCache().Result;
            if (cachedUserAccount != null)
            {
                IsSignedIn = true;
                UpdateClaims();
                
            }
        }
        async Task UpdateClaims()
        {
            _ = await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
            
            var claims = PublicClientSingleton.Instance.MSALClientHelper.AuthResult.ClaimsPrincipal.Claims.Select(c => c.Value);
            IdTokenClaims = claims.ToList();
        }
        [RelayCommand]
        public Task SignIn()
        {
            IAccount cachedUserAccount = PublicClientSingleton.Instance.MSALClientHelper.FetchSignedInUserFromCache().Result;

            return MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (cachedUserAccount == null)
                {
                    await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
                    IAccount cachedUserAccount = PublicClientSingleton.Instance.MSALClientHelper.FetchSignedInUserFromCache().Result;
                    if (cachedUserAccount != null)
                    {
                        IsSignedIn = true;
                        await UpdateClaims();
                    }
                }
            });
        }
        [RelayCommand]
        public Task SignOut()
        {
            return MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await PublicClientSingleton.Instance.MSALClientHelper.SignOutUserAsync();
                IsSignedIn = false;
            });
        }
    }
}
