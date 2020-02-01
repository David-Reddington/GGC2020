using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public enum EHealthState { Healthy, Injured, Severe, Dead };
    EHealthState mHealthState = EHealthState.Healthy;
    protected int mHealth = 100;
    protected int mMaxHealth = 100;
    protected int mMinHealth = 0;


    public EHealthState HealthState
    {
        get { return mHealthState; }
        set { mHealthState = value; }
    }

    public int Health
    {
        get { return mHealth; }
        set { mHealth = value; }
    }

    public void AddHealth(int h)
    {
        Health += h;
        Health = Mathf.Clamp(Health, mMinHealth, mMaxHealth);
        if (Health >= 75 && Health <= mMaxHealth)
        {
            HealthState = EHealthState.Healthy;
        }
        if (Health <= 74 && Health >= 40)
        {
            HealthState = EHealthState.Injured;
        }
        if (Health <= 39 && Health >= 1)
        {
            HealthState = EHealthState.Severe;
        }
    }

    public void Damage(int d)
    {
        if (Health > 0)
        {
            Health -= d;
            if (Health >= 75 && Health <= mMaxHealth)
            {
                HealthState = EHealthState.Healthy;
            }
            if (Health <= 74 && Health >= 40)
            {
                HealthState = EHealthState.Injured;
            }
            if (Health <= 39 && Health >= 1)
            {
                HealthState = EHealthState.Severe;
            }
            if (Health <= 0)
            {
                StartCoroutine(Die());
            }
        }
        else if (Health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        HealthState = EHealthState.Dead;
        GetComponent<Animator>().SetBool("IsDead", true);
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
