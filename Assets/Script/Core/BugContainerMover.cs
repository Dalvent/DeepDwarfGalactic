using System;
using Script.Helpers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;


public class BugContainerMover : MonoBehaviour
{
    // когда дварф по середине одного из контейнеров.

    [Header("Position of spawned")] 
    public float YToSpawnBugs = 4f;
    public Transform Player;
    public Transform BottomYPos;

    [Header("Container")] 
    public RailForEnemies Rail;
    public float SizeOfContainers;
    public int BugInContainerCount = 0;
    public float[] BugsPositionsPresents;

    [Header("Prefab")] 
    public GameObject BugPrefab;

    private Transform _containerWithBugs1;
    private Transform _containerWithBugs2;

    private bool _isTopContainer2 = true;
    
    private bool _isPlayerJumpOnBugs;
    private bool IsPlayerJumpOnBugs
    {
        get => _isPlayerJumpOnBugs;
        set
        {
            if (_isPlayerJumpOnBugs == value)
                return;
            
            _isPlayerJumpOnBugs = value;
            if (_isPlayerJumpOnBugs)
                transform.position = transform.position.SetY(BottomYPos.position.y + SizeOfContainers * 0.5f);
            else
                ResetContainersY();
        }
    }

    public void Awake()
    {
        var bugContainer = new GameObject("BugContainer");
        _containerWithBugs1 = Instantiate(bugContainer, transform.position, quaternion.identity, transform).transform;
        _containerWithBugs2 = Instantiate(bugContainer, transform.position.SetY(transform.position.y + SizeOfContainers), quaternion.identity, transform).transform;
        bugContainer.gameObject.SetActive(false);


        int lastPosIndex = 0;
        for (int i = 0; i < BugInContainerCount; i++)
        {
            float x = Mathf.Lerp(Rail.FromX, Rail.ToX, BugsPositionsPresents[lastPosIndex]);

            lastPosIndex++;
            if (lastPosIndex >= BugsPositionsPresents.Length)
                lastPosIndex = 0;

            float bottomOfContainer = _containerWithBugs1.position.y - SizeOfContainers * 0.5f;
            float y = bottomOfContainer + SizeOfContainers / BugInContainerCount * (i + 1);
            
            InstantiateBug(new Vector3(x, y), _containerWithBugs1);
        }
        
        
        for (int i = 0; i < BugInContainerCount; i++)
        {
            float x = Mathf.Lerp(Rail.FromX, Rail.ToX, BugsPositionsPresents[lastPosIndex]);

            lastPosIndex++;
            if (lastPosIndex >= BugsPositionsPresents.Length)
                lastPosIndex = 0;

            float bottomOfContainer = _containerWithBugs2.position.y - SizeOfContainers * 0.5f;
            float y = bottomOfContainer + SizeOfContainers / BugInContainerCount * (i + 1);
            
            InstantiateBug(new Vector3(x, y), _containerWithBugs2);
        }
    }

    private void ResetContainersY()
    {
        _containerWithBugs1.position = transform.position;
        _containerWithBugs2.position = transform.position.SetY(transform.position.y + SizeOfContainers);
        _isTopContainer2 = true;
    }

    public void Update()
    {
        float playerY = Player.position.y;
        IsPlayerJumpOnBugs = playerY > YToSpawnBugs;

        if (!IsPlayerJumpOnBugs)
            return;

        print($"{nameof(_isTopContainer2)} - {_isTopContainer2}");
        if (_isTopContainer2)
        {
            if (_containerWithBugs2.transform.position.y < playerY)
            {
                Vector3 transformPosition = _containerWithBugs1.transform.position.SetY(_containerWithBugs1.transform.position.y + SizeOfContainers);
                _containerWithBugs1.transform.position = transformPosition;
                _isTopContainer2 = false;
            }
        }
        else
        {
            if (_containerWithBugs1.transform.position.y < playerY)
            {
                Vector3 newPos = _containerWithBugs2.transform.position.SetY(_containerWithBugs2.transform.position.y + SizeOfContainers);
                _containerWithBugs2.transform.position = newPos;
                _isTopContainer2 = true;
            }
        }
    }

    private GameObject InstantiateBug(Vector3 pos, Transform container)
    {
        var bug = Instantiate(BugPrefab, pos, quaternion.identity, container);
        bug.GetComponent<BugOnRail>().Rail = Rail;
        return bug;
    }

    private void OnDrawGizmos()
    {
        void DrawCube2d(float top, float bottom, float left, float right)
        {
            Gizmos.DrawLine(new Vector3(left, top), new Vector3(right, top));
            Gizmos.DrawLine(new Vector3(right, top), new Vector3(right, bottom));
            Gizmos.DrawLine(new Vector3(right, bottom), new Vector3(left, bottom));
            Gizmos.DrawLine(new Vector3(left, bottom), new Vector3(left, top));
        }
        

        float halfSizY = SizeOfContainers * 0.5f;
        // Позиция начала и конца рельсы по Y
        Gizmos.color = Color.yellow;
        float posY1 = transform.position.y;
        DrawCube2d(posY1 - halfSizY,posY1  + halfSizY, Rail.FromX, Rail.ToX);

        Gizmos.color = new Color(1f, 0.52156863f, 0.215686275f, 1f);
        float posY2 = transform.position.y + SizeOfContainers;
        DrawCube2d(posY2 - halfSizY, posY2 + halfSizY, Rail.FromX, Rail.ToX);
    }
}