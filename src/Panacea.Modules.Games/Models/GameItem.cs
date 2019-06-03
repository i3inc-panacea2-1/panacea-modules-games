using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Games.Models
{
    [DataContract]
    public class GameItem
    {
        public GameItem()
        {
            this.Game = new Game();
        }
        public GameItem(Game g, int priority)
        {
            this.Game = g;
            this.Priority = priority;
        }
        [DataMember(Name = "game")]
        public Game Game { get; set; }

        [DataMember(Name = "priority")]
        public int Priority { get; set; }
    }
}
