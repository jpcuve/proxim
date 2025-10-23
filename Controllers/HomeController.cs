using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proxim.Models;
using MySql.Data.MySqlClient;

namespace proxim.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        var cs = @"server=10.0.0.7;userid=gate;password=ga33ere;database=gate";
        using var con = new MySqlConnection(cs);
        con.Open();
        Console.WriteLine($"MySQL version: {con.ServerVersion}");
        var cmd = new MySqlCommand("select version()", con);
        var version = cmd.ExecuteScalar().ToString();
        Console.WriteLine($"MySQL version: {version}");
        var cmd2 = new MySqlCommand("select identifier from clients", con);
        using var rdr = cmd2.ExecuteReader();
        while (rdr.Read())
        {
            Console.WriteLine(rdr.GetString(0));
        }
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}