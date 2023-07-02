using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
    //Object representing a normal mazecell
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject floor;

    public void Init(bool top, bool bottom, bool right, bool left, bool floor)
    {
        //setts walls and flore aktive or not
        topWall.SetActive(top);
        bottomWall.SetActive(bottom);
        rightWall.SetActive(right);
        leftWall.SetActive(left);
        this.floor.SetActive(floor);
    }

    public void SetVisibility(bool floor)
    {
        //disablein or enabeling floor
        this.floor.SetActive(floor);
    }

}
