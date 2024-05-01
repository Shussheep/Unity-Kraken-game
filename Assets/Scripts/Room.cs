using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class Room : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
  
    [SerializeField] bool moveToCenter = false;

    [SerializeField] bool ancorToRightOrUp = true;
    public int roomConectedTONorth = 0;
    public int roomConectedTOEast = 0;
    public int roomConectedTOSouth = 0;
    public int roomConectedTOWest = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.IsPlaying(gameObject))
        {
            // Play logic


        }
        else 
        {
            //editor logic
            PrefabUtility.UnpackPrefabInstance(gameObject, UnityEditor.PrefabUnpackMode.Completely, UnityEditor.InteractionMode.UserAction);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        DrawRoom(point1.position.x, point1.position.y, point2.position.x, point2.position.y);
        if (moveToCenter) 
        {
             Vector3 t = GetMiddel();
            MoveMarkerOnly(t);
            moveToCenter = false;
        }
        if (point2.position.x > point1.position.x  )
        {
            point1.position = new Vector3(1, point1.position.y);
            point2.position = new Vector3(-1, point2.position.y);
            moveToCenter = true;
        }
        if (point1.position.y > point2.position.y)
        {
            point1.position = new Vector3( point1.position.x, -1);
            point2.position = new Vector3( point2.position.x, 1);
            moveToCenter = true;
        }

    }

    public void DrawRoom(float x1, float y1, float x2, float y2)
    {
        Debug.DrawLine(new Vector3(x1, y1), new Vector3(x2, y1), Color.red);
        Debug.DrawLine(new Vector3(x1, y1), new Vector3(x1, y2), Color.green);
        Debug.DrawLine(new Vector3(x2, y1), new Vector3(x2, y2), Color.red);
        Debug.DrawLine(new Vector3(x1, y2), new Vector3(x2, y2), Color.green);
    }
    public Transform GetPoint1() 
    {
        return point1;
    }

    public Transform GetPoint2()
    {
        return point2;
    }

    public Vector3 GetMiddel()
    {
       return new Vector3((point1.position.x + point2.position.x) / 2, (point1.position.y + point2.position.y) / 2);
        
           
          
    }

    public Vector3 GetMiddel(float z)
    {
        z = point1.position.y - point2.position.y;
        if (point1.position.x - point2.position.x > point1.position.y - point2.position.y) 
        {
            z = point1.position.x - point2.position.x;
        }
        return new Vector3((point1.position.x + point2.position.x) / 2, (point1.position.y + point2.position.y) / 2, -Mathf.Abs(z/2));

    }

    public void MoveMarkerOnly( Vector3 NewLoc) 
    {
       
        point1.SetParent(null, true);
        point2.SetParent(null, true);
        transform.position = NewLoc;
        point1.SetParent(gameObject.transform, true);
        point2.SetParent(gameObject.transform, true);

    }
    public void MoveToCenter() 
    {
        moveToCenter = true;
    }

    public float GetHeight() 
    {
        return point2.position.y - point1.position.y;
    }
    public int GetRoomConectedTONorth() 
    {
        return roomConectedTONorth;
    }
    public int GetRoomConectedTOEast()
    {
        return roomConectedTOEast;
    }
    public int GetRoomConectedTOSouth()
    {
        return roomConectedTOSouth;
    }
    public int GetRoomConectedTOWest()
    {
        return roomConectedTOWest;
    }

    public bool GetAncorToRightOrUp ()
    {
        return ancorToRightOrUp;
    }

}
