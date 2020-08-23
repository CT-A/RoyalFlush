using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapHandler : MonoBehaviour
{
    public GameObject[] topLeftMaps;
    public GameObject[] topCenterMaps;
    public GameObject[] topRightMaps;
    public GameObject[] middleLeftMaps;
    public GameObject[] middleCenterMaps;
    public GameObject[] middleRightMaps;
    public GameObject[] bottomLeftMaps;
    public GameObject[] bottomCenterMaps;
    public GameObject[] bottomRightMaps;
    public Vector3 topLeftSpawn;
    public Vector3 topCenterSpawn;
    public Vector3 topRightSpawn;
    public Vector3 middleLeftSpawn;
    public Vector3 middleCenterSpawn;
    public Vector3 middleRightSpawn;
    public Vector3 bottomLeftSpawn;
    public Vector3 bottomCenterSpawn;
    public Vector3 bottomRightSpawn;

    public int tl;
    public int tc;
    public int tr;
    public int ml;
    public int mc;
    public int mr;
    public int bl;
    public int bc;
    public int br;

    public GameObject parentMap;
    //Map Handler should be able to draw a map randomly or from a seed
    //saving should save the seed

    public void CreateNewMap()
    {
        GenerateSeed();
        CreateMap();
    }

    public void CreateMap()
    {
        SetParentMap();
        foreach (Transform child in parentMap.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject tlTile = Instantiate(topLeftMaps[tl], topLeftSpawn, Quaternion.identity) as GameObject;
        tlTile.transform.parent = parentMap.transform;
        GameObject tcTile = Instantiate(topCenterMaps[tc], topCenterSpawn, Quaternion.identity) as GameObject;
        tcTile.transform.parent = parentMap.transform;
        GameObject trTile = Instantiate(topRightMaps[tr], topRightSpawn, Quaternion.identity) as GameObject;
        trTile.transform.parent = parentMap.transform;
        GameObject mlTile = Instantiate(middleLeftMaps[ml], middleLeftSpawn, Quaternion.identity) as GameObject;
        mlTile.transform.parent = parentMap.transform;
        GameObject mcTile = Instantiate(middleCenterMaps[mc], middleCenterSpawn, Quaternion.identity) as GameObject;
        mcTile.transform.parent = parentMap.transform;
        GameObject mrTile = Instantiate(middleRightMaps[mr], middleRightSpawn, Quaternion.identity) as GameObject;
        mrTile.transform.parent = parentMap.transform;
        GameObject blTile = Instantiate(bottomLeftMaps[bl], bottomLeftSpawn, Quaternion.identity) as GameObject;
        blTile.transform.parent = parentMap.transform;
        GameObject bcTile = Instantiate(bottomCenterMaps[bc], bottomCenterSpawn, Quaternion.identity) as GameObject;
        bcTile.transform.parent = parentMap.transform;
        GameObject brTile = Instantiate(bottomRightMaps[br], bottomRightSpawn, Quaternion.identity) as GameObject;
        brTile.transform.parent = parentMap.transform;

    }

    public void GenerateSeed()
    {
        tl = (int)Mathf.Floor(Random.Range(1f, topLeftMaps.Length) - 1);
        tc = (int)Mathf.Floor(Random.Range(1f, topCenterMaps.Length) - 1);
        tr = (int)Mathf.Floor(Random.Range(1f, topRightMaps.Length) - 1);
        ml = (int)Mathf.Floor(Random.Range(1f, middleLeftMaps.Length) - 1);
        mc = (int)Mathf.Floor(Random.Range(1f, middleCenterMaps.Length) - 1);
        mr = (int)Mathf.Floor(Random.Range(1f, middleRightMaps.Length) - 1);
        bl = (int)Mathf.Floor(Random.Range(1f, bottomLeftMaps.Length) - 1);
        bc = (int)Mathf.Floor(Random.Range(1f, bottomCenterMaps.Length) - 1);
        br = (int)Mathf.Floor(Random.Range(1f, bottomRightMaps.Length) - 1);
    }

    public void SetSeed(int topLeft,int topCenter,int topRight,int middleLeft, int middleCenter, int middleRight, int bottomLeft, int bottomCenter, int bottomRight)
    {
        tl = topLeft;
        tc = topRight;
        tr = topRight;
        ml = middleLeft;
        mc = middleCenter;
        mr = middleRight;
        bl = bottomLeft;
        bc = bottomCenter;
        br = bottomRight;
    }

    public void SetParentMap()
    {
        parentMap = GameObject.FindWithTag("ParentMap");
    }
    public void SetAStarGrids()
    {
        AStarGrid pathFinder = GameObject.FindWithTag("PathFinder").GetComponent<AStarGrid>();
        pathFinder.clearGrids();

        foreach (Transform child in parentMap.transform)
        {
            foreach (Transform gChild in child)
            {
                //Debug.Log("checking gChild for collidable tag");
                if (gChild.gameObject.tag == "Collidable")
                {
                    //Debug.Log("trying to add map to pathfinder");
                    pathFinder.collidableMaps.Add(gChild.gameObject.GetComponent<Tilemap>());
                }
            }
        }
        
    }
}
