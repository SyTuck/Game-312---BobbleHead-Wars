﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Animator arenaAnimator;
    public GameObject[] spawnPoints;
    public GameObject player;
    public GameObject alien;
    public GameObject upgradePrefab;
    public GameObject deathFloor;
    public Gun gun;

    public float upgradeMaxTimeSpawn = 7.5f;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int aliensPerSpawn;
    public int maxAliensOnScreen;
    public int totalAliens;

    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;
    private float actualUpgradeTime = 0.0f;
    private float currentUpgradeTime = 0.0f;

    private int aliensOnScreen = 0;
    private bool spawnedUpgrade = false;

    // Start is called before the first frame update
    void Start()
    {
        actualUpgradeTime = Random.Range(upgradeMaxTimeSpawn - 3.0f, upgradeMaxTimeSpawn);
        actualUpgradeTime = Mathf.Abs(actualUpgradeTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            return;
        }
        currentUpgradeTime += Time.deltaTime;
        if (currentUpgradeTime > actualUpgradeTime)
        {
            if (!spawnedUpgrade)
            {
                int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                GameObject spawnLocation = spawnPoints[randomNumber];

                GameObject upgrade = Instantiate(upgradePrefab) as GameObject;
                Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();
                upgradeScript.gun = gun;
                upgrade.transform.position = spawnLocation.transform.position;

                spawnedUpgrade = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);
            }
        }

        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            if ((aliensPerSpawn > 0) && (aliensOnScreen < totalAliens))
            {
                List<int> previousSpawnLocations = new List<int>();
                if (aliensPerSpawn > spawnPoints.Length)
                {
                    aliensPerSpawn = spawnPoints.Length - 1;
                }
                aliensPerSpawn = (aliensPerSpawn > totalAliens) ? aliensPerSpawn - totalAliens : aliensPerSpawn;
                for (int i = 0; i < aliensPerSpawn; i++)
                {
                    if (aliensOnScreen < maxAliensOnScreen)
                    {
                        aliensOnScreen += 1;

                        int spawnPoint = -1;

                        while (spawnPoint == -1)
                        {
                            int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                            if (!previousSpawnLocations.Contains(randomNumber))
                            {
                                previousSpawnLocations.Add(randomNumber);
                                spawnPoint = randomNumber;
                            }
                        }
                        GameObject spawnLocation = spawnPoints[spawnPoint];
                        GameObject newAlien = Instantiate(alien) as GameObject;
                        newAlien.transform.position = spawnLocation.transform.position;

                        Alien alienScript = newAlien.GetComponent<Alien>();
                        alienScript.target = player.transform;
                        Vector3 targetRotation = new Vector3(player.transform.position.x, newAlien.transform.position.y, player.transform.position.z);
                        newAlien.transform.LookAt(targetRotation);

                        alienScript.OnDestroy.AddListener(AlienDestroyed);
                        alienScript.GetDeathParticles().SetDeathFloor(deathFloor);
                    }
                }
            }
        }
    }
    public void AlienDestroyed()
    {
        aliensOnScreen -= 1;
        totalAliens -= 1;
        if (totalAliens == 0)
        {
            Invoke("endGame", 2.0f);
        }
    }

    private void endGame()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
        arenaAnimator.SetTrigger("PlayerWon");
    }

}
