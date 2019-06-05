using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Panacea.Controls;
using Panacea.Core;
using Panacea.Modularity.Favorites;
using Panacea.Modularity.UiManager;
using Panacea.Modules.Games;
using Panacea.Modules.Games.Models;
using Panacea.Modules.Games.Views;
using Panacea.Multilinguality;
using Panacea.Mvvm;

namespace Panacea.Modules.Games.ViewModels
{
    [View(typeof(GameList))]
    public class GameListViewModel : ViewModelBase
    {
        private readonly PanaceaServices _core;
        private readonly GamesPlugin _plugin;
        public GamesProvider Provider { get; }
        public ICommand ItemClickCommand { get; private set; }
        public ICommand InfoClickCommand { get; private set; }
        public ICommand IsFavoriteCommand { get; private set; }
        public AsyncCommand FavoriteCommand { get; private set; }

        public GameListViewModel(PanaceaServices core, GamesPlugin plugin)
        {
            _core = core;
            _plugin = plugin;
            Provider = plugin.Provider;
            SetupCommands();
        }

        private void SetupCommands()
        {
            ItemClickCommand = new RelayCommand(async (arg) =>
            {
                if (_plugin == null) return;
                await _plugin.OpenItemAsync(arg as Game);
            });

            InfoClickCommand = new RelayCommand((arg) =>
            {
                if (_core.TryGetUiManager(out IUiManager _ui))
                {
                    _ui.HideKeyboard();
                    var gmp = new GameMiniPresenterViewModel(_core, _plugin, arg as Game);
                    var pop = _ui.ShowPopup<object>(gmp, "", PopupType.None);
                } else
                {
                    _core.Logger.Error(this, "ui manager not loaded");
                }
            });
            IsFavoriteCommand = new RelayCommand((arg) =>
            {
            }, (arg) =>
            {
                var game = arg as Game;
                if (_plugin.Favorites == null) return false;
                return _plugin.Favorites.Any(l => l.Id == game.Id);
            });

            FavoriteCommand = new AsyncCommand(async (args) =>
            {
                var game = args as Game;
                if (game == null) return;
                if (_core.TryGetFavoritesPlugin(out IFavoritesManager _favoritesManager)){
                    try
                    {
                        await _favoritesManager.AddOrRemoveFavoriteAsync("Games", game);
                        OnPropertyChanged(nameof(IsFavoriteCommand));
                    } catch(Exception e)
                    {
                        _core.Logger.Error(this, e.Message);
                    }
                }
            });
        }
    }
}
