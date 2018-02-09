namespace Snow.StaticFileProcessors
{
    using Extensions;
    using Models;
    using Nancy.Testing;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class TagProcessor : BaseProcessor
    {
        public override string ProcessorName
        {
            get { return "tags"; }
        }

        protected override void Impl(SnowyData snowyData, SnowSettings settings)
        {
            foreach (var tempTag in TestModule.Tags)
            {
                var tag = tempTag;

                var posts = GetPosts(snowyData.Files, tag);

                TestModule.Tag = tag;
                TestModule.GeneratedUrl = settings.SiteUrl + "/tag/" + tag.Url + "/";
                TestModule.PostsInTag = posts.ToList();

                var result = snowyData.Browser.Post("/static");

                result.ThrowIfNotSuccessful(SourceFile);

                var outputFolder = Path.Combine(snowyData.Settings.PostsOutput, "tag", tag.Url);

                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                File.WriteAllText(Path.Combine(outputFolder, "index.html"), result.Body.AsString());
            }
        }

        internal IEnumerable<Post> GetPosts(IList<Post> files, Tag tag)
        {
            return files.Where(x => x.Tags.Contains(tag.Name)).Where(ShouldProcess.Tag);
        }
    }
}