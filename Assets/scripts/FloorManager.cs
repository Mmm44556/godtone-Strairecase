using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
  [SerializeField]  GameObject[] FloorPrefabs;
   public void SpawnFloor()
    {
     int r =  Random.Range(0, FloorPrefabs.Length);
      GameObject floor =  Instantiate(FloorPrefabs[r], transform);
        floor.transform.position = new Vector3(Random.Range(10f,15f),975f,0);
    }
}
