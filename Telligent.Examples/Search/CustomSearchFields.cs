using System.Linq;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace CustomSearchFields
{
    public class CustomSearchFields : IPlugin
    {
        public string Name => "Custom Search Fields";

        public string Description => "Adds additional metadata to the search index when indexing documents.";

        public void Initialize()
        {
            // Create the event hander
            Apis.Get<ISearchIndexing>().Events.BeforeBulkIndex += EventsOnBeforeBulkIndex;
        }

        private void EventsOnBeforeBulkIndex(BeforeBulkIndexingEventArgs e)
        {
            // Get the thread service for use later
            var threadService = Apis.Get<IForumThreads>();

            // Select all documents that are forum threads
            foreach (var doc in e.Documents.Where(d => d.ContentTypeId == threadService.ContentTypeId))
            {
                var thread = threadService.Get(doc.ContentId);
                if(thread == null)
                    continue;

                // Save language as string value
                doc.IndexFields.Add(new IndexField("language_s", thread.ExtendedAttributes["Language"].Value));
            }
        }
    }
}
