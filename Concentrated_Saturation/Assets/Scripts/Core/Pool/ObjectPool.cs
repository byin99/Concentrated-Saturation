using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : RecycleObject
{ 
    public GameObject prefab;

    public int poolSize = 64;

    T[] pool;

    Queue<T> readyQueue;

    public virtual void Initialize()
    {
        if (pool == null)
        {
            pool = new T[poolSize];
            readyQueue = new Queue<T>(poolSize);    

            GenerateObjects(0, poolSize, pool);
        }
        else
        {
            foreach (T obj in pool)
            {
                obj.gameObject.SetActive(false); 
            }
        }
    }

    void GenerateObjects(int start, int end, T[] result)
    {
        for (int i = start; i < end; i++)
        {
            GameObject obj = Instantiate(prefab, transform);   
            obj.name = $"{prefab.name}_{i}";   

            T comp = obj.GetComponent<T>();
            comp.onDisable += () =>
            {
                readyQueue.Enqueue(comp);   
            };
            OnGenerateObject(comp);

            result[i] = comp;       
            obj.SetActive(false);   
        }
    }

    
    protected virtual void OnGenerateObject(T comp)
    {
    }

    
    public T GetObject(Vector3? position = null, Vector3? eulerAngle = null)
    {
        if (readyQueue.Count > 0)
        {
            T comp = readyQueue.Dequeue();          
            comp.gameObject.SetActive(true);       
            comp.transform.position = position.GetValueOrDefault();                    
            comp.transform.rotation = Quaternion.Euler(eulerAngle.GetValueOrDefault());
            return comp;   
        }
        else
        {
            ExpandPool();                           
            return GetObject(position, eulerAngle); 
        }

    }

   
    void ExpandPool()
    {
       
        Debug.LogWarning($"{gameObject.name} 풀 사이즈 증가. {poolSize} -> {poolSize * 2}");

        int newSize = poolSize * 2;         
        T[] newPool = new T[newSize];       
        for (int i = 0; i < poolSize; i++)
        {
            newPool[i] = pool[i];           
        }

        GenerateObjects(poolSize, newSize, newPool);    

        pool = newPool;     
        poolSize = newSize;
    }

}
