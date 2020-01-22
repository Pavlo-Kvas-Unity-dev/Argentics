using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxColliderVisualizer : MonoBehaviour
{
    public Color highlightColor = Color.green;


    public void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;

        Gizmos.color = highlightColor;

        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);

        Gizmos.color = prevColor;
    }

}
