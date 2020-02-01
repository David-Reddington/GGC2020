using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Color gizmosColour = Color.red;

    public enum EEnemyType
    {
        Knight,
        Golem,
        Goblin,
        Wizard
    }

    EEnemyType mEnemyType = EEnemyType.Knight;

    public GameObject mKnightPrefab;
    public GameObject mGolemPrefab;
    public GameObject mGoblinPrefab;
    public GameObject mWizardPrefab;

    private Dictionary<EEnemyType, GameObject> mEnemies = new Dictionary<EEnemyType, GameObject>(4);

    public int mTotalEnemies = 10;
    private int mNumEnemies = 0;
    private int mSpawnedEnemies = 0;
    private int mNumEnemiesMod = 12;

    private int mSpawnID;
    public List<int> mSpawnIDList;

    private bool mWaveSpawn = false;
    public bool mSpawn = true;

    public float mWaveTimer;
    private float mTimeTillWave = 0.0f;


    public int mTotalWaves;
    private int mNumWaves = 0;

    private GameObject mFloor;

    public Vector3 centre;
    public Vector3 size;

    AISpawn m_AISpawner;
    //AIController m_AIController;

    ParticleSystem mParticleSystem;

    float mParticleTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmosColour;
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
        Gizmos.DrawWireCube(centre, size);
    }

    private void SpawnEnemy()
    {
        ++mNumEnemies;
        ++mSpawnedEnemies;
        Vector3 point = new Vector3(Random.Range(-47.5f, 47.5f), -2.75f, Random.Range(-47.5f, 47.5f));
        GameObject e = Instantiate(mEnemies[mEnemyType], point, Quaternion.identity);
        PlaySpawnEffect(e);
        SetID();
        m_AISpawner.SetName(mSpawnID);
        //m_AIController = e.GetComponent<AIController>();
        //m_AIController.SID = mSpawnID;
    }

    void PlaySpawnEffect(GameObject e)
    {
        mParticleSystem.transform.position = e.transform.position;
        while ((mParticleTimer -= (Time.deltaTime * 2)) > 0)
        {
            mParticleSystem.Play();
        }
    }

    void StopSpawnEffect()
    {
        mParticleSystem.Stop();
    }

    public void SetID()
    {
        mSpawnID = Random.Range(1, 1000);
        if (!mSpawnIDList.Contains(mSpawnID))
        {
            mSpawnIDList.Add(mSpawnID);
        }
        else
        {
            SetID();
        }
    }

    public void KillEnemy(int sID)
    {
        for (var i = 0; i < mSpawnIDList.Count; ++i)
        {
            if (sID == mSpawnIDList[i])
            {
                --mNumEnemies;
                mSpawnIDList.Remove(sID);
            }
        }
    }

    public void EnableSpawner(int sID)
    {
        if (mSpawnID == sID)
        {
            mSpawn = true;
        }
    }

    public void DisableSpawner(int sID)
    {
        if (mSpawnID == sID)
        {
            mSpawn = false;
        }
    }

    public float TimeTillWave
    {
        get { return mTimeTillWave; }
        set { mTimeTillWave = value; }
    }

    public void EnableTrigger()
    {
        mSpawn = true;
    }

    public int GetSpawnID
    {
        get { return mSpawnID; }
    }
}
