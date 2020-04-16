using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryScript : MonoBehaviour
{
    public GameObject obj;

    private void Awake()
    {
        obj = this.gameObject;
        DontDestroyOnLoad(obj);

    }

    public void destory()
    {
        Destroy(obj);
    }
}
