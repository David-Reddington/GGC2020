using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum Elements { Fire, Water, Earth, Lightnight };

    [SerializeField]
    private string mEnemyName;

    [SerializeField]
    private uint mID;

    public uint ID
    {
        get { return mID; }
        set { mID = value; }
    }

    [SerializeField]
    private NavMeshAgent mNavAgent;

    [SerializeField]
    private uint mMovementSpeed;

    [SerializeField]
    private uint mBuildingDamage;

    [SerializeField]
    private uint mUnitDamage;

    [SerializeField]
    private float mAttackRate;



    // Start is called before the first frame update
    void Start()
    {
        mNavAgent = GetComponent<NavMeshAgent>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Attack() { }

    protected virtual void Movement() { }

    public GameObject FindClosestBuilding(Vector3 target)
    {
        GameObject closest = null;
        float closestDist = Mathf.Infinity;
        int index = 0;
        foreach (var b in FindObjectsOfType<BuildingBase>())
        {
            var dist = Vector3.Distance(transform.position, b.transform.position);
            if (dist < closestDist)
            {
                closest = b.gameObject;
                closestDist = dist;
            }
            ++index;
        }
        if (!closest)
        {
            return closest;
        }
        return null;
    }
}
