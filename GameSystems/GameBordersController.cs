using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBordersController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform _leftBorder, _rightBorder;

    public static GameBordersController instance;
    private void Awake()
    {
        instance = this;
    }

    public Vector3 getLeftBorder()
    {
        return _leftBorder.position;
    }
    public Vector3 getRightBorder()
    {
        return _rightBorder.position;
    }
}
