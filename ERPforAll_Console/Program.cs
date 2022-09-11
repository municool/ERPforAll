using System.CommandLine;
using System;
using System.Threading.Tasks;

namespace ERPforAll_Console;

internal class Program
{
    static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("Console interface for ERPforAll Db");

        rootCommand.AddCommand(CreateGetCommand());
        rootCommand.AddCommand(CreateInsertCommand());

        rootCommand.SetHandler(() =>{
            Console.WriteLine("Test");
        });

        await rootCommand.InvokeAsync(args);
        // if (args.Length == 0)
        // {
        //     var versionString = Assembly.GetEntryAssembly()?
        //                             .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
        //                             .InformationalVersion
        //                             .ToString();

        //     Console.WriteLine($"erpforall v{versionString}");
        //     Console.WriteLine("-------------");
        //     Console.WriteLine("\nUsage:");
        //     Console.WriteLine("  erpforall <command>");
        //     return;
        // }

        //TODO: Commands to add:
        //          - create 
        //          - get (get all or id)
        //          - update
        //          - remove 

    }

    static Command CreateInsertCommand()
    {
        return new Command("test");
    }

    static Command CreateGetCommand()
    {
        var getCommand = new Command("get", "Get entries from the db");
        var dataOption = new Option<string>("--data", "Which informations you want to get");
        var idOption = new Option<int>("--id", "only gets the entry with the given id");

        getCommand.AddOption(dataOption);
        getCommand.AddOption(idOption);

        var getHandler = new GetHandler();

        getCommand.SetHandler((data, id) => getHandler.Handle(data, id), dataOption, idOption);

        return getCommand;
    }

}