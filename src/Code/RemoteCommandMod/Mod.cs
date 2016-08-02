using System;
using System.IO;
using Buildron.Domain.Mods;
using NanoHttpServer;
using UnityEngine;
using Skahal.Logging;
using System.Threading;

namespace RemoteCommandMod
{
	public class Mod : IMod, IDisposable
	{
		private ModController m_controller;
        private HttpServer m_server;
		private string m_screenshotPath;

        public void Initialize (IModContext context)
		{
			var log = context.Log;
			m_screenshotPath = Path.Combine(Application.temporaryCachePath, "screenshot.png");
            m_controller = new GameObject("RemoteCommandModController").AddComponent<ModController>();
            m_controller.ScreenshotPath = m_screenshotPath;            

			// Handlers.
			var welcome = new HtmlHandler ("/", "<h4>Welcome to Buildron's RemoteComandMod ;)</h4><br>Available commands: <a href='/screenshot'>screenshot</a>");

			// This handler take the screenshot...
			var takeScreenshot = new CallbackHandler ("/screenshot", (ctx) => {
				TakeScreenshot (log);                
			});

			// then this handler write the image to response.
			var responseScreenshot = new ImageHandler ("/screenshot", m_screenshotPath);

            m_server = new HttpServer (new ServerLog(context.Log),  welcome, takeScreenshot, responseScreenshot);
            m_server.Start ();			
		}

		void TakeScreenshot (ISHLogStrategy log)
		{
			log.Debug ("Taking screenshot...");
            m_controller.TakeScreenshot();
            Thread.Sleep(2000);
			log.Debug ("Screenshot saved to {0}", m_screenshotPath);
		}

        public void Dispose()
        {
            m_server.Stop();
            m_server = null;
        }
    }
}
