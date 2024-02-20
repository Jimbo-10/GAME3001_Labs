using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathNode
{
    public GameObject Tile { get; private set; }
    public List<PathConnection> connections;

    public PathNode(GameObject tile)
    {
        Tile = tile;
        connections = new List<PathConnection>();
    }

    public void AddConnection(PathConnection c)
    {
        connections.Add(c);
    }
}

public class PathConnection
{
    public float Cost { get; private set; }

    public PathNode FromNode { get; private set; }

    public PathNode ToNode { get; private set; }

    public PathConnection(PathNode from, PathNode to, float cost = 1f)
    {
        FromNode = from;
        ToNode = to;
        Cost = cost;
    }
}

public class NodeRecord
{
    public PathNode Node { get; set; }
    public NodeRecord FromRecord { get; set; }
    public PathConnection PathConnection { get; set; }
    public float CostSoFar { get; set; }

    public NodeRecord(PathNode node = null)
    {
        Node = node;
        PathConnection = null;
        FromRecord = null;
        CostSoFar = 0f;
    }
}
[System.Serializable]
public class PathManager : MonoBehaviour
{
    public List<NodeRecord> openList;
    public List<NodeRecord> closeList;

    public List<PathConnection> path; // what will be the shortest path

    public static PathManager Instance { get; private set; } // static object of the class

    void Awake()
    {
        if(Instance == null) //  if the object/Instance desent exist yet
        {
            Instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate Instance
        }
    }

    private void Initialize()
    {
        openList = new List<NodeRecord>();
        closeList = new List<NodeRecord>();
        path = new List<PathConnection>();
    }

    // 
    public void GetShortestPath(PathNode start, PathNode goal)
    {
        //TODO
    }

    // Some utility methods
    public NodeRecord GetSmallestNode()
    {
        NodeRecord smallestNode = openList[0];

        //Iterate through the rest of the node of the nodeRecord in the list
        for(int i = 1; i < openList.Count; i++)
        {
            // if the current NodeRecord has a smaller CostSoFar thn the smallestNode, update smallestNode with current NodeRecord
            if (openList[i].CostSoFar < smallestNode.CostSoFar)
            {
                smallestNode = openList[i];
            }
            // if they're the same, flip a coin
            else if (openList[i].CostSoFar == smallestNode.CostSoFar)
            {
                smallestNode = (Random.value < 0.5 ? openList[i] : smallestNode);
            }
        }
        return smallestNode; //  return the NodeRecord with the smallest CostSoFar
    }

    public bool ContainsNode(List<NodeRecord> list, PathNode node)
    {
        foreach(NodeRecord record in list)
        {
            if(record.Node == node) return true;
        }
        return false;
    }

    public NodeRecord GetNodeRecord(List<NodeRecord> list, PathNode node)
    {
        foreach(NodeRecord record in list)
        {
            if(record.Node == node) return record;
        }
        return null;
    }
}
