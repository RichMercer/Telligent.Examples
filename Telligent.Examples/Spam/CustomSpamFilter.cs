using System;
using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;

namespace Telligent.Examples.Spam
{
    public class CustomSpamFilter : IAbuseDetector
    {
        private IAbuseController _abuseController;

        public string Name => "Custom Spam Plugin";

        public string Description => "Scores new content based on a custom spam rule.";

        public void Register(IAbuseController controller)
        {
            _abuseController = controller;
        }

        public string GetAbuseExplanation(Guid contentId, Guid contentTypeId)
        {
            return "This content requires moderation.";
        }

        public void Initialize()
        {
            Apis.Get<IContents>().Events.AfterCreate += EventsOnAfterCreate;
        }

        private void EventsOnAfterCreate(ContentAfterCreateEventArgs e)
        {
            // Ensure the content can be moderated and that the content is a comment.
            if (_abuseController.SupportsAbuse(e.ContentTypeId) && e.ContentTypeId == Apis.Get<IComments>().ContentTypeId)
            {
                // Here you can do your check based on your own criteria to mark the content as requiring moderation. 
                // You can else exclude users/roles from moderation.
                _abuseController.Moderate(e.ContentId, e.ContentTypeId);
            }
        }
    }
}
