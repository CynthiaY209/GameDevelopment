using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUpdate : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;
    private MeshCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = this.GetComponent<SkinnedMeshRenderer>();
        collider = this.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(meshRenderer);
        Debug.Log(collider);
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh); //更新mesh

        collider.sharedMesh = null;
        collider.sharedMesh = colliderMesh; //将新的mesh赋给meshcollider
    }
}
