using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum Elements { Fire, Water, Earth, Lightning };

    public enum EnemyState { Idle, Choosing, Moving, Attacking };

    EnemyState mEnemyState = EnemyState.Idle;

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

    [SerializeField]
    private float mAttackRange;

    private GameObject buildingToGo;

    [SerializeField]
    private float mIdleTime = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        mNavAgent = GetComponent<NavMeshAgent>();
        buildingToGo = new GameObject();
    }



    // Update is called once per frame
    void Update()
    {
        switch(mEnemyState)
        {
            case EnemyState.Idle:
                if ((mIdleTime -= Time.fixedDeltaTime) <= 0.0f) mEnemyState = EnemyState.Choosing; mIdleTime = 2.0f;
                break;
            case EnemyState.Choosing:
                if(PickBestBuilding())
                {
                    mEnemyState = EnemyState.Moving;
                }
                else
                {
                    mEnemyState = EnemyState.Choosing;
                }
                break;
            case EnemyState.Moving:
                Movement();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
        }
    }

    protected virtual void Attack() { }

    private void Movement()
    {
        if(NavAgent)
        {
            if(buildingToGo)
            {
                if(NavAgent.hasPath && !NavAgent.isPathStale)
                {
                    NavAgent.SetDestination(buildingToGo.transform.position);
                    mEnemyState = EnemyState.Moving;
                }
                else
                {
                    NavAgent.SetDestination(buildingToGo.transform.position);
                    mEnemyState = EnemyState.Moving;
                }
            }
            if (Vector3.Distance(buildingToGo.transform.position, NavAgent.transform.position) < mAttackRange)
            {
                mEnemyState = EnemyState.Attacking;
            }
            else
            {
                mEnemyState = EnemyState.Moving;
            }
        }
    }

    private bool PickBestBuilding()
    {
        if(!buildingToGo)
        {
            buildingToGo = FindClosestBuilding(transform.position);
            if(buildingToGo)
            {
                return true;
            }
            return false;
        }
        else
        {
            buildingToGo = FindClosestBuilding(transform.position);
            if(buildingToGo)
            {
                return true;
            }
            return false;
        }
    }

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

    public NavMeshAgent NavAgent
    {
        get { return mNavAgent; }
        set { mNavAgent = value; }
    }
}
