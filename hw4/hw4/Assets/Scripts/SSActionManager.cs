using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback                      //action管理器 
{
	private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();    //将执行的动作的字典集合
	private List<SSAction> actionQueue = new List<SSAction>();                       //等待去执行的动作列表
	private List<int> destroyQueue = new List<int>();                              //等待删除的动作的key                

	protected void Update() {
		foreach (SSAction ac in actionQueue) {
			//存放动作
			actions[ac.GetInstanceID()] = ac;                                     
		}
		actionQueue.Clear();

		foreach (KeyValuePair<int, SSAction> kv in actions)	{  //判断动作是否需要销毁
			SSAction ac = kv.Value;
			if (ac.destroy) {
				destroyQueue.Add(ac.GetInstanceID());
			}
			else if (ac.enable) {
				ac.Update();
			}
		}

		foreach (int key in destroyQueue) {  //销毁完成后的动作
			SSAction ac = actions[key];
			actions.Remove(key);
			DestroyObject(ac);
		}
		destroyQueue.Clear();
	}

	public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager) {
		action.gameobject = gameobject;
		action.transform = gameobject.transform;
		action.callback = manager;
		actionQueue.Add(action);
		action.Start();
	}

	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0, string strParam = null, Object objectParam = null) {
		//游戏对象移动完成后没有的动作
	}
}