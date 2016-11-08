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

        private const decimal RnaAWeight = 329.21m;
        private const decimal RnaCWeight = 305.18m;
        private const decimal RnaGWeight = 345.21m;
        private const decimal RnaUWeight = 306.17m;
        private const decimal RnaWeightCorrection = 159;

        public void CalculateMolecularWeight(LabelledSequence labelledSequence)
        {
            switch (labelledSequence.SequenceType)
            {
                 case SequenceType.Dna:
                    labelledSequence.MolecularWeight = CalculateDnaMolecularWeight(labelledSequence as Dna);
                    break;
                 case SequenceType.Rna:
                    labelledSequence.MolecularWeight = CalculateRnaMolecularWeight(labelledSequence as Rna);
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

        private static decimal CalculateRnaMolecularWeight(Rna rna)
        {
            int[] baseCount = rna.Sequence.CountRnaBases();
            int aCount = baseCount[0];
            int cCount = baseCount[1];
            int gCount = baseCount[2];
            int uCount = baseCount[3];
            decimal molecularWeight = (aCount * RnaAWeight) + (cCount * RnaCWeight) + (gCount * RnaGWeight) +
                                      (uCount * RnaUWeight) + RnaWeightCorrection;
            return molecularWeight;
        }

        private static decimal CalculateProteinMolecularWeight(Protein protein)
        {
            // NOT IMPLEMENTED YET!
            return 12;
        }
    }
}
