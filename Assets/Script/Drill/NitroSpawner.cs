using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum TargetSpawnNitro
{
    Low,
    Normal,
    High
}

public class NitroSpawner : MonoBehaviour
{
    public Transform World;
    [Header("Nitro prefabs")] 
    public GameObject NitroPrefab;
    public int StartPoolSize = 100;

    public TargetSpawnNitro TargetSpawnNitro;
    
    [Header("Nitro settings")]
    public Sprite LowNitroSprite;
    public int LowNitroValue;
    
    public Sprite NormalNitroSprite;
    public int NormalNitroValue;
    
    public Sprite HighNitroSprite;
    public int HighNitroValue;
    
    [Header("Nitro renderer")] 
    public Transform Player;
    public float TopRenderY = 10f;
    public float BottomRenderY = 10f;
    
    [Header("Spawn Position")] 
    public Vector2 SpawnX;

    [Header("Random settings")] 
    public float MinDepthSpawn;
    public float MaxDepthSpawn;

    public NitroByPositionList _positionList = new();
    private float _toNextSpawnDepth;
    private Dictionary<NitroInfo, Nitro> _inActiveNitrs = new();
    private List<Nitro> _inPoolNitrs;
    private (int from, int to) _prevIndexes;
    private List<NitroInfo> _requestToDelete = new();

    public void Awake()
    {
        _inPoolNitrs = new List<Nitro>(StartPoolSize);
        for (int i = 0; i < StartPoolSize; i++)
        {
            var instNitro = InstNitroObject();
            instNitro.gameObject.SetActive(false);
            _inPoolNitrs.Add(instNitro.GetComponent<Nitro>());
        }
    }


    public void Update()
    {
        if (_toNextSpawnDepth < Game.Instance.GameStats.Depth)
        {
            SpawnNitro();
            _toNextSpawnDepth = Game.Instance.GameStats.Depth + Random.Range(MinDepthSpawn, MaxDepthSpawn);
        }

        var indexes = _positionList.FindRange(
            Player.position.y - World.position.y - BottomRenderY, 
            Player.position.y - World.position.y + TopRenderY);
        
        for (int i = _prevIndexes.from; i < indexes.from; i++)
        {
            ReturnToPool(_positionList.List[i]);
        }
        
        for (int i = indexes.to; i < _prevIndexes.to; i++)
        {
            ReturnToPool(_positionList.List[i]);
        }
        
        for (int i = indexes.from; i < indexes.to; i++)
        {
            if (i < _prevIndexes.from || i >= _prevIndexes.to)
            {
                var info = _positionList.List[i];
                if (_inActiveNitrs.ContainsKey(info))
                    continue;
                
                GetOrCreateNewNitroObject(info);
            }
        }

        bool haveDelets = _requestToDelete.Count > 0;
        
        foreach (var toDelete in _requestToDelete)
        {
            ReturnToPool(toDelete);
            _positionList.Remove(toDelete);
        }
        
        if (haveDelets)
            indexes = _positionList.FindRange(
                Player.position.y - World.position.y - BottomRenderY, 
                Player.position.y - World.position.y + TopRenderY);
        
        _requestToDelete.Clear();
        
        _prevIndexes = indexes;
    }

    private void SpawnNitro()
    {
        float fromX = transform.position.x + SpawnX.x;
        float toX = transform.position.x + SpawnX.y;
        float x = Random.Range(fromX, toX);
        
        float y = transform.position.y;

        _positionList.Add(new NitroInfo()
        {
            Sprite = GetSpriteToSpawn(),
            Value = GetValueToSpawn(),
            WorldX = x - World.position.x,
            WorldY = y - World.position.y
        });
    }

    private Sprite GetSpriteToSpawn()
    {
        switch (TargetSpawnNitro)
        {
            case TargetSpawnNitro.Low:
                return LowNitroSprite;
            case TargetSpawnNitro.Normal:
                return NormalNitroSprite;
            case TargetSpawnNitro.High:
                return HighNitroSprite;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private int GetValueToSpawn()
    {
        switch (TargetSpawnNitro)
        {
            case TargetSpawnNitro.Low:
                return LowNitroValue;
            case TargetSpawnNitro.Normal:
                return NormalNitroValue;
            case TargetSpawnNitro.High:
                return HighNitroValue;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Позиция начала и конца рельсы по Y
        Vector3 from = new Vector3(transform.position.x + SpawnX.x, transform.position.y, transform.position.z);
        Vector3 to = new Vector3(transform.position.x + SpawnX.y, transform.position.y, transform.position.z);

        // Рельса — линия
        Gizmos.DrawLine(from, to);

        // Концы рельсы как точки
        Gizmos.DrawSphere(from, 0.05f);
        Gizmos.DrawSphere(to, 0.05f);
    }

    public void DeleteNitro(NitroInfo nitroInfo)
    {
        _requestToDelete.Add(nitroInfo);
    }

    private void ReturnToPool(NitroInfo nitro)
    {
        if (nitro == null)
            print("whata");
        
        var obj = _inActiveNitrs[nitro];
        
        obj.gameObject.SetActive(false);
        _inPoolNitrs.Add(obj);

        obj.transform.position = new Vector2(-1000, -1000);
        
        _inActiveNitrs.Remove(nitro);
    }
    
    private Nitro GetOrCreateNewNitroObject(NitroInfo nitroInfo)
    {
        if (_inPoolNitrs.Count == 0)
        {
            var obj = InstNitroObject();
            _inActiveNitrs.Add(nitroInfo, obj);
            obj.SetInfo(nitroInfo);
            return obj;
        }
        else
        {
            var obj = _inPoolNitrs[0];
            obj.gameObject.SetActive(true);
            _inPoolNitrs.RemoveAt(0);
            _inActiveNitrs.Add(nitroInfo, obj);
            obj.SetInfo(nitroInfo);
            return obj;
        }
    }

    private Nitro InstNitroObject()
    {
        return Instantiate(NitroPrefab, Vector3.zero, Quaternion.identity, World).GetComponent<Nitro>();
    }
}