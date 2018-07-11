using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NotificationBox.Handlers;
using NotificationBox.Messages;
using NotificationBox.Models;
using NotificationBox.Services;

namespace NotificationBox.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IConfiguration configuration;
        private IMessageParseService messageParseService;
        private INotificationHandlerService notificationHandlerService;

        public MessageController(IConfiguration configuration, INotificationHandlerService notificationHandlerService, IMessageParseService messageParseService)
        {
            this.configuration = configuration;
            this.messageParseService = messageParseService;
            this.notificationHandlerService = notificationHandlerService;
        }

        [HttpPost("/send/{handler}")]
        public async Task<ActionResult<SendMessageModel>> SendMessage(string handler)
        {
            INotificationHandler notificationHandler = notificationHandlerService.GetHandler(handler);
            if (notificationHandler == null)
                return NotFound();

            Dictionary<string, string> defaultValues = configuration.GetSection($"Handlers:{notificationHandler.Name}").Get<Dictionary<string, string>>();
            if (defaultValues == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            IMessage messageToSend = messageParseService.Parse(notificationHandler.GetMessageType(), Request.Form, defaultValues);
            if (messageToSend == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            Tuple<bool, string> result = await notificationHandler.SendMessage(messageToSend);
            return Ok(new SendMessageModel
            {
                Result = result.Item1,
                ErrorMessage = result.Item2
            });
        }
    }
}