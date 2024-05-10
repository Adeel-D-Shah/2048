using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{ 
    public GameObject objectPrefab;
    private GameObject[,] gridObjects = new GameObject[4, 4];

    public void Merge(GameObject gm1, GameObject gm2)
    {
        gridObjects[(int)gm1.transform.position.x, (int)gm1.transform.position.y] = null;
        Destroy(gm1.gameObject);
        gm2.GetComponent<box>().Value *= 2;
    }
    public void Update_Position(GameObject obj, int x, int y)
    {
        GameObject temp = obj;
        Vector2 temp_pos = obj.transform.position;
        gridObjects[(int)temp_pos.x,(int)temp_pos.y] = null;
        gridObjects[x,y] = temp;
        temp.transform.position = new Vector2(x, y);

    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(new Vector2(i,j), 0.1f);
            }
        }
    }


    public void add_object(Vector2 pos)
    {
        GameObject tem = Instantiate(objectPrefab, pos, Quaternion.identity);
        gridObjects[(int)pos.x,(int)pos.y] = tem;
    }


    

    private void Start()
    {
       
        add_object(new Vector2 (0,0));
        add_object(new Vector2(0, 1));
        add_object(new Vector2(2, 2));
        add_object(new Vector2(0, 2));
        add_object(new Vector2(1, 1));
        add_object(new Vector2(2, 1));
        add_object(new Vector2(3, 0));
        add_object(new Vector2(3, 1));
        add_object(new Vector2(3, 2));
        add_object(new Vector2(1, 3));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { GameObject[,] tem = gridObjects;  Move_Left(); if (tem == gridObjects) { Debug.Log("Nothing moved"); } }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { Move_Right(); }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { Move_Up(); }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { Move_Down(); }

    }

    public void Move_Right()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 3; j >= 0; j--)
            {
                if (gridObjects[j, i] != null)
                {
                    int z = First_Same_Value_From_Right(i, gridObjects[j, i].GetComponent<box>().Value);
                    if (z > j && z != 10)
                    {
                        Merge(gridObjects[j, i], gridObjects[z, i]);
                    }
                    else
                    {
                        int x = First_Empty_From_Right(i);
                        if (x > j && x != 10)
                        {
                            Update_Position(gridObjects[j, i], x, i);
                        }
                    }
                }
            }
        }
    }

    public void Move_Left()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (gridObjects[j, i] != null)
                {

                    int z = First_Same_Value_From_Left(i, gridObjects[j, i].GetComponent<box>().Value);
                    if (z < j && z != 10)
                    {
                        Merge(gridObjects[j, i], gridObjects[z, i]);
                    }
                    else
                    {
                        int x = First_Empty_From_Left(i);
                        if (x < j && x != 10)
                        {
                            Update_Position(gridObjects[j, i], x, i);
                        }
                    }
                }
            }
        }
    }

    public void Move_Up()
    {
        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                if (gridObjects[i, j] != null)
                {
                    int z = First_Same_Value_From_Up(i, gridObjects[i, j].GetComponent<box>().Value);
                    if (z > j && z != 10)
                    {
                        Merge(gridObjects[i, j], gridObjects[i, z]);
                    }
                    else
                    {
                        int x = First_Empty_From_Up(i);
                        if (x > j && x != 10)
                        {
                            Update_Position(gridObjects[i, j], i, x);
                        }
                    }

                }

            }
        }
    }
    public void Move_Down()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {


                if (gridObjects[i, j] != null)
                {


                    int z = First_Same_Value_From_Down(i, gridObjects[i, j].GetComponent<box>().Value);
                    if (z < j && z != 10)
                    {
                        Merge(gridObjects[i, j], gridObjects[i, z]);
                    }
                    else
                    {
                        int x = First_Empty_From_Down(i);

                        if (x < j && x != 10)
                        {
                            Update_Position(gridObjects[i, j], i, x);
                        }
                    }

                }

            }
        }
    }
    public int First_Same_Value_From_Left(int line, int value)
    {
        for (int i = 0; i < 4; i++)
        {
            if (gridObjects[i, line] != null && gridObjects[i,line].GetComponent<box>().Value == value ) { return i; }
        }
        return 10;
    }

    public int First_Same_Value_From_Right(int line, int value)
    {
        for (int i = 3; i > 0; i--)
        {
            if (gridObjects[i, line] != null && gridObjects[i, line].GetComponent<box>().Value == value) { return i; }
        }
        return 10;
    }

    public int First_Empty_From_Right(int line)
    {
        for(int i = 3;i > 0; i--)
        {
            if (gridObjects[i, line] == null) { return i; }
        }
        return 10;
    }

    public int First_Empty_From_Left(int line)
    {
        for (int i = 0; i <4; i++)
        {
            if (gridObjects[i, line] == null) { return i; }
        }
        return 10;
    }

    public int First_Same_Value_From_Up(int line, int value)
    {
        for (int i = 3; i >=0; i--)
        {
            if (gridObjects[ line,i] != null && gridObjects[line,i ].GetComponent<box>().Value == value) { return i; }
        }
        return 10;
    }
    public int First_Empty_From_Up(int line)
    {
        for (int i =3; i >=0; i--)
        {
            if (gridObjects[line, i] == null) { return i; }
        }
        return 10;
    }
    public int First_Same_Value_From_Down(int line, int value)
    {
        for (int i = 0; i <4; i++)
        {
            if (gridObjects[line, i] != null && gridObjects[line, i].GetComponent<box>().Value == value) { return i; }
        }
        return 10;
    }
    public int First_Empty_From_Down(int line)
    {
        for (int i = 0; i < 4; i++)
        {
            if (gridObjects[line, i] == null) { return i; }
        }
        return 10;
    }





}

