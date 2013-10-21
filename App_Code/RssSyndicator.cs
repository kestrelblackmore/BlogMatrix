using System;
using System.Collections.Generic;
using System.Web;
using System.ServiceModel.Syndication;


public static class RssSyndicator
{
    public static SyndicationFeed GetFeed(HttpContext context) {
        var baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority;

        var feed = new SyndicationFeed();
        var items = new List<SyndicationItem>();

        feed.Id = baseUrl;
        feed.Title = new TextSyndicationContent("Kestrel Blackmore Blog");
        feed.Description = new TextSyndicationContent("Hi, I'm Kestrel. I've been a software developer now for 10+ years and I love it!");
        feed.Copyright = new TextSyndicationContent("Kestrel Blackmore");
        feed.LastUpdatedTime = new DateTimeOffset(DateTime.Now);
        feed.Generator = "BlogMatrix 1.0";
        feed.ImageUrl = new Uri(baseUrl + "/assets/img/githubtile.png");

        // Add the URL that will link to your published feed when it's done
        SyndicationLink link = new SyndicationLink(new Uri(baseUrl + "/feed.xml"));
        link.RelationshipType = "self";
        link.MediaType = "text/html";
        link.Title = "Kestrel Blackmore Feed";
        feed.Links.Add(link);

        // Add your site link
        link = new SyndicationLink(new Uri(baseUrl));
        link.MediaType = "text/html";
        link.Title = "Kestrel Blackmore Blog";
        feed.Links.Add(link);


        var item = new SyndicationItem();
        item.Title = TextSyndicationContent.CreatePlaintextContent("The Three Competing Goals of Software Development");
        item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(baseUrl + "/blog/three-competing-goals-software-development")));
        item.Summary = TextSyndicationContent.CreatePlaintextContent("Developing software is a rather complex task. Ask any developer. It is made even harder by the three competing goals of software development: Time, Money and Features. In an ideal world every software development project would be delivered on time, within budget and with all the features originally requested. Unfortunately this is very rarely the case. Developers know that delivering the holy trifecta of time, money and features all at the same time is hard impossible. Why is this? The answer is simple. All three of these goals compete against each other.");
        item.PublishDate = new DateTimeOffset(DateTime.Now);
        item.Authors.Add(new SyndicationPerson("me@kestrelblackmore.com", "Kestrel Blackmore", "www.kestrelblackmore.com"));

        items.Add(item);

        feed.Items = items;

        return feed;

    }
}
