using System.Collections;
using System.Collections.Generic;
using Argentics;
using UnityEngine;

public class LinearProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().Damage();
            Destroy(gameObject);
        }
    }
}
