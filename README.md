# CompressionApp

**CompressionApp** is a file compression and decompression utility built with **.NET 9**,
leveraging **Blazor Hybrid** and **.NET MAUI**. It supports GZip and ZIP formats and provides
a responsive user interface for both desktop and mobile platforms.

This project showcases clean architecture principles, robust error handling, and a fully testable design.

## Tech Stack

- **.NET 9**
- **Blazor Hybrid**  – web UI running in a native app
- **.NET MAUI** – native controls, cross-platform
- **xUnit** – unit testing framework

## Goals

  - [x] Learn effective I/O operations in .NET
  - [x] Introduction to Blazor Hybrid
  - [x] Progress Bar functionality
  - [x] Path, Folder and File static classes usage

## Features

- Compression

  - Select multiple files using an InputFile component
  - Compress files into a single .zip or .gz archive

- Decompression

  - Decompress .zip or .gz files into a user-selected folder

- UI responsive

  - Cross-platform responsive design for desktop and mobile
  - Real-time progress bar using native MAUI controls
  - ![image](https://github.com/user-attachments/assets/4f051999-eab2-4488-b426-e65820767692)


## Testing

- Unit tests written using **xUnit**
- High test coverage for core compression and decompression services

## Challenges

- Handling multiple file compression and decompression workflows
- Tracking stream progress during read/write operations
- Tracking stream progress during read/write operations
- Managing file and folder permissions on Android
- Validating file size and type before processing

## Lessons Learned

- How **Stream** works under the hood in .NET
- Setting up and configuring a **Blazor Hybrid** project from scratch
- How **.NET MAUI** integrates and renders Blazor components
- Implementing ZIP and GZip compression using .NET APIs
- Building a custom Stream wrapper to track progress
- Using platform-specific file pickers and savers in MAUI
- Integrating native MAUI UI components with Blazor

## Areas to Improve

- Expand knowledge of advanced I/O patterns in .NET
- Improve UI using Blazor + HTML5/CSS3 best practices
- Increase unit test coverage for edge cases
- Implement drag-and-drop for file selection
- Add support for additional archive formats (e.g., .tar, .7z)

## Resources used

- [Blazor Hybrid Documentation](https://learn.microsoft.com/es-es/aspnet/core/blazor/hybrid/?view=aspnetcore-9.0)
- [Compression Example](https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-compress-and-extract-files)
- [Blazor File Upload Series Video](https://www.youtube.com/watch?v=fb84DSypeWk&list=PLLWMQd6PeGY2yqBcxsKvsNHplMrGa-SeK&pp=0gcJCV8EOCosWNin)
- StackOverflow posts
- ChatGPT
