using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fuel = 100f;
    public int moveValue =0 ;
    private float _moveSpeed = 5f;
    private float minAxis = -4f;
    private float maxAxis = 4f;

    void Start()
    {
        StartCoroutine(DecreaseFuel());
    }

    void Update()
    {
        if (fuel < 0)
        {
            return;
        }

        switch (moveValue)
        {
            case 0:
                break;
            case 1:
                transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
                break;
            case 2:
                transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
                break;
        }

        if (transform.position.x > maxAxis)
        {
            transform.position = new Vector3(maxAxis, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < minAxis)
        {
            transform.position = new Vector3(minAxis, transform.position.y, transform.position.z);
        }
    }

    public void MoveControl(int x)
    {
        moveValue = x;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fuel")
        {
            if (fuel > 0)
            {
                fuel = Mathf.Clamp(fuel + 30f, 0, 100f);
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator DecreaseFuel()
    {
        if (fuel < 0)
        {
            StopAllCoroutines();
            yield break;
        }
        
        fuel -= 10f;
        yield return new WaitForSeconds(1f);
        StartCoroutine(DecreaseFuel());
    }
}
