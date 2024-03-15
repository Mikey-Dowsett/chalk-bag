using Postgrest.Attributes;
using Postgrest.Models;
using SendTracker.Data;

namespace SendTracker.Models;

[Table("Routes")]
public class Route : BaseModel {
    public Route() { }

    public Route(string sendName, string climbType, string grade, string technique, string attempts, string notes,
        string rockType, string photoPath, DateTime date, string duration, int pitches, bool proposed, int rests,
        int falls) {
        SendName = sendName;
        ClimbType = climbType;
        Grade = grade;
        Technique = technique;
        Attempts = attempts;
        Notes = notes;
        RockType = rockType;
        PhotoPath = photoPath;
        Date = date;
        Duration = duration;
        Pitches = pitches;
        Proposed = proposed;
        Rests = rests;
        Falls = falls;
        UserId = SupabaseSessionHandler.Session.User.Id;

        LoadEmoji();
    }

    [PrimaryKey("id")] public int Id { get; set; }
    [Column("send_name")] public string? SendName { get; set; }
    [Column("climb_type")] public string? ClimbType { get; set; }
    [Column("grade")] public string? Grade { get; set; }
    [Column("technique")] public string? Technique { get; set; }
    [Column("attempts")] public string? Attempts { get; set; }
    [Column("notes")] public string? Notes { get; set; }
    [Column("rock_type")] public string? RockType { get; set; }
    [Column("photo_path")] public string? PhotoPath { get; set; }
    [Column("date")] public DateTime? Date { get; set; }
    [Column("duration")] public string? Duration { get; set; }
    [Column("pitches")] public int? Pitches { get; set; }
    [Column("proposed")] public bool? Proposed { get; set; }
    [Column("falls")] public int? Falls { get; set; }
    [Column("rests")] public int? Rests { get; set; }
    [Column("attempt_emoji")] public string? AttemptEmoji { get; set; }
    [Column("user_id")] public string? UserId { get; set; }

    public override string ToString() {
        return $"{SendName}, {ClimbType}, {Grade}";
    }

    public void LoadEmoji() {
        switch (Attempts) {
            case "Flash":
                AttemptEmoji = "\u26a1";
                break;
            case "Redpoint":
                AttemptEmoji = "\ud83d\udccc";
                break;
            case "On Sight":
                AttemptEmoji = "\ud83d\udc41\ufe0f";
                break;
            case "In Progress":
                AttemptEmoji = "\ud83e\udea2";
                break;
            default:
                AttemptEmoji = Attempts;
                break;
        }
    }
}