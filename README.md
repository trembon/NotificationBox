# NotificationBox

### What?
An API to gather some services to send messages with, like mail and slack.

Idea was to use it as an internal application with no need for authentication to simplify sending messages in my other application with just a HTTP post request.

### Why?
Don't know, because I can? Well, its always fun to play around with Reflection and just to learn, even though it surely could have been made with an already existing online service.

## Examples

### Slack

```
HTTP POST
URL: /send/slack
Content-Type: application/x-www-form-urlencoded; charset=utf-8
Request Body:
message=hello world&channel=random
```

### Mail

```
HTTP POST
URL: /send/mail
Content-Type: application/x-www-form-urlencoded; charset=utf-8
Request Body:
subject=hello&message=world&to=some@mail.com
```