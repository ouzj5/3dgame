using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGUI : MonoBehaviour {

	// Use this for initialization
	Disk disk;

	public void setDisk(Disk disk){
		this.disk = disk;
	}
	void OnMouseDown(){
		ScoreRecorder.Instance ().AddPoint (disk.kind + 1);
		disk.ReturnToPool ();
	}
}
