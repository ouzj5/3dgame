using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PatrolFactory : UnitySingleton<PatrolFactory>{
	//patrol init position
	private Vector3[] pos = new Vector3[] { new Vector3(-6, 0, 16), new Vector3(-1, 0, 19),
		new Vector3(6, 0, 16), new Vector3(-5, 0, 7), new Vector3(0, 0, 7), new Vector3(6, 0, 7)};
	GameObject[] patrolList = new GameObject[6];
	GameObject player;
	public PatrolFactory() {
		
	}
	public void load() {
		//load and init the player
		GameObject img = GameObject.Find("ImageTarget");
		player = Instantiate(Resources.Load<GameObject> ("Prefabs/Player"));
		player.transform.position =new Vector3(-8, 0, 20);
		player.name = "Player";
		player.AddComponent<Player> ();								//add the script
		player.AddComponent<PlayerGUI> ();							//add click response
		PlayerListener.Instance ().addPlayer(player);				//add listener

		for (int i = 0; i < 6; i++) {
			patrolList [i] = Instantiate (Resources.Load<GameObject> ("Prefabs/Patrol"));
			patrolList [i].transform.position = pos [i];
			patrolList [i].name = "Patrol" + i;
			patrolList [i].AddComponent<Patrol> ();					//add the script
			PlayerListener.Instance ().addPatrol(patrolList[i]);	//add listener

		}	
	}
	public void restart() {
		//restart by init object position
		player.transform.position =new Vector3(-8, 0, 20);
		for (int i = 0; i < 6; i++) {
			patrolList [i].transform.position = pos [i];
			patrolList [i].GetComponent<Patrol> ().send();
			print(patrolList [i].GetComponent<Patrol> ().done);
		}
	}
}

