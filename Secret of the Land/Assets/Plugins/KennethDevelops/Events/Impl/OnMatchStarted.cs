using System;

namespace Plugins.KennethDevelops.Events.Impl {
    
    [Serializable]
    public class OnMatchStarted : BaseEvent<OnMatchStarted> {
        
        public MatchType matchType;
        public string    matchHostUsername;
        public int       matchHostId;
        
        public bool waitForPlayersToJoin;
        public bool crossplatform;

        public bool        triggerGameStart;
        public OnGameStart onGameStart;
    }
    
    public enum MatchType {
        Solo,
        Multiplayer,
        StreamMode
    }
}