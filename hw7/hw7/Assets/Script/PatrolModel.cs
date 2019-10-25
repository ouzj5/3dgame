using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolModel {
	GameObject patrol;
	Direction curDir;
	public Vector3 [] move = {new Vector3(-1f, 0, 0), new Vector3(0, 0, 1f), new Vector3(1f, 0, 0), new Vector3(0, 0, -1f)};
	public int index;

	public PatrolModel (GameObject patrol) {
		curDir = (Direction)Random.Range (0, 4);
		this.patrol = patrol;
		index = SceneModel.Instance ().getAreaIndex (patrol.transform.position);
	}
	public Direction genRandomDir() {
		//generagte a random diretcion
		Direction randomDir = (Direction)Random.Range(0, 4);
		while (curDir == randomDir || outOfRange(randomDir)) {
			//cant't be the same dir
			randomDir = (Direction)Random.Range(0, 4);
		}
		return randomDir;
	}
	public bool outOfRange(Direction dir) {
		//judege whther out of range
		return SceneModel.Instance ().getAreaIndex (patrol.transform.position + move [(int)dir]) != index;
	}	
	public Vector3 getNextPos() {
		//get next random pos
		curDir = genRandomDir ();
		return patrol.transform.position + move [(int)curDir];
	}
	public Vector3 getTargetPos() {
		//set the targte to player
		GameObject player = Player.Instance ().gameObject;
		patrol.transform.LookAt(player.transform);
		Vector3 diff = player.transform.position - patrol.transform.position;
		return patrol.transform.position + diff / 4.0f;
	}
	public GameObject getObject() {
		return patrol;
	}
}

