using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueLineController : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer lr;

    [SerializeField] private Transform tongueTrnasform;
    [SerializeField] private Transform[] points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }


    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        lr.startColor = Color.red;
        lr.endColor = Color.red;

        Vector3[] positions = new Vector3[2];
        positions[0] = this.transform.parent.position;
        positions[1] = tongueTrnasform.position;
        lr.positionCount = positions.Length;


        lr.SetPositions(positions);




    }
}
