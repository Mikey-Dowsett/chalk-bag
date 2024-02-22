using SQLite;

namespace SendTracker.Data;

public class Constants {
    public const string DatabaseFilename = "RoutesSQLite.db3";

    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

    public static string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}