using System;

namespace ConsoleApp
{
  internal class Application
  {
    private const int EXIT_SUCCESS = 0;
    private const int EXIT_FAILURE = 1;

    public int Run(string[] args)
    {
      Configuration.Instance.Load();

      System.Console.WriteLine($"Message = {Configuration.Instance.Setting.Message}");
      System.Console.WriteLine($"Sub.N1 = {Configuration.Instance.Setting.Sub.N1}");
      System.Console.WriteLine($"Sub.N2 = {Configuration.Instance.Setting.Sub.N2}");
      System.Console.WriteLine($"Sub.N3 = {Configuration.Instance.Setting.Sub.N3}");

      Configuration.Instance.Setting.Message = "Rewrite Message...";

      Configuration.Instance.Save();

      return EXIT_SUCCESS;
    }
  }
}
