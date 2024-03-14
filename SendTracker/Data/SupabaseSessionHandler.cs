using System.Diagnostics;
using Postgrest.Responses;
using SendTracker.Models;
using Supabase;
using Supabase.Storage.Exceptions;
using FileOptions = Supabase.Storage.FileOptions;

namespace SendTracker.Data;

public class SupabaseSessionHandler : DefaultSupabaseSessionHandler {
    private readonly Client client;

    public SupabaseSessionHandler() {
        client = new Client(Constants.SUPABASE_URL, Constants.SUPABASE_KEY);
    }

    // Route Table Functions
    public async Task CreateRoute(Route newRoute) {
        await client.From<Route>().Insert(newRoute);
    }

    public async Task<List<Route>> GetRoute() {
        ModeledResponse<Route> response = await client.Postgrest.Table<Route>().Get();
        return response.Models;
    }

    public async Task<Route> GetRoute(int id) {
        ModeledResponse<Route> response = await client.Postgrest.Table<Route>().Where(x => x.Id == id).Get();
        return response.Model;
    }

    public async Task UpdateRoute(Route route, int id) {
        await client.From<Route>().Where(x => x.Id == id).Update(route);
    }

    public async Task DeleteRoute(int id) {
        await client.From<Route>().Where(x => x.Id == id).Delete();
    }

    //Photo Bucket Functions
    public async Task UploadPhoto(string imagePath, string fileName) {
        //Change to be user folder later.
        await client.Storage.From("post-images").Upload(imagePath, fileName,
            new FileOptions { CacheControl = "3600", Upsert = false });
    }

    public async Task<MemoryStream> DownloadPhoto(string imagePath) {
        byte[] bytes = await client.Storage.From("post-images")
            .DownloadPublicFile(imagePath);
        return new MemoryStream(bytes);
    }
    
    public async Task DeletePhoto(string imagePath) {
        await client.Storage.From("post-images").Remove(new List<string> {$"public/{imagePath}"});
    }
}