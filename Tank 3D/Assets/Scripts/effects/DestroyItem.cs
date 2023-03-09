using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour,IDestroy
{
    public float upForce = 1f;
    public float sideForce = 0.1f;
    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(-upForce, upForce);
        float zForce = Random.Range(-sideForce,sideForce);
        Vector3 force = new Vector3(xForce, yForce, zForce);
        GetComponent<Rigidbody>().velocity = force;
        StartCoroutine(delay());
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }



}
