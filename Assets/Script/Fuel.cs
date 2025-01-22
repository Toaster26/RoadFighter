using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float fallingSpeed;
    private float _destroyTime;
    void Update()
    {
        if (_destroyTime > 10f)
        {
            Destroy(this);
            return;
        }
        _destroyTime += Time.deltaTime;
        transform.position += Vector3.back * fallingSpeed * Time.deltaTime;
    }
}
