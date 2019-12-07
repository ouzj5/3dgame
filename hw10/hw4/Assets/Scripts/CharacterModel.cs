using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterModel {
	GameObject character;
	int Ctype;
	private bool Onboat;

	public CharacterModel(string name) {
		if(name == "priest"){
			character = Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity,null) as GameObject;
			Ctype = 0;
		}
		else{
			character = Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity,null) as GameObject;
			Ctype = 1;
		}
	}
	public void setName(string name){
		character.name = name;
	}
	public string getName(){
		return character.name;
	}
	public void setPosition(Vector3 postion){
		character.transform.position = postion;
	}
	public int getType(){
		return Ctype;
	}
	public void setObjectParent(Transform objtransform) {
		character.transform.parent = objtransform;
	}
	public void setState(bool state) {
		Onboat = state;
	}
	public GameObject getObject() {
		return character;
	}
	public bool isOnBoat(){
		return Onboat;
	}
}