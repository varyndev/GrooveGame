using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BoogieDownGames {

	public class SongItem {
		public string id;        // unique id that refers to the AudioClip of the sound file
		public string title;     // Full title of the song
		public string artist;    // Song artist attribution
		public string duration;  // length of song in seconds
		public string icon;      // Image asset reference to show (album art)
		public bool paid;        // true indicates this is a locked song can be unlocked by paying for it. false for free songs.
		public float demoOffset; // Offset from start of track to demo track
		public float demoDuration; // number of seconds to play demo song from demoOffset.

		public SongItem (string _id, string _title, string _artist, string _duration, string _icon, bool _paid, float _demoOffset, float _demoDuration) {
			id = _id;
			title = _title;
			artist = _artist;
			duration = _duration;
			icon = _icon;
			paid = _paid;
			demoOffset = _demoOffset;
			demoDuration = _demoDuration;
		}
	}

	// TODO: TextMachine is completely misnamed, this class is only responsible for managing Song display information in the UI.

	public class TextMachine : MonoBehaviour {
		
		[SerializeField]
		Text m_songTitleText;

		[SerializeField]
		Text m_songArtistText;

		[SerializeField]
		Text m_songDurationText;
		
		[SerializeField]
		Image m_songIcon;

		[SerializeField]
		Image m_songLockedIcon;
		
		[SerializeField]
		private List<AudioClip> m_soundClips;

		private List<SongItem> m_songList;


		//TODO: May need to adjust the demoOffset and demoDuration if the song is altered for any reason
		// 		Demo times are listed in seconds
		void Awake () {
			m_songList = new List<SongItem>();
			m_songList.Add (new SongItem ("SheSaid", "She Said", "Dani Feat. Sky Silver", "1:52", "", false, 14.14f, 26.26f));
			m_songList.Add (new SongItem ("FryAndSizzle", "Fry & Sizzle", "Delsa & Sky Silver", "1:47", "", false, 7.332f, 22.319f));
			m_songList.Add (new SongItem ("AloneTonight", "Alone Tonight", "Dani", "2:00", "", false, 4.488f, 22.117f));
			m_songList.Add (new SongItem ("ItsFilth", "It's Filth", "Sky Silver", "1:50", "", false, 12.706f, 35.225f));
			m_songList.Add (new SongItem ("IGotYou", "I Got You", "Sky Girl Jo", "2:06", "", false, 7.50f, 29.142f));
			m_songList.Add (new SongItem ("ImSexyNow", "I'm Sexy Now", "Sky Silver & the Sky Girls", "2:04", "", true, 32.239f, 22.919f));
			m_songList.Add (new SongItem ("ShutUpDance", "Shut Up, Dance", "Delsa Feat. Ai Man", "1:43", "", true, 0.0f, 24.50f));
			m_songList.Add (new SongItem ("InFashion", "In Fashion", "Sky Silver", "1:25", "", true, 7.238f, 14.165f));
			m_songList.Add (new SongItem ("SheSmiles", "She Smiles", "Sky Silver", "2:00", "", true, 34.532f, 31.608f));
			m_songList.Add (new SongItem ("LayWithMe", "Lay With Me", "Sky Girl Jo", "2:51", "", true, 137.2f, 33.5f));
			m_songList.Add (new SongItem ("YouHaveIt", "You Have It", "Tori Martin", "1:29", "", true, 30.0f, 26.826f));

			NotificationCenter.DefaultCenter.AddObserver(this, "SetSong");
			NotificationCenter.DefaultCenter.AddObserver (this, "GetDemoInfo");
			//NotificationCenter.
		}
		
		public void SetSong (NotificationCenter.Notification notification) {
			SetSongInfo ((string)notification.data["songid"], (float)notification.data["length"]);
		}

		public bool IsSongLocked (int songIndex)
		{
			bool isLocked = m_songList [songIndex].paid;
			if (isLocked) {
				// TODO: determine if the player has unlocked this item
				isLocked = ! Player.Instance.IsSongUnlocked(songIndex);
			}
			return isLocked;
		}

		//Used for transfering song demo info to AudioController
		public void GetDemoInfo(NotificationCenter.Notification notification){
			Hashtable messageData = new Hashtable();
			foreach (SongItem song in m_songList) {
				if(song.id == (string)notification.data["songId"]){
					messageData.Add("demoOffset", song.demoOffset);
					messageData.Add("demoDuration", song.demoDuration);
					NotificationCenter.DefaultCenter.PostNotification(this, "SetDemoInfo", messageData);
					break;
				}
			}
		}


		private void SetSongInfo (string songId, float duration) {
			if (songId != null && m_songTitleText != null) {
				int songIndex = 0;
				foreach (SongItem song in m_songList) {
					if (song.id == songId) {
						m_songTitleText.text = song.title;
						m_songArtistText.text = song.artist;
						m_songDurationText.text = ConvertSecondsToMMSS(duration);
						m_songLockedIcon.enabled = IsSongLocked (songIndex);
						break;
					}
					songIndex ++;
				}
			}
		}

		private string ConvertSecondsToMMSS (float duration)
		{
			string result;
			int seconds = (int)Mathf.RoundToInt (duration);
			int minutes = seconds / 60;
			seconds = seconds % 60;

			result = string.Format("{0}:{1:00}", minutes, seconds);
			return result;
		}

	}
}