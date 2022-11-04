using System.Reflection;
using System.Text;

namespace Language.Resources;

public class ResourceHelper
{
    private const string _resourcePath = "HotChocolate.Language.Resources";
    private readonly Assembly _assembly;

    public ResourceHelper()
    {
        _assembly = GetType().Assembly;
    }

    public string GetResourceString(string fileName)
    {
        var stream = GetResourceStream(fileName);
        
        if (stream is not null)
        {
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }
        
        throw new FileNotFoundException(
            "Could not find the specified resource file",
            fileName);
    }

    private Stream? GetResourceStream(string fileName)
        => _assembly.GetManifestResourceStream($"{_resourcePath}.{fileName}");
}
