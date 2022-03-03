using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private const int SHOTS = 10;

    [SerializeField]
    private Transform[] weapons;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float spawnOffset;

    private int currentShots = 0;

    private void Start()
    {
        GameState.instace.OnLifeChanged += lifes =>
        {
            if(lifes<=0)
            {
                currentShots = 0;
                SetActiveWeapon(false);
            }
        };
    }

    private void OnDrawGizmosSelected()
    {
        foreach(Transform t in weapons)
        {
            Gizmos.DrawWireSphere(t.position+Vector3.up*spawnOffset, 0.05f);
        }
    }
    public void Shoot()
    {
        if(currentShots>0)
        {
            currentShots--;
            foreach(Transform t in weapons)
            {
                GameObject g = Instantiate(bullet);
                g.transform.position = t.position + Vector3.up * spawnOffset;
            }
        }

        if(currentShots<=0)
        {
            SetActiveWeapon(false);
        }
    }

    public void AddWeapon()
    {
        currentShots = SHOTS;
        SetActiveWeapon(true);
    }

    private void SetActiveWeapon(bool active)
    {
        foreach(Transform t in weapons)
        {
            t.gameObject.SetActive(active);
        }
    }
    
}
