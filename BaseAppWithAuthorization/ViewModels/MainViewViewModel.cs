using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
        [ObservableProperty] List<string> idTokenClaims = new();
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
            IAccount? cachedUserAccount = null; ;
                cachedUserAccount  = PublicClientSingleton.Instance.MSALClientHelper.FetchSignedInUserFromCache().Result;
         
            
            return MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (cachedUserAccount == null)
                {
                    string token = null;
                    try
                    {
                        token = await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
                    } catch (Exception ex)
                    {
                       
                        Console.WriteLine(ex.Message);
                    }
                    if (token == null)
                    {
                        //todo: display error message to user that they need to sign in and the sign process was exited before completing sign in   
                    }
                    else
                    {

                        IAccount cachedUserAccount = PublicClientSingleton.Instance.MSALClientHelper.FetchSignedInUserFromCache().Result;
                        if (cachedUserAccount != null)
                        {
                            IsSignedIn = true;
                            await UpdateClaims();
                        }
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
        [RelayCommand]
        public async Task CallProtectedFunction()
        {
            // string token = PublicClientSingleton.Instance.MSALClientHelper.AuthResult.AccessToken;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(130); // Set custom timeout value
                    //string[] scopes = { "https://MyNextbook.onmicrosoft.com/MyNextBookAPIs/GetSecrets.Read" };
                    string token  = await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
                    //todo: check if authResult is null and expiration
                    //tring token = authResult.AccessToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                    string functionUrl = "https://myrecipebookmakerbackend.azurewebsites.net/api/GetSecrets";
                    HttpResponseMessage response = await client.GetAsync(functionUrl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

}