using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Panacea.Controls;
using Panacea.Core;
using Panacea.Modularity.UiManager;
using Panacea.Modules.Games;
using Panacea.Modules.Games.Models;
using Panacea.Modules.Games.Views;
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
        public ICommand FavoriteCommand { get; private set; }

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

            IsFavoriteCommand = new RelayCommand((arg) => { _core.Logger.Debug(this, "TODO: FAVORITES"); });
            //TODO: FAVORITES!!!
            //IsFavoriteCommand = new RelayCommand((arg) =>
            //{
            //}, (arg) =>
            //{
            //    var game = arg as Game;
            //    if (plugin.Favorites == null) return false;
            //    return plugin.Favorites.Any(l => l.Id == game.Id);
            //});
            FavoriteCommand = new RelayCommand((arg) => { _core.Logger.Debug(this, "TODO: FAVORITES"); });
            //FavoriteCommand = new RelayCommand((args) =>
            //{
            //    if (userManager.User.ID == null)
            //    {
            //        window.RequestLogin(new Translator("Games").Translate("You need an account to add favorites"),
            //            null, null);
            //        return;
            //    }
            //    var game = args as Game;
            //    if (game == null) return;
            //    try
            //    {
            //        if (plugin.Favorites.Any(mm => mm.Id == game.Id))
            //        {
            //            _server.FavoriteRemove("Games", game.Id);
            //            plugin.Favorites.Remove(plugin.Favorites.First(mm => mm.Id == game.Id));
            //            window.ThemeManager.Toast(new Translator("Games").Translate("This game has been removed from your favorites"));
            //        }
            //        else
            //        {
            //            _server.FavoriteNotify("Games", game.Id);
            //            plugin.Favorites.Add(game);
            //            window.ThemeManager.Toast(new Translator("Games").Translate("This game has been added to your favorites"));
            //        }
            //        OnPropertyChanged("IsFavoriteCommand");
            //    }
            //    catch
            //    {
            //    }
            //});
        }
    }
}
