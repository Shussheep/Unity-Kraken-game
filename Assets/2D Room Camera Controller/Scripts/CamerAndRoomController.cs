using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CamerAndRoomController : MonoBehaviour
{
    [Header("list of rooms")]
    [SerializeField] private GameObject[] roomMarkers = new GameObject[2];
    [SerializeField] private bool cameraFollowPlayer = false;
    [SerializeField] private int roomIN = 1;
    [Header("Player info")]
    [SerializeField] private Transform player;
    [SerializeField] private float playSpeed = 5;
    [Header("camera bevavior controls")]
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
            gameObject.transform.position = roomMarkers[roomIN - 1].GetComponent<Room>().GetMiddel();
            GetComponent<Camera>().orthographicSize = cameraOffSet;
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
                    x = room.GetPoint2().position.x + cameraOuterBoundX;
                if (player.position.x > room.GetPoint1().position.x - cameraOuterBoundX)
                    x = room.GetPoint1().position.x - cameraOuterBoundX;
                if (player.position.y > room.GetPoint2().position.y - cameraOuterBoundY)
                    y = room.GetPoint2().position.y - cameraOuterBoundY;
                if (player.position.y < room.GetPoint1().position.y + cameraOuterBoundY)
                    y = room.GetPoint1().position.y + cameraOuterBoundY;
                gameObject.transform.position = new Vector3(x, y, -10);
            }
            else 
            {
                gameObject.transform.position = roomMarkers[roomIN - 1].GetComponent<Room>().GetMiddel();
            }
            GetComponent<Camera>().orthographicSize = cameraOffSet;

            //float playSpeed = player.GetComponent<Player>().GetPlayerSpeed();

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

                   

                   
                    if (roomMarkers[num - 1].GetComponent<Room>().GetAllWallSameSize())
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(temp.GetPoint1().transform.position.x, p1.position.y);

                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(temp.GetPoint2().transform.position.x, p2.position.y);
                    }
                    else if (roomMarkers[num - 1].GetComponent<Room>().GetAncorToRightOrUp())
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(temp.GetPoint2().transform.position.x, p2.position.y);
                    }
                    else 
                    {
                        
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(temp.GetPoint1().transform.position.x, p1.position.y);
                    }


                        Transform p2y = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p2y.position = new Vector3(p2y.position.x, temp.GetPoint2().transform.position.y);
                    

                }
                if (temp.GetRoomConectedTOEast() != 0)
                {
                    int num = temp.GetRoomConectedTOEast();
                    roomMarkers[num - 1].GetComponent<Room>().MoveToCenter();

                   


                    if (roomMarkers[num - 1].GetComponent<Room>().GetAllWallSameSize())
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(p2.position.x, temp.GetPoint2().transform.position.y);

                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(p1.position.x, temp.GetPoint1().transform.position.y);
                    }
                    else if (roomMarkers[num - 1].GetComponent<Room>().GetAncorToRightOrUp())
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(p2.position.x, temp.GetPoint2().transform.position.y);
                    }
                    else 
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(p1.position.x, temp.GetPoint1().transform.position.y);
                    }




                    //p2x = point 2 minipulated on x
                    Transform p2x = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                    p2x.position = new Vector3(temp.GetPoint1().transform.position.x, p2x.position.y);

                }
                if (temp.GetRoomConectedTOSouth() != 0)
                {
                    int num = temp.GetRoomConectedTOSouth();
                    roomMarkers[num - 1].GetComponent<Room>().MoveToCenter();

                   

                    if (roomMarkers[num - 1].GetComponent<Room>().GetAllWallSameSize())
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(temp.GetPoint1().transform.position.x, p1.position.y);

                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(temp.GetPoint2().transform.position.x, p2.position.y);
                    }
                    else if (roomMarkers[num - 1].GetComponent<Room>().GetAncorToRightOrUp())
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(temp.GetPoint2().transform.position.x, p2.position.y);
                    }
                    else
                    {

                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(temp.GetPoint1().transform.position.x, p1.position.y);
                    }

                    
                        Transform p1y = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p1y.position = new Vector3(p1y.position.x, temp.GetPoint1().transform.position.y);
                   

                }
                if (temp.GetRoomConectedTOWest() != 0)
                {
                    int num = temp.GetRoomConectedTOWest();
                    roomMarkers[num - 1].GetComponent<Room>().MoveToCenter();

                   

                    //menipulation on the y axis 
                 
                    if (roomMarkers[num - 1].GetComponent<Room>().GetAllWallSameSize())
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(p1.position.x, temp.GetPoint1().transform.position.y);

                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(p2.position.x, temp.GetPoint2().transform.position.y);
                    }
                    else if (roomMarkers[num - 1].GetComponent<Room>().GetAncorToRightOrUp())
                    {
                        Transform p2 = roomMarkers[num - 1].GetComponent<Room>().GetPoint2().transform;
                        p2.position = new Vector3(p2.position.x, temp.GetPoint2().transform.position.y);
                    }
                    else 
                    {
                        Transform p1 = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                        p1.position = new Vector3(p1.position.x, temp.GetPoint1().transform.position.y);
                    }

                    //menipulation on the x axis 
                   
                       
                    Transform p1x = roomMarkers[num - 1].GetComponent<Room>().GetPoint1().transform;
                    p1x.position = new Vector3(temp.GetPoint2().transform.position.x, p1x.position.y);

                }
            }
        }
    }
}
