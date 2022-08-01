public struct TaskPerformed
{
    public string taskName;
    public string username;
    public bool result;
    public int timePenalty;
    public float scorePenalty;
    public float timestamp;

    public TaskPerformed(string tn, string un, bool r, int tpen, float spen, float time)
    {
        this.taskName = tn;
        this.username = un;
        this.result = r;
        this.timePenalty = tpen;
        this.scorePenalty = spen;
        this.timestamp = time;
    }
}
