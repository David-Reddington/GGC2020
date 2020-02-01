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

    private uint mSpawnID;
    public List<uint> mSpawnIDList;

    private bool mWaveSpawn = false;
    public bool mSpawn = true;

    public float mWaveTimer;
    private float mTimeTillWave = 0.0f;


    public int mTotalWaves;
    private int mNumWaves = 0;

    private GameObject mTerrain;

    GameObject[] mSpawnPoints;

    public Vector3 centre;
    public Vector3 size;

    AISpawn mAISpawner;
    EnemyAI mEnemyAI;

    ParticleSystem mParticleSystem;

    float mParticleTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        mAISpawner = GetComponent(typeof(AISpawn)) as AISpawn;
        mTerrain = FindObjectOfType<Terrain>().gameObject;
        mSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnArea");
    }

    // Update is called once per frame
    void Update()
    {
        if(mSpawn)
        {
            if(mNumWaves < mTotalWaves + 1)
            {
                if(mWaveSpawn)
                {
                    SpawnEnemy();
                }
                if(mNumEnemies == 0)
                {
                    ++mNumWaves;
                    mWaveSpawn = true;
                    mTotalEnemies += mNumEnemiesMod;
                }
                if(mNumEnemies == mTotalEnemies)
                {
                    mWaveSpawn = false;
                }
            }
            else
            {
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Go To End Screen
            }
        }
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
        var b = mSpawnPoints[Random.Range(0, mSpawnPoints.Length)];
        Vector3 point = new Vector3(b.transform.position.x, mTerrain.transform.position.y + 0.5f, b.transform.position.z);
        GameObject e = Instantiate(mEnemies[mEnemyType], point, Quaternion.identity);
        PlaySpawnEffect(e);
        SetID();
        mAISpawner.SetName(mSpawnID);
        mEnemyAI = e.GetComponent<EnemyAI>();
        mEnemyAI.ID = mSpawnID;
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
        mSpawnID = (uint)Random.Range(1, 1000);
        if (!mSpawnIDList.Contains(mSpawnID))
        {
            mSpawnIDList.Add(mSpawnID);
        }
        else
        {
            SetID();
        }
    }

    public void KillEnemy(uint sID)
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

    public void EnableSpawner(uint sID)
    {
        if (mSpawnID == sID)
        {
            mSpawn = true;
        }
    }

    public void DisableSpawner(uint sID)
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

    public uint GetSpawnID
    {
        get { return mSpawnID; }
    }
}
