using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panacea.Controls;
using Panacea.Core;
using Panacea.Modularity.UiManager;
using Panacea.Modules.Games.Models;
using Panacea.Mvvm;

namespace Panacea.Modules.Games.ViewModels
{
    [View(typeof(GameMiniPresenterViewModel))]
    public class GameMiniPresenterViewModel : PopupViewModelBase<object>
    {
        private PanaceaServices _core;
        private GamesPlugin _plugin;
        Game _game;
        public Game Game
        {
            get => _game;
            set
            {
                _game = value;
                SetupCommands();
                OnPropertyChanged();
            }
        }

        public GameMiniPresenterViewModel(PanaceaServices core, GamesPlugin plugin, Game game)
        {
            _core = core;
            _plugin = plugin;
            _game = game;
        }

        public RelayCommand ExecuteGameCommand { get; private set; }

        void SetupCommands()
        {
            var canExecute = CanExecute();
            ExecuteGameCommand = new RelayCommand(ExecuteGame,
            (arg) => canExecute);
        }
        bool CanExecute()
        {
            if (Game == null) return false;
            return Game.GameType == "Flash";
        }
        async void ExecuteGame(object arg)
        {
            SetResult(null);
            await _plugin.OpenItemAsync(arg as Game);
        }
    }
}
