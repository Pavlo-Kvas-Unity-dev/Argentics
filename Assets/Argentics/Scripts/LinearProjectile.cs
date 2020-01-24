using System.Collections;
using System.Collections.Generic;
using Argentics;
using UnityEngine;
    
[RequireComponent(typeof(Rigidbody))]
public class LinearProjectile : MonoBehaviour
{
    [SerializeField] private float destroyDistance = 20f;
    [SerializeField] private float speed = 1f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        if (Vector3.Distance(startPos, transform.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().Damage();
        }

        Destroy(gameObject);
    }
}
