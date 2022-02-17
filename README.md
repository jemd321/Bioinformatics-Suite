Bioinformatics-Suite
======================

Welcome to Bioinformatics Suite. This application is a toolkit for manipulating DNA, RNA and protein
sequences, and converting them between different formats.

This program is intended as a personal project to learn C#, WPF with MVVM, PRISM and design concepts such as
dependency injection, loosely coupled/modular structure and scalability.

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

- EMBL -> FASTA

- Translate EMBL -> FASTA

- Genbank -> FASTA

- Translate Genbank -> FASTA

- Split FASTA sequences

- Combine FASTA sequences


Planned Improvements
-----------------

- Improve Invalid user input notifications, making them more informative and better looking.
- Make custom save and loading system, since Win32 file dialogs violate MVVM and aren't really testable.
- Investigate performance issues with very large sequences in text boxes
- Documentation


Planned Features
-----------------

- Extensive help system with links to scientific websites, with separate resizeable windows that can be placed alongside
  tool windows while working.
- Explore possibility of graphical sequence annotation
- More format conversion, and more extensive format validators for non-FASTA format files.
