using System;

namespace Plugins.KennethDevelops.Events.Impl {

    [Serializable]
    public class OnGameOver : BaseEvent<OnGameOver> {

        public int winnerId;

    }
}