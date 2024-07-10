using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraCapture : MonoBehaviour
{
    public RenderTexture renderTexture;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CaptureAndSaveImage());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CaptureAndSaveImage()
    {
        yield return new WaitForEndOfFrame();

        // Render Texture 변수에서 이미지 읽어오기
        RenderTexture.active = renderTexture;
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        // 읽어온 이미지 JPEG 포맷으로 디코딩
        byte[] bytes = texture2D.EncodeToJPG();

        // 디코딩 결과 byte 자료형 배열에 저장
        string path = Path.Combine(Application.dataPath, "CapturedImage.jpg");
        File.WriteAllBytes(path, bytes);

        Debug.Log("Image saved to: " + path);

        // Clean up
        RenderTexture.active = null;
        Destroy(texture2D);
    }
}
