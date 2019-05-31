using Panacea.ContentControls;
using Panacea.Core;
using Panacea.Modules.Games.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Games
{
    public class GamesProvider : HospitalServerLazyItemProvider<Game>
    {
        public GamesProvider(PanaceaServices core)
            : base(core, "games_collection/get_categories_only/", "games_collection/get_category_limited/{0}/{1}/{2}/", "games_collection/find_games/{0}/{1}/{2}/{3}/", 10)
        {

        }

    }
}
