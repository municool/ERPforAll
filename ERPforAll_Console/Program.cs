using System.CommandLine;
using System;
using System.Threading.Tasks;

namespace ERPforAll_Console;

internal class Program
{
    static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("Console interface for ERPforAll Db");

        rootCommand.SetHandler(() =>{
            Console.WriteLine("Test");
        });

        var idOption = new Option<int>("--id");
        var dataOption = new Option<string>("--data");

        var vendorsCommand = new Command("vendors"); 
        vendorsCommand.AddOption(idOption);
        var customersCommand = new Command("customers");
        customersCommand.AddOption(idOption);
        var revenueCommand = new Command("revenue");
        revenueCommand.AddOption(idOption);
        var pendingPaymentsCommand = new Command("ppay");
        pendingPaymentsCommand.AddOption(idOption);
        var warehousesCommand = new Command("warehouse");
        warehousesCommand.AddOption(idOption);
        var stocksCommand = new Command("stock");
        stocksCommand.AddOption(idOption);
        var sellsCommand = new Command("sells");
        sellsCommand.AddOption(idOption);
        var purchasesCommand = new Command("purchase");
        purchasesCommand.AddOption(idOption);

        //Get Command
        var getCommand = new Command("get", "Get entries from the db");


        rootCommand.AddCommand(getCommand);
        await rootCommand.InvokeAsync(args);
    }
}