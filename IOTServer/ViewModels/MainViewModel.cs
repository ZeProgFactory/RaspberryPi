using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Timers;
using ZPF.AT;
using ZPF.SQL;

namespace ZPF
{
    public partial class MainViewModel : BaseViewModel
    {
        // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -

        static MainViewModel _Instance = null;

        public JsonSerializerOptions jsonSerializerOptions { get; private set; }

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

            jsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All),
                WriteIndented = true
            };

            // - - -  - - - 

            if (Debugger.IsAttached)
            {
                DataFolder = System.IO.Path.GetFullPath(@"..\Data\");
            }
            else
            {
                DataFolder = CleanPath(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\IOTServer\");
            };

            if (!Directory.Exists(DataFolder))
            {
                Directory.CreateDirectory(DataFolder);
            };

            Debug.WriteLine("Data: " + DataFolder);
            Console.WriteLine("Data: " + DataFolder);

            // - - -  - - - 

            //Load();
            OpenDB();

            // - - -  - - - 

            //var timerGetTemp = new System.Timers.Timer(TimeSpan.FromHours(1));
            //timerGetTemp.Elapsed += TimerGetTemp_Elapsed;
            // timerGetTemp.Start();
        }

        // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

        //System.Timers.Timer timerGetTemp = null;

        //private void TimerGetTemp_Elapsed(object? sender, ElapsedEventArgs e)
        //{
        //    OpenDB();
        //}

        // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

        public string CleanPath(string Path)
        {
            string Result = null;

            if (Environment.OSVersion.ToString().ToUpper().Contains("WINDOWS"))
            {
                Result = Path.Replace("/", @"\");
                while (Result.IndexOf(@"\\") > -1) Result = Result.Replace(@"\\", @"\");
            }
            else
            {
                Result = Path.Replace(@"\", "/");
                while (Result.IndexOf(@"//") > -1) Result = Result.Replace(@"//", @"/");
            };

            return Result;
        }

        // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

        public void Prologue(HttpRequest request)
        {
            OpenDB();
        }

        // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

        public void OpenDB()
        {
            if (DB_SQL._ViewModel == null || !DB_SQL._ViewModel.CheckConnection())
            {
                string Server = @"";
                string DBase = CleanPath(DataFolder + @"\IOTServer.db3");
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

                DB_SQL.CreateTable(typeof(Reading));
                //DB_SQL.CreateTable(typeof(Spooler), "Stats");
                //DB_SQL.CreateTable(typeof(Current));

                if (Connection.DBType == DBType.SQLite)
                {
                    DB_SQL.QuickQuery("VACUUM");
                };

                {
                    //DateTime dt = DateTime.Now.AddYears(-2);

                    //DB_SQL.QuickQuery($"delete from Spooler where DATE(CreatedOn) <= {DB_SQL.DateTimeToSQL(DB_SQL._ViewModel.DBType, dt)}");

                    //// Sodexo.Safran
                    //dt = DateTime.Now.AddHours(-4);
                    //DB_SQL.QuickQuery($"delete from Current where DATE(CreatedOn) <= {DB_SQL.DateTimeToSQL(DB_SQL._ViewModel.DBType, dt)}");
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
            get { return "IOTServer" + " - " + Version; }
        }

        // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

        //public Params Config { get; private set; } = new Params();

        //internal void Load()
        //{
        //   WriteLog("MainViewModel.Load()", $"Begin");

        //   // - - - config - - -

        //   try
        //   {
        //      string FileName = DataFolder + @"Syscall.Params.json";

        //      WriteLog("MainViewModel.Load()", $"{FileName}");

        //      if (System.IO.File.Exists(FileName))
        //      {
        //         string json = File.ReadAllText(FileName);
        //         WriteLog("MainViewModel.Load()", $"ReadAllText");
        //         WriteLog("MainViewModel.Load():", json);

        //         try
        //         {
        //            if (json == null)
        //            {
        //               WriteLog("MainViewModel.Load()", $"json == null");
        //            }
        //            else
        //            {
        //               var p = JsonSerializer.Deserialize<Params>(json);

        //               if (p != null)
        //               {
        //                  Config = p;
        //               };
        //            };
        //         }
        //         catch (Exception ex)
        //         {
        //            WriteLog("MainViewModel.Load() Deserialize", ex);
        //         };
        //      }
        //      else
        //      {
        //         WriteLog("MainViewModel.Load()", $"! System.IO.File.Exists{FileName}");
        //      };
        //   }
        //   catch (Exception ex)
        //   {
        //      WriteLog("MainViewModel.Load() config", ex);
        //   };
        //}

        private void WriteLog(string tag, Exception ex)
        {
            string log = $"{ex.Message}{Environment.NewLine}{(ex.StackTrace != null ? ex.StackTrace : string.Empty)}";
            WriteLog(tag, log);
        }

        private void WriteLog(string tag, string message)
        {
            return;

            if (message == null)
            {
                message = "(null)";
            };

            string FileName = DataFolder + @"wsSyscall.log";

            string log = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy") + Environment.NewLine;
            log += $"{tag}{Environment.NewLine}{message}";
            log += Environment.NewLine;
            log += Environment.NewLine;

            System.IO.File.AppendAllText(FileName, log);
        }

        //internal void Save()
        //{
        //   {
        //      string FileName = DataFolder + @"Syscall.Params.json";

        //      var json = JsonSerializer.Serialize(Config, jsonSerializerOptions);
        //      File.WriteAllText(FileName, json);
        //   }
        //}

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

            return (userName == "Syscall" && password == "ZPF");
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
