using System;

namespace ConsoleApp
{
  internal sealed class Configuration
  {
    public static Configuration Instance { get; } = new();

    public AppSetting? Setting { get; private set; }

    private string path;

    private Configuration()
    {
      this.path = System.IO.Path.Combine(
        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
        @"Configuration.json"
      );
    }

    public void Load()
    {
      if (!System.IO.File.Exists(this.path))
      {
        using (var stream = new System.IO.StreamWriter(this.path, false, System.Text.Encoding.UTF8))
        {
          stream.WriteLine(
            System.Text.Json.JsonSerializer.Serialize(
              new AppSetting(),
              new System.Text.Json.JsonSerializerOptions()
              {
                WriteIndented = true, // インデント指定
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // 文字列をエンコードしない
              }
            )
          );
        }
      }

      using (var stream = new System.IO.StreamReader(this.path))
      {
        this.Setting = System.Text.Json.JsonSerializer.Deserialize<AppSetting>(
          stream.ReadToEnd(),
          new System.Text.Json.JsonSerializerOptions()
          {
            AllowTrailingCommas = true, // 末尾カンマ有効
          }
        );
      }
    }

    public void Save()
    {
      using (var stream = new System.IO.StreamWriter(this.path, false, System.Text.Encoding.UTF8))
      {
        stream.WriteLine(
          System.Text.Json.JsonSerializer.Serialize(
            this.Setting,
            new System.Text.Json.JsonSerializerOptions()
            {
              WriteIndented = true, // インデント指定
              Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // 文字列をエンコードしない
            }
          )
        );
      }
    }

    internal class AppSetting
    {
      [System.Text.Json.Serialization.JsonPropertyName("Message")]
      public string Message { get; set; } = "Sample Message...";

      [System.Text.Json.Serialization.JsonPropertyName("Sub")]
      public SubSetting Sub { get; set; } = new();

      internal class SubSetting
      {
        [System.Text.Json.Serialization.JsonPropertyName("N1")]
        public int N1 { get; set; } = 1;

        [System.Text.Json.Serialization.JsonPropertyName("N2")]
        public int N2 { get; set; } = 2;

        [System.Text.Json.Serialization.JsonPropertyName("N3")]
        public int N3 { get; set; } = 3;
      }
    }
  }
}
