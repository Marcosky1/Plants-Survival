using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false; 
    }

    void Update()
    {

    }

    public void ActivateCollider()
    {
        boxCollider.enabled = true; 
    }
}

