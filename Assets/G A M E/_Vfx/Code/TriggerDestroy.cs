
using UnityEngine;

public class TriggerDestroy : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); 
        }
        Instantiate(_brickPrefab, transform.position, Quaternion.identity);
    }
}