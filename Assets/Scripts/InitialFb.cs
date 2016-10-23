using UnityEngine;
using System;
using Facebook.Unity;

public class InitialFb : MonoBehaviour {

	// Awake function from Unity's MonoBehavior
	void Awake ()
	{
    	if (!FB.IsInitialized) {
        	// Initialize the Facebook SDK
        	FB.Init(InitCallback, OnHideUnity);
    	} else {
        	// Already initialized, signal an app activation App Event
        	FB.ActivateApp();
    	}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			Debug.Log("Initialisation of Facebook SDK is completed");
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void shareScore() {
		FB.FeedShare(string.Empty, new Uri("https://developers.facebook.com/"),"linkname","linkcaption","linkdescription",new Uri ("http://a4.mzstatic.com/us/r30/Purple69/v4/73/b4/dd/73b4dda6-564a-c73e-d0bb-ed457333216d/icon175x175.png"),"mediasource",callback:ShareCallback);
	}

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			Debug.Log("ShareLink Error: "+result.Error);
		} else if (!String.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}
}
