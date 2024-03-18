using Supabase;
using FileOptions = Supabase.Storage.FileOptions;


namespace SendTracker.Data;

public static class StorageHandler {
    private static readonly Client client = new(Constants.SUPABASE_URL, Constants.SUPABASE_KEY);

    public static async Task UploadPhoto(string imagePath, string fileName) {
        //Change to be user folder later.
        await client.Storage.From($"post-images/{SessionHandler.UserSession?.User?.Id}").Upload(imagePath, fileName,
            new FileOptions { CacheControl = "3600", Upsert = false });
    }

    public static async Task<MemoryStream> DownloadPhoto(string imagePath, string userId) {
        byte[] bytes = await client.Storage.From($"post-images/{userId}")
            .DownloadPublicFile(imagePath);
        return new MemoryStream(bytes);
    }

    public static async Task DeletePhoto(string imagePath) {
        await client.Storage.From($"post-images/{SessionHandler.UserSession?.User?.Id}")
            .Remove(new List<string> { $"public/{imagePath}" });
    }
}