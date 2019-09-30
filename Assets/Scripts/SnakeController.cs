using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private const float accelerateCost = 10.0f;
    private const float excuteTimesPerSecond = 1 / 100.0f;
    private const float barLength = 315.0f;

    private const float turnAnglePerSecond = 250.0f;

    private int length;
    private LevelController lvControl;
    private GameObject bodyPrefab;

    private Transform allBody;
    private Transform firstBody;
    private Transform snake;
    private RectTransform energyBar;

    private RectTransform healthBar;

    private float movingSpeed;
    private float steeringSpeed;

    private float currentEnergy;
    private float maxEnergy;

    private float currentHealth;
    private float maxHealth;


    private Rigidbody2D rigi;
    float timer;

    private int frameTimer;
    private int framesUpdateBody;

    private PolygonCollider2D polygonC;

    private List<Transform> equipments;
    [SerializeField]
    private int currentEquipment;  // -1 stands for currently no equipment
    [SerializeField]
    public bool canControll;
    [Range(1, 100)]
    public int initialLength;

    private Vector3 lastPosi;
    private float distanceFromLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        frameTimer = 0;
        framesUpdateBody = 2;
        //
        snake = transform.parent;
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        movingSpeed = lvControl.speed;
        steeringSpeed = 10.0f;

        maxEnergy = 100.0f;
        currentEnergy = maxEnergy;

        maxHealth = 100.0f;
        currentHealth = maxHealth;
        healthBar = GameObject.Find("Health").GetComponent<RectTransform>();
        energyBar = GameObject.Find("Energy").GetComponent<RectTransform>();
        bodyPrefab = Resources.Load<GameObject>("Prefabs/Body");


        allBody = GameObject.Find("SnakeBody").transform;
        rigi = snake.GetComponent<Rigidbody2D>();
        length = initialLength;

        for (int n = 0; n < length; n++)
        {
            GameObject newBody = Instantiate(bodyPrefab, allBody);
            newBody.transform.position = new Vector2(snake.position.x, snake.position.y - 0.2f * n);
            newBody.GetComponent<SpriteRenderer>().sortingOrder = -n;
            newBody.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -n - 3;
        }
        firstBody = allBody.GetChild(0);

        // GetComponent<EdgeCollider2D>().enabled = true;

        equipments = new List<Transform>();
        AddEquipment(Equipments.Axe);
        AddEquipment(Equipments.FireworkStand);
        currentEquipment = (int)Equipments.Axe;

        //canControll = false;

        // rigi.velocity = snake.up * movingSpeed;
        lastPosi = snake.position;
    }

    void Update()
    {
        CheckSwitchEquip();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    Vector2[] ps = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };
        //    polygonC.points = ps;
        //}

        frameTimer++;
        //if (rigi.velocity.magnitude >= 0.005f)
        //{
        if (frameTimer >= framesUpdateBody)
        {
            frameTimer = 0;

            rigi.velocity = snake.up * movingSpeed;
            firstBody.up = snake.position - firstBody.position;
            firstBody.position = snake.position;

            distanceFromLastFrame = Vector2.Distance(snake.position, lastPosi);
            lastPosi = snake.position;

            for (int n = length - 1; n > 0; n--)
            {
                allBody.GetChild(n).transform.up = allBody.GetChild(n - 1).transform.position - allBody.GetChild(n).transform.position;

                //
                allBody.GetChild(n).Translate(0, distanceFromLastFrame, 0);
                //allBody.GetChild(n).transform.position = allBody.GetChild(n - 1).transform.position;
                //

            }
            framesUpdateBody = 2;
        }
        //   }
        AbilitiesDetection();
        MovementDetection();

    }

    public void GotDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.anchoredPosition = new Vector2((currentHealth - maxHealth) - (damage * barLength / maxEnergy), 0);
        healthBar.anchoredPosition = new Vector2(healthBar.anchoredPosition.x - (damage * barLength / maxEnergy), 0);
    }

    public void ExtendBody(int bodies)
    {
        for (int n = 0; n < 5 * bodies; n++)
        {
            GameObject newBody = Instantiate(bodyPrefab, allBody);
            newBody.transform.position = new Vector2(allBody.GetChild(length - 1).position.x, allBody.GetChild(length - 1).position.y);
            newBody.transform.rotation = allBody.GetChild(length - 1).rotation;

            newBody.GetComponent<SpriteRenderer>().sortingOrder = -length - n;
            newBody.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -length - n - 3;
        }
        length += 5 * bodies;
    }

    private void RecoverEnergy(float value)
    {
        currentEnergy += value;
    }

    public void RestoreEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        energyBar.anchoredPosition = new Vector2((currentEnergy - maxEnergy) * (barLength / maxEnergy), 0);
    }

    private void AbilitiesDetection()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (currentEnergy >= 5.0f)
            {
                framesUpdateBody = 1;
                movingSpeed = 20.0f;
                // steeringSpeed = 10.0f;
                currentEnergy -= excuteTimesPerSecond * accelerateCost;
                energyBar.anchoredPosition = new Vector2((currentEnergy - maxEnergy) * (barLength / maxEnergy), 0);
            }
            else
            {
                movingSpeed = 10.0f;
                // steeringSpeed = 10.0f;
            }
        }
        else
        {
            movingSpeed = 10.0f;
            // steeringSpeed = 10.0f;
        }
    }

    private void MovementDetection()
    {
        if (canControll)
        {
            if (Input.GetKey(KeyCode.A))
            {
                timer += Time.fixedDeltaTime;
                if (timer <= 0.5f)
                {
                    snake.Rotate(0, 0, steeringSpeed * timer);
                }
                else
                {
                    snake.Rotate(0, 0, turnAnglePerSecond * Time.fixedDeltaTime);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                timer += Time.fixedDeltaTime;
                if (timer <= 0.5f)
                {
                    snake.Rotate(0, 0, -steeringSpeed * timer);
                }
                else
                {
                    snake.Rotate(0, 0, -turnAnglePerSecond * Time.fixedDeltaTime);
                }
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                timer = 0;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                timer = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                GotDamage(5.0f);
                break;
            case "Sheep":
                // collision.transform.GetComponent<SheepController>().CollideWithSnake();
                break;
            default:
                break;
        }
    }

    public void CheckSwitchEquip()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchEquipment(false);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchEquipment(true);
        }
    }

    public void AddEquipment(Equipments equipment)
    {
        equipments.Add(snake.GetChild(1).GetChild((int)equipment));
    }

    public void SwitchEquipment(bool leftToRight)
    {
        if (equipments.Count > 1)
        {
            if (leftToRight)
            {
                if (currentEquipment < equipments.Count - 1)
                {
                    equipments[currentEquipment].gameObject.SetActive(false);
                    currentEquipment++;
                    equipments[currentEquipment].gameObject.SetActive(true);
                }
                else
                {
                    equipments[currentEquipment].gameObject.SetActive(false);
                    currentEquipment = 0;
                    equipments[currentEquipment].gameObject.SetActive(true);
                }
            }
            else
            {
                if (currentEquipment > 0)
                {
                    equipments[currentEquipment].gameObject.SetActive(false);
                    currentEquipment--;
                    equipments[currentEquipment].gameObject.SetActive(true);
                }
                else
                {
                    equipments[currentEquipment].gameObject.SetActive(false);
                    currentEquipment = equipments.Count - 1;
                    equipments[currentEquipment].gameObject.SetActive(true);
                }
            }
        }
        lvControl.WeaponChanged(equipments, currentEquipment);
    }
}


