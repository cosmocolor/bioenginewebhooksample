namespace EjemploWebhookBioengine.Models;

public class MatchDto
{
    public MatchDto() { }

   
    public int MatchId { get; set; }
        
    public int UniqueId { get; set; }
    public int Score { get; set; }
    public int FingerprintsScore { get; set; }
    public int IrisScore { get; set; }
    public int FaceScore { get; set; }
}