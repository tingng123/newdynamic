using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonScript : MonoBehaviour
{
    public void BodyBreakDown(GameObject[] bodypart, Vector3 position)
    {

        for (int i = 0; i < bodypart.Length; i++)
        {
            GameObject temp = Instantiate(bodypart[i], position, Quaternion.identity);
            Destroy(temp, 5f);
        }
    }

}
