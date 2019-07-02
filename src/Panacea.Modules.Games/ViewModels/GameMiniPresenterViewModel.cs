using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Panacea.Controls;
using Panacea.Core;
using Panacea.Modularity.UiManager;
using Panacea.Modules.Games.Models;
using Panacea.Modules.Games.Views;
using Panacea.Mvvm;

namespace Panacea.Modules.Games.ViewModels
{
    [View(typeof(GameMiniPresenter))]
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
                OnPropertyChanged();
            }
        }

        public GameMiniPresenterViewModel(PanaceaServices core, GamesPlugin plugin, Game game)
        {
            _core = core;
            _plugin = plugin;
            _game = game;
            SetupCommands();
        }
        void SetupCommands()
        {
            var canExecute = CanExecute();
            ExecuteGameCommand = new RelayCommand(ExecuteGame,
            (arg) => canExecute);
        }

        ICommand _executeGameCommand;
        public ICommand ExecuteGameCommand {
            get => _executeGameCommand;
            protected set
            {
                _executeGameCommand = value;
                OnPropertyChanged();
            }
        }

        bool CanExecute()
        {
            if (Game == null) return false;
            return Game.GameType == "Flash";
        }
        async void ExecuteGame(object arg)
        {
            SetResult(null);
            await _plugin.OpenItemAsync(Game);
        }
    }
}
