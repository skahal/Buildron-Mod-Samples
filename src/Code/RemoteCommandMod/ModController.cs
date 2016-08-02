using System;
using UnityEngine;
using System.Collections;
using System.IO;

namespace RemoteCommandMod
{
	public class ModController : MonoBehaviour
	{
		private bool m_takeScreenshot;

        public string ScreenshotPath { get; set; }

        void Start()
        {
            StartCoroutine(WaitTakeScreenshot());
        }

        public void TakeScreenshot()
        {
            m_takeScreenshot = true;
        }

        IEnumerator WaitTakeScreenshot()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                if (m_takeScreenshot)
                {
					yield return new WaitForEndOfFrame();
                 	var tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
					tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
					tex.Apply();
					File.WriteAllBytes(ScreenshotPath, tex.EncodeToPNG());
                    m_takeScreenshot = false;
                }
            }
        }
    }
}
