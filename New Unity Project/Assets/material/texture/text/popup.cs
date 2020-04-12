using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{

    public float destroy = 10f;
    public Vector3 Offset = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        Vector3 Offset = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.8f, 1.0f), 0);
      
        transform.localPosition += Offset;
        transform.eulerAngles = new Vector3(0,0 , Random.Range(-30f,30f));
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroy);
    }
}
