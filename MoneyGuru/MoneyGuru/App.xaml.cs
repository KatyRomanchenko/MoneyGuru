using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace MoneyGuru
{
    public partial class App : Application
    {
        static SQLiteConnection db;

        public App()
        {
            MainPage = new PrestartPage();
        }

        public static SQLiteConnection Database
        {
            get
            {
                if (db == null)
                {
                    var sqliteFilename = "UserSQLite.db3";
                    string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    var path = Path.Combine(folder, sqliteFilename);
                    db = new SQLiteConnection(path);
                    db.CreateTable<User>();
                }
                return db;
            }
        }
        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}
