Bioinformatics-Suite
======================

Welcome to Bioinformatics Suite. This program is intended as a personal project
to support my learning of C# and WPF with MVVM, particularly of concepts such as
dependency injection,loosely coupled structure and scalability.

This application is a toolkit for manipulating DNA, RNA and protein
sequences via conversion between commonly used bioinformatic formats, finding the 
results of various processes such as translation, restriction digest products 
and open reading frames.

Sample sequences for demonstration are available in the BioinformaticsSuite.Module/Resources folder.


Features
=========

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

RNA
-----

- Translate RNA

- RNA Molecular Weight 

Protein
--------

- Open Reading Frames

- Protein Statistics

- Protein Molecular Weight



Format Conversion
------------------

- EMBL -> Fasta

- Translate EMBL -> Fasta

- Genbank -> Fasta

- Translate Genbank -> Fasta

- Split Fasta sequences

- Combine Fasta sequences


Planned Improvements
-----------------

- Generally improve look of UI
- Improve Invalid user input notifications, making them more informative and better looking.
- Make custom save and loading system, since the Win32 system is a pain to test.
- Improve test coverage of viewModels
- Investigate performance issues with very large sequences in text boxes
- Documentation


Planned Features
-----------------

- Extensive help system with links to scientific websites, with separate resizeable windows that can be placed alongside
  tool windows while working.
- Explore possibility of graphical sequence annotation
- More format conversion, and more extensive format validators for non-FASTA format files.


Author
-------

Jem Davis
jemd321+programming@gmail.com