using AutoIniProperty;

Console.WriteLine("Begin");

var main = new Main();
var one = main.GetOne;
Console.WriteLine("Count: {0}", one.Count());
Console.WriteLine("Count: {0}", one.Count());
one = main.GetOne;
Console.WriteLine("Count: {0}", one.Count());
Console.WriteLine("Count: {0}", one.Count());
var two = main.GetOne;
Console.WriteLine("Count: {0}", two.Count());
Console.WriteLine("Count: {0}", two.Count());

Console.WriteLine("End");