using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORTS.Core.GameObjects;
namespace ORTS.Core
{
    public class GameState
    {
        public List<Player> Players { get; set; }
        public List<IGameObject> GameObjects { get; set; }
        public GameState()
        {
            Players = new List<Player>();
            GameObjects = new List<IGameObject>();
        }
    }
}
