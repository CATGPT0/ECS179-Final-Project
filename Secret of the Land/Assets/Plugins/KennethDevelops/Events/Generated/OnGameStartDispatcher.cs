using Plugins.KennethDevelops.Events;
using Plugins.KennethDevelops.Events.Impl;

// This class is automatically generated.
// Modifications will be overwritten.

namespace Plugins.KennethDevelops.Events
{
	public class OnGameStartDispatcher : BaseDispatcher<OnGameStart>
	{
		public OnGameStart eventData;
		protected override BaseEvent<OnGameStart> GetEventData()
		{
			return eventData;
		}
	}
}
