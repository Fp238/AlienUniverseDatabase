using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using AlienUniverseDatabaseIII.Models;
using AlienUniverseDatabaseIII.Services;
using ReactiveUI;
using System.Reactive;

namespace AlienUniverseDatabaseIII.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Film> Films { get; } = new();
        public ObservableCollection<Character> Characters { get; } = new();
        public ObservableCollection<Character> FilteredCharacters { get; } = new();

        private Film? _selectedFilm;
        public Film? SelectedFilm
        {
            get => _selectedFilm;
            set => this.RaiseAndSetIfChanged(ref _selectedFilm, value);
        }

        private Character? _selectedCharacter;
        public Character? SelectedCharacter
        {
            get => _selectedCharacter;
            set => this.RaiseAndSetIfChanged(ref _selectedCharacter, value);
        }

        private string _selectedRace = "Wszystkie rasy";
        public string SelectedRace
        {
            get => _selectedRace;
            set => this.RaiseAndSetIfChanged(ref _selectedRace, value);
        }

        public ObservableCollection<string> Races { get; } = new() { "Wszystkie rasy" };

        public ReactiveCommand<Unit, Unit> ShowCharactersCommand { get; }

        public MainWindowViewModel()
        {
            ShowCharactersCommand = ReactiveCommand.Create(UpdateFilteredCharacters);

            LoadFilms();
            LoadCharacters();
            GenerateRaces();
        }

        private void LoadFilms()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Assets", "films.txt");

            if (!File.Exists(path))
            {
                Console.WriteLine($"Plik nie znaleziony: {path}");
                return;
            }

            var loaded = FilmLoader.LoadFromFile(path);

            foreach (var film in loaded)
                Films.Add(film);
        }

        private void LoadCharacters()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Assets", "characters.txt");

            if (!File.Exists(path))
            {
                Console.WriteLine($"Plik nie znaleziony: {path}");
                return;
            }

            var loaded = CharacterLoader.LoadFromFile(path);

            foreach (var character in loaded)
                Characters.Add(character);
        }

        private void GenerateRaces()
        {
            var races = Characters
                .Select(c => c.Species)
                .Distinct()
                .OrderBy(r => r);

            foreach (var race in races)
                if (!string.IsNullOrWhiteSpace(race))
                    Races.Add(race);
        }

        public void UpdateFilteredCharacters()
        {
            FilteredCharacters.Clear();

            if (SelectedFilm == null)
                return;

            var filtered = Characters
                .Where(c => c.Films.Contains(SelectedFilm.TytulOryginalny))
                .Where(c => SelectedRace == "Wszystkie rasy" || c.Species == SelectedRace);

            foreach (var c in filtered)
                FilteredCharacters.Add(c);
        }
    }
}
