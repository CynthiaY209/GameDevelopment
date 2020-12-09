using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
