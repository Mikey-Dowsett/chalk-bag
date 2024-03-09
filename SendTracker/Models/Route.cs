using SQLite;

namespace SendTracker.Models;

public class Route {
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

        LoadEmoji();
    }

    [PrimaryKey] [AutoIncrement] public int Id { get; set; }
    public string SendName { get; set; }
    public string ClimbType { get; set; }
    public string Grade { get; set; }
    public string Technique { get; set; }
    public string Attempts { get; set; }
    public string Notes { get; set; }
    public string RockType { get; set; }
    public string PhotoPath { get; set; }
    public DateTime Date { get; set; }
    public string Duration { get; set; }
    public int Pitches { get; set; }
    public bool Proposed { get; set; }
    public int Falls { get; set; }
    public int Rests { get; set; }
    public string AttemptEmoji { get; set; }

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