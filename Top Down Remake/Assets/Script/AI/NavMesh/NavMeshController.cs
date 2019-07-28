using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public NavMeshSurface _navMeshSurface;
    
    void Update()
    {
        _navMeshSurface.BuildNavMesh();
    }
}
