using MyZmei;
var input = Console.ReadLine();

while(input != null && input != "stop")
{
    Console.WriteLine(Z_Python.Eval(input));
    input = Console.ReadLine();
} 