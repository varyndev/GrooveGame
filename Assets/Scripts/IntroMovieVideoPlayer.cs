using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AudioSource))]


// This script is designed to play the intro video and determine when it is done.

public class IntroMovieVideoPlayer : MonoBehaviour {

#if ! (UNITY_IOS || UNITY_ANDROID)
	private MovieTexture movie;
#else
	private GameObject movie;
#endif

	void Start () {
#if ! (UNITY_IOS || UNITY_ANDROID)
		if (GetComponent<Renderer>() != null) {
			movie = (MovieTexture) GetComponent<Renderer>().material.mainTexture;
		}
#else
		movie = null;
#endif
	}

	public void StartMovie () {
#if ! (UNITY_IOS || UNITY_ANDROID)
		if (movie != null) {
			Debug.Log("playing movie");
			GetComponent<AudioSource>().clip = movie.audioClip;
			GetComponent<AudioSource>().Play ();
			movie.Play();
		}
#else
		Handheld.PlayFullScreenMovie ("groove_game.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
#endif
	}

	public void StopMovie () {
		if (movie != null) {
#if ! (UNITY_IOS || UNITY_ANDROID)
			movie.Stop ();
#endif
		}
	}

	public bool IsPlaying () {
		if (movie != null) {
#if ! (UNITY_IOS || UNITY_ANDROID)
			return movie.isPlaying;
#else
			return false;
#endif
		} else {
			return false;
		}
	}
}
