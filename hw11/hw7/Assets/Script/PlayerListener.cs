using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListener : UnitySingleton<PlayerListener>{
	GameObject player;
	int pindex = 0;
	List<GameObject> patrol = new List<GameObject>();
	public PlayerListener () {
		
	}
	public void addPlayer(GameObject player) {
		this.player = player;
	}
	public void addPatrol(GameObject patrol) {
		this.patrol.Add (patrol);
	}
	public void Update() {
		//listen player area index
		int curIndex = SceneModel.Instance ().getAreaIndex (player.transform.position);
		patrol [curIndex].GetComponent<Patrol> ().setChase (true);
		if (pindex != curIndex) {
			UserGUI.Instance ().addScore ();
			patrol [pindex].GetComponent<Patrol> ().setChase (false);
			pindex = curIndex;
		}
	}
}

