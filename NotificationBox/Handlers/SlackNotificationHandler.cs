using NotificationBox.Messages;
using NotificationBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationBox.Handlers
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
                Slack.Webhooks.SlackMessage slackMessage = new Slack.Webhooks.SlackMessage
                {
                    Username = message.Username,
                    Channel = message.Channel,
                    Text = message.Message
                };

                if (string.IsNullOrWhiteSpace(message.Emoji) && Enum.TryParse(message.Emoji, out Slack.Webhooks.Emoji emoji))
                {
                    slackMessage.IconEmoji = emoji;
                }

                Slack.Webhooks.SlackClient slackClient = new Slack.Webhooks.SlackClient(message.WebHookURL);
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
