using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AudioSource))]


// This script is designed to play the intro video and determine when it is done.

public class IntroMovieVideoPlayer : MonoBehaviour {

	private MovieTexture movie;

	void Start () {
		if (GetComponent<Renderer>() != null) {
			movie = (MovieTexture) GetComponent<Renderer>().material.mainTexture;
		}
	}

	public void StartMovie () {
		if (movie != null) {
			GetComponent<AudioSource>().clip = movie.audioClip;
			GetComponent<AudioSource>().Play ();
			movie.Play();
		}
	}

	public void StopMovie () {
		if (movie != null) {
			movie.Stop ();
		}
	}

	public bool IsPlaying () {
		if (movie != null) {
			return movie.isPlaying;
		} else {
			return false;
		}
	}
}
