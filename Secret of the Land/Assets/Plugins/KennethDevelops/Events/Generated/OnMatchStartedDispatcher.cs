using Plugins.KennethDevelops.Events;
using Plugins.KennethDevelops.Events.Impl;

// This class is automatically generated.
// Modifications will be overwritten.

namespace Plugins.KennethDevelops.Events
{
	public class OnMatchStartedDispatcher : BaseDispatcher<OnMatchStarted>
	{
		public OnMatchStarted eventData;
		protected override BaseEvent<OnMatchStarted> GetEventData()
		{
			return eventData;
		}
	}
}
