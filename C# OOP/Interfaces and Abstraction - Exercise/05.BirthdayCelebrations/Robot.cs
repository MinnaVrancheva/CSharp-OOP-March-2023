﻿namespace BirthdayCelebrations;

public class Robot : IIdentifier
{
    public Robot(string id, string model)
    {
        Id = id;
        Model = model;
    }

    public string Model { get; private set; }
    public string Id {get; private set;}
}
