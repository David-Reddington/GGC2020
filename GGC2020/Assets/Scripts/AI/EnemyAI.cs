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
}
