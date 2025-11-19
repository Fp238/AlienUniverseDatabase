using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlienUniverseDatabaseIII.Models;

namespace AlienUniverseDatabaseIII.Services;

public static class FilmLoader
{
    public static List<Film> LoadFromFile(string path)
    {
        if (!File.Exists(path))
            return new List<Film>();

        var lines = File.ReadAllLines(path)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        var films = new List<Film>();

        for (int i = 0; i + 11 < lines.Count; i += 12)
        {
            films.Add(new Film
            {
                TytulOryginalny = lines[i],
                TytulPolski     = lines[i + 1],
                RokPremiery     = int.Parse(lines[i + 2]),
                Rezyser         = lines[i + 3],
                Scenariusz      = lines[i + 4],
                Gatunek         = lines[i + 5],
                CzasTrwania     = lines[i + 6],
                Ocena           = lines[i + 7],
                GlownePostacie  = lines[i + 8],
                Statek          = lines[i + 9],
                OpisFabuly      = lines[i + 10],
                Ciekawostka     = lines[i + 11]
            });
        }

        return films;
    }
}