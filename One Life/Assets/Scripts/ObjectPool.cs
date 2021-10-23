using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	// ---How to get PooledObject---
   //  GameObject obj = ObjectPool.instance.GetPooledObject("tag");
   //  ----------------------------

   public static ObjectPool instance;
	
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}
	
	public class PooledObjects
	{
		public string tag;
		public GameObject prefab;
		
		public PooledObjects(string _tag, GameObject _prefab)
		{
			tag = _tag;
			prefab = _prefab;
		}
	}
	
	public List<Pool> pools;
	public List<PooledObjects> pooledObjects = new List<PooledObjects>();
	
	void Start()
	{
		for(int i = 0; i < pools.Count; i++)
		{
			for(int e = 0; e < pools[i].size; e++)
			{
				GameObject obj = Instantiate(pools[i].prefab);
				obj.SetActive(false);
				pooledObjects.Add(new PooledObjects(pools[i].tag, obj));
			}
		}
	}
	
	public GameObject GetPooledObject(string _tag)
	{
		for(int i = 0; i < pooledObjects.Count; i++)
		{
			if(!pooledObjects[i].prefab.activeInHierarchy && pooledObjects[i].tag == _tag)
			{
				return pooledObjects[i].prefab;
			}
		}
		
		return null;
	}
	
	void Update()
	{
		
	}
	
	void Awake()
	{
		instance = this;
	}
}
