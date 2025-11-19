using System;
using System.Collections.ObjectModel;
using System.IO;
using AlienUniverseDatabaseIII.Models;
using AlienUniverseDatabaseIII.Services;
using ReactiveUI;

namespace AlienUniverseDatabaseIII.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Film> Films { get; } = new();

    private Film? _selectedFilm;
    public Film? SelectedFilm
    {
        get => _selectedFilm;
        set => this.RaiseAndSetIfChanged(ref _selectedFilm, value);
    }

    public MainWindowViewModel()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Assets", "films.txt");
        
        
        if (!File.Exists(path))
            Console.WriteLine($"Plik nie znaleziony: {path}");
        else
            Console.WriteLine($"Plik znaleziony: {path}");

        
        var loaded = FilmLoader.LoadFromFile(path);

        foreach (var film in loaded)
        {
            Films.Add(film);
        }
    }
}