using System;
using System.Collections.Generic;
using System.Web;
using System.ServiceModel.Syndication;


public static class RssSyndicator
{
    private const string _baseurl = "http://www.kestrelblackmore.com";
    private const string _firstappeared = "<p><a href='{0}'>Read More</a></p><p>The post <a rel='nofollow' href='{0}'>{1}</a> appeared first on <a rel='nofollow' href='http://kestrelblackmore.com'>KestrelBlackmore.com</a>.</p>";

    public static SyndicationFeed GetFeed(SortedDictionary<string, dynamic> pPostMetaData) {
        
        var feed = GetBlogInformation(_baseurl);
        
        feed.Items = GetPosts(_baseurl, pPostMetaData);

        return feed;

    }

    private static SyndicationFeed GetBlogInformation(string pbaseUrl) {
        var feed = new SyndicationFeed();

        // Add basic blog information
        feed.Id = pbaseUrl;
        feed.Title = new TextSyndicationContent("Kestrel Blackmore Blog");
        feed.Description = new TextSyndicationContent("Hi, I'm Kestrel. I've been a software developer now for 10+ years and I love it!");
        feed.Copyright = new TextSyndicationContent("KestrelBlackmore.com");
        feed.LastUpdatedTime = new DateTimeOffset(DateTime.Now);
        feed.Generator = "BlogMatrix 1.0";
        feed.ImageUrl = new Uri(pbaseUrl + "/assets/img/blackmore_logo.png");

        // Add the URL that will link to your published feed when it's done
        SyndicationLink link = new SyndicationLink(new Uri(pbaseUrl + "/feed.xml"));
        link.RelationshipType = "self";
        link.MediaType = "text/html";
        link.Title = "Kestrel Blackmore Feed";
        feed.Links.Add(link);

        // Add your site link
        link = new SyndicationLink(new Uri(pbaseUrl));
        link.MediaType = "text/html";
        link.Title = "Kestrel Blackmore Blog";
        feed.Links.Add(link);

        return feed;
    }

    private static List<SyndicationItem> GetPosts(string pbaseUrl, SortedDictionary<string, dynamic> pPostMetaData) {
        var items = new List<SyndicationItem>();

        foreach(var post in pPostMetaData) {
            var item = new SyndicationItem();
            var title = post.Value.title;
            var url = pbaseUrl + post.Value.link.url;
            var content = "<p>" + post.Value.summary + "</p>" + FirstAppeared(url, title);

            item.Title = TextSyndicationContent.CreatePlaintextContent(title);
            item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(url)));
            item.Summary = new CDataSyndicationContent(new TextSyndicationContent(content, TextSyndicationContentKind.Html));
            //item.Summary = TextSyndicationContent.CreatePlaintextContent(post.Value.summary);
            //item.Summary = TextSyndicationContent.CreateHtmlContent("<![CDATA[" + "<p>" + post.Value.summary + "</p>" + FirstAppeared(url, title) + "]]>");
            var date = DateTime.Parse(post.Value.publish_date);
            item.PublishDate = new DateTimeOffset(date);
            item.Authors.Add(new SyndicationPerson(post.Value.author.email, post.Value.author.name, post.Value.author.url));  // the parameters are the wrong way around!

            items.Add(item);

        }

        return items;
    }

    private static string FirstAppeared(string url, string title) {
        
        return String.Format(_firstappeared, url, title);
    }
}

public class CDataSyndicationContent : TextSyndicationContent
{
    public CDataSyndicationContent(TextSyndicationContent content)
        : base(content)
    {}

    protected override void  WriteContentsTo(System.Xml.XmlWriter writer)
    {
        writer.WriteCData(Text);
    }
}
