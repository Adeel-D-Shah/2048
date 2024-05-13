using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        int x = UnityEngine.Random.Range(0, 10);
        if (x == 0 || x == 1) { tem.GetComponent<box>().Value = 4; }
        gridObjects[(int)pos.x,(int)pos.y] = tem;
    }


    
    public void Random_Spawn()
    {
        List<Vector2> empty_spots= new List<Vector2>();

        
            for (int i = 0; i < gridObjects.GetLength(0); i++)
            {
                for (int j = 0; j < gridObjects.GetLength(1); j++)
                {
                    if (gridObjects[i, j] == null)
                    {
                        empty_spots.Add(new Vector2(i,j));
                    }
                }
            }

            if(empty_spots.Count > 0)
        {
            int x = UnityEngine.Random.Range(0, gridObjects.GetLength(0));
            add_object(empty_spots[x]);


        }
        else { Debug.Log("no empty spots"); }
        
    }
    private void Start()
    {
        Random_Spawn();
        Random_Spawn();

    }

    GameObject[,] tem = new GameObject[4, 4];

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {Move_Left(); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { Move_Right(); }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { Move_Up(); }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { Move_Down(); }
        if (Input.GetKeyDown(KeyCode.Space)) { Random_Spawn(); }
        if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

    }

    public void Move_Right()
    {
        System.Array.Copy(gridObjects, tem, gridObjects.Length);
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
        if (!AreArraysEqual(tem, gridObjects)) { Random_Spawn(); }
    }

    public void Move_Left()
    {
        System.Array.Copy(gridObjects, tem, gridObjects.Length);
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
        if (!AreArraysEqual(tem, gridObjects)) { Random_Spawn(); }
    }

    public void Move_Up()
    {
        System.Array.Copy(gridObjects, tem, gridObjects.Length);
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
        if (!AreArraysEqual(tem, gridObjects)) { Random_Spawn(); }
    }
    public void Move_Down()
    {
        System.Array.Copy(gridObjects, tem, gridObjects.Length);
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
        if (!AreArraysEqual(tem, gridObjects)) { Random_Spawn(); }
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

    bool AreArraysEqual(GameObject[,] array1, GameObject[,] array2)
    {
        if (array1.GetLength(0) != array2.GetLength(0) || array1.GetLength(1) != array2.GetLength(1))
            return false;

        for (int i = 0; i < array1.GetLength(0); i++)
        {
            for (int j = 0; j < array1.GetLength(1); j++)
            {
                if (array1[i, j] != array2[i, j])
                    return false;
            }
        }
        return true;
    }



}

