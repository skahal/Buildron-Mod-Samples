using System;
using NanoHttpServer;
using Skahal.Logging;

namespace RemoteCommandMod
{
	public class ServerLog : ILog
	{
		private ISHLogStrategy m_underlying;

		public ServerLog (ISHLogStrategy underlying)
		{
			m_underlying = underlying;
		}

		#region ILog implementation

		public void Debug (string message, params object[] args)
		{
			m_underlying.Debug (message, args);
		}

		public void Warning (string message, params object[] args)
		{
			m_underlying.Warning (message, args);
		}

		public void Error (string message, params object[] args)
		{
			m_underlying.Error (message, args);
		}

		#endregion
	}
}

