using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombPhysic : MonoBehaviour
{
    BasePlayer PlayerClass;

    public float BombPower =100;
    public float Radius = 100;

    public GameObject CurrentOBject;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        GetComponent<Transform>().localScale = new Vector3(4, 4, 4);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        CurrentOBject = other.gameObject;


        if (other.tag == "Ground")
        {
            return;
        }

        if (other.tag == "Foliage")
        {
            Destroy(other.gameObject);
        }

        //Debug.Log(other);
        if (other.tag == "Player")
        {
            // Does the ray intersect any objects excluding the player layer

            PlayerClass = other.GetComponent<BasePlayer>();
            if (PlayerClass.Death == true)
            {
                return;
            }

            if (Physics.Linecast(transform.position, other.transform.position))
            {
                PlayerClass.SetHealth(-1);
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }

        if (other.GetComponent<BaseObject>() != null)
        {
            BaseObject bo = other.GetComponent<BaseObject>();
            bo.EffectedByExplosion = true;
            bo.indexCounter++;
        }

        Rigidbody PhysicalObject = other.GetComponent<Rigidbody>();

        PhysicalObject.AddExplosionForce(BombPower, transform.position, Radius, 1, ForceMode.Impulse);



    }

}
