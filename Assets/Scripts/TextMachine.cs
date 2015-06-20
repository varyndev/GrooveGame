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

		public SongItem (string _id, string _title, string _artist, string _duration, string _icon, bool _paid) {
			id = _id;
			title = _title;
			artist = _artist;
			duration = _duration;
			icon = _icon;
			paid = _paid;
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


		void Awake () {
			m_songList = new List<SongItem>();
			m_songList.Add (new SongItem ("ImSexyNow", "I'm Sexy Now", "Sky Silver & the Sky Girls", "2:00", "", true));
			m_songList.Add (new SongItem ("SheSmiles", "She Smiles", "Sky Silver", "2:00", "", true));
			m_songList.Add (new SongItem ("IGotYou", "I Got You", "Sky Girl Jo", "2:07", "", false));
			m_songList.Add (new SongItem ("AloneTonight", "Alone Tonight", "Dani", "1:30", "", false));
			m_songList.Add (new SongItem ("ItsFilth", "It's Filth", "Sky Silver", "1:44", "", false));
			m_songList.Add (new SongItem ("FryAndSizzle", "Fry & Sizzle", "Delsa & Sky Silver", "1:46", "", false));
			m_songList.Add (new SongItem ("ShutUpAndDance", "Shut Up & Dance", "Delsa Feat. Ai Man", "1:31", "", true));
			m_songList.Add (new SongItem ("SheSaid", "She Said", "Dani Feat. Sky Silver", "1:53", "", false));
			m_songList.Add (new SongItem ("InFashion", "In Fashion", "Sky Silver", "1:33", "", false));
			m_songList.Add (new SongItem ("LayWithMe", "Lay With Me", "Sky Girl Jo", "1:17", "", true));
			m_songList.Add (new SongItem ("YouHaveIt", "You Have It", "Tori Martin", "1:29", "", true));

			NotificationCenter.DefaultCenter.AddObserver(this, "SetSong");
		}
		
		public void SetSong (NotificationCenter.Notification notification) {
			SetSongInfo ((string)notification.data["songid"], (float)notification.data["length"]);
		}

		public bool isSongLocked (int songIndex)
		{
			bool isLocked = m_songList [songIndex].paid;
			if (isLocked) {
				// TODO: determine if the player has unlocked this item
				isLocked = ! Player.Instance.IsSongUnlocked(songIndex);
			}
			return isLocked;
		}

		private void SetSongInfo (string songId, float duration) {
			if (songId != null && m_songTitleText != null) {
				int i = 0;
				bool isLocked;
				foreach (SongItem song in m_songList) {
					if (song.id == songId) {
						m_songTitleText.text = song.title;
						m_songArtistText.text = song.artist;
						m_songDurationText.text = ConvertSecondsToMMSS(duration);
						m_songLockedIcon.enabled = isSongLocked (i);
						break;
					}
					i ++;
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