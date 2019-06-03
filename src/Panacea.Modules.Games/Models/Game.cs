using Panacea.Models;
using Panacea.Multilinguality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Panacea.Modules.Games.Models
{
    [DataContract]
    public class Game : ServerItem
    {
        public Game()
            : base()
        {
           
        }
        [IsTranslatable(72)]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        private String _gameType;

        [DataMember(Name = "gameType")]
        public String GameType
        {
            get
            {
                return this._gameType;
            }
            set
            {
                if (value != _gameType)
                {
                    this._gameType = value;
                }
            }
        }
        private DataUrl _dataUrl;
        [DataMember(Name = "dataUrl")]
        public DataUrl DataUrl
        {
            get
            {
                return this._dataUrl;
            }
            set
            {
                if (value != _dataUrl)
                {
                    this._dataUrl = value;
                }
            }
        }

        private Color _tileColor;

        [DataMember(Name = "tileColor")]
        public Color TileColor
        {
            get
            {
                return this._tileColor;
            }
            set
            {
                if (value != _tileColor)
                {
                    this._tileColor = value;
                }
            }
        }

        private int _rating;

        [DataMember(Name = "rating")]
        public int Rating
        {
            get
            {
                return this._rating;
            }
            set
            {
                if (value != _rating)
                {
                    this._rating = value;
                }
            }
        }
    }

    [DataContract]
    public class DataUrl
    {
        public DataUrl()
        {
        }
        public DataUrl(String dataType, String url)
        {
            this.DataType = dataType;
            this.Url = url;
        }

        [DataMember(Name = "dataType")]
        public String DataType { get; set; }

        private String _url;

        [DataMember(Name = "url")]
        public String Url
        {
            get
            {
                return this._url;
            }
            set
            {
                _url = value;
            }
        }
    }
}