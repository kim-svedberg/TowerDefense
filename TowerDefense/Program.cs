using System.Windows.Forms;
using TowerDefense;

// To customize application configuration such as set high DPI settings or default font,
// see https://aka.ms/applicationconfiguration.
ApplicationConfiguration.Initialize();

using var game = new Game1();
game.Run();
