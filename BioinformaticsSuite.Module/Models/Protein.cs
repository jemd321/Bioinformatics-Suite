namespace BioinformaticsSuite.Module.Models
{
    public class Protein : LabelledSequence
    {
        public Protein(string label, string sequence) : base(label, sequence) {}

        public Protein(Dna dna) : base(dna) {}

    }
}