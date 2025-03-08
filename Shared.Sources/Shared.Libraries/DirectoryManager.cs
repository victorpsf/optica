using System.Reflection;

namespace Shared.Libraries;

public class DirectoryManager
{
    public static string MainDirectory = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName ?? ".\\";

    public string CurrentDirectory { get; private set; }

    public DirectoryManager(string currentDirectory)
    {
        this.CurrentDirectory = currentDirectory;
    }

    public DirectoryManager(): this(DirectoryManager.MainDirectory)
    { }

    public static string Concat(params string[] values)
        => string.Join(Path.DirectorySeparatorChar, values);

    public bool HasFile(string fileName)
        => File.Exists(DirectoryManager.Concat(this.CurrentDirectory, fileName));

    public bool HasDirectory(string directoryName)
        => Directory.Exists(DirectoryManager.Concat(this.CurrentDirectory, directoryName));

    public List<string> LsDir()
        => Directory.GetDirectories(this.CurrentDirectory).ToList();

    public List<string> LsFiles()
        => Directory.GetFiles(this.CurrentDirectory).ToList();

    public string MkDir(string directory)
    {
        var path = DirectoryManager.Concat(this.CurrentDirectory, directory);

        if (this.HasDirectory(directory))
            return path;

        Directory.CreateDirectory(path);
        return path;
    }

    public byte[] WriteFile(string fileName, byte[] bytes)
    {
        File.WriteAllBytes(DirectoryManager.Concat(this.CurrentDirectory, fileName), bytes);
        return bytes;
    }

    public string WriteFile(string fileName, string content)
    {
        File.WriteAllText(DirectoryManager.Concat(this.CurrentDirectory, fileName), content);
        return content;
    }

    public byte[] ReadBytesFile(string fileName)
        => File.ReadAllBytes(DirectoryManager.Concat(this.CurrentDirectory, fileName));

    public string ReadFile(string fileName)
        => File.ReadAllText(DirectoryManager.Concat(this.CurrentDirectory, fileName));

    public DirectoryManager NextDirectory(string directory)
    {
        if (!this.HasDirectory(directory))
            throw new ArgumentException($"Directory: {directory} not exists");

        return new DirectoryManager(
            DirectoryManager.Concat(this.CurrentDirectory, directory)
        );
    }
}
