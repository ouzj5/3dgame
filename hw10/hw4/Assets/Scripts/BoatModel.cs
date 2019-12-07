using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatModel{
	readonly GameObject boat;
	readonly Vector3 fromPos = new Vector3 (5, 1, 0);
	readonly Vector3 toPos = new Vector3 (-5, 1, 0);
	readonly Vector3[] from_pos;  //记录船在from位置时人可以站的位置
	readonly Vector3[] to_pos;    //记录船在to位置时人可以站的位置
	private int TFflag;  //记录船在原(from)位置还是目标(to)位置
	public CharacterController[] passenger = new CharacterController[2];

	public BoatModel(){
		TFflag = 1;
		fromPos = new Vector3 (5, 1, 0);
		toPos = new Vector3 (-5, 1, 0);
		from_pos = new Vector3[]{ new Vector3 (4.5f, 1.5f, 0), new Vector3 (5.5f, 1.5f, 0) };
		to_pos = new Vector3[]{ new Vector3 (-5.5f, 1.5f, 0), new Vector3 (-4.5f, 1.5f, 0) };
		boat = Object.Instantiate (Resources.Load ("Prefabs/Boat", typeof(GameObject)), fromPos, Quaternion.identity, null) as GameObject;
		boat.name = "boat";
		boat.AddComponent (typeof(ClickGUI));
	}
	public Vector3 getToPos() {
		return toPos;
	}
	public Vector3 getFromPos() {
		return fromPos;
	}

	public int getEmptyIndex(){
		for (int i = 0; i < passenger.Length; i++) {
			if (passenger [i] == null)
				return i;
		}
		return -1;
	}
	public bool isEmpty(){
		for (int i = 0; i < passenger.Length; i++) {
			if (passenger [i] != null)
				return false;
		}
		return true;
	}
	public Vector3 getEmptyPosition(){
		Vector3 pos;
		int index = getEmptyIndex ();
		if (TFflag == 1) {
			pos = from_pos [index];
		} else {
			pos = to_pos [index];
		}
		return pos;
	}
	public GameObject getGameObject(){
		return boat;
	}
	public int getTFflag(){
		return TFflag;
	}
	public void setTFflag(int flag) {
		TFflag = flag;
	}
	public void setPassenger(int index, CharacterController ch) {
		passenger [index] = ch;
	}
	public CharacterController getPassenger(string name) {
		for (int i = 0; i < passenger.Length; i++) {
			if (passenger [i] != null && passenger [i].getCharacterModel().getName () == name) {
				CharacterController temp_character = passenger [i];
				passenger [i] = null;
				return temp_character;
			}
		}
		return null;
	}
	public int[] getCharacterNum(){
		int[] count = { 0, 0 };
		for (int i = 0; i < passenger.Length; i++) {
			if (passenger [i] == null)
				continue;
			if (passenger [i].getCharacterModel().getType () == 0) {
				count [0]++;
			} else {
				count [1]++;
			}
		}
		return count;
	}
	public void reset() {
		passenger = new CharacterController[2];
	}

}
