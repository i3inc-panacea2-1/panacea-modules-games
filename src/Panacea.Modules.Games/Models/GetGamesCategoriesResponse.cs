using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Games.Models
{
    [DataContract]
    public class GetGamesCategoriesResponse
    {
        [DataMember(Name = "Games")]
        public GameCategoryWrapper GamesCollection { get; set; }
    }

    [DataContract]
    public class GameCategoryWrapper
    {
        [DataMember(Name = "gameCategories")]
        public List<GamesCategory> GameCategories { get; set; }
    }
}
