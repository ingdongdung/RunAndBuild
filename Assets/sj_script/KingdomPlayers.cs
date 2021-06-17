using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomPlayers : MonoBehaviour
{
    bool isBuilding;
    Vector3[] beforeBuildPos = new Vector3[3];
    FollowPlayerCamera camera;
    KingdomPlayer[] kingdomPlayers;
    protected GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        isBuilding = false;
        camera = FindObjectOfType<FollowPlayerCamera>();
        kingdomPlayers = Resources.FindObjectsOfTypeAll<KingdomPlayer>();
        tiles = GameObject.FindGameObjectsWithTag("Tile"); // 0 ~ 659
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.isBuilding && !isBuilding)
        {
            isBuilding = true;

            for (int i = 0; i < 3; ++i)
            {
                kingdomPlayers[i].isBuilding = true;
                transform.GetChild(i).gameObject.SetActive(false);
                beforeBuildPos[i] = transform.GetChild(i).position;
            }
        }

        if (!camera.isBuilding && isBuilding)
        {
            isBuilding = false;
          
            for (int i = 0; i < 3; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);

                Vector3 pos = new Vector3(tiles[Random.Range(0, tiles.Length)].transform.position.x, 
                    beforeBuildPos[i].y, tiles[Random.Range(0, tiles.Length)].transform.position.z);

                transform.GetChild(i).position = pos;
                kingdomPlayers[i].eBuilding = KingdomPlayer.BUILDING.buildingEnd;
                kingdomPlayers[i].isStartCoroutine = true;
            }
        }
    }
}
