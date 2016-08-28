using Buildron.Domain.Builds;
using Buildron.Domain.Mods;
using UnityEngine;

namespace ToastyMod
{
    /// <summary>
    /// A mod to mimics the Mortal Kombat's Toasty! easter egg.
    /// </summary>
    public class Mod : IMod 
	{
		public void Initialize (IModContext context)
		{
			context.Preferences.Register (
				new Preference ("ShowOnSuccess", "Show on success", PreferenceKind.Bool, true),
				new Preference ("ShowOnRunning", "Show on running", PreferenceKind.Bool, false),
				new Preference ("ShowOnFailed", "Show on failed", PreferenceKind.Bool, false));

			var holder = context.CreateGameObjectFromPrefab("ToastyHolderPrefab");

			context.BuildStatusChanged += (sender, e) => {
				var status = e.Build.Status;
				var preferences = context.Preferences;
				var active = false;

				switch(status)
				{  
				case BuildStatus.Success:
					active = preferences.GetValue<bool>("ShowOnSuccess");
					break;
						
				case BuildStatus.Running:
					active = preferences.GetValue<bool>("ShowOnRunning");
					break;

				case BuildStatus.Failed:
					active = preferences.GetValue<bool>("ShowOnFailed");
					break;
				}

				holder.SetActive(active);
			};
		}		
	}
}
