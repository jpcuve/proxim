using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using proxim.Models;

namespace proxim.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MySqlConnection _mysqlConnection;

    public HomeController(
        ILogger<HomeController> logger,
        MySqlConnection mysqlConnection
    ) {
        _logger = logger;
        _mysqlConnection = mysqlConnection;
        _mysqlConnection.Open();
        Console.WriteLine($"MySQL version: {_mysqlConnection.ServerVersion}");
        var cmd = new MySqlCommand("select version()", _mysqlConnection);
        var version = cmd.ExecuteScalar().ToString();
        Console.WriteLine($"MySQL version: {version}");
        var cmd2 = new MySqlCommand("select INSTITUTE_GROUP from meta_institute_group", _mysqlConnection);
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