using SendTracker.Models;
using SQLite;

namespace SendTracker.Data;

public class RoutesDatabase {
    private SQLiteAsyncConnection Database;

    private async Task Init() {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        CreateTableResult result = await Database.CreateTableAsync<Route>();
    }

    public async Task<List<Route>> GetRoutesAsync() {
        await Init();
        return await Database.Table<Route>().OrderByDescending(route => route.Id).ToListAsync();
    }

    public async Task<Route> GetRouteAsync(int id) {
        await Init();
        return await Database.Table<Route>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveRouteAsync(Route route) {
        await Init();
        if (route.Id != 0)
            return await Database.UpdateAsync(route);
        return await Database.InsertAsync(route);
    }

    public async Task<int> DeleteRouteAsync(Route route) {
        await Init();
        return await Database.DeleteAsync(route);
    }

    public async Task DeleteDatabase() {
        if (Database is null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await Database.DropTableAsync<Route>();
    }
}