using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : TruongMonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs;
    [SerializeField] protected Transform holder;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHolder();
        this.LoadPrefabs();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.LogWarning(transform.name + ": LoadHolder", gameObject);
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabObjs = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObjs)
        {
            this.prefabs.Add(prefab);
        }

        this.HidePrefabs();
        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }

    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);

        if (prefab == null)
        {
            Debug.LogError("Prefab not found" + prefabName);
            return null;
        }

        return this.Spawn(prefab, spawnPos, rotation);
    }

    public virtual Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, rotation);

        newPrefab.SetParent(this.holder);

        return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        if (this.poolObjs.Contains(obj)) return;

        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in this.poolObjs)
        {
            if (poolObj == null) continue;

            if (poolObj.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;

        return newPrefab;
    }

    protected virtual Transform GetPrefabByName(string name)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == name) return prefab;
        }

        return null;
    }
}
