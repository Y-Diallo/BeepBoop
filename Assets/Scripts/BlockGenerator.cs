using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject floorPrefab;
    public float blockSpeed = 5.0f;
    public float blockGenerationDistance = 20.0f;
    public float floorGenerationDistance = 100.0f;
    public int numberOfLanes = 3;
    public float laneDistance = 3.0f;

    private List<GameObject> activeBlocks = new List<GameObject>();
    private List<GameObject> activeFloors = new List<GameObject>();
    private int currentLane = 1;
    private float nextBlockGenerationPosition = 0.0f;
    private float nextFloorGenerationPosition = 0.0f;

    void Start()
    {
        GenerateFloor();
        GenerateBlock();
        GenerateBlock();
        GenerateBlock();
    }

    void Update()
    {
        // Move the active blocks towards the player
        foreach (GameObject block in activeBlocks)
        {
            block.transform.Translate(Vector3.back * blockSpeed * Time.deltaTime);
        }

        // Check if a new block needs to be generated
        if (nextBlockGenerationPosition - transform.position.z < blockGenerationDistance)
        {
            GenerateBlock();
        }

        // foreach (GameObject floor in activeFloors)
        // {
        //     floor.transform.Translate(Vector3.back * blockSpeed * Time.deltaTime);
        // }
        // Check if a new floor needs to be generated
        if (nextFloorGenerationPosition - transform.position.z < floorGenerationDistance)
        {
            GenerateFloor();
        }
    }

    void GenerateBlock()
    {
        // Calculate the position of the new block
        int lane = Random.Range(0, numberOfLanes);
        float xPos = (lane - currentLane) * laneDistance;
        float zPos = nextBlockGenerationPosition;

        // Create the new block
        GameObject newBlock = Instantiate(blockPrefab, new Vector3(xPos, 0.0f, zPos), Quaternion.identity);
        activeBlocks.Add(newBlock);

        // Update the next block generation position
        nextBlockGenerationPosition += blockGenerationDistance;
    }

    void GenerateFloor()
    {
        // Calculate the position of the new floor
        float zPos = nextFloorGenerationPosition;

        // Create the new floor
        GameObject newFloor = Instantiate(floorPrefab, new Vector3(0.0f, -1.0f, zPos), Quaternion.identity);
        activeFloors.Add(newFloor);
        // Update the next floor generation position
        nextFloorGenerationPosition += floorGenerationDistance;
    }
}
