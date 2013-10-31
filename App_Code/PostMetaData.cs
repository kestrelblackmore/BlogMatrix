using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for PostMetaData
/// </summary>
public static class PostMetaData
{

    public static SortedDictionary<string, dynamic> LoadPostMetaData(string pPostMetaDataPath) {
        var files  = new DirectoryInfo(pPostMetaDataPath).GetFiles();
        var result = new SortedDictionary<string, dynamic>(new ReverseComparer<string>(Comparer<string>.Default));

        foreach(var file in files) {
            var bits = GetPostMetaData(file.FullName);
            
            if (bits != null)
                result.Add(bits.publish_date, bits);
        }

        return result;
    }

    public static dynamic GetPostMetaData(string pFilename) {
        dynamic metadata = null;

        using (var s = new StreamReader(pFilename)) {
            metadata = System.Web.Helpers.Json.Decode(s.ReadToEnd());
        }

        return metadata;

    }
}
