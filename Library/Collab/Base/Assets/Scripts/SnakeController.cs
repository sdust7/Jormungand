﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SnakeController : MonoBehaviour
{
    private int length;
    private Slider energyBar;
    private Slider healthBar;
    private LevelController lvControl;
    private GameObject bodyPrefab;
    private Transform allBody;
    private Transform firstBody;
    private float movingSpeed;
    private float steeringSpeed;
    private float energy;

    private Rigidbody2D rigi;
    float timer;
  
    // Start is called before the first frame update
    void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        movingSpeed = lvControl.speed;
        steeringSpeed = 10.0f;
        bodyPrefab = Resources.Load<GameObject>("Prefabs/Body");
        allBody = GameObject.Find("SnakeBody").transform;
        rigi = transform.GetComponent<Rigidbody2D>();
        length = 20;
        energy = 100;
        energyBar = GameObject.Find("EnergyBar").GetComponent<Slider>(); 
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();

        for (int n = 0; n < length; n++)
        {
            GameObject newBody =  Instantiate(bodyPrefab,allBody);
            newBody.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        firstBody = allBody.GetChild(0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        rigi.velocity = transform.up*movingSpeed;
        //firstBody.position = transform.position;
        firstBody.GetComponent<Rigidbody2D>().velocity = rigi.velocity;
        for (int n = length-1; n > 0; n--)
        {
            //allBody.GetChild(n).transform.position =allBody.GetChild(n-1).transform.position;
            allBody.GetChild(n).GetComponent<Rigidbody2D>().velocity = allBody.GetChild(n - 1).GetComponent<Rigidbody2D>().velocity;
        }
        energyBar.value = energy;
        AbilitiesDetection();
        MovementDetection();
      
    }

    public void GetApple()
    {
        for (int n = 0; n < 5; n++)
        {
            GameObject newBody = Instantiate(bodyPrefab, allBody);
            newBody.transform.position = new Vector2(allBody.GetChild(length-1).position.x, allBody.GetChild(length - 1).position.y);
        }
        length += 5;
    }

    private void RecoverEnergy(float value)
    {
        energy += value;
    }

    private void AbilitiesDetection()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (energy >= 10.0f)
            {
                movingSpeed = 20.0f;
                steeringSpeed = 10.0f;
                energy -= 0.1f;
            }
            else
            {
                movingSpeed = 10.0f;
                steeringSpeed = 10.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            movingSpeed = 10.0f;
            steeringSpeed = 10.0f;
        }
    }

    private void MovementDetection()
    {
        if (Input.GetKey(KeyCode.A))
        {
            timer += Time.fixedDeltaTime;
            if (timer <= 0.5f)
            {
                transform.Rotate(0, 0, steeringSpeed * timer);
            }
            else
            {
                transform.Rotate(0, 0, 5);

            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            timer += Time.fixedDeltaTime;
            if (timer <= 0.5f)
            {
                transform.Rotate(0, 0, -steeringSpeed * timer);
            }
            else
            {
                transform.Rotate(0, 0, -5);

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