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
        var s = "";
        if (keyValue.Value == null)
        {
            var ss = string.Join(",\n", keyValue.Children.ConvertAll(kv => $"\t{PrintString(kv)}"));
            s = $"\"{keyValue.Name}\": {{ \n{ss} \n}}";
        }
        else
        {
            s = $"\"{keyValue.Name}\" : \"{keyValue.Value}\"";
        }

        return s;
    }
    public static KeyValue? CustomIndex(KeyValue kv, string index)
    {
        var indexes = index.Split("/");

        if (indexes.Length == 0) return kv;

        var i = indexes[0];
        var k = kv.Children.Where(kv => kv.Name == i);

        if (k.ToArray().Length == 0) return null;
        var kk = k.ToArray()[0];

        return CustomIndex(kk, string.Join("/", index.Split("/").Skip(1)));
    }
}

