using Plugins.KennethDevelops.Events;
using Plugins.KennethDevelops.Events.Impl;

// This class is automatically generated.
// Modifications will be overwritten.

namespace Plugins.KennethDevelops.Events
{
	public class OnGameOverDispatcher : BaseDispatcher<OnGameOver>
	{
		public OnGameOver eventData;
		protected override BaseEvent<OnGameOver> GetEventData()
		{
			return eventData;
		}
	}
}
