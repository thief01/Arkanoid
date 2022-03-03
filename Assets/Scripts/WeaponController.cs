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

    private void OnDrawGizmosSelected()
    {
        foreach(Transform t in weapons)
        {
            Gizmos.DrawWireSphere(t.position+Vector3.up*spawnOffset, 0.05f);
        }
    }
    public void Shoot()
    {

    }

    public void AddWeapon()
    {

    }
    
}
