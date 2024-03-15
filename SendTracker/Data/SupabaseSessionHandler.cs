using System.Diagnostics;
using Postgrest.Responses;
using SendTracker.Models;
using Supabase;
using Supabase.Gotrue;
using Client = Supabase.Client;
using FileOptions = Supabase.Storage.FileOptions;

namespace SendTracker.Data;

public class SupabaseSessionHandler : DefaultSupabaseSessionHandler {
    private static Client client = new Client(Constants.SUPABASE_URL, Constants.SUPABASE_KEY);
    public static Session Session;

    //Authentication
    public static async Task CreateUser(string email, string password) {
        Session = await client.Auth.SignUp(email, password);
    }

    public static async Task LoginUser(string email, string password) {
        Session = await client.Auth.SignIn(email, password);
    }
    

    // Route Table Functions
    public static async Task CreateRoute(Route newRoute) {
        await client.From<Route>().Insert(newRoute);
    }

    public static async Task<List<Route>> GetRoute() {
        ModeledResponse<Route> response = await client.Postgrest.Table<Route>()
            .Where(x => x.UserId == SupabaseSessionHandler.Session.User.Id).Get();
        return response.Models;
    }

    public static async Task<Route> GetRoute(int id) {
        ModeledResponse<Route> response = await client.Postgrest.Table<Route>().Where(x => x.Id == id).Get();
        return response.Model;
    }

    public static async Task UpdateRoute(Route route, int id) {
        await client.From<Route>().Where(x => x.Id == id).Update(route);
    }

    public static async Task DeleteRoute(int id) {
        await client.From<Route>().Where(x => x.Id == id).Delete();
    }

    //Photo Bucket Functions
    public static async Task UploadPhoto(string imagePath, string fileName) {
        //Change to be user folder later.
        await client.Storage.From("post-images").Upload(imagePath, fileName,
            new FileOptions { CacheControl = "3600", Upsert = false });
    }

    public static async Task<MemoryStream> DownloadPhoto(string imagePath) {
        byte[] bytes = await client.Storage.From("post-images")
            .DownloadPublicFile(imagePath);
        return new MemoryStream(bytes);
    }

    public static async Task DeletePhoto(string imagePath) {
        await client.Storage.From("post-images").Remove(new List<string> { $"public/{imagePath}" });
    }
}