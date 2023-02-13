using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;

namespace TowerDefence
{
    public class UnityServicesSDK : MonoBehaviour
    {
        async void Start()
        {
            await UnityServices.InitializeAsync();
            Debug.Log($"Service UnityServicesSDK: {UnityServices.State}" );
            checkSessionTokenExists();
            SignInAnonymouslyAsync();
        }

        void checkSessionTokenExists()
        {
            if (!AuthenticationService.Instance.SessionTokenExists) 
            {
                Debug.Log( $"!AuthenticationService.Instance.SessionTokenExists" );
            }
            else
            {
                Debug.Log( $"SessionToken is Exists !!!" );
            }
        }
        async Task SignInAnonymouslyAsync()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Sign in anonymously succeeded!");
        
                // Shows how to get the playerID
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 

            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
        }
        void SetupEvents() {
            AuthenticationService.Instance.SignedIn += () => {
                // Shows how to get a playerID
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

                // Shows how to get an access token
                Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

            };

            AuthenticationService.Instance.SignInFailed += (err) => {
                Debug.LogError(err);
            };

            AuthenticationService.Instance.SignedOut += () => {
                Debug.Log("Player signed out.");
            };
 
            AuthenticationService.Instance.Expired += () =>
            {
                Debug.Log("Player session could not be refreshed and expired.");
            };
        }
    }
}
