﻿namespace NGpt.ChatCompletion;

public class ChatResponse
{
    public string Id { get; set; }

    public string Object { get; set; }

    public long Created { get; set; }

    public string Model { get; set; }

    public Usage Usage { get; set; }

    public List<Choice> Choices { get; set; }
}
