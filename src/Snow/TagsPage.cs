namespace Snow
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class TagsPage
    {
        public static List<Tag> Create(IEnumerable<Post> posts)
        {
            var publishedPosts = posts.Where(ShouldProcess.Tag);

            var tags = (from c in publishedPosts.SelectMany(x => x.Tags)
                              group c by c
                                  into g
                                  select new Tag
                                  {
                                      Name = g.Key,
                                      Count = g.Count()
                                  }).OrderBy(x => x.Name).ToList();

            var filteredItems = tags.Where(ShouldProcess.Tag);

            var distinctItems =
                filteredItems.GroupBy(x => x.Name.ToLower()).Select(group => @group.Last()).ToList();

            return distinctItems;
        }
    }
}