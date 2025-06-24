namespace CompressionApp.UILibrary.Validators;

internal class FileValidator
{
    public (bool result, string errorMessage) ValidateFiles((string files, long size)[] filesData)
    {
        if (filesData.Length > 10)
            return (false, "No more than 10 files are allowed.");

        var validation = ValidateFilenames(filesData.Select(fd => fd.files).ToArray());
        if (!validation.result)
            return validation;

        if (filesData.Any(fd => fd.size > 500_000))
            return (false, "Files exceeded 500 kB.");

        return (true, string.Empty);
    }

    public (bool result, string errorMessage) ValidateFilenames(string[] files)
    {
        foreach (var file in files)
        {
            var validation = ValidateFilename(file);
            if (!validation.result)
                return validation;
        }

        return (true, string.Empty);
    }

    public (bool result, string errorMessage) ValidateFilename(string file)
    {
        if (file.Any(c => c.Equals(Path.GetInvalidFileNameChars())))
            return (false, "File doesn't exist.");

        return (true, string.Empty);
    }
}