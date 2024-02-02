// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using System.Linq;

Console.WriteLine("Hello, World!");
List<Meters> ls =
[
    new Meters() { meter_no="12071234567890",faham_code="001042"},
    new Meters() { meter_no="12071234567891",faham_code="001042"},
    new Meters() { meter_no="12071234567892",faham_code="001042"},
];

var d = ls.Where(c => c.meter_no == "12071234567893").Select(g => true);
if (d.Any())
{
    Console.WriteLine("i find it");
}
else
{
    Console.WriteLine("i did not found it");
}