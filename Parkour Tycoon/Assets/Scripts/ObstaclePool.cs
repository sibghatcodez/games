using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] Transform player;
    private float nextCheckpoint = 105f; //starting point of obstacles!
    [SerializeField] List<Transform> ColliderBlocks = new List<Transform>();
    float ColliderBlockLimit = 275;
    string[] numberList = new string[5];
    int index = 0;
    int zIncrement = 10;

    void Start()
    {
    }
    void Update()
    {
        CheckColliderBlock();
        PlaceObstacle();
    }

    void CheckColliderBlock()
    {
        float playerPosZ = Mathf.Floor(player.transform.position.z);
        if (playerPosZ.Equals(ColliderBlockLimit) || playerPosZ.Equals(ColliderBlockLimit - 2) || playerPosZ.Equals(ColliderBlockLimit - 1) || playerPosZ.Equals(ColliderBlockLimit + 1) || playerPosZ.Equals(ColliderBlockLimit + 2))
        {
            foreach (var item in ColliderBlocks)
            {
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z + ColliderBlockLimit);
                Debug.Log("Collider pos has changed, look it up!");
            }
            ColliderBlockLimit += 275;
        }
    }
    void PlaceObstacle()
    {
        float playerPosZ = Mathf.Floor(player.transform.position.z);
        if (playerPosZ == nextCheckpoint)
        {
            nextCheckpoint += 50f;
            zIncrement = 15;

            string[] obstacleIDs = new string[5];
            string[] difficulty = new string[5];
            int score = (int) player.transform.position.z;

            if (score < 100)
            {
                difficulty = new string[] { "easy", "easy", "easy", "easy", "normal" };
            }
            else if (score < 200)
            {
                difficulty = new string[] { "easy", "easy", "easy", "normal", "normal" };
            }
            else if (score < 250)
            {
                difficulty = new string[] { "easy", "easy", "normal", "normal", "normal" };
            }
            else if (score < 400)
            {
                difficulty = new string[] { "easy", "easy", "normal", "normal", "hard" };
            }
            else if (score < 450)
            {
                difficulty = new string[] { "easy", "easy", "normal", "hard", "hard" };
            }
            else if (score < 500)
            {
                difficulty = new string[] { "easy", "normal", "hard", "hard", "hard" };
            }
            else if (score < 700)
            {
                difficulty = new string[] { "normal", "hard", "hard", "hard", "hard" };
            }
            else
            {
                difficulty = new string[] { "hard", "hard", "hard", "hard", "hard" };
            }

            for (int i = 0; i < obstacleIDs.Length; i++)
            {
                obstacleIDs[i] = RandomObstacle(difficulty[i]).ToString();
            }
            foreach (string obj in obstacleIDs)
            {
                foreach (var item in obstacles)
                {
                    if (obj.Equals(item.name))
                    {
                        item.transform.position = RandomObstaclePositions(int.Parse(item.name));
                        //Debug.Log("Player Pos" + player.transform.position + " ITEM POS:" + item.transform.position + " ZINCREMENT:"+zIncrement+" CP:"+nextCheckpoint);
                    }
                }
            }
        }
    }
    bool IfNumberIsUnique(int id)
    {
        foreach (var item in numberList)
        {
            if (id.ToString().Equals(item))
            {
                return false;
            }
        }
        return true;
    }
    int RandomObstacle(string category)
    {
        int random;
        do
        {
            switch (category)
            {
                case "easy":
                    random = UnityEngine.Random.Range(1, 6);
                    break;
                case "normal":
                    random = UnityEngine.Random.Range(6, 11);
                    break;
                case "hard":
                    random = UnityEngine.Random.Range(11, 15);
                    break;
                default:
                    return -1;
            }
        }
        while (!IfNumberIsUnique(random));


        if (index < numberList.Length)
        {
            numberList[index] = random.ToString();
            index++;
        }
        return random;
    }

    Vector3 RandomObstaclePositions(int obj)
    {
        Vector3[] positions;
        Vector3 position;
        int random = UnityEngine.Random.Range(1, 3);
        float z = player.transform.position.z + zIncrement;

        switch (obj)
        {
            case 1:
                positions = new Vector3[]
                {
                         new Vector3(311.25f,-147.323547f,z),
                         new Vector3(311.25f,-147.323547f,z),
                         new Vector3(311.25f,-147.323547f,z),
                };
                position = positions[random];
                break;

            case 2:
                positions = new Vector3[]
                {
                        new Vector3(309.470001f,-147.75f,z),
                        new Vector3(307.470001f,-147.75f,z),
                        new Vector3(-1f,-0.75f,z),
                };
                position = positions[random];
                break;
            case 3:
                positions = new Vector3[]
                {
                        new Vector3(309.531555f,-148.25f,z),
                        new Vector3(309.531555f,-148.25f,z),
                        new Vector3(309.531555f,-148.25f,z),
                };
                position = positions[random];
                break;
            case 4:
                positions = new Vector3[]
                {
                        new Vector3(309.394745f,-148.25f,z),
                        new Vector3(307.144745f,-148.25f,z),
                        new Vector3(312.144745f,-148.25f,z),
                };
                position = positions[random];
                break;
            case 5:
                positions = new Vector3[]
                {
                        new Vector3(308.100006f,-148.25f,z),
                        new Vector3(308.100006f,-148.25f,z),
                        new Vector3(308.100006f,-148.25f,z),
                };
                position = positions[random];
                break;
            case 6:
                positions = new Vector3[]
                {
                        new Vector3(307.654327f,-148.068253f,z),
                        new Vector3(309.404327f,-148.068253f,z),
                        new Vector3(311.904327f,-148.068253f,z),
                };
                position = positions[random];
                break;
            case 7:
                positions = new Vector3[]
                {
                        new Vector3(312.243439f,-147,z),
                        new Vector3(309.993439f,-147,z),
                        new Vector3(307.243439f,-147,z),
                };
                position = positions[random];
                break;
            case 8:
                positions = new Vector3[]
                {
                        new Vector3(312.149994f,-148.229996f,z),
                        new Vector3(309.149994f,-148.229996f,z),
                        new Vector3(306.649994f,-148.229996f,z),
                };
                position = positions[random];
                break;
            case 9:
                positions = new Vector3[]
                {
                        new Vector3(308.890015f,-147.174896f,z),
                        new Vector3(308.890015f,-147.174896f,z),
                        new Vector3(311.140015f,-147.174896f,z),
                };
                position = positions[random];
                break;
            case 10:
                positions = new Vector3[]
                {
                        new Vector3(305.241791f,-151.547958f,z),
                        new Vector3(307.589996f,-151.547958f,z),
                        new Vector3(310.339996f,-151.547958f,z),
                };
                position = positions[random];
                break;
            case 11:
                positions = new Vector3[]
                {
                        new Vector3(311.945648f,-147.78009f,z),
                        new Vector3(311.945648f,-147.78009f,z),
                        new Vector3(311.945648f,-147.78009f,z),
                };
                position = positions[random];
                break;
            case 12:
                positions = new Vector3[]
                {
                        new Vector3(306.899902f,-148.554367f,z),
                        new Vector3(309.149902f,-148.554367f,z),
                        new Vector3(311.899902f,-148.554367f,z),
                };
                position = positions[random];
                break;
            case 13:
                positions = new Vector3[]
                {
                        new Vector3(309.700012f,-144.290161f,z),
                        new Vector3(309.700012f,-144.290161f,z),
                        new Vector3(309.700012f,-144.290161f,z),
                };
                position = positions[random];
                break;
            case 14:
                positions = new Vector3[]
                {
                        new Vector3(309.619995f,-145.66394f,z),
                        new Vector3(309.619995f,-145.66394f,z),
                        new Vector3(309.619995f,-145.66394f,z),
                };
                position = positions[random];
                break;


            default:
                position = new Vector3(0, 0, 0);
                break;
        }

        zIncrement += 13;
        return position;
    }
}