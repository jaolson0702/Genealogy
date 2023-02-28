using KinshipCompute;
using RelCompute;

Rel rel = new Kinship(Kins.Sibling.ToHalf(), Kins.Sibling);

Console.WriteLine(rel);
Console.WriteLine(rel.ToString(Gender.Male));
Console.WriteLine(rel.ToString(Gender.Female));