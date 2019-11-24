using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    Vector3 move = new Vector3(0, 0, .01f);
    int count = 0;
    bool increasing = true;
    void Update()
    {
        if (increasing)
        {
            transform.position += move;
            count++;
            if(count == 180)
            {
                increasing = false;
            }
        }
        else
        {
            transform.position -= move;
            count--;
            if (count == -180)
            {
                increasing = true;
            }
        }
    }
}
