using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log($"Interacting with ${transform.gameObject.name}");
    }
}
