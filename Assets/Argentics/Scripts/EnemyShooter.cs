using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform BarrelEnd;
    
    private float? lastShotTime;
    private float reloadTime = 2f;
    private Transform targetTransform;

    void Awake()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Assert.IsNotNull(targetTransform);
    }

    void Start()
    {
        FireShot();
    }

    void FixedUpdate()
    {
        var forward = BarrelEnd.TransformDirection(Vector3.forward);

        Vector3 toTarget = targetTransform.position - transform.position;

        if (Vector3.Dot(toTarget, forward) < 0)
        {
            TurnAround();
        }
         
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

    [ContextMenu("Turn Around")]
    void TurnAround()
    {
        transform.Rotate(Vector3.up*180);
    }
}
