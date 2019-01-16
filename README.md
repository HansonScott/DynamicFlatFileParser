# DynamicFlatFileParser
a windows desktop flat file parser with dynamic format assembly loading

## Overview
This POC demonstrates the ability to load an assembly dynamically and use the content within it to drive data formats within the application.

## Background
This concept was necessary for a complex ETL routine that loaded a variety of file formats dynamically.  Instead of hard coding the formats within the application ad requiring full deployment for every format change, this extracts the format to its own assembly allowing for a parital deployment of just the format assembly and leaves the application runnign in prod meanwhile.

## Features
* an application that loads assemblies dymamically
* a series of format assemblies all following a known assembly template
* a simple UI showing the format provided by the asembly.
