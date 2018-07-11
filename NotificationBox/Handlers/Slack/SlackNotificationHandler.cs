using System;
using System.Threading.Tasks;
using SlackAPI = Slack.Webhooks;

namespace NotificationBox.Handlers.Slack
{
    public class SlackNotificationHandler : BaseNotificationHandler, INotificationHandler<SlackMessage>
    {
        public override Task<Tuple<bool, string>> SendMessage(IMessage message)
        {
            return SendMessage(message as SlackMessage);
        }

        public async Task<Tuple<bool, string>> SendMessage(SlackMessage message)
        {
            try
            {
                SlackAPI.SlackMessage slackMessage = new SlackAPI.SlackMessage
                {
                    Username = message.Username,
                    Channel = message.Channel,
                    Text = message.Message
                };

                if (string.IsNullOrWhiteSpace(message.Emoji) && Enum.TryParse(message.Emoji, out SlackAPI.Emoji emoji))
                {
                    slackMessage.IconEmoji = emoji;
                }

                SlackAPI.SlackClient slackClient = new SlackAPI.SlackClient(message.WebHookURL);
                bool sendResult = await slackClient.PostAsync(slackMessage);

                return new Tuple<bool, string>(sendResult, null);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, $"Failed to send message to slack ({ex.Message}).");
            }
        }
    }
}
