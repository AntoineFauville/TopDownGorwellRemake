using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public NavMeshSurface _navMeshSurface;

    void Start()
    {
        StartCoroutine(SlowUpdate());
    }

    IEnumerator SlowUpdate()
    {
        _navMeshSurface.BuildNavMesh();
        yield return new WaitForSeconds(1);
        _navMeshSurface.BuildNavMesh();
    }
}
