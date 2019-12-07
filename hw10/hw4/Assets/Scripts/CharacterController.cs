using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController{
	ClickGUI clickgui;
	private CoastController coastcontroller;
	MySceneActionManager actionManager;
	private CharacterModel charactermodel;

	public CharacterController(string Myname, MySceneActionManager am){
		actionManager = am;
		charactermodel = new CharacterModel (Myname);
		clickgui = charactermodel.getObject().AddComponent(typeof(ClickGUI)) as ClickGUI;
		clickgui.setController(this);
	}


	public void moveCharacter(Vector3 middle, Vector3 dest){
		//move.SetDestination (dest);
		actionManager.moveCharacter(charactermodel.getObject(), middle, dest, 25);
	}
	public void getOnBoat(BoatController boat){
		coastcontroller = null;
		charactermodel.setObjectParent (boat.getGameObject ().transform);
		charactermodel.setState (true);
	}
	public void getOnCoast(CoastController coast_to){
		coastcontroller = coast_to;
		charactermodel.setObjectParent (null);
		charactermodel.setState (false);
	}
	public CharacterModel getCharacterModel() {
		return charactermodel;
	}
	public CoastController getCoastController(){
		return coastcontroller;
	}
	public void reset(){
		coastcontroller = (Director.get_Instance ().curren as MySceneController).coast_from;
		getOnCoast(coastcontroller);
		charactermodel.setPosition (coastcontroller.getCoastModel().getEmptyPosition ());
		coastcontroller.getOnCoast (this);
	}
	public bool isName(string objectname) {
		return charactermodel.getName () == objectname;
	}
	public bool isType(int type) {
		return charactermodel.getType () == type;
	}
}