using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderupdate : MonoBehaviour
{
    public  SkinnedMeshRenderer meshRenderer;
    public  MeshCollider colliderofObj;
    float timetoRefresh;
    public bool gothit;
    public string opponent;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        colliderofObj = GetComponent<MeshCollider>();
        timetoRefresh = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>timetoRefresh)
        { 

            UpdateCollider();
            timetoRefresh = Time.time + 0.1f;
        }
    }
    

    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        colliderofObj.sharedMesh = null;
        colliderofObj.sharedMesh = colliderMesh;
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == opponent)
        {
            gothit = true;
        }
    }


}
