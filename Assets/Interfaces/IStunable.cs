public interface IStunable
{
    bool P1hit { get; set; }
    bool P2hit { get; set; }
    void Stun();
    bool IsStuned();
}