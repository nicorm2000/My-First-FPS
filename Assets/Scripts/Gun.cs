using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform camera;

    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;

    private void Awake()
    {
        camera = Camera.main.transform;
    }

    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, range))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
