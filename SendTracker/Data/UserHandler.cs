using System.Diagnostics;
using Postgrest.Responses;
using SendTracker.Models;
using Supabase;

namespace SendTracker.Data;

public class UserHandler {
    private static readonly Client client = new(Constants.SUPABASE_URL, Constants.SUPABASE_KEY);

    public static async Task CreateUser(User user) {
        try {
            await client.From<User>().Insert(user);
        }
        catch (Exception ex) {
            Debug.WriteLine($"User Error: {ex.Message}");
        }
    }

    public static async Task<User> GetUser(string userId) {
        ModeledResponse<User> response = await client.From<User>().Where(x => x.UserId == userId).Get();
        return response.Model;
    }

    public static async Task UpdateUser(User user) {
        await client.From<User>().Where(x => x.UserId == user.Id).Update(user);
    }

    public static async Task DeleteUser(string userId) {
        await client.From<User>().Where(x => x.UserId == userId).Delete();
    }
}