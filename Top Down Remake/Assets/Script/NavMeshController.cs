using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public NavMeshSurface _navMeshSurface;
    
    void Start()
    {
        _navMeshSurface.BuildNavMesh();
    }
}
