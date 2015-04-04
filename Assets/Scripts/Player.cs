using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoogieDownGames {

	public class Player : UnitySingletonPersistent<Player> {

		[SerializeField]
		public int m_currentScore;

		[SerializeField]
		public int m_misses; //Keep track of the misses 

		public int m_maxMisses; 

		// Use this for initialization
		void Start ()
		{
		
		}
		
		// Update is called once per frame
		void Update () 
		{
		
		}

		public void load()
		{
			if(File.Exists( Application.persistentDataPath + "/playerInfo.dat")) {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
				PlayerData data = (PlayerData)bf.Deserialize(file);
				file.Close();
				m_currentScore = data.m_lastScore;

			}
		}

		public void save()
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = new PlayerData();
			//Save data here
			data.m_highScore = 100;
			data.m_lastScore = 10;
			bf.Serialize(file,data);
			file.Close();
		}
	}

	[Serializable]
 	class PlayerData
	{
		public int m_lastScore;
		public int m_highScore;

	}

	[Serializable]
	class PlayerPrefs
	{

	}
}