using UnityEngine;

public class TextScroller : MonoBehaviour
{
    public enum Dir
    {
        None = 0,
        Left = -1,
        Right = 1,
    }
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float startPosition;
    [SerializeField] private Dir dir = Dir.None;
    [SerializeField] private float minWidth;
    [SerializeField] private float speed;

    private void Start()
    {
        startPosition = transform.localPosition.x;
    }

    private void Update()
    {
        if (rectTransform.sizeDelta.x <= minWidth) return;
        float position = transform.localPosition.x;
        position += speed * (int)dir;

        if(position < startPosition +(rectTransform.sizeDelta.x * (int)dir))
        {
            position = startPosition + -(minWidth * (int)dir);
        }

        transform.localPosition = new Vector3(
            position,
            rectTransform.localPosition.y,
            rectTransform.localPosition.z
            );
    }
}
