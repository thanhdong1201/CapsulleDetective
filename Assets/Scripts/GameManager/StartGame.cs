using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Start()
    {
        Transform playerGameObject = Instantiate(player, transform.position, Quaternion.identity);
    }
}
