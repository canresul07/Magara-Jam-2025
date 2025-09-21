using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    HashSet<string> keys = new HashSet<string>();   // benzersiz anahtar ID'leri

    void Awake() {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddKey(string id)      { keys.Add(id); }
    public int  KeyCount               => keys.Count;
    public bool HasAtLeast(int need)   => keys.Count >= need;
}
