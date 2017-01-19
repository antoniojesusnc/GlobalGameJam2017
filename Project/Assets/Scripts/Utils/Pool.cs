using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    class PoolInfo
    {
        public PoolInfo()
        {
            index = 0;
            objects = new List<GameObject>();
        }
        public int index;
        public List<GameObject> objects;
    } // PoolInfo

    Dictionary<GameObject, PoolInfo> _pools;
	// Use this for initialization
	void Start () {
        _pools = new Dictionary<GameObject, PoolInfo>();
    }
	
    public void createPool(GameObject original, int amount)
    {
        GameObject temp;
        PoolInfo info = new PoolInfo();
        for (int i = 0; i < amount; i++)
        {
            temp = Instantiate(original, transform);
            temp.gameObject.SetActive(false);
            info.objects.Add(temp);
        }
    } // createPool

    public void expandPool(GameObject original, int amount)
    {
        GameObject temp;
        PoolInfo info = _pools[original];
        for (int i = 0; i < amount; i++)
        {
            temp = Instantiate(original, transform);
            info.objects.Add(temp);
        }
    } // expandPool


    public GameObject GetObject(GameObject original, bool expandIfAllBussy = false)
    {
        PoolInfo info = _pools[original];
        if (info.objects[info.index].activeSelf)
        {
            int numberObj = info.objects.Count;
            int testIndex; 
            for (int i = 0; i < numberObj; ++i)
            {
                testIndex = (info.index + i)%numberObj;
                if (!info.objects[info.index].activeSelf){
                    info.index = testIndex;
                    return info.objects[info.index];
                }
            }

        }
        return null;
    } // GetObject

    public void Free(GameObject obj)
    {
        obj.SetActive(false);
    }
}
