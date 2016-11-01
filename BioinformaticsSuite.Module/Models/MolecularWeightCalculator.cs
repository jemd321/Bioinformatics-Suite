using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Utility;

namespace BioinformaticsSuite.Module.Models
{
    public interface IMolecularWeightCalculator
    {
        void CalculateMolecularWeight(LabelledSequence labelledSequence);
    }

    public class MolecularWeightCalculator : IMolecularWeightCalculator
    {
        private const decimal DnaAWeight = 313.21m;
        private const decimal DnaCWeight = 289.18m;
        private const decimal DnaGWeight = 329.21m;
        private const decimal DnaTWeight = 304.2m;
        private const decimal DnaWeightCorrection = 61.96m;

        private const decimal MRnaAWeight = 329.21m;
        private const decimal MRnaCWeight = 305.18m;
        private const decimal MRnaGWeight = 345.21m;
        private const decimal MRnaUWeight = 306.17m;
        private const decimal MRnaWeightCorrection = 159;

        public void CalculateMolecularWeight(LabelledSequence labelledSequence)
        {
            switch (labelledSequence.SequenceType)
            {
                 case SequenceType.Dna:
                    labelledSequence.MolecularWeight = CalculateDnaMolecularWeight(labelledSequence as Dna);
                    break;
                 case SequenceType.MRna:
                    labelledSequence.MolecularWeight = CalculateMRnaMolecularWeight(labelledSequence as MRna);
                    break;
                case SequenceType.Protein:
                    labelledSequence.MolecularWeight = CalculateProteinMolecularWeight(labelledSequence as Protein);
                    break;
                default:
                    throw new Exception("labelled sequence type not correctly labelled");
            }
        }

        private static decimal CalculateDnaMolecularWeight(Dna dna)
        {
            int[] baseCount = dna.Sequence.CountDnaBases();
            int aCount = baseCount[0];
            int cCount = baseCount[1];
            int gCount = baseCount[2];
            int tCount = baseCount[3];
            decimal molecularWeight = (aCount*DnaAWeight) + (cCount*DnaCWeight) + (gCount*DnaGWeight) +
                                      (tCount*DnaTWeight) - DnaWeightCorrection;
            return molecularWeight;
        }

        private static decimal CalculateMRnaMolecularWeight(MRna mRna)
        {
            int[] baseCount = mRna.Sequence.CountMRnaBases();
            int aCount = baseCount[0];
            int cCount = baseCount[1];
            int gCount = baseCount[2];
            int uCount = baseCount[3];
            decimal molecularWeight = (aCount * MRnaAWeight) + (cCount * MRnaCWeight) + (gCount * MRnaGWeight) +
                                      (uCount * MRnaUWeight) + MRnaWeightCorrection;
            return molecularWeight;
        }

        private static decimal CalculateProteinMolecularWeight(Protein protein)
        {
            // NOT IMPLEMENTED YET!
            return 12;
        }
    }
}
