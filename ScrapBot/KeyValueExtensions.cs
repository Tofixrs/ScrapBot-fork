using SteamKit2;

namespace ScrapBot;

public static class KeyValueExtensions
{
    public static string[]? GetStoreTagsIfExists(this KeyValue keyValue)
    {
        var store_tags = CustomIndex(keyValue, "common/store_tags");

        if (store_tags == null) return null;

        return store_tags.Children.ConvertAll(kv => kv.Value).ToArray();
    }
    public static string PrintString(this KeyValue keyValue)
    {
        var returnString = "";
        if (keyValue.Value == null)
        {
            var children = string.Join(",\n", keyValue.Children.ConvertAll(kv => $"\t{PrintString(kv)}"));
            returnString = $"\"{keyValue.Name}\": {{ \n{children} \n}}";
        }
        else
        {
            returnString = $"\"{keyValue.Name}\" : \"{keyValue.Value}\"";
        }

        return returnString;
    }
    public static KeyValue? CustomIndex(KeyValue kv, string index)
    {
        var indexes = index.Split("/");

        if (indexes.Length == 0) return kv;

        var i = indexes[0];
        var foundChildren = kv.Children.Where(kv => kv.Name == i);

        if (foundChildren.ToArray().Length == 0) return null;
        var firstFoundChild = foundChildren.ToArray()[0];

        return CustomIndex(firstFoundChild, string.Join("/", index.Split("/").Skip(1)));
    }
}

