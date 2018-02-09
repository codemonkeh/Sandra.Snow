namespace Snow.Models
{
    using Extensions;

    public class Tag
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public string Url
        {
            get { return Name.ToUrlSlug(); }
        }
    }
}