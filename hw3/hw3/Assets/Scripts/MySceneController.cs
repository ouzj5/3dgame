using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneController : MonoBehaviour, SceneController, UserAction{
	readonly Vector3 water_pos = new Vector3 (0, 0.5f, 0);
	UserGUI user;
	public CoastController coast_from;
	public CoastController coast_to;
	public BoatController boat;
	private List<CharacterController> team;

	void Awake(){
		Director director = Director.get_Instance ();
		director.curren = this;
		user = gameObject.AddComponent<UserGUI> () as UserGUI;
		team = new List<CharacterController>();
		loadResources ();
	}
	public void loadResources() {
		//加载资源
		GameObject water = Instantiate (Resources.Load ("Prefabs/water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
		water.name = "water";

		coast_from = new CoastController (1);
		coast_to = new CoastController (-1);
		boat = new BoatController ();
		//创建好牧师与恶魔对象，并设置好唯一的名字
		for (int i = 0; i < 3; i++) {
			CharacterController tem = new CharacterController ("priest");
			tem.getCharacterModel().setName ("priest" + i);
			tem.getCharacterModel().setPosition (coast_from.getCoastModel().getEmptyPosition ());
			tem.getOnCoast (coast_from);
			coast_from.getOnCoast (tem);
			team.Add (tem);
		}

		for (int i = 0; i < 3; i++) {
			CharacterController tem = new CharacterController ("devil");
			tem.getCharacterModel().setName ("devil" + i);
			tem.getCharacterModel().setPosition (coast_from.getCoastModel().getEmptyPosition ());
			tem.getOnCoast (coast_from);
			coast_from.getOnCoast (tem);
			team.Add (tem);
		}
	}
	public void moveboat(){
		//船要有人才能移动
		if (boat.getModel().isEmpty ())
			return;
		boat.boatMove ();
		//每次移动完检查游戏是否已经结束
		UserGUI.outcome = checkGameOver();
	}
	public void isClickChar (CharacterController tem_char){
		//点击的是人物，判断是否移动人物
		if (Move.can_move == 1) //暂停或者是游戏已经结束
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
			tem_char.moveToPosition (tem_coast.getCoastModel().getEmptyPosition ());
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
			tem_char.moveToPosition (boat.getModel().getEmptyPosition ());
			tem_char.getOnBoat (boat);
			boat.getOnBoat (tem_char);
		}
		//check whether game over;
		UserGUI.outcome = checkGameOver();
	}
	public void restart(){
		//调用各个对象的复位函数
		boat.reset ();
		coast_from.reset ();
		coast_to.reset ();
		foreach (CharacterController i in team) {
			i.reset ();
		}
		Move.can_move = 0;
	}

	private int checkGameOver(){
		//判断游戏是否已经结束
		if (Move.can_move == 1)
			return 0;
		int from_priest = 0;
		int from_devil = 0;
		int to_priest = 0;
		int to_devil = 0;

		//分别求出两岸边的恶魔和牧师的数量
		int[] from_count = coast_from.getCoastModel().getCharacterNum ();
		from_priest = from_count [0];
		from_devil = from_count [1];

		int[] to_count = coast_to.getCoastModel().getCharacterNum ();
		to_priest = to_count [0];
		to_devil = to_count [1];

		if (to_devil + to_priest == 6)
			//所有的恶魔以及牧师都移动到了另外一边，游戏赢了
			return 1;
		int[] boat_count = boat.getModel().getCharacterNum();
		if (boat.getModel().getTFflag () == 1) {
			//判断输赢是还要把船上的人也计算在内
			from_priest += boat_count [0];
			from_devil += boat_count [1];
		} else {
			to_priest += boat_count [0];
			to_devil += boat_count [1];
		}
		if (from_priest < from_devil && from_priest > 0)
			//右边的恶魔大于牧师，游戏输了
			return -1;
		if(to_priest < to_devil && to_priest > 0)
			//左边的恶魔大于牧师，游戏输了
			return -1;
		
		return 0;//游戏继续
	}

}
