namespace EjemploWebhookBioengine.Models;

public class OperacionDto{
    public int IdOperacion { get; set; }
    public int IdResultadoOperacion { get; set; }
    public int Msg { get; set; }
    public List<MatchDto>? MatchesResult { get; set; }
}