namespace BioinformaticsSuite.Module.Models
{
    public class DigestFragment
    {
        public DigestFragment(string enzyme, int cutPosition, string fragment)
        {
            Enzyme = enzyme;
            CutPosition = cutPosition;
            Fragment = fragment;
        }

        public string Enzyme { get; }
        public int CutPosition { get; }
        public string Fragment { get; }
    }
}