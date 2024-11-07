# eMZedder

### Short description

A C#/.NET 8 project to help explore WPF and WinForms project implementing a LC-MS data viewing app. Currently actively developing the Chromatogram and MS viewers. Very much a work progressing!

### Languages used

- C-Sharp

### Overview

I've worked with many different mass spectrometry raw data viewers and liked different features of each. This is my attempt to make my own raw data file viewer, eMZedder. The primary motivation is to delve deeper in to C# and particulary WPF and WinForms applications as these are used extensively by most mass spec manufacturers.  Initially this will working with Thermo raw data files as I have previous experience a C# console app that interacts with these and so I can focus on the GUI development. Functionality will be extended to other vendors when time and if an API is readily avaiable.

Current project status:
-Main branch
	- eMZedder class library fully implemented and tested with xUnit to facilitate access to Thermo Data files
-WPF_GUI branch
	- Adds a WPF application to view chromatograms and mass spectra. 
-WinForm_GUI branch
	- Implements chromatogram and mass spectra viewer in WinForms.

### Currently development
- Chromatogram and Mass Spec viewer in WinForms
- Normalised Intensity for y axis 

### Future Features
- Plot stacking to view multiple chromatogram/specra
	- add/remove spectra
	- reorder plot stacks
- Extracted ion chromatogram view
	- including mass tolerance in ppm, Th and amu
- Peak labelling
	- dynamic peak labelling to avoid spectrum overcrowding