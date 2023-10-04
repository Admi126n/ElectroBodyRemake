using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] int speed = 6;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<GameObject> cannons;

    private Camera _mainCamera;
    GameObject targerPosition;
    bool moveEnabled = false;

    private readonly List<GameObject> _clonedEnemies = new();
    private readonly List<GameObject> _clonedCannons = new();

    private void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        targerPosition = new();

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        foreach (GameObject cannon in cannons)
        {
            cannon.SetActive(false);
        }
    }

    private void Update()
    {
        if (moveEnabled)
        {
            _mainCamera.transform.position = Vector3.Lerp(_mainCamera.gameObject.transform.position,
                targerPosition.transform.position,
                speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Player) && !collision.isTrigger)
        {
            targerPosition.transform.position = new(gameObject.transform.position.x, gameObject.transform.position.y, -10f);
            moveEnabled = true;
            SpawnEnemies();
            SpawnCannons();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Player) && !collision.isTrigger)
        {
            moveEnabled = false;
            StartCoroutine(WaitBeforeDespawning());
        }
    }

    private IEnumerator WaitBeforeDespawning()
    {
        yield return new WaitForSeconds(0.4f);

        DespawnEnemies();
        DespawnCannons();
    }

    private void SpawnEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            GameObject newEnemy = Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            newEnemy.SetActive(true);
            _clonedEnemies.Add(newEnemy);
        }
    }

    private void DespawnEnemies()
    {
        int destroyedEnemies = 0;

        for (int i = 0; i < _clonedEnemies.Count; i++)
        {
            if (_clonedEnemies[i] == null || !_clonedEnemies[i].GetComponent<BoxCollider2D>().isActiveAndEnabled)
            {
                Destroy(enemies[i - destroyedEnemies]);
                enemies.RemoveAt(i - destroyedEnemies);
                destroyedEnemies++;
            }

            Destroy(_clonedEnemies[i]);
        }

        _clonedEnemies.Clear();
    }

    private void SpawnCannons()
    {
        foreach (GameObject cannon in cannons)
        {
            GameObject newCannon = Instantiate(cannon, cannon.transform.position, cannon.transform.rotation);
            newCannon.SetActive(true);
            _clonedCannons.Add(newCannon);
        }
    }

    private void DespawnCannons()
    {
        int destroyedCannonss = 0;
        bool isActive = true;

        for (int i = 0; i < _clonedCannons.Count; i++)
        {
            EnemyBullet[] bullets = _clonedCannons[i].GetComponentsInChildren<EnemyBullet>();
            foreach (EnemyBullet bullet in bullets)
            {
                Destroy(bullet.gameObject);
            }

            try
            {
                isActive = _clonedCannons[i].GetComponent<Cannon>().IsActive();
            }
            catch (NullReferenceException) { }

            if (isActive)
            {
                Destroy(_clonedCannons[i]);
            }
            else
            {
                Destroy(cannons[i - destroyedCannonss]);
                cannons.RemoveAt(i - destroyedCannonss);
                destroyedCannonss++;
            }
        }
        _clonedCannons.Clear();
    }
}
