using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// This object will act as the Player data model. It will hold all player-centric data and preferences.

namespace BoogieDownGames {

	public class Player : UnitySingletonPersistent<Player> {

		public int bestScore;
		public int coinsTotal;
		public int missCount;
		public int lastCharacterPlayed;
		public int lastSongPlayed;
		public int lastScenePlayed;
		public DateTime lastDatePlayed;
		public List<int> unlockedCharacters;
		public List<int> unlockedSongs;
		public List<int> unlockedScenes;

		private static string saveFileName = "playerInfo.dat";
		private static string saveFilePath = Application.persistentDataPath + "/" + saveFileName;

		[Serializable]
		class PlayerData
		{
			public int bestScore;
			public int coinsTotal;
			public int missCount;
			public int lastCharacterPlayed;
			public int lastSongPlayed;
			public int lastScenePlayed;
			public string lastDatePlayed;
			public List<int> unlockedCharacters;
			public List<int> unlockedSongs;
			public List<int> unlockedScenes;
		}
		
		void Start ()
		{
			bestScore = 0;
			coinsTotal = 0;
			missCount = 0;
			lastCharacterPlayed = 0;
			lastSongPlayed = 0;
			lastScenePlayed = 0;
			lastDatePlayed = DateTime.MinValue;
			unlockedCharacters = new List<int>();
			unlockedSongs = new List<int>();
			unlockedScenes = new List<int>();
			Load ();
		}

		public bool SetBestScore (int newBestScore) {
			bool wasSet = false;
			if (newBestScore > bestScore) {
				bestScore = newBestScore;
				wasSet = true;
			}
			return wasSet;
		}

		public void AddCoins (int coins) {
			coinsTotal += coins;
		}
		
		public bool UnlockCharacter (int characterIndex)
		{
			bool unlocked = false;
			if (unlockedCharacters == null) {
				unlockedCharacters = new List<int>();
			}
			if ( ! unlockedCharacters.Contains (characterIndex)) {
				unlockedCharacters.Add (characterIndex);
				unlocked = true;
			}
			return unlocked;
		}
		
		public bool IsCharacterUnlocked (int characterIndex) {
			bool isUnlocked = false;
			if (unlockedCharacters != null) {
				isUnlocked = unlockedCharacters.Contains (characterIndex);
			}
			return isUnlocked;
		}

		public bool UnlockSong (int songIndex)
		{
			bool unlocked = false;
			if (unlockedSongs == null) {
				unlockedSongs = new List<int>();
			}
			if ( ! unlockedSongs.Contains (songIndex)) {
				unlockedSongs.Add (songIndex);
				unlocked = true;
			}
			return unlocked;
		}

		public bool IsSongUnlocked (int songIndex) {
			bool isUnlocked = false;
			if (unlockedSongs != null) {
				isUnlocked = unlockedSongs.Contains (songIndex);
			}
			return isUnlocked;
		}
		
		public bool UnlockScene (int sceneIndex)
		{
			bool unlocked = false;
			if (unlockedScenes == null) {
				unlockedScenes = new List<int>();
			}
			if ( ! unlockedScenes.Contains (sceneIndex)) {
				unlockedScenes.Add (sceneIndex);
				unlocked = true;
			}
			return unlocked;
		}
		
		public bool IsSceneUnlocked (int sceneIndex) {
			bool isUnlocked = false;
			if (unlockedScenes != null) {
				isUnlocked = unlockedScenes.Contains (sceneIndex);
			}
			return isUnlocked;
		}

		public bool IsFirstPlay () {
			return lastDatePlayed == DateTime.MinValue;
		}

		public void SetLastPlayed (int sceneIndex, int characterIndex, int songIndex) {

			// Use this to save settings when going to play a new game so we remember the game settings (in case game craps out before it ends)

			lastCharacterPlayed = characterIndex;
			lastSongPlayed = songIndex;
			lastScenePlayed = sceneIndex;
			lastDatePlayed = DateTime.Now;
			Save ();
		}
		
		public void SetLastPlayCompleted (int score, int coins, int sceneIndex, int characterIndex, int songIndex) {

			// use this to save a completed game

			SetBestScore (score);
			AddCoins (coins);
			lastCharacterPlayed = characterIndex;
			lastSongPlayed = songIndex;
			lastScenePlayed = sceneIndex;
			lastDatePlayed = DateTime.Now;
			Save ();
		}
		
		public void Load()
		{
			if (File.Exists(saveFilePath)) {
				FileStream file = File.Open(saveFilePath, FileMode.Open);
				if (file != null) {
					BinaryFormatter bf = new BinaryFormatter();
					PlayerData playerData = (PlayerData)bf.Deserialize(file);
					file.Close();
					bestScore = playerData.bestScore;
					coinsTotal = playerData.coinsTotal;
					missCount = playerData.missCount;
					lastCharacterPlayed = playerData.lastCharacterPlayed;
					lastSongPlayed = playerData.lastSongPlayed;
					lastScenePlayed = playerData.lastScenePlayed;
					lastDatePlayed = Convert.ToDateTime(playerData.lastDatePlayed);
					unlockedCharacters = playerData.unlockedCharacters;
					unlockedSongs = playerData.unlockedSongs;
					unlockedScenes = playerData.unlockedScenes;
				}
			}
		}

		public void Save()
		{
			FileStream file = File.Open(saveFilePath, FileMode.OpenOrCreate);
			if (file != null) {
				BinaryFormatter bf = new BinaryFormatter ();
				PlayerData playerData = new PlayerData ();
				playerData.bestScore = bestScore;
				playerData.coinsTotal = coinsTotal;
				playerData.missCount = missCount;
				playerData.lastCharacterPlayed = lastCharacterPlayed;
				playerData.lastSongPlayed = lastSongPlayed;
				playerData.lastScenePlayed = lastScenePlayed;
				playerData.lastDatePlayed = lastDatePlayed.ToString();
				playerData.unlockedCharacters = unlockedCharacters;
				playerData.unlockedSongs = unlockedSongs;
				playerData.unlockedScenes = unlockedScenes;
				bf.Serialize (file, playerData);
				file.Close ();
			}
		}
	}
}