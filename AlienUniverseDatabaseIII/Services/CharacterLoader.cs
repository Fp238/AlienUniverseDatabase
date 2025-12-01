using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlienUniverseDatabaseIII.Models;

namespace AlienUniverseDatabaseIII.Services
{
    public static class CharacterLoader
    {
        public static List<Character> LoadFromFile(string path)
        {
            if (!File.Exists(path))
                return new List<Character>();

            var lines = File.ReadAllLines(path)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            var characters = new List<Character>();

            for (int i = 0; i + 6 < lines.Count; i += 7)
            {
                characters.Add(new Character
                {
                    Name = lines[i],
                    Films = lines[i + 1],
                    Role = lines[i + 2],
                    Actor = lines[i + 3],
                    Species = lines[i + 4],
                    BirthYear = lines[i + 5],
                    Summary = lines[i + 6]
                });
            }

            return characters;
        }
    }
}