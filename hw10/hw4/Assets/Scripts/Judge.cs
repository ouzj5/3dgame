using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge {
	//裁判类
	private CoastController coast_from;
	private CoastController coast_to;
	private BoatController boat;

	public Judge (CoastController from, CoastController to, BoatController b) {
		coast_from = from;
		coast_to = to;
		boat = b;
	}
	public int checkGameOver() {
		//判断游戏是否已经结束

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


