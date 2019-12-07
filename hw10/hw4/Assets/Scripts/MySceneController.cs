using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneController : MonoBehaviour, SceneController, UserAction{
	readonly Vector3 water_pos = new Vector3 (0, 0.5f, 0);
	UserGUI user;
	public CoastController coast_from;
	public CoastController coast_to;
	public BoatController boat;
	private Judge judge;
	int can_move = 0; // 记录是否可以移动对象，1为不可以，0为可以
	private List<CharacterController> team;
	public MySceneActionManager actionManager;

	void Awake(){
		Director director = Director.get_Instance ();
		director.curren = this;
		user = gameObject.AddComponent<UserGUI> () as UserGUI;
		actionManager = gameObject.AddComponent<MySceneActionManager>() as MySceneActionManager; 
		team = new List<CharacterController>();
		loadResources ();
		judge = new Judge (coast_from, coast_to, boat);
	}
	public void loadResources() {
		//加载资源
		GameObject water = Instantiate (Resources.Load ("Prefabs/water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
		water.name = "water";

		coast_from = new CoastController (1);
		coast_to = new CoastController (-1);
		boat = new BoatController (actionManager);
		//创建好牧师与恶魔对象，并设置好唯一的名字
		for (int i = 0; i < 3; i++) {
			CharacterController tem = new CharacterController ("priest", actionManager);
			tem.getCharacterModel().setName ("priest" + i);
			tem.getCharacterModel().setPosition (coast_from.getCoastModel().getEmptyPosition ());
			tem.getOnCoast (coast_from);
			coast_from.getOnCoast (tem);
			team.Add (tem);
		}

		for (int i = 0; i < 3; i++) {
			CharacterController tem = new CharacterController ("devil", actionManager);
			tem.getCharacterModel().setName ("devil" + i);
			tem.getCharacterModel().setPosition (coast_from.getCoastModel().getEmptyPosition ());
			tem.getOnCoast (coast_from);
			coast_from.getOnCoast (tem);
			team.Add (tem);
		}
	}
	public void setMove(int v) {
		can_move = v;
	}
	public void moveboat(){
		//船要有人才能移动
		if (boat.getModel().isEmpty ())
			return;
		boat.boatMove ();
		//每次移动完检查游戏是否已经结束
		UserGUI.outcome = judge.checkGameOver();
	}
	public void isClickChar (CharacterController tem_char){
		//点击的是人物，判断是否移动人物
		if (can_move == 1) //暂停或者是游戏已经结束
			return;
		if (tem_char.getCharacterModel().isOnBoat ()) {
			//在船上就移动到岸上
			CoastController tem_coast;
			if (boat.getModel().getTFflag () == -1) {
				tem_coast = coast_to;
			} else {
				tem_coast = coast_from;
			}
			boat.getOffBoat (tem_char.getCharacterModel().getName ());
			tem_char.moveCharacter (tem_coast.getCoastModel().getMidPosition(), tem_coast.getCoastModel().getEmptyPosition ());
			tem_char.getOnCoast (tem_coast);
			tem_coast.getOnCoast (tem_char);
		} else {
			//在岸上就移动到船上
			CoastController tem_coast2 = tem_char.getCoastController ();
			if (boat.getModel().getEmptyIndex () == -1)
				return;
			if (boat.getModel().getTFflag () != tem_coast2.getCoastModel().getTFflag ())
				return;
			tem_coast2.getOffCoast (tem_char.getCharacterModel().getName());
			tem_char.moveCharacter (tem_coast2.getCoastModel().getMidPosition(), boat.getModel().getEmptyPosition ());
			tem_char.getOnBoat (boat);
			boat.getOnBoat (tem_char);
		}
		//check whether game over;
		UserGUI.outcome = judge.checkGameOver();

	}
	public void restart(){
		//调用各个对象的复位函数
		boat.reset ();
		coast_from.reset ();
		coast_to.reset ();
		foreach (CharacterController i in team) {
			i.reset ();
		}
		can_move = 0;
	}



}
