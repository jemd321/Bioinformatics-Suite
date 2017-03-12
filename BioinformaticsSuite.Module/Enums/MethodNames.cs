namespace BioinformaticsSuite.Module.Enums
{
    public static class MethodNames
    {
        // Wish I could use an enum here but Delegate Command doesn't take value types...

        public const string DnaFindMotif = "DnaFindMotif";
        public const string DnaMolecularWeight = "DnaMolecularWeight";
        public const string DnaReadingFrame = "DnaReadingFrame";
        public const string DnaRestrictionDigest = "DnaRestrictionDigest";
        public const string DnaStatistics = "DnaStatistics";
        public const string DnaTranscribe = "DnaTranscribe";
        public const string DnaTranslate = "DnaTranslate";

        public const string ProteinOpenReadingFrame = "ProteinOpenReadingFrame";
        public const string ProteinStatistics = "ProteinStatistics";

        public const string ConversionFastaCombine = "ConversionFastaCombine";
        public const string ConversionFastaSplit = "ConversionFastaSplit";
        public const string ConversionEmblFasta = "ConversionEmblFasta";
        public const string ConversionEmblTranslate = "ConversionEmblTranslate";
        public const string ConversionGenbankFasta = "ConversionGenbankFasta";
        public const string ConversionGenbankTranslate = "ConversionGenbankTranslate";
    }
}
