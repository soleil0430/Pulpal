using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jet : MonoBehaviour
{
    public float MoveSpeed;

    public AudioClip jetSOund;

    public int Direction;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = jetSOund;
        audio.Play();

        if (Direction == 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        else if (Direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (Direction == 0)
        {
            transform.Translate(-(Vector3.forward) * MoveSpeed * Time.deltaTime);
        }

        else if (Direction == 1)
        {
            transform.Translate(-(Vector3.forward) * MoveSpeed * Time.deltaTime);
        }
    }
}
