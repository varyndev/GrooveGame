using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BoogieDownGames {

	public class AudioController : UnitySingleton<AudioController> {

		[SerializeField]
		private List<AudioClip> m_soundClips;
		
		[SerializeField]
		private int m_initialSound; //Index of the initial sound
		
		[SerializeField]
		private int m_currentIndex;

		[SerializeField]
		private bool m_isPaused;

		#region PROPERTIES

		public List<AudioClip> AudioClips
		{
			get { return m_soundClips; }
		}

		public bool IsPaused
		{
			get { return m_isPaused; }
			set { m_isPaused = true; }
		}

		#endregion

		void Awake()
		{
			//We going to make sure that the list has something in it first
			if( m_soundClips.Count > 0 ) {
				GetComponent<AudioSource>().clip = m_soundClips[m_initialSound];
				m_currentIndex = m_initialSound;
			}
		}
		
		public bool DetectEndOfSong()
		{
			if(!GetComponent<AudioSource>().isPlaying) {
				return true;
			} else {
				return false;
			}
		}
		
		void Start()
		{
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayCurrentSong");
			NotificationCenter.DefaultCenter.AddObserver(this, "PauseSong");
			//playAtIndex(m_initialSound);
		}
		
		public void SetVolume(float p_vol)
		{
			GetComponent<AudioSource>().volume = p_vol;
		}
		
		public void playAtIndex(int p_index)
		{
			if (p_index > m_soundClips.Count - 1) {
				p_index = m_soundClips.Count -1;
			}
			if (p_index < 0) {
				p_index = 0;
			}
			if (m_soundClips.Count > 0) {
				m_currentIndex = p_index;
				GetComponent<AudioSource> ().clip = m_soundClips [p_index];
				GetComponent<AudioSource> ().Play ();
				PostMessage ("SetText", m_soundClips [p_index].name);
				GameMaster.Instance.CurrentSong = m_currentIndex;
			}
		}

		public void PlayCurrentSong()
		{
			GetComponent<AudioSource>().Play();
			m_isPaused = false;
		}
		
		public AudioClip GetClipAtIndex(int p_index)
		{
			if(p_index > m_soundClips.Count - 1) {
				p_index = m_soundClips.Count -1;
			}
			m_currentIndex = p_index;
			PostMessage("SetText",m_soundClips[m_currentIndex].name);
			return m_soundClips[p_index];
		}
		
		public void NextSong()
		{
			m_currentIndex++;
			if(m_currentIndex > m_soundClips.Count -1) {
				m_currentIndex = 0;
			}
			GetComponent<AudioSource>().clip = m_soundClips[m_currentIndex];
			GetComponent<AudioSource>().Play();
			PostMessage("SetText",m_soundClips[m_currentIndex].name);
			GameMaster.Instance.CurrentSong = m_currentIndex;
		}
		
		public void PrevSong()
		{
			m_currentIndex--;
			if(m_currentIndex <= 0) {
				m_currentIndex = m_soundClips.Count -1;
			}
			GetComponent<AudioSource>().clip = m_soundClips[m_currentIndex];
			GetComponent<AudioSource>().Play();
			PostMessage("SetText",m_soundClips[m_currentIndex].name);
			GameMaster.Instance.CurrentSong = m_currentIndex;
		}
		
		public void StopSong()
		{
			GetComponent<AudioSource>().Stop();
			PostMessage("SetText",m_soundClips[m_currentIndex].name + " Stopped");
		}
		
		public void PauseSong()
		{
			GetComponent<AudioSource>().Pause();
			m_isPaused = true;
		}
		
		public void UnPauseSong()
		{
			GetComponent<AudioSource>().Play();
			m_isPaused = false;
		}
		
		public void PostMessage(string p_func, string p_message)
		{
			Hashtable dat = new Hashtable();
			dat.Add("msg",p_message);
			NotificationCenter.DefaultCenter.PostNotification(this,p_func,dat);
		}
	}
}