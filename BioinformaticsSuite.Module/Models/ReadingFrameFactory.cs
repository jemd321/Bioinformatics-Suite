namespace BioinformaticsSuite.Module.Models
{
    public class ReadingFrameFactory : IReadingFrameFactory
    {
        public ReadingFrame GetReadingFrames(Dna dna)
        {
            var readingFrame = new ReadingFrame(dna);
            return readingFrame;
        }
    }

    public interface IReadingFrameFactory
    {
        ReadingFrame GetReadingFrames(Dna dna);
    }
}