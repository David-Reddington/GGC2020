using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class BuildingBase : MonoBehaviour
{
    [Range(0, 1000)]
    public uint mOccupancy;

    public GameObject visionLightPrefab;

    [SerializeField]
    string mTag;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject visionLight = Instantiate(visionLightPrefab, new Vector3(0, 0.5f, 0), Quaternion.Euler(90.0f, 0.0f, 0.0f)) as GameObject;
        visionLight.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
