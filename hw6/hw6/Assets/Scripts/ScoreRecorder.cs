using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : UnitySingleton<ScoreRecorder>
{
	public int score;                   //分数
	void Start () {
		score = 0;
	}
	//记录分数
	public void AddPoint(int point) {
		score += point;
	}

	public void Reset() {
		score = 0;
	}
}