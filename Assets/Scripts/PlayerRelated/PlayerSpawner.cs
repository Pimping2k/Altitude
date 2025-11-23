using System;
using UnityEngine;

namespace PlayerRelated
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _spawnPoint;

        private void Start()
        {
            Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);
        }
    }
}