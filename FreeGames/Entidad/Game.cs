using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeGames
{
    public class Game
    {
        public string title { get; set; }
        public string thumbnail { get; set; }
        public string short_description { get; set; }
        public string game_url { get; set; }
        public string genre { get; set; }
        public string platform { get; set; }
        public string release_date { get; set; }
    }
}