using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Entraces:
// 0 -> Up
// 1 -> Right
// 2 -> Down
// 3 -> Right

public class Room : MonoBehaviour
{
    [SerializeField]
    private List<Door> _doors;

    [SerializeField]
    private List<GameObject> _doorPrefabs;

    [SerializeField]
    private RoomType _roomType;

    [SerializeField]
    private bool _discovered;

    [SerializeField]
    private GameObject _discoveredSprite;

    private bool _roomFinished = true;

    private Vector2 _position;

    public Vector2 GetPosition() => _position;

    public void SetPosition(Vector2 position) => _position = position;

    public RoomType GetRoomType() => _roomType;

    public void Initialize()
    {
        GetDoors();

        foreach (var door in _doors)
        {
            door.gameObject.SetActive(false);
        }
        
        _discoveredSprite.SetActive(true);
    }

    private void Update()
    {
        if (_roomFinished)
        {
            foreach (var door in _doors)
            {
                door.OpenDoor();
            }
        }
    }

    public void GetDoors()
    {
        _doors.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            if (child.name.Contains("Door Points"))
            {
                for (int j = 0; j < child.transform.childCount; j++)
                {
                    var child1 = child.transform.GetChild(j);

                    if (child1.name.Contains("Door"))
                    {
                        _doors.Add(child1.GetComponent<Door>());
                    }
                }

                break;
            }
        }
    }

    public void SetEntrance(int doorNumber, Door neighbourDoor)
    {
        _doors[doorNumber].SetDoorLinked(neighbourDoor);
    }
    
    

    public void DiscoverRoom()
    {
        _discovered = true;
        _discoveredSprite.SetActive(false);
    }

    public void SpawnDoors()
    {
        // Spawn Doors
        foreach (var door in _doors)
        {
            if (door.HaveDoorLinked())
            {
                door.gameObject.SetActive(true);
            }
        }

        // Create doors instead activate Game object
    }

    public Door GetEntracePoint(int getEntracePoint)
    {
        return _doors[getEntracePoint];
    }
}