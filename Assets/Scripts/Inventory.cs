using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    HashSet<string> keys = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddKey(string id)    => keys.Add(id);
    public bool HasKey(string id)    => keys.Contains(id);
    public void RemoveKey(string id) => keys.Remove(id);
}
