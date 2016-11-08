Bioinformatics-Suite
-----------------------

Welcome to Bioinformatics Suite. This program is intended as a personal project
to support my learning of C# and WPF with MVVM, particularly of concepts such as
dependency injection,loosely coupled structure and scalability.

This application is a toolkit for manipulating DNA, RNA and protein
sequences via conversion between commonly used bioinformatic formats, finding the 
results of various processes such as translation, restriction digest products 
and open reading frames.

Sample sequences for demonstration are available in the BioinformaticsSuite.Module/Resources folder.

The program is still under development, and as of yet only DNA sequence
manipulation is completed.


Supported Features
--------------------

DNA
-----

- Finding a Motif: searching for a specific sequence.

- Calculating Molecular Weight.

- Finding Reading Frames: returns the six frames in which a DNA sequence can
			  be translated from based on the start point.

- Restricition Digest: Finding the products of a sequence when cut into fragments
		       by a number of different enzymes.

- DNA statistics: calculates the percentage make up of sequences by each base.

- Transcription: produces an RNA sequence from DNA.

- Translation: produces a protein sequence from DNA.



Planned Features
-----------------

RNA
-----

- Translation: produces a protein sequence from RNA.


Protein
--------

- Find Open Reading Frames:  Finds regions of protein sequences that can be translated
			   based on the presence of a start or stop codon in each of
			   the six reading frames.

- Protein Statistics: Calculates the percentage amino acid makeup of a protein sequence.


Conversion
-----------

- Conversion between formts from the major bioinformatcs websites.
  eg. Fasta, Uniprot etc.


Author
-------

Jem Davis
jemd321+programming@gmail.com