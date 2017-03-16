using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioinformaticsSuite.Module.Enums;
using BioinformaticsSuite.Module.Utility;
using Microsoft.Practices.ObjectBuilder2;

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

        private const decimal ProteinWaterWeight = 18.02m;
        private static readonly Dictionary<char, decimal> AminoAcidWeights = new Dictionary<char, decimal>()
        {
            {'A', 71.08m},
            {'C', 103.14m},
            {'D', 115.09m},
            {'E', 129.12m},
            {'F', 147.18m},
            {'G', 57.06m},
            {'H', 137.15m},
            {'I', 113.17m},
            {'K', 128.18m},
            {'L', 113.17m},
            {'M', 131.21m},
            {'N', 114.11m},
            {'P', 97.12m},
            {'Q', 128.41m},
            {'R', 156.20m},
            {'S', 87.08m},
            {'T', 101.11m},
            {'V', 99.14m},
            {'W', 186.21m},
            {'Y', 163.18m}
        };

        public void CalculateMolecularWeight(LabelledSequence labelledSequence)
        {
            switch (labelledSequence.SequenceType)
            {
                 case SequenceType.Dna:
                    labelledSequence.MolecularWeight = CalculateDnaMolecularWeight((Dna)labelledSequence);
                    break;
                 case SequenceType.Rna:
                    labelledSequence.MolecularWeight = CalculateRnaMolecularWeight((Rna)labelledSequence);
                    break;
                case SequenceType.Protein:
                    labelledSequence.MolecularWeight = CalculateProteinMolecularWeight((Protein)labelledSequence);
                    break;
                default:
                    throw new Exception("labelled sequence type not correctly labelled");
            }
        }

        private static decimal CalculateDnaMolecularWeight(Dna dna)
        {
            // Add up the total number of each base in the sequence, then multiply by base weight, then apply weight correction
            int[] baseCount = dna.Sequence.CountDnaBases();
            var aCount = baseCount[0];
            var cCount = baseCount[1];
            var gCount = baseCount[2];
            var tCount = baseCount[3];
            decimal molecularWeight = (aCount*DnaAWeight) + (cCount*DnaCWeight) + (gCount*DnaGWeight) +
                                      (tCount*DnaTWeight) - DnaWeightCorrection;
            return molecularWeight;
        }

        private static decimal CalculateRnaMolecularWeight(Rna rna)
        {
            int[] baseCount = rna.Sequence.CountRnaBases();
            var aCount = baseCount[0];
            var cCount = baseCount[1];
            var gCount = baseCount[2];
            var uCount = baseCount[3];
            decimal molecularWeight = (aCount * RnaAWeight) + (cCount * RnaCWeight) + (gCount * RnaGWeight) +
                                      (uCount * RnaUWeight) + RnaWeightCorrection;
            return molecularWeight;
        }

        private static decimal CalculateProteinMolecularWeight(Protein protein)
        {
            string sequence = protein.Sequence;
            decimal molecularWeight = 0;

            var aminoAcidCount = sequence.CountAminoAcids();
            foreach (var count in aminoAcidCount)
            {
                decimal weight;
                if (!AminoAcidWeights.TryGetValue(count.Key, out weight))
                {
                    throw new Exception("Amino acid key not found, have invalid amino acids made it through UI input validation?");
                }
                molecularWeight += count.Value*weight;
            }
            // Correct for the weight of water
            molecularWeight += ProteinWaterWeight;
            return molecularWeight;
        }
    }
}
