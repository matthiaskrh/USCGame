using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct PlacementProfile{
    public GameObject prefab;
    public int amount;
    public float minHeightScale;
    public float maxHeightScale;
}


public class TerrainForester : MonoBehaviour
{
    public GameObject navMesh;

    public GameObject terrainObject;
    private Terrain terrain;

    public List<PlacementProfile> profiles;

    public GameObject parentForObjects;
    // Start is called before the first frame update
    void Start()
    {
        terrain = terrainObject.GetComponent<Terrain>();

        float minx = terrainObject.transform.position.x;
        float minz = terrainObject.transform.position.z;
        float maxx = minx + terrain.terrainData.size.x;
        float maxz = minz + terrain.terrainData.size.z;

        foreach(PlacementProfile profile in profiles){
            for(int i = 0; i < profile.amount; i++){
                float randx = Random.Range(minx, maxx);
                float randz = Random.Range(minz, maxz);
                float height = terrain.SampleHeight(new Vector3(randx, 0, randz));
                GameObject obj = GameObject.Instantiate(profile.prefab, new Vector3(randx, height, randz), Quaternion.identity,
                parentForObjects.transform);
                float randHeightScale = Random.Range(profile.minHeightScale, profile.maxHeightScale);
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, randHeightScale, obj.transform.localScale.z);
            }
        }

        // Baking navmesh again
        navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
