using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGUI : MonoBehaviour {

	// Use this for initialization
	UserAction action;
	CharacterController character;

	public void setController(CharacterController tem){
		character = tem;
	}
	void Start(){
		action = Director.get_Instance ().curren as UserAction;
	}
	void OnMouseDown(){
		if (UserGUI.outcome != 0)
			return;
		if (gameObject.name == "boat") {
			action.moveboat ();
		} else {
			action.isClickChar (character);
		}
	}
}
