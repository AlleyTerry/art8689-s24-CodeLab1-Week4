using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WhackADot : MonoBehaviour
{
    void OnMouseDown()
    {
        transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        GameManager.instance.Score++;
        
    }
}
