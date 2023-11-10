using System.Collections.Generic;
using UnityEngine;

public class MovingManager : MonoBehaviour
{
    public GameObject collectablePrefab;
    public GameObject floorPrefab;
    public float blockSpeed = 5.0f;
    public float distanceBetweenCollectables = 30.0f;
    public float collectableGenerationDistance = 30.0f;
    public float distanceBetweenBlocks = 30.0f;
    public float blockGenerationDistance = 30.0f;
    public float floorGenerationDistance = 10.0f;
    public int numberOfLanes = 3;

    private ObstacleFactory obstacleFactory;
    private CollectableFactory collectableFactory;
    private float distanceBetweenFloors = 10.0f;
    private float laneDistance = 3.0f;
    private List<Obstacle> activeBlocks = new List<Obstacle>();
    private List<Collectable> activeCollectables = new List<Collectable>();
    private List<GameObject> activeFloors = new List<GameObject>();
    private float nextBlockGenerationPosition = 10.0f;
    private float nextCollectableGenerationPosition = 10.0f;
    private float nextFloorGenerationPosition = -50.0f;
    // TODO WIP obstacleGenerationMode
    // private string obstacleGenerationMode = "default"; // "boss1","boss2","boss3" (consider enum val)
    public PlayerController playerController;

    void Start()
    {
        nextFloorGenerationPosition = -floorGenerationDistance;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        var factory = GameObject.Find("Factory");
        obstacleFactory = factory.GetComponent<ObstacleFactory>();
        collectableFactory = factory.GetComponent<CollectableFactory>();

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
        // all blocks queried to move, blocks can manage this in their impl
        foreach (Obstacle block in activeBlocks)
        {
            block.moveObstacle(blockSpeed);
        }

        // Check if a new block needs to be generated
        if (nextBlockGenerationPosition - playerController.z <= distanceBetweenBlocks)
        {
            int lane = Random.Range(0, numberOfLanes)-1;
            GenerateBlock(lane,"stillBig");
        }
        if (nextCollectableGenerationPosition - playerController.z <= distanceBetweenCollectables)
        {
            int lane = Random.Range(0, numberOfLanes)-1;
            GenerateCollectable(lane,"damaging");
        }

        if (nextFloorGenerationPosition - playerController.z <= distanceBetweenFloors)
        {
            // Debug.Log("Generating floor");
            GenerateFloor();
        }
    }

    void GenerateBlock(int lane, string type)
    {
        // Calculate the position of the new block
        float xPos = lane * laneDistance;
        float zPos = nextBlockGenerationPosition + blockGenerationDistance;

        // Create the new block
        Obstacle newBlock = obstacleFactory.createObstacle(type, new Vector3(xPos, 0.0f, zPos));
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
    void GenerateCollectable(int lane, string type)
    {
        // Calculate the position of the new block
        float xPos = lane * laneDistance;
        float zPos = nextCollectableGenerationPosition + collectableGenerationDistance;

        // Create the new block
        Collectable newCollectable = collectableFactory.createCollectable(type, new Vector3(xPos, 3.0f, zPos));
        activeCollectables.Add(newCollectable);

        // Update the next block generation position
        nextCollectableGenerationPosition += distanceBetweenCollectables;

        // Destroy the oldest block after a new one is generated with a buffer of 3
        if (activeCollectables.Count > 15)
        {
            Destroy(activeCollectables[0]);
            activeCollectables.RemoveAt(0);
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
