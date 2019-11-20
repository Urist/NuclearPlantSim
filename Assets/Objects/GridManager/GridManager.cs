using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject gridSquare;

    public int rowCount;
    public int colCount;

    // Start is called before the first frame update
    void Start()
    {
        CreateGameGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGameGrid()
    {
        for (int c = 0; c < colCount; c++)
        {
            for (int r = 0; r < rowCount; r++)
            {
                var newObj = Instantiate(gridSquare, new Vector3(r, c, 0), Quaternion.identity);
                newObj.transform.SetParent(this.transform);
            }
        }
    }
}
