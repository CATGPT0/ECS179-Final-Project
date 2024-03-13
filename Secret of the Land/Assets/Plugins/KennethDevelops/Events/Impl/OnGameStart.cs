using System;

namespace Plugins.KennethDevelops.Events.Impl {
    
    [Serializable]
    public class OnGameStart : BaseEvent<OnGameStart> {

        public GameModeType gameMode;
        public DateTime     startMatchTime;

    }

    public enum GameModeType {
        Solo,
        Multiplayer,
        Coop
    }
}