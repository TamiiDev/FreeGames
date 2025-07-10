using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;
using System.Web.UI;
using System;

namespace FreeGames
{
    public partial class index : System.Web.UI.Page
    {
        private HttpClient client;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                addPlatform();
                addSortBy();
                addCategory();

                LoadGames();
            }
        }

        void addPlatform()
        {
            var itemPlatform = new[]
                {
                    new { Text = "Platform", Value = "all" },
                    new { Text = "PC", Value = "pc" },
                    new { Text = "Browser", Value = "browser" }
                };

            ddlPlatform.DataSource = itemPlatform;
            ddlPlatform.DataTextField = "Text";
            ddlPlatform.DataValueField = "Value";
            ddlPlatform.DataBind();

            ddlPlatformFilter.DataSource = itemPlatform;
            ddlPlatformFilter.DataTextField = "Text";
            ddlPlatformFilter.DataValueField = "Value";
            ddlPlatformFilter.DataBind();
        }

        void addSortBy()
        {
            var itemSortBy = new[]
                {
                    new { Text = "Sort by", Value = "" },
                    new { Text = "Release Date", Value = "release-date" },
                    new { Text = "Popularity", Value = "popularity" },
                    new { Text = "Alphabetical", Value = "alphabetical" },
                    new { Text = "Relevance", Value = "relevance" }
                };

            ddlSortBy.DataSource = itemSortBy;
            ddlSortBy.DataTextField = "Text";
            ddlSortBy.DataValueField = "Value";
            ddlSortBy.DataBind();

            ddlSortByFilter.DataSource = itemSortBy;
            ddlSortByFilter.DataTextField = "Text";
            ddlSortByFilter.DataValueField = "Value";
            ddlSortByFilter.DataBind();
        }

        void addCategory()
        {
            var itemCategory = new[]
            {
                new { Text = "Category", Value = "" },
                new { Text = "MMORPG", Value = "mmorpg" },
                new { Text = "Shooter", Value = "shooter" },
                new { Text = "Strategy", Value = "strategy" },
                new { Text = "MOBA", Value = "moba" },
                new { Text = "Racing", Value = "racing" },
                new { Text = "Sports", Value = "sports" },
                new { Text = "Social", Value = "social" },
                new { Text = "Sandbox", Value = "sandbox" },
                new { Text = "Open World", Value = "open-world" },
                new { Text = "Survival", Value = "survival" },
                new { Text = "PvP", Value = "pvp" },
                new { Text = "PvE", Value = "pve" },
                new { Text = "Pixel", Value = "pixel" },
                new { Text = "Voxel", Value = "voxel" },
                new { Text = "Zombie", Value = "zombie" },
                new { Text = "Turn Based", Value = "turn-based" },
                new { Text = "First Person", Value = "first-person" },
                new { Text = "Third Person", Value = "third-Person" },
                new { Text = "Top Down", Value = "top-down" },
                new { Text = "Tank", Value = "tank" },
                new { Text = "Space", Value = "space" },
                new { Text = "Sailing", Value = "sailing" },
                new { Text = "Side Scroller", Value = "side-scroller" },
                new { Text = "Superhero", Value = "superhero" },
                new { Text = "Permadeath", Value = "permadeath" },
                new { Text = "Card", Value = "card" },
                new { Text = "Battle Royale", Value = "battle-royale" },
                new { Text = "MMO", Value = "mmo" },
                new { Text = "MMOFPS", Value = "mmofps" },
                new { Text = "MMOTPS", Value = "mmotps" },
                new { Text = "3D", Value = "3d" },
                new { Text = "2D", Value = "2d" },
                new { Text = "Anime", Value = "anime" },
                new { Text = "Fantasy", Value = "fantasy" },
                new { Text = "Sci-Fi", Value = "sci-fi" },
                new { Text = "Fighting", Value = "fighting" },
                new { Text = "Action RPG", Value = "action-rpg" },
                new { Text = "Action", Value = "action" },
                new { Text = "Military", Value = "military" },
                new { Text = "Martial Art", Value = "martial-arts" },
                new { Text = "Flight", Value = "flight" },
                new { Text = "Low Spec", Value = "low-spec" },
                new { Text = "Tower Defense", Value = "tower-defense" },
                new { Text = "Horror", Value = "horror" },
                new { Text = "MMORTS", Value = "mmorts" }
            };

            ddlCategory.DataSource = itemCategory;
            ddlCategory.DataTextField = "Text";
            ddlCategory.DataValueField = "Value";
            ddlCategory.DataBind();

            ddlCategoryFilter.DataSource = itemCategory;
            ddlCategoryFilter.DataTextField = "Text";
            ddlCategoryFilter.DataValueField = "Value";
            ddlCategoryFilter.DataBind();
        }

        private void LoadGames()
        {
            client = new HttpClient();
            string url = "https://www.freetogame.com/api/games";
            string query = "";

            if (!string.IsNullOrEmpty(ddlPlatform.SelectedValue) && ddlPlatform.SelectedValue != "all")
            {
                query += $"platform={ddlPlatform.SelectedValue}&";
            }

            if (!string.IsNullOrEmpty(ddlSortBy.SelectedValue))
            {
                query += $"sort-by={ddlSortBy.SelectedValue}&";
            }

            if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
            {
                query += $"category={ddlCategory.SelectedValue}&";
            }

            // Eliminamos el '&' final si la consulta no está vacía
            if (query.EndsWith("&"))
            {
                // Nueva cadena sin el último carácter
                query = query.Substring(0, query.Length - 1);
            }

            if (!string.IsNullOrEmpty(query))
            {
                url += "?" + query;
            }

            client.DefaultRequestHeaders.Clear();
            var response = client.GetAsync(url).Result;
            var readResponse = response.Content.ReadAsStringAsync().Result;

            List<Game> listGames = JsonConvert.DeserializeObject<List<Game>>(readResponse);

            // Almacenar los datos en la sesión
            Session["listGames"] = listGames;

            lvGames.DataSource = listGames;
            lvGames.DataBind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            client = new HttpClient();
            string url = "https://www.freetogame.com/api/games";

            if (ddlPlatformFilter.SelectedValue != "all" && ddlSortByFilter.SelectedValue != "" && ddlCategoryFilter.SelectedValue != "")
            {
                string query = $"platform={ddlPlatformFilter.SelectedValue}&category={ddlCategoryFilter.SelectedValue}&sort-by={ddlSortByFilter.SelectedValue}";
                url += "?" + query;
                lblprueba.Text = "ENTRÓ";
            }

            client.DefaultRequestHeaders.Clear();
            var response = client.GetAsync(url).Result;
            var readResponse = response.Content.ReadAsStringAsync().Result;

            List<Game> listGames = JsonConvert.DeserializeObject<List<Game>>(readResponse);

            // Almacenar los datos en la sesión
            Session["listGames"] = listGames;

            lvGames.DataSource = listGames;
            lvGames.DataBind();
        }

        protected void ddlPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Setear el SelectedIndex en cero para volver a la primera página 
            DataPagerGames.SetPageProperties(0, DataPagerGames.PageSize, true);
            LoadGames();
        }

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPagerGames.SetPageProperties(0, DataPagerGames.PageSize, true);
            LoadGames();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPagerGames.SetPageProperties(0, DataPagerGames.PageSize, true);
            LoadGames();
        }

        protected void ddlPlatformFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPagerGames.SetPageProperties(0, DataPagerGames.PageSize, true);
            LoadGames();
        }

        protected void ddlSortByFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPagerGames.SetPageProperties(0, DataPagerGames.PageSize, true);
            LoadGames();
        }

        protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPagerGames.SetPageProperties(0, DataPagerGames.PageSize, true);
            LoadGames();
        }

        protected void lvGames_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPagerGames.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            // Cargar los datos desde la sesión
            var listGames = (List<Game>)Session["listGames"];

            lvGames.DataSource = listGames;
            lvGames.DataBind();
        }
    }
}
