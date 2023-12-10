
using System.Collections.Generic;
using UnityEngine;

public class MovingManager : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public GameObject floorPrefab;
    public float blockSpeed = 5.0f;
    public float distanceBetweenCollectables = 30.0f;
    public float collectableGenerationDistance = 30.0f;
    public float distanceBetweenObstacles = 30.0f;
    public float obstacleGenerationDistance = 30.0f;
    public float floorGenerationDistance = 10.0f;
    public int numberOfLanes = 3;
    public ObstacleFactory obstacleFactory { get; private set; }
    public CourseFactory courseFactory { get; private set; }
    public CollectableFactory collectableFactory { get; private set; }
    private BossFactory bossFactory;
    private float distanceBetweenFloors = 10.0f;
    public float laneDistance { get; private set; } = 3.0f;
    public List<GameObject> activeBlocks { get; private set; } = new List<GameObject>();
    public List<GameObject> activeCollectables { get; private set; } = new List<GameObject>();
    private List<GameObject> activeFloors = new List<GameObject>();
    private GameObject bossHealthBar;
    private Vector3 bossHealthBarOffset = new Vector3(0.0f, 2f, -2.0f);
    public GameObject boss { get; private set; }
    private int bossHealth;
    private bool bossAlive = false;
    public float nextObstacleGenerationPosition { get; private set; } = 10.0f;
    public float nextCollectableGenerationPosition { get; private set; } = 10.0f;
    private int blocksSpawned = 0; //used to determine when the boss is spawned
    private float nextFloorGenerationPosition = -50.0f;
    private CourseCreator courseCreator;
    private int currentBossThreshold = 10;
    public PlayerController playerController;
    private string currentBoss = "boss1";

    void Start()
    {
        nextFloorGenerationPosition = -floorGenerationDistance;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        var factory = GameObject.Find("Factory");
        obstacleFactory = factory.GetComponent<ObstacleFactory>();
        collectableFactory = factory.GetComponent<CollectableFactory>();
        bossFactory = factory.GetComponent<BossFactory>();
        courseFactory = factory.GetComponent<CourseFactory>();

        courseCreator = new CourseCreator(this);


        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();
        GenerateFloor();

    }

    void Update()
    {
        if(!bossAlive && blocksSpawned > currentBossThreshold){
            Vector3 bossSpawnLocation = new Vector3(playerController.x, 8.0f, playerController.z+20.0f);
            boss = bossFactory.createBoss(currentBoss,bossSpawnLocation);
            bossHealthBar = Instantiate(healthBarPrefab, bossSpawnLocation+bossHealthBarOffset, Quaternion.identity);

            //set boss health
            bossHealth = boss.GetComponent<Boss>().getBossHealth();
            bossAlive = true;

            //set health bar max health
            bossHealthBar.GetComponentInChildren<HealthBar>().SetMaxHealth(bossHealth);

            //get boss course creator
            courseCreator = courseFactory.createCourse(currentBoss,this);

        }else if(bossAlive){//boss is active
            boss.GetComponent<Boss>().moveBoss(12.0f);
            
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
        if (nextObstacleGenerationPosition - playerController.z <= distanceBetweenObstacles)
        {
            GenerateObstacle();
        }
        if (nextCollectableGenerationPosition - playerController.z <= distanceBetweenCollectables)
        {
            GenerateCollectable();
        }

        if (nextFloorGenerationPosition - playerController.z <= distanceBetweenFloors)
        {
            // Debug.Log("Generating floor");
            GenerateFloor();
        }
    }

    void GenerateObstacle()
    {
        activeBlocks.AddRange(courseCreator.GenerateObstacle());

        // Update the next obstacle generation position
        nextObstacleGenerationPosition += distanceBetweenObstacles;

        // Destroy the oldest block after a new one is generated with a buffer of 3
        if (activeBlocks.Count > 15)
        {
            Destroy(activeBlocks[0]);
            activeBlocks.RemoveAt(0);
        }
        blocksSpawned++;
    }

    void GenerateCollectable()
    {
        activeCollectables.AddRange(courseCreator.GenerateCollectable());

        // Update the next obstacle generation position
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
        courseCreator = new CourseCreator(this);
        if(currentBoss == "boss1"){
            currentBoss = "boss2";
        } else {
            currentBoss = "boss1";
            playerController.winGame();
        }
    }
    
}
