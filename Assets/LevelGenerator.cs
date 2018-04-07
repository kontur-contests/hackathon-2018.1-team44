using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
  public GameObject[] Items;
  public GameObject[] Cells;
  public int LevelWidth;
  public int LevelHeight;
  public int CellOffset;
  public int MaxItemsPerLevel;

  private int[,] _cellFields;

  // Use this for initialization
  void Start()
  {
    _cellFields = new int[LevelWidth, LevelHeight];
    StartCoroutine(GenerateLevel());
  }

  IEnumerator GenerateLevel()
  {
    const float cellHeight = 1.0f;
    const float falloutDistanceCoef = 10.0f;
    float levelHalfWidth = LevelWidth / 2.0f * (float)CellOffset;
    float levelHalfHeight = LevelHeight / 2.0f * (float)CellOffset;
    float levelRadius = Mathf.Sqrt(levelHalfWidth * levelHalfWidth + levelHalfHeight * levelHalfHeight);

    Debug.Log(string.Format("levelHalfWidth: {0}", levelHalfWidth));
    Debug.Log(string.Format("levelHalfHeight: {0}", levelHalfHeight));
    Debug.Log(string.Format("levelRadius: {0}", levelRadius));

    //place cells
    for (int i=0; i< LevelWidth; ++i)
    {
      for (int j = 0; j < LevelHeight; ++j)
      {        
        float offsetX = (i * CellOffset) - levelHalfWidth;
        float offsetZ = (j * CellOffset) - levelHalfHeight;
        float distanceToOrigin = (new Vector2(offsetX, offsetZ)).magnitude;
        float invertedDistanceToOrigin = levelRadius - distanceToOrigin;

        int cellType = GetRandomCellType(distanceToOrigin);
        float falloutDelay = invertedDistanceToOrigin * falloutDistanceCoef + Random.Range(0, (int)falloutDistanceCoef);
        Vector3 position = new Vector3(offsetX, -1.0f, offsetZ);
        PlaceCell(position, cellType, falloutDelay);
        _cellFields[i, j] = cellType;
      }
    }

    //place items
    for (int i=0; i< MaxItemsPerLevel; ++i)
    {
      int itemType = Random.Range(0, Items.Length);
      int itemColumn = Random.Range(1, LevelWidth - 1);
      int itemRow = Random.Range(1, LevelHeight - 1);
      int cellType = _cellFields[itemColumn, itemRow];

      float offsetX = (itemColumn * CellOffset) - levelHalfWidth;
      float offsetY = cellType * cellHeight + 1.0f;
      float offsetZ = (itemRow * CellOffset) - levelHalfHeight;
      var itemPosition = new Vector3(offsetX, offsetY, offsetZ);
      PlaceItem(itemPosition, itemType);
    }

    yield return 0;
  }

  int GetRandomCellType(float distanceToOrigin)
  {
    int cellTypeSelectValue = Random.Range(0, 100);
    int cellType = 0;
    if (cellTypeSelectValue > 60)
      cellType = 1;
    if (cellTypeSelectValue > 85)
      cellType = 2;
    if (cellTypeSelectValue > 95)
      cellType = 3;

    if (distanceToOrigin < CellOffset)
      cellType = 0;
    return cellType;
  }

  void PlaceCell(Vector3 position, int cellType, float falloutDelay)
  {    
    GameObject cellObject = Instantiate(Cells[cellType], position, Quaternion.identity);    
    var controller = cellObject.GetComponent<CellController2>();
    controller.FalloutDelaySecs = falloutDelay;    
  }

  void PlaceItem(Vector3 position, int itemType)
  {
    GameObject cellObject = Instantiate(Items[itemType], position, Quaternion.identity);
    cellObject.tag = "Star";
  }
}
