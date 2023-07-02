using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCellObject : MonoBehaviour
{
    //representing a normal trappcell
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject leftWall;
    public void Init(bool top, bool bottom, bool right, bool left)
    {
        //setting setting the walls aktive or not given the parameters of the funktion
        topWall.SetActive(top);
        bottomWall.SetActive(bottom);
        rightWall.SetActive(right);
        leftWall.SetActive(left);
    }
}
