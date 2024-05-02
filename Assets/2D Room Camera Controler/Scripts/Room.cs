using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class Room : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    [Header("Settings")]
    [SerializeField] bool ancorToRightOrUp = true;
    [SerializeField] bool allWallSameSize = true;
    [Header("Direction Conections")]
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
            if (PrefabUtility.IsPartOfAnyPrefab(gameObject))
            PrefabUtility.UnpackPrefabInstance(gameObject, UnityEditor.PrefabUnpackMode.Completely, UnityEditor.InteractionMode.UserAction);
        }
            
    }
   
    // Update is called once per frame
    void Update()
    {
        DrawRoom(point1.position.x, point1.position.y, point2.position.x, point2.position.y);
        
        if (point2.position.x > point1.position.x  )
        {
            Debug.Log("x");
            point1.localPosition = new Vector3(1, point1.localPosition.y);
            point2.localPosition = new Vector3(-1, point2.localPosition.y);
            MoveToCenter();
        }
        if (point1.position.y > point2.position.y)
        {
            Debug.Log("y");
            point1.localPosition = new Vector3( point1.localPosition.x, -1);
            point2.localPosition = new Vector3( point2.localPosition.x, 1);
            MoveToCenter();
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
    public void MoveToCenter()
    {
        Vector3 t = GetMiddel();
        MoveMarkerOnly(t);
    }

    public Transform GetPoint2()
    {
        return point2;
    }

    public Vector3 GetMiddel()
    {
       return new Vector3((point1.position.x + point2.position.x) / 2, (point1.position.y + point2.position.y) / 2, -10);
        
           
          
    }

    public void MoveMarkerOnly( Vector3 NewLoc) 
    {
       
        point1.SetParent(null, true);
        point2.SetParent(null, true);
        transform.position = NewLoc;
        point1.SetParent(gameObject.transform, true);
        point2.SetParent(gameObject.transform, true);

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

    public bool GetAllWallSameSize() 
    {
        return allWallSameSize;
    }

    public void MakeRoomConection(int direction, int toConectTo) 
    {
        if (direction == 0)
        {
            roomConectedTONorth = toConectTo;
        }
        else if (direction == 1)
        {
            roomConectedTOEast = toConectTo;
        }
        else if (direction == 2)
        {
            roomConectedTOSouth = toConectTo;
        }
        else if (direction == 3)
        {
            roomConectedTOWest = toConectTo;
        }
        else 
        {
            Debug.Log("Error");
        }
    }
}
[CustomEditor(typeof(Room))]
public class MyPlayerEditorAlternative : Editor
{
    int _selected = 0;
    string[] _options = new string[4] { "North", "East", "South", "West" };
    static int roomToConect = 0;
    public override void OnInspectorGUI()
    {
        Room mp = (Room)target;

        DrawDefaultInspector();
        
        this._selected = EditorGUILayout.Popup("Add Room Conection TO ", _selected, _options);
         roomToConect = EditorGUILayout.IntField("roomToConect", roomToConect);
        if (GUILayout.Button("Make Room Conectons"))
        {
            mp.MakeRoomConection(_selected, roomToConect);
        }
        if (GUILayout.Button("Move To Center"))
        {
            mp.MoveToCenter();
        }
    }
}