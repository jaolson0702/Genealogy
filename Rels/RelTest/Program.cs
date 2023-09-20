using Rels;

Rel rel = RelFactory.Progenitor(2) + RelFactory.Cousin();

Console.WriteLine(rel);