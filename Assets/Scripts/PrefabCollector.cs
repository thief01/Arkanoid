using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCollector<T> where T : MonoBehaviour
{
    private readonly string WARRNING_MESSAGE = $"The sketch is null for: {typeof(T)}";
    public static PrefabCollector<T> Instance
    {
        get
        {
            if (instance == null)
                instance = new PrefabCollector<T>();
            return instance;
        }
    }

    private static PrefabCollector<T> instance;
    private class Prefab
    {
        public bool isUsing;
        public T prefab;

        private bool coroutineIsActive = false;

        public void Destroy(float time)
        {
            if(!coroutineIsActive)
                prefab.StartCoroutine(Timer(time));
        }

        private IEnumerator Timer(float time)
        {
            coroutineIsActive = true;
            yield return new WaitForSeconds(time);
            prefab.gameObject.SetActive(false);
            isUsing = false;
            coroutineIsActive = false;
        }
    }

    private List<Prefab> prefabs = new List<Prefab>();
    private T sketch;

    public void SetSketch(T sketch)
    {
        this.sketch = sketch;
    }

    public void SetObjectActive(bool active, T objectRef)
    {
        prefabs.Find(ctg => ctg.prefab == objectRef).isUsing = active;
        objectRef.gameObject.SetActive(!active);
    }

    public T GetFreePrefab()
    {
        if(sketch==null)
        {
            Debug.LogWarning(WARRNING_MESSAGE);
            return null;
        }

        T prefab = TryGetFreePrefab();
        if (prefab == null)
        {
            prefab = InitNewPrefab();
        }
        return prefab;
    }

    public void Destroy(T objectRef, float time = 0)
    {
        Prefab p = prefabs.Find(ctg => ctg.prefab == objectRef);
        if (time <= 0)
        {
            p.isUsing = false;
            p.prefab.gameObject.SetActive(false);
        }
        else
        {
            p.Destroy(time);
        }
    }

    private T TryGetFreePrefab()
    {
        foreach (Prefab p in prefabs)
        {
            if (!p.isUsing)
            {
                p.isUsing = true;
                return p.prefab;
            }
        }

        return null;
    }

    private T InitNewPrefab()
    {
        T g = GameObject.Instantiate(sketch.gameObject).GetComponent<T>();
        prefabs.Add(new Prefab() { isUsing = true, prefab = g });
        return g;
    }
}
