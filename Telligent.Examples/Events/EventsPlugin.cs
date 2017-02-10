using Telligent.Evolution.Extensibility;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Version1;


namespace HandlingEvents
{
    public class EventsPlugin : IPlugin
    {
        public string Name => "Demo Event Handler Plugin";

        public string Description => "";

        public void Initialize()
        {
            Apis.Get<IUsers>().Events.BeforeCreate += EventsOnBeforeCreate;
            Apis.Get<IForumThreads>().Events.BeforeCreate += EventsOnBeforeCreate;
        }

        private void EventsOnBeforeCreate(ForumThreadBeforeCreateEventArgs e)
        {
            // Allows you to view and change the properties of the thread before
            // it's created.
        }

        private void EventsOnBeforeCreate(UserBeforeCreateEventArgs e)
        {
            // Here you can perform anything you like, such as pulling in profile 
            // data from an external system. You could also log the user being created 
            // or create a user in another platform e.g. marketing DB.
        }
    }
}