using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RoomType
{
    Normal,
    Entry,
    Boss,
    Treasure,
    Secret
}

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private int _iterations;

    [SerializeField]
    private int _roomSize;

    [SerializeField, Range(0, 10)]
    private int _maxForwardSteps;

    [SerializeField]
    private List<Room> _roomsPrefabs;

    [SerializeField]
    private List<Room> _spawnedRooms = new List<Room>();

    [SerializeField]
    private List<Vector2> _emptyOutline;

    private Vector2 _lastRoomPosition;

    [SerializeField, Range(0, 30)]
    private int _seed;

    private float _timer;
    private float _timerInit = 0.1f;


    private void Start()
    {
        ProceduralGenerationWithSeed();
        GeneratePlayer();
    }

    public void ProceduralGeneration()
    {
        Random.seed = Random.Range(0, int.MaxValue);

        ClearRoomSpawnedList();

        GenerateRooms();
        GenerateOutlinePositions();
        GenerateSecretRooms();
        GenerateDoors();
    }

    public void ProceduralGenerationWithSeed()
    {
        Random.seed = _seed;

        ProceduralGeneration();
    }


    public void ClearRoomSpawnedList()
    {
        ClearLog();

        _lastRoomPosition = Vector2.zero;


        if (!_spawnedRooms.Exists(x => x == null))
        {
            for (int i = 0; i < _spawnedRooms.Count; i++)
            {
                var room = _spawnedRooms[i];
                DestroyImmediate(room.gameObject);
            }

            _spawnedRooms.Clear();
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                if (child != null) DestroyImmediate(child.gameObject);
            }

            _spawnedRooms = new List<Room>();
        }

        _emptyOutline.Clear();
    }

    private void GenerateRooms()
    {
        // Generate entry room
        SpawnRoomAt(Vector2.zero, RoomType.Entry, "Room Entry");

        CheckEmptySpace(new Vector2(5, 5));

        var position = new Vector2();

        var lastDir = 0;
        var countForward = 0;
        for (int i = 0; i < _iterations; i++)
        {
            var rand = Random.Range(0f, 1f);
            var startPos = position;


            if (rand < 0.25f)
            {
                // Up
                // Debug.Log("Up");
                position.y++;
            }
            else if (rand >= 0.25f && rand < 0.5f)
            {
                // Right
                // Debug.Log("Right");
                position.x++;
            }
            else if (rand >= 0.5f && rand < 0.75f)
            {
                // Down
                // Debug.Log("Down");
                position.y--;
            }
            else if (rand >= 0.75f)
            {
                // Left
                // Debug.Log("Left");
                position.x--;
            }

            if (!CheckEmptySpace(position))
            {
                // Debug.Log($"Empty space false\nPosition:{startPos}, Space to Check:{position}");

                // Check if there any empty space at edges
                bool founded = false;
                int distance = 1;
                while (!founded)
                {
                    // first check on cross positions
                    for (var x = (int) startPos.x - distance; x < (int) startPos.x + distance + 2; x++)
                    {
                        var positionToCheck = new Vector2(x, startPos.y);

                        if (CheckEmptySpace(positionToCheck))
                        {
                            position = positionToCheck;
                            founded = true;
                            break;
                        }
                    }

                    for (var y = (int) startPos.y - distance; y < (int) startPos.y + distance + 2; y++)
                    {
                        var positionToCheck = new Vector2(startPos.x, y);

                        if (CheckEmptySpace(positionToCheck))
                        {
                            position = positionToCheck;
                            founded = true;
                            break;
                        }
                    }

                    distance++;
                }
            }


            if (i == _iterations - 1)
            {
                SpawnRoomAt(position, RoomType.Boss, "Room Boss");
            }
            else
            {
                SpawnRoomAt(position, RoomType.Normal);
            }
        }
    }


    private void GenerateOutlinePositions()
    {
        foreach (var room in _spawnedRooms)
        {
            var startPos = room.GetPosition();

            // Horizontal Check
            for (int x = (int) startPos.x - 1 * _roomSize; x < startPos.x + 2 * _roomSize; x += _roomSize)
            {
                var positionToCheck = new Vector2(x, (int) startPos.y);

                if (!_emptyOutline.Contains(positionToCheck))
                {
                    if (GetRoomAtGridPosition(positionToCheck) == null)
                    {
                        _emptyOutline.Add(positionToCheck);
                    }
                }
            }

            // Vertical Check

            for (int y = (int) startPos.y - 1 * _roomSize; y < startPos.y + 2 * _roomSize; y += _roomSize)
            {
                var positionToCheck = new Vector2((int) startPos.x, y);

                if (!_emptyOutline.Contains(positionToCheck))
                {
                    if (GetRoomAtGridPosition(positionToCheck) == null)
                    {
                        _emptyOutline.Add(positionToCheck);
                    }
                }
            }
        }
    }

    private Room SpawnRoomAt(Vector2 roomPosition, RoomType roomType, string customName = "Room")
    {
        // Instantiate(_roomsPrefabs[0], roomPosition, quaternion.identity);
        roomPosition *= _roomSize;
        Room room;

        var prefab = _roomsPrefabs.Find(x => x.GetRoomType() == roomType);

        GameObject roomGO;
        
        roomGO = prefab != null ? Instantiate(prefab.gameObject, roomPosition, Quaternion.identity) : null;


        if (roomGO == null)
        {
            roomGO = Instantiate(_roomsPrefabs[0].gameObject, roomPosition, Quaternion.identity);
        }

        room = roomGO.GetComponent<Room>();

        room.name = $"{customName} {roomPosition}";
        room.transform.SetParent(transform);
        room.SetPosition(roomPosition);
        room.Initialize();
        _spawnedRooms.Add(room);

        // Debug.Log(roomPosition);
        // _lastRoomPosition = roomPosition;

        return room;
    }

    private bool CheckEmptySpace(Vector2 positionToCheck)
    {
        positionToCheck *= _roomSize;

        foreach (var room in _spawnedRooms)
        {
            if (room.GetPosition() == positionToCheck)
            {
                return false;
            }
        }

        return true;
    }

    private void GenerateSecretRooms()
    {
        var getRandomPosition = _emptyOutline[Random.Range(0, _emptyOutline.Count)];

        var secretRoom = SpawnRoomAt(getRandomPosition / _roomSize, RoomType.Treasure, "Room Treasure");
    }

    private void GenerateDoors()
    {
        for (var i = 0; i < _spawnedRooms.Count; i++)
        {
            Room room = _spawnedRooms[i];
            var startPos = room.GetPosition();

            for (int x = (int) startPos.x - 1 * _roomSize; x < (int) startPos.x + 2 * _roomSize; x++)
            {
                if (x - startPos.x != 0)
                {
                    if (GetRoomAtGridPosition(x, (int) startPos.y) != null)
                    {
                        if (x - startPos.x == -1 * _roomSize)
                        {
                            // Left
                            room.SetEntrance(3, GetRoomAtGridPosition(x, (int) startPos.y).GetEntracePoint(1));
                        }
                        else if (x - startPos.x == 1 * _roomSize)
                        {
                            // Right
                            room.SetEntrance(1, GetRoomAtGridPosition(x, (int) startPos.y).GetEntracePoint(3));
                        }
                    }
                }
            }

            for (int y = (int) startPos.y - 1 * _roomSize; y < (int) startPos.y + 2 * _roomSize; y++)
            {
                if (y - startPos.y != 0)
                {
                    if (GetRoomAtGridPosition((int) startPos.x, y) != null)
                    {
                        if (y - startPos.y == -1 * _roomSize)
                        {
                            // Down
                            room.SetEntrance(2, GetRoomAtGridPosition((int) startPos.x, y).GetEntracePoint(0));
                        }
                        else if (y - startPos.y == 1 * _roomSize)
                        {
                            // Up
                            room.SetEntrance(0, GetRoomAtGridPosition((int) startPos.x, y).GetEntracePoint(2));
                        }
                    }
                }
            }
        }

        foreach (var room in _spawnedRooms)
        {
            room.SpawnDoors();
        }
    }

    private Room GetRoomAtGridPosition(int x, int y)
    {
        Vector2 pos = new Vector2(x, y);

        return _spawnedRooms.FirstOrDefault(room => room.GetPosition() == pos);
    }

    private Room GetRoomAtGridPosition(Vector2 positionToCheck)
    {
        Vector2 pos = positionToCheck;

        return _spawnedRooms.FirstOrDefault(room => room.GetPosition() == pos);
    }

    private List<Room> GetNeighbours(Room room)
    {
        var neighbours = new List<Room>();

        return neighbours;
    }
    
    
    private void GeneratePlayer()
    {
        foreach (var room in _spawnedRooms)
        {
            if (room.GetRoomType() == RoomType.Entry)
            {
                room.DiscoverRoom();
                var player = GameObject.FindWithTag("Player");

                player.transform.position = room.transform.position;
            }
        }

        
    }

    private void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    

    public int GetCurrentSeed() => Random.seed;
}