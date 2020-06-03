using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BasicBurn : MonoBehaviour
{
    Color TargetSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetSprite = GetComponent<SpriteRenderer>().color;

        TargetSprite.a = TargetSprite.a - 0.0007f;
        GetComponent<SpriteRenderer>().color = TargetSprite;

    }
}
