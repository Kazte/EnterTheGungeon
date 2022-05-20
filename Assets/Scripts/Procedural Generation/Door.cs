using System;
using Sirenix.Utilities;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool _isOpened;

    private Door _doorLinked;

    private Room _room;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private GameObject _doorGameObject;

    [SerializeField]
    private GameObject _doorCollider;

    [SerializeField]
    private GameObject _wallCollider;

    [SerializeField]
    private Transform _tpPoint;

    [SerializeField] 
    private PlayerStatsData _playerStatsData;
    
    
    public void SetDoorLinked(Door doorToLink)
    {
        _doorLinked = doorToLink;

        _doorGameObject.SetActive(true);

        _wallCollider.SetActive(false);
        _doorCollider.SetActive(true);

        _room = GetComponentInParent<Room>();
    }

    public bool HaveDoorLinked() => _doorLinked;

    public void OpenDoor()
    {
        _isOpened = true;
        _anim.SetTrigger("Open");
    }

    public Vector3 GetTpPoint() => _tpPoint.position;

    public void Interact()
    {
        _playerStatsData.PlayerGameObject.transform.position = _doorLinked.GetTpPoint();
        _doorLinked.DiscoverRoom();
        
        Debug.Log("Interacted with " + name);
    }

    public void DiscoverRoom()
    {
        _room.DiscoverRoom();
    }
}