using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Butler2 : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    public float playerspeed;
    float startTime;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = Point1.position;
        startTime = Time.time+0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            transform.position = Point1.position;
        }
        if (Time.time > startTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point2.position, playerspeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="NextScene")
        {
            SceneManager.LoadScene("Level3_Room2");
        }
    }
}
