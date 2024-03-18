using System.Diagnostics;
using Newtonsoft.Json;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;
using Client = Supabase.Client;
using User = SendTracker.Models.User;

namespace SendTracker.Data;

public class SessionHandler : IGotrueSessionPersistence<Session> {
    public static readonly Client client = new(Constants.SUPABASE_URL, Constants.SUPABASE_KEY);
    public static Session? UserSession { get; set; }

    public void SaveSession(Session session) {
        string cacheFileName = ".gotrue.cache";

        try {
            string cacheDir = FileSystem.CacheDirectory;
            string path = Path.Join(cacheDir, cacheFileName);
            string str = JsonConvert.SerializeObject(session);

            using (StreamWriter file = new(path)) {
                file.Write(str);
            }

            Debug.WriteLine("Session Saved!");
        }
        catch (Exception ex) {
            Debug.WriteLine("Failed to write cache file: " + ex.Message);
        }
    }

    public void DestroySession() {
        string cacheFileName = ".gotrue.cache";
        string cacheDir = FileSystem.CacheDirectory;
        string path = Path.Join(cacheDir, cacheFileName);
        if (File.Exists(path)) File.Delete(path);
        Debug.WriteLine("Session Destroyed!");
    }

    public Session? LoadSession() {
        try {
            string cacheFileName = ".gotrue.cache";
            string cacheDir = FileSystem.CacheDirectory;
            string path = Path.Join(cacheDir, cacheFileName);

            using StreamReader reader = new(path);
            string json = reader.ReadToEnd();
            Debug.WriteLine("Session Loaded!");
            return JsonConvert.DeserializeObject<Session>(json);
        }
        catch (Exception ex) {
            Debug.WriteLine("Failed to load session: " + ex.Message);
            return null;
        }
    }

    public static async Task SignUp(string email, string password) {
        UserSession = await client.Auth.SignUp(email, password);
    }

    public static async Task Login(string email, string password) {
        try {
            UserSession = await client.Auth.SignIn(email, password);
            SessionHandler sessionHandler = new();
            sessionHandler.SaveSession(UserSession);
        }
        catch (Exception ex) {
            Debug.WriteLine("Authentication Error: " + ex.Message);
        }
    }

    public static async Task Logout() {
        UserSession = null;
        SessionHandler sessionHandler = new();
        sessionHandler.DestroySession();
    }
}