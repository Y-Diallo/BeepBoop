
using System.Collections.Generic;
using UnityEngine;

public class MovingManager : MonoBehaviour
{
    public GameObject healthBarPrefab;
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
    private BossFactory bossFactory;
    private float distanceBetweenFloors = 10.0f;
    private float laneDistance = 3.0f;
    private List<GameObject> activeBlocks = new List<GameObject>();
    private List<GameObject> activeCollectables = new List<GameObject>();
    private List<GameObject> activeFloors = new List<GameObject>();
    private GameObject bossHealthBar;
    private Vector3 bossHealthBarOffset = new Vector3(0.0f, 2f, -2.0f);
    private GameObject boss;
    private int bossHealth;
    private bool bossAlive = false;
    private float nextBlockGenerationPosition = 10.0f;
    private float nextCollectableGenerationPosition = 10.0f;
    private int blocksSpawned = 0; //used to determine when the boss is spawned
    private float nextFloorGenerationPosition = -50.0f;
    // TODO WIP obstacleGenerationMode // used to have custom block spawning modes for bosses
    // private string obstacleGenerationMode = "default"; // "level1","level2","level3" (consider enum val)

    //temporary switches for generation type, use more sophisticated method
    private string blockGenerationType = "stillBig";
    private string collectableGenerationType = "bullet";
    private int currentBossThreshold = 10;
    public PlayerController playerController;

    void Start()
    {
        nextFloorGenerationPosition = -floorGenerationDistance;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        var factory = GameObject.Find("Factory");
        obstacleFactory = factory.GetComponent<ObstacleFactory>();
        collectableFactory = factory.GetComponent<CollectableFactory>();
        bossFactory = factory.GetComponent<BossFactory>();


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
        if(!bossAlive && blocksSpawned > currentBossThreshold){
            Vector3 bossSpawnLocation = new Vector3(playerController.x, 8.0f, playerController.z+20.0f);
            boss = bossFactory.createBoss("level1",bossSpawnLocation);
            bossHealthBar = Instantiate(healthBarPrefab, bossSpawnLocation+bossHealthBarOffset, Quaternion.identity);

            //set boss health
            bossHealth = boss.GetComponent<Boss>().getBossHealth();
            bossAlive = true;

            //set health bar max health
            bossHealthBar.GetComponentInChildren<HealthBar>().SetMaxHealth(bossHealth);
        }else if(bossAlive){//boss is active
            boss.GetComponent<Boss>().moveBoss(12.0f);

            collectableGenerationType = boss.GetComponent<Boss>().getCollectableGenerationMode();
            blockGenerationType = boss.GetComponent<Boss>().getObstacleGenerationMode();

            bossHealth = boss.GetComponent<Boss>().getBossHealth();
            bossAlive = bossHealth > 0;

            //update health bar
            bossHealthBar.GetComponentInChildren<HealthBar>().SetHealth(bossHealth);
            bossHealthBar.transform.position = boss.transform.position + bossHealthBarOffset;

            if(!bossAlive){ //boss is dead
                onBossDeath();
            }
        }
        // Move the active blocks towards the player
        // all blocks queried to move, blocks can manage this in their impl
        foreach (GameObject block in activeBlocks)
        {
            block.GetComponent<Obstacle>().moveObstacle(blockSpeed);
        }

        // Check if a new block needs to be generated
        if (nextBlockGenerationPosition - playerController.z <= distanceBetweenBlocks)
        {
            int lane = Random.Range(0, numberOfLanes)-1;
            GenerateBlock(lane,blockGenerationType);
        }
        if (nextCollectableGenerationPosition - playerController.z <= distanceBetweenCollectables)
        {
            int lane = Random.Range(0, numberOfLanes)-1;
            GenerateCollectable(lane,collectableGenerationType);
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
        GameObject newBlock = obstacleFactory.createObstacle(type, new Vector3(xPos, 0.0f, zPos));
        activeBlocks.Add(newBlock);

        // Update the next block generation position
        nextBlockGenerationPosition += distanceBetweenBlocks;

        // Destroy the oldest block after a new one is generated with a buffer of 3
        if (activeBlocks.Count > 15)
        {
            Destroy(activeBlocks[0]);
            activeBlocks.RemoveAt(0);
        }
        blocksSpawned++;
    }

    void GenerateCollectable(int lane, string type)
    {
        // Calculate the position of the new block
        float xPos = lane * laneDistance;
        float zPos = nextCollectableGenerationPosition + collectableGenerationDistance;

        // Create the new block
        GameObject newCollectable = collectableFactory.createCollectable(type, new Vector3(xPos, 3.0f, zPos));
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
    void updateBossThreshold(){
        currentBossThreshold = blocksSpawned + 10;
    }

    void onBossDeath(){
        updateBossThreshold();
        bossHealthBar.SetActive(false);
        blockGenerationType = "stillBig";
        collectableGenerationType = "bullet";
    }
}
