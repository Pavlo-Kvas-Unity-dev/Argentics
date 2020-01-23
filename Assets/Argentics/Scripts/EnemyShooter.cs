using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform BarrelEnd;
    
    private float? lastShotTime;
    private float reloadTime = 2f;

    void Start()
    {
        FireShot();
    }

    void FixedUpdate()
    {
        FireShot();
    }

    private void FireShot()
    {
        if (lastShotTime == null || Time.time - lastShotTime > reloadTime)
        {
            lastShotTime = Time.time;
            Instantiate(shotPrefab, BarrelEnd.position, BarrelEnd.rotation);
        }
            
    }
}
