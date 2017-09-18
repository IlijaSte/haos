using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager {

	[Serializable]
	public class SaveData
	{
		public int totalNumStars;
		public List<int>[] allCollected;

		public SaveData(){
			allCollected = new List<int>[256];
		}
			
	}

	public static void saveGame()
	{

		FileStream fileStream = new FileStream(Application.persistentDataPath + "/save/save.sav", FileMode.Create);

		SaveData saveData = new SaveData();
		saveData.totalNumStars = CollectManager.totalNumCollected;

		for (int i = 0; i < 256; i++) {

			saveData.allCollected[i] = CollectManager.allCollected[i];
		}
		//OVDE SE DODAJE JOS

		XmlSerializer serializer = new XmlSerializer (typeof(SaveData));

		serializer.Serialize (fileStream, saveData);
		fileStream.Close();
	}

	public static void loadGame()
	{

		if (File.Exists (Application.persistentDataPath + "/save/save.sav")) {
			FileStream fileStream = new FileStream (Application.persistentDataPath + "/save/save.sav", FileMode.Open);

			XmlSerializer serializer = new XmlSerializer (typeof(SaveData));
			SaveData saveData = serializer.Deserialize (fileStream) as SaveData;
			fileStream.Close ();


			CollectManager.totalNumCollected = saveData.totalNumStars;
		
			for (int i = 0; i < 256; i++) {
				CollectManager.allCollected [i] = saveData.allCollected [i];
			}
			//OVDE SE DODAJE JOS

		} else {

			for (int i = 0; i < 256; i++) {
				CollectManager.allCollected [i] = new List<int> ();
			}

		}

	}


}