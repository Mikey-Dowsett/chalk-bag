using SendTracker.Data;

namespace SendTracker.Models;

public class DisplayRoute {
    public DisplayRoute(int id, string sendName, string grade, string emoji, string notes, DateTime? date,
        MemoryStream imageStream) {
        Id = id;
        SendName = sendName;
        Grade = grade;
        AttemptEmoji = emoji;
        Notes = notes;
        Date = date;
        ThumbnailImageSource = ImageSource.FromStream(() => imageStream);
    }

    public async Task InitializeAsync(string userId) {
        User user = await UserHandler.GetUser(userId);
        User = user.Username;
    }

    public int Id { get; set; }
    public string User { get; set; }
    public string SendName { get; set; }
    public string Grade { get; set; }
    public string AttemptEmoji { get; set; }
    public string Notes { get; set; }
    public DateTime? Date { get; set; }
    public ImageSource ThumbnailImageSource { get; set; }
}