using UnityEngine;

public class Camera189 : MonoBehaviour
{
    public Camera cameraGameplay;
    public bool contantWidth = true;
    public float originOrthographicSize = 6.4f;

    float heightConfig = 1920;
    float widthConfig = 1080;
    float ratio;

    private void Awake()
    {
        ratio = heightConfig * 1.0f / widthConfig;
        SetOrthographicSize();
    }

    private void SetOrthographicSize()
    {
        float num = (float)Screen.height / (float)Screen.width;
        if (num > ratio && contantWidth)
        {
            // contant nghia la khi screen thay doi thi width hoac height cua camera khong doi
            cameraGameplay.orthographicSize = originOrthographicSize * (Screen.height * 1.0f / Screen.width) * (widthConfig / heightConfig); // contant width
        }
        else if(contantWidth == false)
        {
            cameraGameplay.orthographicSize = originOrthographicSize; // contant height
        }
    }
}