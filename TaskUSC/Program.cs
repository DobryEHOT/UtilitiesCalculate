// See https://aka.ms/new-console-template for more information
using TaskUSC.Architecture.GoF.Creational;
using TaskUSC.Debug;
using TaskUSC.States.Global;

Console.WriteLine("Hello, World!");
PreInitialise();
var globalStateMachine = new GlobalStateMachine();
globalStateMachine.Run();

Console.ReadKey();



void PreInitialise()
{
    Singleton<Debuger>.MainSingleton.SetLogMethod(Console.WriteLine);
}
