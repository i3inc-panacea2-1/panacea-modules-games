using Panacea.Core;
using Panacea.Models;
using Panacea.Modularity.Billing;
using Panacea.Modularity.Content;
using Panacea.Modularity.Favorites;
using Panacea.Modularity.UiManager;
using Panacea.Modularity.WebBrowsing;
using Panacea.Modules.Games.Models;
using Panacea.Modules.Games.ViewModels;
using Panacea.Multilinguality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Games
{
    public class GamesPlugin : ICallablePlugin, IHasFavoritesPlugin, IContentPlugin
    {
        public readonly Translator translator = new Translator("Games");
        public List<ServerItem> Favorites { get; set; }
        private readonly PanaceaServices _core;
        readonly GamesProvider _provider;
        public GamesProvider Provider { get => _provider; }

        public GamesPlugin(PanaceaServices core)
        {
            _core = core;
            _provider = new GamesProvider(core);
        }
        public Task BeginInit()
        {
            return Task.CompletedTask;
        }

        public void Call()
        {
            if (_core.TryGetUiManager(out IUiManager ui))
            {
                var gamesList = new GameListViewModel(_core, this);
                ui.Navigate(gamesList);
            }
            _core.WebSocket.PopularNotifyPage("Games");
        }

        public void Dispose()
        {
            return;
        }

        public async Task OpenItemAsync(ServerItem item)
        {
            var game = item as Game;
            if (game == null) return;
            if (_core.TryGetBilling(out IBillingManager _billing))
            {
                if (!_billing.IsPluginFree("Games"))
                {
                    var service = await _billing.GetOrRequestServiceForItemAsync(translator.Translate("This Game requires service."), "Games", game);
                    if (service == null)
                    {
                        return;
                    }
                }
            }
            switch (game.GameType)
            {
                case "Flash":
                    if (_core.TryGetWebBrowser(out IWebBrowserPlugin _browser))
                    {
                        _core.WebSocket.PopularNotify("Games", "Game", game.Id, _core.UserService.User?.Id ?? "0");
                        _browser.OpenUnmanaged(game.DataUrl.Url);
                    }
                    break;
            }
        }

        public Task EndInit()
        {
            return Task.CompletedTask;
        }

        public Task Shutdown()
        {
            return Task.CompletedTask;
        }

        public Type GetContentType()
        {
            return typeof(Game);
        }
    }
}
