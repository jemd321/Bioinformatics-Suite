Bioinformatics-Suite
-----------------------

Welcome to Bioinformatics Suite. This program is intended as a personal project
to support my learning of C# and WPF with MVVM, particularly of concepts such as
dependency injection, a loosely couple structure and scalablity.

The application is intended to be a toolkit for manipulating DNA, mRNA and protein
sequences via conversion between commonly used bioinformatic formats, finding the 
results of various processes such as translation, restricition digest products 
and open reading frames.

The application is still under development, and as of yet only DNA sequence
manipulation is supported.


Supported Features
--------------------

DNA
-----

- Finding a Motif: searching for a specific sequence.

- Calculating Molecular Weight.

- Finding Reading Frames: returns the six frames in which a DNA sequence can
			  be translated from based on the start point.

- DNA statistics: calculates the percentage make up of sequences by each base.

- Transcription: produces an mRNA sequence from DNA.

- Translation: produces a protein sequence from DNA.



Planned Features
-----------------

DNA
----

- Restricition Digest: Finding the products of a sequence when cut into fragments
		      by a number of different enzymes.


mRNA
-----



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