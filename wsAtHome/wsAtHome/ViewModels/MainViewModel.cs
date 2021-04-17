using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using ZPF.AT;
using ZPF.SQL;
using System.Text.Json;
using AtHome;

namespace ZPF
{
   public partial class MainViewModel : BaseViewModel
   {
      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -

      static MainViewModel _Instance = null;

      public static MainViewModel Current
      {
         get
         {
            if (_Instance == null)
            {
               _Instance = new MainViewModel();
            };

            return _Instance;
         }

         set
         {
            _Instance = value;
         }
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -


      public MainViewModel()
      {
         _Instance = this;

         // - - -  - - - 

         DataFolder = CleanPath(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\ZPF\");

         if (!Directory.Exists(DataFolder))
         {
            Directory.CreateDirectory(DataFolder);
         };

         // - - -  - - - 

         Load();
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public string CleanPath(string Path)
      {
         if (Environment.OSVersion.ToString().ToUpper().Contains("WINDOWS"))
         {
            return Path.Replace("/", @"\");
         }
         else
         {
            return Path.Replace(@"\", "/");
         };
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public void Prologue(HttpRequest request)
      {
         OpenDB(request);
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public void OpenDB(HttpRequest request)
      {
         if (DB_SQL._ViewModel == null || !DB_SQL._ViewModel.CheckConnection())
         {
            string Server = @"";
            string DBase = @"C:\ProgramData\ZPF\AtHome.SQLite.db3";
            string User = "";
            string Password = "";

            string ConnectionString = DB_SQL.GenConnectionString(DBType.SQLite, Server, DBase, User, Password);

            if (string.IsNullOrEmpty(ConnectionString))
            {
               MainViewModel.Current.Connection.LastError = "No ConnectionString ...";
               return;
            };

            //Connection = new DBSQLViewModel(new SQLServerEngine());
            Connection = new DBSQLViewModel(new MSSQLiteEngine());
            DB_SQL._ViewModel = Connection;
            Log.Write("", $"{ConnectionString} {(Connection.Open(ConnectionString, true) ? "OK" : "KO")}");

            //ToDo: CleanAuditTrail();

            //DB_SQL.QuickQuery(Spooler.SQLCreate_SQLite);
            //DB_SQL.QuickQuery(Spooler.SQLCreate_SQLite.Replace("Spooler", "Current"));

            if (Connection.DBType == DBType.SQLite)
            {
               DB_SQL.QuickQuery("VACUUM");
            };

            {
               DateTime dt = DateTime.Now.AddYears(-1);

               //ToDo aUDITtRAIL
               DB_SQL.QuickQuery($"delete from Spooler where DATE(CreatedOn) <= {DB_SQL.DateTimeToSQL(DB_SQL._ViewModel.DBType, dt)}");
            };
         };
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -

      string _Version = "1.10";

      public string Version
      {
         get { return _Version; }
         set { _Version = value; }
      }

      public string ProgramCaption
      {
         get { return "AtHome" + " - " + Version; }
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public Params Config { get; private set; } = new Params();

      internal void Load()
      {
         // - - - config - - -

         {
            string FileName = DataFolder + @"AtHome.Params.json";

            if (System.IO.File.Exists(FileName))
            {
               string json = File.ReadAllText(FileName);

               try
               {
                  var p = JsonSerializer.Deserialize<Params>(json);
                  if (p != null)
                  {
                     Config = p;
                  };
               }
               catch (Exception ex)
               {
               };
            };
         }
      }

      internal void Save()
      {
         {
            string FileName = DataFolder + @"AtHome.Params.json";

            var json = JsonSerializer.Serialize<Params>(Config, new JsonSerializerOptions { WriteIndented=true });
            File.WriteAllText(FileName, json);
         }
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -

      public DBSQLViewModel Connection { get; private set; }
      public string DataFolder { get; internal set; }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

      internal bool CheckAuthorization(string authorization)
      {
         string authenticationToken = authorization.Replace("Basic ", "");
         string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
         string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
         string userName = usernamePasswordArray[0];
         string password = usernamePasswordArray[1];

         return (userName == "AtHome" && password == "ZPF");
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

      internal string GetContent(HttpRequest request)
      {
         int l = (int)request.ContentLength;

         var buffer = new byte[l];
         Stream stream = request.Body;

         int i = 0;
         do
         {
            i = stream.Read(buffer, 0, l);
         } while (i > 0);

         return System.Text.Encoding.Unicode.GetString(buffer);
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - - 

      internal bool SaveContentFile(HttpRequest request)
      {
         int l = (int)request.ContentLength;

         var buffer = new byte[l];
         Stream stream = request.Body;

         int i = 0;
         do
         {
            i = stream.Read(buffer, 0, l);
         } while (i > 0);

         var st = System.Text.Encoding.Unicode.GetString(buffer);

         return true;
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  

   }
}
