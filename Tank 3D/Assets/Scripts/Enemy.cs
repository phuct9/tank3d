using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject objText;//text show live count

    private int live;
    private TextMesh textMesh;
    private MeshRenderer materials;

    void Start()
    {
        live = Random.Range(1, 5) * 10;
        textMesh = objText.GetComponent<TextMesh>();
        textMesh.text = "" + live;
        materials = GetComponent<MeshRenderer>();
        updateMaterial();
    }

    void Update()
    {
        if (transform.position.z < Camera.main.transform.position.z)
        {
            Destroy(gameObject);
        }
    }

    public void destroy()
    {
        live--;
        if (live <= 0)
        {
            Destroy(gameObject);
            for(int i=0;i<3;i++)
                DestroyEffectPooling.Instance.spawnFromPool("EnemyDestroyItem", transform.position, Quaternion.identity);
        }
        else
        {
            textMesh.text = "" + live;
            updateMaterial();
        }
    }

    /// <summary>
    /// update material theo muc live
    /// </summary>
    private void updateMaterial()
    {
        if (live <= 10)
        {
            materials.material.color = Color.green;
        }
        else if (live <= 20)
        {
            materials.material.color = Color.yellow;
        }
        else
        {
            materials.material.color = Color.red;
        }
    }

}
