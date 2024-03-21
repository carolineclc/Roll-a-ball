using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{

    public float speed = 1.5f;
    public float limit=75f;
    public bool randomStart = false;

    public bool inverted = false;
    private float random = 0;


    void Awake(){
        if (randomStart)
        random = Random.Range(0f,1f);

    }


    void Update()
    {

        if (inverted){
        float angle = limit * Mathf.Sin(Time.time * speed);
        transform.localRotation = Quaternion.Euler(0,0,angle);
        }
        else{
        float angle = limit * Mathf.Sin(Time.time + 1f * speed);
        transform.localRotation = Quaternion.Euler(0,0,angle);            
        }

    }
}
