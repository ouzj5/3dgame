using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoastController{
	CoastModel model;

	public CoastController(int side){
		model = new CoastModel();
		model.CreateCoast (side);
	}
	public void getOnCoast(CharacterController cha){
		int index = model.getEmptyIndex();
		model.setCharacter (index, cha);
	}
	public void getOffCoast(string name) {
		int index = model.getIndexByName (name);
		model.setCharacter (index, null);
	}
	public void reset() {
		model.reset ();
	}
	public CoastModel getCoastModel() {
		return model;
	}
}