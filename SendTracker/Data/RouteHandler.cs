using Postgrest.Responses;
using SendTracker.Models;
using Supabase;

namespace SendTracker.Data;

public static class RouteHandler {
    private static readonly Client client = new(Constants.SUPABASE_URL, Constants.SUPABASE_KEY);

    public static async Task CreateRoute(Route route) {
        await client.From<Route>().Insert(route);
    }

    public static async Task<List<Route>> GetRoute() {
        ModeledResponse<Route> response = await client.Postgrest.Table<Route>()
            .Order("date", Postgrest.Constants.Ordering.Descending).Get();
        return response.Models;
    }
    
    public static async Task<Route?> GetRoute(int id) {
        ModeledResponse<Route> response = await client.Postgrest.Table<Route>().Where(x => x.Id == id).Get();
        return response.Model;
    }

    public static async Task<List<Route>> GetUserRoutes() {
        ModeledResponse<Route> response = await client.Postgrest.Table<Route>()
            .Where(x => x.UserId == SessionHandler.UserSession!.User!.Id)
            .Order("date", Postgrest.Constants.Ordering.Descending).Get();
        return response.Models;
    }

    public static async Task UpdateRoute(Route route) {
        await client.From<Route>().Where(x => x.Id == route.Id).Update(route);
    }

    public static async Task DeleteRoute(int id) {
        await client.From<Route>().Where(x => x.Id == id).Delete();
    }
}