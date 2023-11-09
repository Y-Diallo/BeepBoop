using System.Collections.Generic;
using UnityEngine;

public class MovingManager : MonoBehaviour
{
    public GameObject collectablePrefab;
    public GameObject floorPrefab;
    public float blockSpeed = 5.0f;
    public float distanceBetweenBlocks = 30.0f;
    public float blockGenerationDistance = 30.0f;
    public float floorGenerationDistance = 10.0f;
    public int numberOfLanes = 3;

    private ObstacleFactory obstacleFactory;
    private float distanceBetweenFloors = 10.0f;
    private float laneDistance = 3.0f;
    private List<Obstacle> activeBlocks = new List<Obstacle>();
    private List<GameObject> activeFloors = new List<GameObject>();
    private int currentLane = 1;
    private float nextBlockGenerationPosition = 10.0f;
    private float nextFloorGenerationPosition = -50.0f;
    public PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        obstacleFactory = GameObject.Find("ObstacleFactory").GetComponent<ObstacleFactory>();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        // GenerateBlock();
        // GenerateBlock();
        // GenerateBlock();
    }

    void Update()
    {
        // Move the active blocks towards the player
        foreach (Obstacle block in activeBlocks)
        {
            block.moveObstacle(blockSpeed);
        }

        // Check if a new block needs to be generated
        if (nextBlockGenerationPosition - playerController.z <= distanceBetweenBlocks)
        {
            GenerateBlock();
        }

        // foreach (GameObject floor in activeFloors)
        // {
        //     floor.transform.Translate(Vector3.back * blockSpeed * Time.deltaTime);
        // }
        // Check if a new floor needs to be generated
        // Debug.Log("nextFloorGenerationPosition: " + nextFloorGenerationPosition);
        // Debug.Log("playerController.z: " + playerController.z);
        // // next floor - player z
        // Debug.Log("nextFloorGenerationPosition - playerController.z: " + (nextFloorGenerationPosition - playerController.z));
        // Debug.Log("floorGenerationDistance: " + floorGenerationDistance);
        if (nextFloorGenerationPosition - playerController.z <= distanceBetweenFloors)
        {
            // Debug.Log("Generating floor");
            GenerateFloor();
        }
    }

    void GenerateBlock()
    {
        // Calculate the position of the new block
        int lane = Random.Range(0, numberOfLanes);
        float xPos = (lane - currentLane) * laneDistance;
        float zPos = nextBlockGenerationPosition + blockGenerationDistance;

        // Create the new block
        Obstacle newBlock = obstacleFactory.createObstacle("big", new Vector3(xPos, 0.0f, zPos));
        activeBlocks.Add(newBlock);

        // Update the next block generation position
        nextBlockGenerationPosition += distanceBetweenBlocks;

        // Destroy the oldest block after a new one is generated with a buffer of 3
        if (activeBlocks.Count > 15)
        {
            Destroy(activeBlocks[0]);
            activeBlocks.RemoveAt(0);
        }
    }

    void GenerateFloor()
    {
        // Calculate the position of the new floor
        float zPos = nextFloorGenerationPosition + floorGenerationDistance;

        // Create the new floor
        GameObject newFloor = Instantiate(floorPrefab, new Vector3(0.0f, -1.0f, zPos), Quaternion.identity);
        activeFloors.Add(newFloor);
        // Update the next floor generation position
        nextFloorGenerationPosition += distanceBetweenFloors;

        // Destroy the oldest floor after a new one is generated with a buffer of 3
        if (activeFloors.Count > 15)
        {
            Destroy(activeFloors[0]);
            activeFloors.RemoveAt(0);
        }
    }
}
