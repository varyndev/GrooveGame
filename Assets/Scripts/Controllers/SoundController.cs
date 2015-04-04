using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BoogieDownGames {


	[RequireComponent (typeof (AudioSource))]
	public class SoundController : UnitySingleton<SoundController> {

		[SerializeField]
		private List<AudioClip> m_soundClips;

		[SerializeField]
		private int m_initialSound; //Index of the initial sound

		[SerializeField]
		private int m_currentIndex;

		public bool DetectEndOfSound()
		{
			if(!GetComponent<AudioSource>().isPlaying) {
				return true;
			} else {
				return false;
			}
		}

		void Start()
		{
		}

		public void SetVolume(float p_vol)
		{
			GetComponent<AudioSource>().volume = p_vol;
		}

		public void playAtIndex(int p_index)
		{
			if(p_index > m_soundClips.Count - 1) {
				p_index = m_soundClips.Count -1;
			}
			Debug.Log("Playing clip " + m_soundClips[m_currentIndex].name);
			m_currentIndex = p_index;
			PlayCurrent ();
		}

		public AudioClip GetClipAtIndex(int p_index)
		{
			if(p_index > m_soundClips.Count - 1) {
				p_index = m_soundClips.Count -1;
			}
			m_currentIndex = p_index;
			//PostMessage("SetText",m_soundClips[m_currentIndex].name);
			return m_soundClips[p_index];
		}

		public void NextSound()
		{
			m_currentIndex++;
			if (m_currentIndex >= m_soundClips.Count) {
				m_currentIndex = 0;
			}
			PlayCurrent ();
		}

		public void PrevSound()
		{
			m_currentIndex--;
			if (m_currentIndex <= 0) {
				m_currentIndex = m_soundClips.Count -1;
			}
			PlayCurrent ();
		}

		public void PlayCurrent ()
		{
			//audio.clip = m_soundClips[m_currentIndex];
			GetComponent<AudioSource>().PlayOneShot(m_soundClips[m_currentIndex]);
			//PostMessage("SetText",m_soundClips[m_currentIndex].name);
		}
		
		public void StopSound()
		{
			GetComponent<AudioSource>().Stop();
			PostMessage("SetText",m_soundClips[m_currentIndex].name + " Stopped");
		}

		public void PauseSound()
		{
			GetComponent<AudioSource>().Pause();
		}

		public void UnPauseSound()
		{
			GetComponent<AudioSource>().Play();
		}

		public void PostMessage(string p_func, string p_message)
		{
			Hashtable dat = new Hashtable();
			dat.Add("msg",p_message);
			NotificationCenter.DefaultCenter.PostNotification(this,p_func,dat);
		}
	}
}