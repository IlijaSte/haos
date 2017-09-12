using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager {

	//private ArrayList classList = new ArrayList();


	public static ArrayList dataRef = new ArrayList();

	[Serializable]
	public class SaveData
	{
		public ArrayList data = new ArrayList();

		public SaveData(){
			data.Add(CollectDetector.totalNumCollected);
		}
			
	}

	public static void saveGame()
	{
		BinaryFormatter bF = new BinaryFormatter();
		dataRef.Clear ();
		dataRef.Add (CollectDetector.totalNumCollected);
		//OVDE SE UBACUJU OSTALI PODACI

		FileStream fileStream = new FileStream(Application.persistentDataPath + "/save/save.sav", FileMode.Create);

		SaveData saveData = new SaveData();


		bF.Serialize(fileStream, saveData);
		fileStream.Close();
	}

	public static void loadGame()
	{
		BinaryFormatter bF = new BinaryFormatter();
		if (File.Exists(Application.persistentDataPath + "/save/save.sav")){
			FileStream fileStream = new FileStream(Application.persistentDataPath + "/save/save.sav", FileMode.Open);

			SaveData saveData = bF.Deserialize(fileStream) as SaveData;
			fileStream.Close();

			if(dataRef.Count == 0)
				dataRef.Add (CollectDetector.totalNumCollected);

			for (int i = 0; i < dataRef.Count; i++) {

				dataRef[i] = saveData.data [i];
			}

		}

	}


}