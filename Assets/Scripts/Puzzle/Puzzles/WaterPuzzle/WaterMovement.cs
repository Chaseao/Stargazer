using UnityEngine;
using UnityEngine.UI;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] float width;

    Vector3 direction = Vector3.right;

    public void SetSpeed(float speed)
    {
        direction.x = speed;
    }

    public void Update()
    {
        if (transform.localPosition.x > Screen.width)
        {
            GetComponent<RectTransform>().localPosition = Vector2.left * Screen.width;
        }

        transform.position += direction * Time.deltaTime;
    }
}
