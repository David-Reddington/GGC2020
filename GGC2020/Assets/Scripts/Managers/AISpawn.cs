using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawn : MonoBehaviour
{
    private GameObject obj;
    public List<int> SpawnerID;

    // Use this for initialization
    void Start()
    {
        SpawnerID = new List<int>();
        obj = GameObject.FindWithTag("AISpawner");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Remove()
    {
        obj.BroadcastMessage("KillEnemy", SpawnerID);
    }

    public void SetName(int sName)
    {
        SpawnerID.Add(sName);
    }
}
