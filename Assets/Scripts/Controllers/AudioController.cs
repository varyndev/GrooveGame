using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BoogieDownGames {

	public class AudioController : UnitySingleton<AudioController> {

		private static bool passedLoad = false;

		[SerializeField]
		private List<AudioClip> m_soundClips;
		
		[SerializeField]
		private int m_initialSound; // Index of the initial sound
		
		[SerializeField]
		private int m_currentIndex;

		[SerializeField]
		private bool m_isPaused;

		[SerializeField]
		private bool m_isMenu;

		//Stores song demo info for menu
		private float m_currentDemoOffset;
		private float m_currentDemoDuration;

		private AudioSource m_audioSourceReference; // Holds reference to the audio source component on this GameObject

		// TODO: Alter this list if order changes
		private List<string> m_defaultSongOrder = new List<string>
		{
			"SheSaid",
			"FryandSizzle",
			"AloneTonight",
            "I_Got_You",
			"ItsFilth",
			"ImSexyNow",
			"ShutUpDance",
			"InFashion",
			"SheSmiles",
			"LayWithMe",
			"YouHaveIt",
            "JapanJapan",
		};

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

		public List<string> DefaultSongOrder
		{
			get { return m_defaultSongOrder; }
		}

		#endregion

		void Awake()
		{
			// Make sure we are properly initialized and the list has something in it before we start
			if (m_soundClips.Count > 0) {
				m_initialSound = Mathf.Clamp(m_initialSound,0,m_soundClips.Count-1);
//				if (m_initialSound < 0) {
//					m_initialSound = 0;
//				} else if (m_initialSound > m_soundClips.Count - 1) {
//					m_initialSound = m_soundClips.Count;

				m_currentIndex = m_initialSound;
				GetComponent<AudioSource>().clip = m_soundClips[m_currentIndex];
			}
		}

		void Start()
		{
			NotificationCenter.DefaultCenter.AddObserver(this, "PlayCurrentSong");
			NotificationCenter.DefaultCenter.AddObserver(this, "PauseSong");
			PostSongChange(m_soundClips[m_currentIndex].name, m_soundClips[m_currentIndex].length);

			NotificationCenter.DefaultCenter.AddObserver (this, "SetDemoInfo");

				if (m_isMenu) {
					if (passedLoad == false){
						passedLoad = true;
						menuStart();
					} else {
						GameObject.Find ("AdsGameObject").GetComponent<ShowAdOnLoad> ().enabled = true;
					}
					
				} else {
					PlayCurrentSong ();
				}

		}

		void Update(){
			if(UnityAdsHelper.lastAdEnded){
				menuStart();
				UnityAdsHelper.lastAdEnded = false;
			}
		}

		public void menuStart(){
			UpdateDemoInfo (m_soundClips [m_currentIndex].name);
			menuLoop ();
		}

		
		public bool DetectEndOfSong()
		{
			return ! GetComponent<AudioSource> ().isPlaying;
		}
		
		public void SetVolume(float p_vol)
		{
			GetComponent<AudioSource>().volume = p_vol;
		}
		
		public void playAtIndex(int p_index)
		{
//			if (p_index > m_soundClips.Count - 1) {
//				p_index = m_soundClips.Count -1;
//			}
//			if (p_index < 0) {
//				p_index = 0;
//			}
			// Make sure index is not past the index range
			p_index = Mathf.Clamp(p_index,0,m_soundClips.Count-1);
			if (m_soundClips.Count > 0) {
				m_currentIndex = p_index;
				GetComponent<AudioSource> ().clip = m_soundClips [p_index];
				GetComponent<AudioSource> ().Play ();
				PostSongChange(m_soundClips[p_index].name, m_soundClips[p_index].length);
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
			if (p_index > m_soundClips.Count - 1) {
				p_index = m_soundClips.Count - 1;
			}
			m_currentIndex = p_index;
			PostSongChange(m_soundClips[m_currentIndex].name, m_soundClips[m_currentIndex].length);
			return m_soundClips[p_index];
		}
		
		public void NextSong()
		{
			m_currentIndex ++;
			if (m_currentIndex > m_soundClips.Count - 1) {
				m_currentIndex = 0;
			}
			GetComponent<AudioSource>().clip = m_soundClips[m_currentIndex];
			menuStart ();
			PostSongChange(m_soundClips[m_currentIndex].name, m_soundClips[m_currentIndex].length);
			GameMaster.Instance.CurrentSong = m_currentIndex;
		}
		
		public void PrevSong()
		{
			m_currentIndex --;
			if (m_currentIndex < 0) {
				m_currentIndex = m_soundClips.Count - 1;
			}
			GetComponent<AudioSource>().clip = m_soundClips[m_currentIndex];
			menuStart ();
			PostSongChange(m_soundClips[m_currentIndex].name, m_soundClips[m_currentIndex].length);
			GameMaster.Instance.CurrentSong = m_currentIndex;
		}

		// TODO: Find a way to store and recall the appropriate info for each song
		//Default numbers in for testing
		public void menuLoop(){
			GetComponent<AudioSource> ().time = m_currentDemoOffset;
			CancelInvoke ();
			GetComponent<AudioSource> ().Play ();
			Invoke ("StopSong", m_currentDemoDuration);
			Invoke ("menuLoop", m_currentDemoDuration + 1);
		}
		
		public void StopSong()
		{
			GetComponent<AudioSource>().Stop();
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
		
		public void PlayCurrent ()
		{
			GetComponent<AudioSource>().PlayOneShot(m_soundClips[m_currentIndex]);
		}

		public void PostSongChange(string songId, float duration)
		{
			Hashtable messageData = new Hashtable();
			string messageFunction = "SetSong";
			messageData.Add("songid", songId);
			messageData.Add("length", duration);
			NotificationCenter.DefaultCenter.PostNotification(this, messageFunction, messageData);
		}

		//Used for recieving song demo info from TextMachine
		public void SetDemoInfo(NotificationCenter.Notification notification){
			m_currentDemoOffset = (float) notification.data ["demoOffset"];
			//print("Demo offset: " + m_currentDemoOffset); //print for reference
			m_currentDemoDuration = (float) notification.data ["demoDuration"];
			//print("Demo duration: " + m_currentDemoDuration); //print for refrence
		}

		private void UpdateDemoInfo(string songId){
			Hashtable messageData = new Hashtable();
			messageData.Add("songId", songId);
			NotificationCenter.DefaultCenter.PostNotification(this, "GetDemoInfo", messageData);
		}

		[ContextMenu("Sort Songs")]
		private void SortSongs()
		{
			m_soundClips.Sort (CompareSongsByCustomOrder);
		}

	 	private int CompareSongsByCustomOrder(AudioClip x, AudioClip y)
		{
			int xIndex = m_defaultSongOrder.FindIndex (p => p == x.name);
			int yIndex = m_defaultSongOrder.FindIndex (p => p == y.name);
			return Mathf.Clamp(xIndex-yIndex,-1,1);
		}
	}
}