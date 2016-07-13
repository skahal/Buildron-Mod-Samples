using Buildron.Domain.Builds;
using Buildron.Domain.Mods;
using UnityEngine;

namespace ToastyMod
{
    public class Mod : IMod 
	{
		#region IMod implementation
		public void Initialize (IModContext context)
		{
			var holder = GameObject.Instantiate(context.Assets.Load ("ToastyHolderPrefab") as Object) as GameObject;

			context.BuildStatusChanged += (sender, e) => {
				if (e.Build.Status == BuildStatus.Success)
				{
					holder.SetActive(true);
				}
			};
		}
		#endregion
	}
}
