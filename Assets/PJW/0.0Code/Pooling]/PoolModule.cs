using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PoolModule : MonoBehaviour
{
    public Stack<GameObject> pool = new();

    public int Count { get;private set;  }

    public GameObject original;

    public Transform SpawnPos {  get; private set; }

    protected virtual void Init()
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject prefab = Instantiate(original,transform.position,quaternion.identity);
            pool.Push(prefab);
            prefab.SetActive(false);
        }
    }

    public virtual void PopPool()
    {
        GameObject prefab;

        if (pool.Count > 0)
        {
            prefab = pool.Pop();
            prefab.SetActive(true);
        }
        else
        {
            prefab = Instantiate(original, transform.position, Quaternion.identity);
            pool.Push(prefab);
        }

        prefab.transform.position = SpawnPos.position;
    }
}
