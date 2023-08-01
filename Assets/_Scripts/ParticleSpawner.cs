using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
    }
}
