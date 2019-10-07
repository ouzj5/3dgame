using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAction : SSAction, ISSActionCallback {
	public List<SSAction> sequence;    //一系列动作
	public int repeat = -1;            //是否循环的标志，-1表示无限循环
	public int cur = 0;              //当前做的动作的索引

	public static SequenceAction GetSSAcition(int repeat, int start, List<SSAction> sequence) {
		SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>(); //创建实例
		action.repeat = repeat;
		action.sequence = sequence;
		action.cur = start;
		return action;
	}

	public override void Update() {
		if (sequence.Count == 0) return; //没有动作则返回
		if (cur < sequence.Count) {
			sequence[cur].Update();     //调用一系列动作
		}
	}

	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0, string strParam = null, Object objectParam = null) {
		source.destroy = false;          
		cur ++;
		if (cur >= sequence.Count) {
			cur = 0;
			if (repeat > 0) 
				repeat --;
			if (repeat == 0) {
				this.destroy = true;               //组合完成后，删除动作
				this.callback.SSActionEvent(this); //告诉组合动作的管理对象组合做完了
			}
		}
	}

	public override void Start() {
		foreach (SSAction action in sequence) {
			action.gameobject = this.gameobject;
			action.transform = this.transform;
			action.callback = this;                //组合动作的每个小的动作的回调是这个组合动作
			action.Start();
		}
	}

	void OnDestroy() {
		
	}
}