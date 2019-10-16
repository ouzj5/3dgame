using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskProductor : UnitySingleton<DiskProductor>{

	public static int POOL_SIZE = 20;			//每个飞碟对象池的大小。
	public static int DISK_KIND = 3;
	/* 
	 * 用二维数组存放三种不同的飞碟
	 * 三种飞碟分别有不同的颜色，速度，以及分数
	 *
	*/
	private GameObject[][] diskPool = new GameObject[DISK_KIND][];		
	private GameObject[] diskPool1 = new GameObject[POOL_SIZE];			
	private GameObject[] diskPool2 = new GameObject[POOL_SIZE];
	private GameObject[] diskPool3 = new GameObject[POOL_SIZE];
	/*
	 *三个对象池中的第一个可用的对象，null表示没有空闲的对象
	*/ 
	private GameObject[] firstAvailable = new GameObject[DISK_KIND];
	void Awake() {
		Init();
	}

	void Init() { 
		//初始化对象池
		initPool (diskPool1, "Prefabs/disk1", 0);
		initPool (diskPool2, "Prefabs/disk2", 1);
		initPool (diskPool3, "Prefabs/disk3", 2);
		diskPool [0] = diskPool1;
		diskPool [1] = diskPool2;
		diskPool [2] = diskPool3;
	}
	void initPool(GameObject[] pool, string objpath, int kind) {
		//游戏对象池初始化函数
		for (int i = 0; i < POOL_SIZE; i++) {
			GameObject obj = Instantiate(Resources.Load<GameObject> (objpath));
			obj.SetActive(false);
			pool [i] = obj;

			pool [i].GetComponent<Disk> ().kind = kind;						//标记每个对象为哪种飞碟
			pool [i].GetComponent<Disk> ().poolIndex = i;					//将每个游戏对象在对象池中的索引记录在类中
			if (i == 0) continue;
			pool[i - 1].GetComponent<Disk> ().nextDisk = pool[i];			//记录接着当前飞碟的空闲飞碟
		}
		pool[POOL_SIZE - 1].GetComponent<Disk>().nextDisk = null;
		firstAvailable[kind] = pool[0];
	}

	public Disk Create(Vector3 position, Vector3 velocity, float lifeTime, int kind) {
		//如果池子满了就拿屏幕上的一个来创建。
		if (firstAvailable[kind] == null) firstAvailable[kind] = diskPool[kind][0];

		Disk _Disk = firstAvailable[kind].GetComponent<Disk>();
		_Disk.init(position, velocity, lifeTime);
		firstAvailable[kind].SetActive(true);
		firstAvailable[kind] = _Disk.nextDisk;
		return _Disk;
	}

	public void Return(int _index, int kind) {
		//将飞碟返回到对象池数组中
		diskPool[kind][_index].GetComponent<Disk>().nextDisk = firstAvailable[kind];
		diskPool[kind][_index].SetActive(false);
		firstAvailable[kind] = diskPool[kind][_index];
	}

	public void Reset() {
		//重新初始化对象池
		for (int j = 0; j < DISK_KIND; j++) {
			for (int i = 0; i < POOL_SIZE; i++) {
				diskPool[j][i].SetActive (false);
				if (i == 0)
					continue;
				diskPool[j][i - 1].GetComponent<Disk> ().nextDisk = diskPool[j][i];
			}
			firstAvailable[j] = diskPool[j][0];
		}

	}
}