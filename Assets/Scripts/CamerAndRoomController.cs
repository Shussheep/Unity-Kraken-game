using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]

public class CamerAndRoomController : MonoBehaviour
{
    
    [SerializeField] private GameObject[] roomMarkers = new GameObject[2];
    [SerializeField] private bool allWallAreSameSize = false;
    [SerializeField] private bool cameraFollowPlayer = false;
    [SerializeField] private int roomIN = 1;
    [SerializeField] private Transform player;
    [SerializeField] private float cameraOffSet = -10;
    [SerializeField] private float cameraOuterBoundX = 5;
    [SerializeField] private float cameraOuterBoundY = 5;


    // Start is called before the first frame update
    void Start()
    {
        if (Application.IsPlaying(gameObject))
        {
            // Play logic
            for (int i = 0; i < roomMarkers.Length; i++)
            {
                Room temp = roomMarkers[i].GetComponent<Room>();
                if (temp.GetRoomConectedTONorth() != 0) 
                {
                    roomMarkers[temp.GetRoomConectedTONorth() -1 ].GetComponent<Room>().roomConectedTOSouth = i + 1;            
                        }

                if (temp.GetRoomConectedTOEast() != 0) 
                {
                    roomMarkers[temp.GetRoomConectedTOEast() -1].GetComponent<Room>().roomConectedTOWest = i + 1;
                }

                if (temp.GetRoomConectedTOSouth() != 0)
                {
                    roomMarkers[temp.GetRoomConectedTOSouth()-1].GetComponent<Room>().roomConectedTONorth = i + 1;
                }

                if (temp.GetRoomConectedTOWest() != 0)
                {
                    roomMarkers[temp.GetRoomConectedTOWest() -1].GetComponent<Room>().roomConectedTOEast = i + 1;
                }

            }


        }
        else 
        {
        
        }
    }

    // Update is called once per frame
    void Update()
    {





        if (Application.IsPlaying(gameObject))
        {
            // Play logic
            Room room = roomMarkers[roomIN -1].GetComponent<Room>();
            if (cameraFollowPlayer) 
            {
                float x = player.position.x;
                float y = player.position.y;
                if (player.position.x < room.GetPoint2().position.x + cameraOuterBoundX)
                    x = gameObject.transform.position.x;
                if (player.position.x > room.GetPoint1().position.x - cameraOuterBoundX)
                    x = gameObject.transform.position.x;
                if (player.position.y > room.GetPoint2().position.y - cameraOuterBoundY)
                    y = gameObject.transform.position.y;
                gameObject.transform.position = new Vector3(x, y, cameraOffSet);
            }
            else
            {
                gameObject.transform.position = room.GetMiddel(cameraOffSet);
            }
           
            float playSpeed = player.GetComponent<Player>().GetPlayerSpeed();

            if (player.position.y > room.GetPoint2().position.y) 
            {
                if (room.GetRoomConectedTONorth() != 0)
                {
                    roomIN = room.GetRoomConectedTONorth();
                }
                else
                { 
                 player.transform.Translate(new Vector3(0, -playSpeed * Time.deltaTime, 0));
                }
            }

            if (player.position.y < room.GetPoint1().position.y)
            {
                if (room.GetRoomConectedTOSouth() != 0)
                {
                    roomIN = room.GetRoomConectedTOSouth();
                }
                else
                {
                    player.transform.Translate(new Vector3(0, playSpeed * Time.deltaTime, 0));
                }
            }

            if (player.position.x > room.GetPoint1().position.x)
            {
                if (room.GetRoomConectedTOEast() != 0)
                {
                    roomIN = room.GetRoomConectedTOEast();
                }
                else
                {
                    player.transform.Translate(new Vector3(-playSpeed * Time.deltaTime,0, 0));
                }
            }
            if (player.position.x < room.GetPoint2().position.x)
            {
                if (room.GetRoomConectedTOWest() != 0)
                {
                    roomIN = room.GetRoomConectedTOWest();
                }
                else
                {
                    player.transform.Translate(new Vector3(playSpeed * Time.deltaTime, 0, 0));
                }
            }



        }
        else
        {
            // Editor logic
            foreach (GameObject roomMarker in roomMarkers)
            {
                Room temp = roomMarker.GetComponent<Room>();
                if (temp.GetRoomConectedTONorth() != 0)
                {
                    int num = temp.GetRoomConectedTONorth();
                    roomMarkers[num - 1].GetComponent<Room>().MoveToCenter();

                    //independent is the one that you added the conection number to and dose not change shape when moved
                    float dependentY = roomMarkers[num - 1].transform.position.y;
                    float independentY = roomMarker.transform.position.y;

                    Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                    p1.position = new Vector3(temp.GetPoint1().transform.position.x, p1.position.y);
                    if (allWallAreSameSize)
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(temp.GetPoint2().transform.position.x, p2.position.y);
                    }


                    if (dependentY < independentY)
                    {
                        p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p1.position = new Vector3(p1.position.x, temp.GetPoint1().transform.position.y);
                    }
                    else
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p2.position = new Vector3(p2.position.x, temp.GetPoint2().transform.position.y);
                    }

                }
                if (temp.GetRoomConectedTOEast() != 0)
                {
                    int num = temp.GetRoomConectedTOEast();
                    roomMarkers[num - 1].GetComponent<Room>().MoveToCenter();

                    //independent is the one that you added the conection number to and dose not change shape when moved
                    float dependentX = roomMarkers[num - 1].transform.position.x;
                    float independentX = roomMarker.transform.position.x;

                    Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                    p2.position = new Vector3(p2.position.x, temp.GetPoint2().transform.position.y);
                    if (allWallAreSameSize)
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(p1.position.x, temp.GetPoint1().transform.position.y);
                    }


                    if (dependentX < independentX)
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(temp.GetPoint2().transform.position.x, p1.position.y);
                    }
                    else
                    {
                        p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(temp.GetPoint1().transform.position.x, p2.position.y);
                    }
                }
                if (temp.GetRoomConectedTOSouth() != 0)
                {
                    int num = temp.GetRoomConectedTOSouth();
                    roomMarkers[num - 1].GetComponent<Room>().MoveToCenter();

                    //independent is the one that you added the conection number to and dose not change shape when moved
                    float dependentY = roomMarkers[num - 1].transform.position.y;
                    float independentY = roomMarker.transform.position.y;

                    Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                    p2.position = new Vector3(temp.GetPoint2().transform.position.x, p2.position.y);
                    if (allWallAreSameSize)
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(temp.GetPoint1().transform.position.x, p1.position.y);
                    }
                    if (dependentY < independentY)
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p1.position = new Vector3(p1.position.x, temp.GetPoint1().transform.position.y);
                    }
                    else
                    {
                        p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p2.position = new Vector3(p2.position.x, temp.GetPoint2().transform.position.y);
                    }

                }
                if (temp.GetRoomConectedTOWest() != 0)
                {
                    int num = temp.GetRoomConectedTOWest();
                    roomMarkers[num - 1].GetComponent<Room>().MoveToCenter();

                    //independent is the one that you added the conection number to and dose not change shape when moved
                    float dependentX = roomMarkers[num - 1].transform.position.x;
                    float independentX = roomMarker.transform.position.x;

                    Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                    p1.position = new Vector3(p1.position.x, temp.GetPoint1().transform.position.y);
                    if (allWallAreSameSize)
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(p2.position.x, temp.GetPoint2().transform.position.y);
                    }

                    if (dependentX < independentX)
                    {
                        p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(temp.GetPoint2().transform.position.x, p1.position.y);
                    }
                    else
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(temp.GetPoint1().transform.position.x, p2.position.y);
                    }
                }


            }
        
        }
    }

   
}
