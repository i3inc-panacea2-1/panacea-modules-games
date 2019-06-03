using Panacea.Models;
using Panacea.Multilinguality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Games.Models
{
    [DataContract]
    public class GamesResponse
    {
        [DataMember(Name = "GamesCollection")]
        public GamesCollection GamesCollection { get; set; }
    }

    [DataContract]
    public class GamesCollection
    {
        [DataMember(Name = "gamesCategories")]
        public List<GamesCategory> Categories { get; set; }
    }



    [DataContract]
    public class GamesCategory : ServerGroupItem
    {
        public GamesCategory()
            : base()
        {
            //this.name = new TranslatableObject();
            //this.Description = new TranslatableObject();
        }
        /*
        public GamesCategory(String id, String title, List<GameItem> gameItems)
            : base(id)
        {
            this.name = new TranslatableObject(title);
            this.games = gameItems;
        }
        private TranslatableObject _name;
        public TranslatableObject name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != _name)
                {
                    this._name = value;
                }
            }
        }
        */

        [IsTranslatable(72)]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        private int _priority;
        [DataMember(Name = "priority")]
        public int Priority
        {
            get
            {
                return this._priority;
            }
            set
            {
                if (value != _priority)
                {
                    this._priority = value;
                }
            }
        }

        private List<GameItem> _games;

        [DataMember(Name = "games")]
        public List<GameItem> Games
        {
            get
            {
                return this._games;
            }
            set
            {
                this._games = value;
            }
        }
        /*
        private String _img;
        public String img
        {
            get
            {
                return this._img;
            }
            set
            {
                if (value != _img)
                {
                    this._img = value;
                    NotifyPropertyChanged("img");
                    Utils.ImageFromWeb(value, (file) =>
                    {
                        if (value != _img)
                        {
                            this._img = file;
                            NotifyPropertyChanged("img");
                        }
                    });
                }
            }
        }
        */
    }
}
